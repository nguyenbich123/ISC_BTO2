using AuthorizationAPI.DTOs.Request;
using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Repositories.IRepo;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationAPI.Services
{
    public class AllowAccessService
    {
        private readonly IAllowAccessRepository _allowAccessRepository;

        public AllowAccessService(IAllowAccessRepository allowAccessRepository)
        {
            _allowAccessRepository = allowAccessRepository;
        }

        public async Task<ApiResponse<ICollection<AllowAccess>>> GetAllAllowAccessAsync()
        {
            var allowAccesss = await _allowAccessRepository.GetAllAllowAccessAsync();
            return new ApiResponse<ICollection<AllowAccess>>("success", "Lấy danh sách phân quyền truy cập thành công", allowAccesss);
        }

        public async Task<ApiResponse<AllowAccess>> GetAllowAccessByRoleIdAsync(int id)
        {
            var allowAccess = await _allowAccessRepository.GetAllowAccessByIdAsync(id);

            if (allowAccess == null)
            {
                return new ApiResponse<AllowAccess>("error", "Không tìm thấy quyền truy cập", null);
            }
            return new ApiResponse<AllowAccess>("success", "Tìm thấy quyền truy cập", allowAccess); ;
        }

        public async Task<ApiResponse<AllowAccess>> AddAllowAccessAsync(AllowAccessRequest request)
        {
            try
            {
                var newAllowAccess = new AllowAccess
                {
                    RoleId = request.RoleId,
                    TableName = request.TableName,
                    AccessProperties = request.AccessProperties,
                };
                var allowAccess = await _allowAccessRepository.AddAllowAccessAsync(newAllowAccess);
                return new ApiResponse<AllowAccess>("success", "Thêm quyền truy cập thành công", allowAccess);
            }
            catch (Exception ex)
            {
                return new ApiResponse<AllowAccess>("error", "Lỗi thêm quyền truy cập", null);
            }

        }

        public async Task<ApiResponse<AllowAccess>> UpdateAllowAccessAsync(int id, AllowAccessRequest request)
        {
            try
            {
                var existAllowAccess = await _allowAccessRepository.GetAllowAccessByIdAsync(id);
                if (existAllowAccess == null)
                {
                    return new ApiResponse<AllowAccess>("error", "Không tìm thấy quyền truy cập", null);
                }

                existAllowAccess.RoleId = request.RoleId;
                existAllowAccess.TableName = request.TableName;
                existAllowAccess.AccessProperties = request.AccessProperties;
                var updateAllowAccess = await _allowAccessRepository.UpdateAllowAccessAsync(existAllowAccess);

                return new ApiResponse<AllowAccess>("success", "Cập nhật quyền truy cập", updateAllowAccess);
            }
            catch (Exception ex)
            {
                return new ApiResponse<AllowAccess>("error", "Lỗi cập nhật quyền truy cập", null);
            }

        }

        public async Task<ApiResponse<AllowAccess>> DeleteAllowAccessAsync(int id)
        {
            try
            {
                var existAllowAccess = await _allowAccessRepository.GetAllowAccessByIdAsync(id);
                if (existAllowAccess == null)
                {
                    return new ApiResponse<AllowAccess>("error", "Không tìm thấy quyền truy cập", null);
                }
                await _allowAccessRepository.DeleteAllowAccessAsync(id);
                return new ApiResponse<AllowAccess>("success", "Xóa quyền truy cập thành công", null);

            }
            catch (Exception ex)
            {
                return new ApiResponse<AllowAccess>("error", "Lỗi xóa quyền truy cập", null);
            }

        }
    }
}
