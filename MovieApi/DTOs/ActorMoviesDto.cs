namespace MovieApi.DTOs
{
    public class ActorMoviesDto
    {
        //Id is included for future manipulation.
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BirthYear { get; set; }
        public IEnumerable<MovieDto> Movies { get; set; } = new List<MovieDto>();
    }
}
