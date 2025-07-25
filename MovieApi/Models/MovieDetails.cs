﻿using System.Text.Json.Serialization;

namespace MovieApi.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = null!;
        public string Language { get; set; } = null!;
        public int Budget { get; set; }

        //Foreign key
        public int MovieId { get; set; }

        //Navigation property
        public Movie Movie { get; set; } = null!;
    }
}
