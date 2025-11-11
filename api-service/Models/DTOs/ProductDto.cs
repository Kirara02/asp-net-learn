using System.ComponentModel.DataAnnotations;

namespace ApiService.Models.DTOs
{
    public class ProductCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    
        [Required]
        public decimal Price { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }
    }

    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
    }
}