namespace AuthorizationAPI.Models
{
    public class AllowAccess
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string TableName { get; set; }
        public string AccessProperties { get; set; } // Lưu danh sách cột được phép truy cập
    }

}
