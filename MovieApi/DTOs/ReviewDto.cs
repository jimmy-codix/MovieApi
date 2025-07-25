﻿using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class ReviewDto
    {
        //Id is included for future manipulation.
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }
    }
}
