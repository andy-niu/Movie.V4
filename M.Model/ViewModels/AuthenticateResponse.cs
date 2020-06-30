using System;
using M.Repository.Entity;
using Newtonsoft.Json;

namespace M.Models.ViewModels
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public string Avatar { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.UserId;
            Username = user.UserName;
            Email = user.Email;
            Avatar = user.Avatar;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}