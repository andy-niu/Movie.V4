using M.Repository.Entity;
using System.Threading.Tasks;

namespace M.Repository.Interfaces
{
    public interface IRefreshTokenRepository : IBaseRepository<Entity.RefreshToken>
    {
        Task<bool> UpdateIsActive(RefreshToken entity);
    }
}
