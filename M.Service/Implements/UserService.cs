using M.Models.ViewModels;
using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace M.Service.Implements
{
	///<summary>
	///User
	///</summary>
    public class UserService : BaseService<User>, IUserService
    {
        private readonly Repository.Interfaces.IUserRepository _repository;
        private readonly Repository.Interfaces.IRefreshTokenRepository _refreshTokenRepository;
        private readonly Models.Options.JwtOptions _jwtOptions;
        public UserService(ILogger<UserService> logger,
            Microsoft.Extensions.Caching.Distributed.IDistributedCache cache, 
            Repository.Interfaces.IUserRepository repository,
            Repository.Interfaces.IRefreshTokenRepository  refreshTokenRepository,
            IOptions<Models.Options.JwtOptions> jwtOptions) : base(cache)
        {
            base._baseRepository = repository as Repository.Interfaces.IBaseRepository<User>;
            base._logger = logger;
            _repository = repository;
            _jwtOptions = jwtOptions.Value;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = await _repository.GetEntity(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = generateJwtToken(user);
            var refreshToken = generateRefreshToken(ipAddress);

            var result2 = await _repository.Update(user);

            // save refresh token
            user.RefreshTokens.Add(refreshToken);
            // RevokeToken by userid
            await _refreshTokenRepository.UpdateIsActive(refreshToken);

            var result = await _refreshTokenRepository.Add(refreshToken);

            if (result)
            {
                return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetEntities(model => true);
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var user = await _baseRepository.GetEntity(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);

            var result = await _refreshTokenRepository.Update(refreshToken);
            //_context.Update(user);
            //_context.SaveChanges();

            var result2 = await _repository.Update(user);
            if (!result)
            {
                return null;
            }
            // generate new jwt
            var jwtToken = generateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user =await _baseRepository.GetEntity(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            //_context.Update(user);
            //_context.SaveChanges();
            var result = await _refreshTokenRepository.UpdateIsActive(refreshToken);

            var result2 = await _repository.Update(user);
            if (!result)
            {
                return false;
            }

            return true;
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.UserRoleRelation.FirstOrDefault().Role.Name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessExpiration.Value),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshExpiration.Value),
                    CreatedAt = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
    }
}
