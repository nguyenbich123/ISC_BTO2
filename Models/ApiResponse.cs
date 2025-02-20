namespace AuthorizationAPI.Models
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }  // "success" hoặc "error"
        public string Message { get; set; } // Nội dung thông báo
        public T Data { get; set; }         // Dữ liệu trả về (generic)

        public ApiResponse(string status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
