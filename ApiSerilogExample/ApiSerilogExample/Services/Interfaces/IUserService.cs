using ApiSerilogExample.Models;

namespace ApiSerilogExample.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(User user);
    }
}
