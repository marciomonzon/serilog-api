using ApiSerilogExample.Models;
using ApiSerilogExample.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSerilogExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> PostUser(User user)
        {
            try
            {
                _logger.LogInformation("POST request received at {Time}", DateTime.UtcNow);

                var result = await _userService.AddUserAsync(user);

                return result ? Ok("User added successfully.")
                       : BadRequest("User was not added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "Internal server error");
            }
            finally
            {
                _logger.LogInformation("POST request processing completed at {Time}", DateTime.UtcNow);
            }
        }
    }
}
