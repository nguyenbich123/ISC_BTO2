using System.Data;

namespace AuthorizationAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

}
