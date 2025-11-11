namespace ApiService.Models.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public ApiError? Error { get; set; }
        public int Status { get; set; }

        public static ApiResponse<T> Ok(T? data, int status = 200, string? message = null)
            => new()
            {
                Success = true,
                Message = message ?? "Request successful.",
                Data = data,
                Status = status
            };

        public static ApiResponse<T> Fail(string message, int status = 400, string? details = null)
            => new()
            {
                Success = false,
                Message = message,
                Error = new ApiError
                {
                    Code = status,
                    Message = message,
                    Details = details
                },
                Status = status
            };
    }

    public class ApiError
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }
    }
}
