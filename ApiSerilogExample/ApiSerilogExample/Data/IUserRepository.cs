using ApiSerilogExample.Models;

namespace ApiSerilogExample.Data
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User user);
    }
}
