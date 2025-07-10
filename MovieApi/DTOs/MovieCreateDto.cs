using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class MovieCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; } = null!;
        [Required]
        [Range(1000, 9999)]
        public int Year { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Genre { get; set; } = null!;
        [Required]
        [Range(1, 10000)]
        public int Duration { get; set; }
    }
}
