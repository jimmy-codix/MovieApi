namespace MovieApi.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BirthYear { get; set; }
        public int MoveId { get; set; }

        public int MovieId { get; set; }

    }
}
