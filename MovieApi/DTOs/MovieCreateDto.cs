using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    //TODO : Change to record type.
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

        //Details
        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Synopsis { get; set; } = null!;
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Language { get; set; } = null!;
        [Required]
        [Range(1000, int.MaxValue)]
        public int Budget { get; set; }
    }
}
