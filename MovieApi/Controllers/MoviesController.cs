using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieApiContext _context;

        public MoviesController(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie()
        {
            var movies = await _context.Movie
                .Select(movie => new MovieDto()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Year = movie.Year,
                    Genre = movie.Genre,
                    Duration = movie.Duration,
                    Language = movie.MovieDetails.Language,
                    Synopsis = movie.MovieDetails.Synopsis,
                    Budget = movie.MovieDetails.Budget.ToString()
                })
                .ToListAsync();

            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movieExists = await _context.Movie.AnyAsync(m => m.Id == id);
            if (!movieExists)
                return NotFound();

            var movieDto = await _context.Movie
                .Where(m => m.Id == id)
                .Select(movie => new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Year = movie.Year,
                    Genre = movie.Genre,
                    Duration = movie.Duration,
                    Language = movie.MovieDetails.Language,
                    Synopsis = movie.MovieDetails.Synopsis,
                    Budget = movie.MovieDetails.Budget.ToString()
                })
                .FirstOrDefaultAsync();

            //The null check is done at the beginning of the method.
            return movieDto!;
        }

        // GET: api/Movies/5/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
        {
            var movieExists = await _context.Movie.AnyAsync(m => m.Id == id);
            if (!movieExists)
                return NotFound();
            
            var movie = await _context.Movie
                .Where(m => m.Id == id)
                .Select(movie => new MovieDetailDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Year = movie.Year,
                    Genre = movie.Genre,
                    Duration = movie.Duration,
                    Language = movie.MovieDetails.Language,
                    Synopsis = movie.MovieDetails.Synopsis,
                    Budget = movie.MovieDetails.Budget,
                    Actors = movie.Actors.Select(actor => new ActorDto
                    {
                        Name = actor.Name,
                        BirthYear = actor.BirthYear
                    }),
                    Reviews = movie.Reviews.Select(reniew => new ReviewDto
                    {
                        ReviewerName = reniew.ReviewerName,
                        Comment = reniew.Comment,
                        Rating = reniew.Rating
                })
            })
                //.AsNoTracking() // Optional: Use AsNoTracking for read-only queries
                .FirstOrDefaultAsync(); // Use SingleOrDefaultAsync to get a single movie by id

            return Ok(movie);
        }


        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto movieDto)
        {

            if (id != movieDto.Id)
            {
                return BadRequest();
            }

            var movie = await _context.Movie
                .Include(m => m.MovieDetails) // Include MovieDetails to update it
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            // Update the movie properties from the DTO
            movie.Title = movieDto.Title;
            movie.Year = movieDto.Year;
            movie.Genre = movieDto.Genre;
            movie.Duration = movieDto.Duration;
            // Update the MovieDetails properties from the DTO
            movie.MovieDetails.Synopsis = movieDto.Synopsis;
            movie.MovieDetails.Language = movieDto.Language;
            movie.MovieDetails.Budget = movieDto.Budget;


            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto dto)
        {
            var movie = new Movie()
            {
                Title = dto.Title,
                Year = dto.Year,
                Genre = dto.Genre,
                Duration = dto.Duration,
                MovieDetails = new MovieDetails()
                {
                    Synopsis = dto.Synopsis,
                    Language = dto.Language,
                    Budget = dto.Budget
                }
            };

            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, dto);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
