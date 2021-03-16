using CoreAPI.Data;
using CoreAPI.Extenstions;
using CoreAPI.Models;
using CoreAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly OrderContext _context;

        public MovieController(OrderContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var movies = _context.Movies.ToList();

            return Ok(movies);
        }

        [HttpGet("{id}", Name = "MovieById")]
        [ServiceFilter(typeof(MovieValidateEntityExistsAttribute<Movie>))]
        public IActionResult Get(int id)
        {
            var dbMovie = HttpContext.Items["entity"] as Movie;

            return Ok(dbMovie);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Post([FromBody] Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return CreatedAtRoute("MovieById", new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(MovieValidateEntityExistsAttribute<Movie>))]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            var dbMovie = HttpContext.Items["entity"] as Movie;

            dbMovie.Map(movie);

            _context.Movies.Update(dbMovie);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(MovieValidateEntityExistsAttribute<Movie>))]
        public IActionResult Delete(Guid id)
        {
            var dbMovie = HttpContext.Items["entity"] as Movie;

            _context.Movies.Remove(dbMovie);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
