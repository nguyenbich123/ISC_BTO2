using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Repositories.IRepo;
using AuthorizationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthController(JwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Ok(new ApiResponse<User>("error", "Tên đăng nhập hoặc mật khẩu chưa chính xác", null));
            }

            var roleName = user.Role?.RoleName ?? "Unknown";
            if (roleName == "Unknown")
            {
                return BadRequest(new ApiResponse<object> (  "error", "User has no assigned role", null ));
            }


            var token = _jwtService.GenerateToken(user.UserId.ToString(), user.Role.RoleName, user.RoleId.ToString());
            return Ok(new ApiResponse<object>("success", "Đăng nhập thành công!!", new { token = token, user = user }));
        }
    }
}
