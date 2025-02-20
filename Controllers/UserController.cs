using AuthorizationAPI.Models;
using AuthorizationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] // Chỉ Admin mới có quyền lấy danh sách user
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.UserId) return BadRequest();
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _userService.AuthenticateUserAsync(loginRequest.Username, loginRequest.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { token });
        }
    }
}
