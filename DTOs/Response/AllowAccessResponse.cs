namespace AuthorizationAPI.DTOs.Response
{
    public class AllowAccessResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string ColumnName { get; set; } = string.Empty;
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
    }
}
