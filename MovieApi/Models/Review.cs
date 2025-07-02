using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        [Range(1,5)]
        public int Rating { get; set; }

        //nav prop
        public Movie MovieId { get; set; } = null!;
    }
}
