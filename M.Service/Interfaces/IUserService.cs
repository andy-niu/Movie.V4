using M.Models.ViewModels;
using M.Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M.Service.Interfaces
{
	///<summary>
	///User
	///</summary>
	public interface IUserService : IBaseService<Repository.Entity.User>
	{
		Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress);
		Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);
		Task<bool> RevokeToken(string token, string ipAddress);
		Task<IEnumerable<User>> GetAll();
	}
}	 
