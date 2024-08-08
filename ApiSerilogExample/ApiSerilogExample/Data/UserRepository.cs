using ApiSerilogExample.Models;

namespace ApiSerilogExample.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);

            return await _context.SaveChangesAsync() > 1;
        }
    }
}
