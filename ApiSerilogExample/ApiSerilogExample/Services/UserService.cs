using ApiSerilogExample.Data;
using ApiSerilogExample.Models;
using ApiSerilogExample.Services.Interfaces;

namespace ApiSerilogExample.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            HandleValidations(user);

            var result = await _userRepository.AddAsync(user);

            return result;
        }

        private void HandleValidations(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ArgumentException("Name is required.");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email is required.");

            if (!IsValidEmail(user.Email))
                throw new ArgumentException("Invalid email format.");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
