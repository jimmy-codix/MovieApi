using MovieApi.Models;

namespace MovieApi.DTOs
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string Genre { get; set; } = null!;
        public int Duration { get; set; }
        public string Language { get; set; } = null!;
        public string Synopsis { get; set; } = null!;
        public int Budget { get; set; }

        //TODO implement MovieDetails
        //public MovieDetails MovieDetails { get; set; } = null!;

        public IEnumerable<ReviewDto> Reviews { get; set; } = Enumerable.Empty<ReviewDto>();
        public IEnumerable<ActorDto> Actors { get; set; } = Enumerable.Empty<ActorDto>();
    }
}
