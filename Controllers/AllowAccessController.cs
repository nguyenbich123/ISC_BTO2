using AuthorizationAPI.DTOs.Request;
using AuthorizationAPI.Models;
using AuthorizationAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetAllAllowAccess()
        {
            var response = await _allowAccessService.GetAllAllowAccessAsync();
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{roleId}")]
        [Authorize]
        public async Task<IActionResult> GetAllowAccessByRoleId(int roleId)
        {
            var response = await _allowAccessService.GetAllowAccessByRoleIdAsync(roleId);
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAllowAccess([FromBody] AllowAccessRequest allowAccess)
        {
            var response =  await _allowAccessService.AddAllowAccessAsync(allowAccess);
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{roleId}")]
        [Authorize]
        public async Task<IActionResult> UpdateAllowAccess(int roleId, [FromBody] AllowAccessRequest allowAccess)
        {
            var response =  await _allowAccessService.UpdateAllowAccessAsync(roleId, allowAccess);
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAllowAccess(int id)
        {
            var response =  await _allowAccessService.DeleteAllowAccessAsync(id);
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }
    }
}
