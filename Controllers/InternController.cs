using AuthorizationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InternController : ControllerBase
    {
        private readonly InternService _internService;

        public InternController(InternService internService)
        {
            _internService = internService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInterns()
        {
            var roleId = int.Parse(User.FindFirst("RoleId")?.Value ?? "0");
            var interns = await _internService.GetInternsByRoleAsync(roleId);
            return Ok(interns);
        }
    }
}
