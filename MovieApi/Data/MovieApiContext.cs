using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data
{
    public class MovieApiContext : DbContext
    {
        public MovieApiContext (DbContextOptions<MovieApiContext> options)
            : base(options)
        {
        }

        public DbSet<MovieApi.Models.Movie> Movie { get; set; } = default!;
        public DbSet<MovieApi.Models.Actor> Actor { get; set; } = default!;
        public DbSet<MovieApi.Models.Review> Review { get; set; } = default!;
    }
}
