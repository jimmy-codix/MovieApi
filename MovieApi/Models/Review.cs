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

        //FK
        public int MovieId { get; set; }

        //nav prop
        public Movie Movie { get; set; } = null!;
    }
}
