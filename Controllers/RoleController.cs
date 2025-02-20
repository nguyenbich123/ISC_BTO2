using AuthorizationAPI.DTOs.Request;
using AuthorizationAPI.Models;
using AuthorizationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRolesAsync();
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response  = await _roleService.GetRoleByIdAsync(id);
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRole([FromBody] RoleRequest role)
        {
            var response = await _roleService.AddRoleAsync(role);
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleRequest role)
        {
            var response = await _roleService.UpdateRoleAsync(id, role);
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response =  await _roleService.DeleteRoleAsync(id);
            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }
    }
}
