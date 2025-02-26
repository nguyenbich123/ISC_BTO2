using AuthorizationAPI.DTOs.Request;
using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ApiResponse<ICollection<Role>>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return new ApiResponse<ICollection<Role>>
                ("success", "Lấy danh sách phân quyền thành công", roles);
        }

        public async Task<ApiResponse<Role>> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);

            if (role == null)
            {
                return new ApiResponse<Role>("error", "Không tìm thấy quyền", null);
            }
            return new ApiResponse<Role>("success", "Tìm thấy quyền", role); ;
        }

        public async Task<ApiResponse<Role>> AddRoleAsync(RoleRequest request)
        {
            try
            {
                if (await _roleRepository.AnyAsync(u => u.RoleName == request.Name))
                {
                    return new ApiResponse<Role>("error", "Tên quyền đã tồn tại", null);
                }
                var newRole = new Role
                {
                    RoleName = request.Name,
                };
                var role = await _roleRepository.AddRoleAsync(newRole);
                return new ApiResponse<Role>("success", "Thêm quyền thành công", role);
            }
            catch (Exception ex)
            {
                return new ApiResponse<Role>("error", "Lỗi thêm quyền", null);
            }
            
        }

        public async Task<ApiResponse<Role>> UpdateRoleAsync(int id, RoleRequest request)
        {
            try
            {
                var existRole = await _roleRepository.GetRoleByIdAsync(id);
                if (existRole == null)
                {
                    return new ApiResponse<Role>("error", "Không tìm thấy quyền", null);
                }

                if (await _roleRepository.AnyAsync(u => u.RoleName == request.Name && u.RoleId != id))
                {
                    return new ApiResponse<Role>("error", "Tên quyền đã tồn tại", null);
                }

                existRole.RoleName = request.Name;
                var updateRole = await _roleRepository.UpdateRoleAsync(existRole);

                return new ApiResponse<Role>("success", "Cập nhật quyền thành công", updateRole);
            }
            catch (Exception ex)
            {
                return new ApiResponse<Role>("error", "Lỗi cập nhật quyền", null);
            }
            
        }

        public async Task<ApiResponse<Role>> DeleteRoleAsync(int id)
        {
            try
            {
                var existRole = await _roleRepository.GetRoleByIdAsync(id);
                if (existRole == null)
                {
                    return new ApiResponse<Role>("error", "Không tìm thấy quyền", null);
                }
                await _roleRepository.DeleteRoleAsync(id);
                return new ApiResponse<Role>("success", "Xóa quyền thành công", null);

            }
            catch (Exception ex)
            {
                return new ApiResponse<Role>("error", "Lỗi xóa quyền", null);
            }
            
        }
    }
}
