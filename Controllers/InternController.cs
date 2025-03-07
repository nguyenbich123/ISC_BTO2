﻿using AuthorizationAPI.Services;
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

        [Authorize] 
        [HttpGet]
        public async Task<IActionResult> GetInterns()
        {
            var roleIdClaim = User.FindFirst("RoleId");

            if (roleIdClaim == null)
            {
                return Unauthorized(new { status = "error", message = "Không tìm thấy RoleId trong token" });
            }

            int roleId = int.Parse(roleIdClaim.Value);
            var response = await _internService.GetInternsByRoleAsync(roleId);

            return response.Status.Equals("success") ? Ok(response) : BadRequest(response);
        }

    }
}
