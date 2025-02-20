using AuthorizationAPI.Models;

namespace AuthorizationAPI.DTOs.Request
{
    public class UserRequest
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
