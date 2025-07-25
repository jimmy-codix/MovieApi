﻿namespace MovieApi.DTOs
{
    public class ActorDto
    {
        //Id is included for future manipulation.
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int BirthYear { get; set; }
    }
}
