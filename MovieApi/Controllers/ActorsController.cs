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
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MovieApiContext _context;

        public ActorsController(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Actors
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Actor>>> GetActor()
        //{
        //    return await _context.Actor.ToListAsync();
        //}

        //GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorMoviesDto>> GetActor(int id)
        {
            var dto = await _context.Actor
                .Where(a => a.Id == id)
                .Select(actor => new ActorMoviesDto
                {
                    Id = actor.Id,
                    Name = actor.Name,
                    BirthYear = actor.BirthYear,
                    Movies = actor.Movies.Select(movie => new MovieDto
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Year = movie.Year,
                        Genre = movie.Genre,
                        Duration = movie.Duration,
                        Language = movie.MovieDetails.Language
                    })
                })
                .FirstOrDefaultAsync();

            //TODO: (update later) implement problem details.
            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutActor(int id, Actor actor)
        //{
        //    if (id != actor.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(actor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ActorExists(id))
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

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Ignore controller route. This could have been in MoviesController, but the exercise requires it to be here.
        [Route("~/api/movies/{movieId}/actors/{actorId}")]
        [HttpPost]
        public async Task<ActionResult<ActorDto>> AddActorToMovie([FromRoute] int movieId, [FromRoute] int actorId)
        {
            //Check if the movie exists, and fetch it
            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null) 
                return NotFound("Movie not found.");

            //Check if the actor exists, and fetch it
            var actor = await _context.Actor
                .FirstOrDefaultAsync(a => a.Id == actorId);

            if (actor == null)
                return NotFound("Actor not found.");   
            
            //This is for the return.
            ActorDto actorDto = new ActorDto
            {
                Id = actor.Id,
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };

            movie.Actors.Add(actor);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actorDto);
        }

        // DELETE: api/Actors/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteActor(int id)
        //{
        //    var actor = await _context.Actor.FindAsync(id);
        //    if (actor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Actor.Remove(actor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ActorExists(int id)
        //{
        //    return _context.Actor.Any(e => e.Id == id);
        //}
    }
}
