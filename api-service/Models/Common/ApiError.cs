namespace ApiService.Models.Common
{
    public class ApiError
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Details { get; set; } // ubah jadi object? juga
    }
}