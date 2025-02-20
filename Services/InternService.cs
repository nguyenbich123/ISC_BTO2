using AuthorizationAPI.Models;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Repositories.IRepo;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AuthorizationAPI.Services
{
    public class InternService
    {
        private readonly IInternRepository _internRepository;

        public InternService(IInternRepository internRepository)
        {
            _internRepository = internRepository;
        }

        public async Task<ApiResponse<ICollection<object>>> GetInternsByRoleAsync(int roleId)
        {
            Console.WriteLine("id nè:" + roleId);
            var intern  = await _internRepository.GetInternsByRoleAsync(roleId);
            return new ApiResponse<ICollection<object>>("success", "Lấy danh sách thực tập sinh thành công", intern);
        }
    }
}
