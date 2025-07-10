using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
