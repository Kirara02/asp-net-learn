namespace ApiService.Models.DTOs.Common
{
    public class QueryParamsDto
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public string? Search { get; set; }
        public string? Sort { get; set; }
    }
}
