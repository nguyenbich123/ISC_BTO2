using AuthorizationAPI.Models;
using AuthorizationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowAccessController : ControllerBase
    {
        private readonly AllowAccessService _allowAccessService;

        public AllowAccessController(AllowAccessService allowAccessService)
        {
            _allowAccessService = allowAccessService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAllowAccess()
        {
            var allowAccessList = await _allowAccessService.GetAllAllowAccessAsync();
            return Ok(allowAccessList);
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetAllowAccessByRoleId(int roleId)
        {
            var allowAccess = await _allowAccessService.GetAllowAccessByRoleIdAsync(roleId);
            return allowAccess != null ? Ok(allowAccess) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddAllowAccess([FromBody] AllowAccess allowAccess)
        {
            await _allowAccessService.AddAllowAccessAsync(allowAccess);
            return CreatedAtAction(nameof(GetAllowAccessByRoleId), new { roleId = allowAccess.RoleId }, allowAccess);
        }

        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateAllowAccess(int roleId, [FromBody] AllowAccess allowAccess)
        {
            if (roleId != allowAccess.RoleId) return BadRequest();
            await _allowAccessService.UpdateAllowAccessAsync(allowAccess);
            return NoContent();
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteAllowAccess(int roleId)
        {
            await _allowAccessService.DeleteAllowAccessAsync(roleId);
            return NoContent();
        }
    }
}
