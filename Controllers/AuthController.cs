using Microsoft.AspNetCore.Mvc;
using JsonLoginApi.Models;
using JsonLoginApi.Services;

namespace JsonLoginApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "UserId and Password are required."
                });
            }

            var user = _userService.ValidateUser(request.UserId, request.Password);

            if (user == null)
            {
                return Unauthorized(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid userId or password."
                });
            }

            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Login successful."
            });
        }
    }
}
