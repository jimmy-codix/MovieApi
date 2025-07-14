using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly MovieApiContext _context;

        public ReviewsController(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Reviews
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Review>>> GetReview()
        //{
        //    return await _context.Review.ToListAsync();
        //}

        // GET: api/Reviews/5
        [Route("/api/movies/{movieId}/reviews")]
        [HttpGet("{movieId}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int movieId)
        {
            //TODO : Two stage rocket.
            var movie = await _context.Movie
                                            .Include(m => m.Reviews)
                                            .FirstOrDefaultAsync(m => m.Id == movieId);

            var existe = await _context.Movie.AnyAsync(m => m.Id == movieId);
            if (!existe)
            {
                return NotFound();
            }
          

            //).Where(r => r.MovieId == movieId).FirstOrDefaultAsync();
            var reviws = await _context.Review.Where(r => r.MovieId == movieId).Select(r => new ReviewDto
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating

            }).ToListAsync();

            if (movie == null) return NotFound();

            var reviews = movie.Reviews.Select(r => new ReviewDto()
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating,
            }).ToList();

            return Ok(reviews);
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutReview(int id, Review review)
        //{
        //    if (id != review.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(review).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await ReviewExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Review>> PostReview(Review review)
        //{
        //    _context.Review.Add(review);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetReview", new { id = review.Id }, review);
        //}

        // DELETE: api/Reviews/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteReview(int id)
        //{
        //    var review = await _context.Review.FindAsync(id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Review.Remove(review);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private async Task<bool> ReviewExists(int id)
        //{
        //    return await _context.Review.AnyAsync(e => e.Id == id);
        //}
    }
}
