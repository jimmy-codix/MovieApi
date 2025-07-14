namespace MovieApi.DTOs
{
    public class MovieDto
    {
        //Id is included for future manipulation.
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string Genre { get; set; } = null!;
        public int Duration { get; set; }

        public string Language { get; set; } = null!;
    }
}
