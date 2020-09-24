using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IMDB.Movies.API.Application;
using IMDB.Movies.API.Application.Models;
using IMDB.Movies.API.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Movies.API.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [Authorize(Roles = Roles.ADMINISTRATOR)]
        [HttpPost, Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] MovieViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _movieRepository.Add(model.Map());

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet, Route("{movieId:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] int movieId)
        {
            var movie = await _movieRepository.Get(movieId);

            if (movie == null)
                return NotFound();

            return Ok(new MovieResponse(movie));
        }

        [Authorize(Roles = Roles.USER)]
        [HttpPut, Route("{movieId:int}/rating")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Rating(
            [FromRoute] int movieId,
            [FromBody] MovieRatingViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var movie = await _movieRepository.Get(movieId);

            if (movie == null) return NotFound();

            movie.UpdateRating(model.Rating);

            await _movieRepository.Save();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet, Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> List(
            [FromQuery] string name,
            [FromQuery] string directorName,
            [FromQuery] string gender,
            [FromQuery] List<string> actors,
            [FromQuery] int index = 1,
            [FromQuery] int pageSize = 8)
        {
            var result = await _movieRepository.List(name, directorName, gender, actors, pageSize, index);

            return Ok(new PageDataResponse<MovieResponse>
            (
                result.movies.Select(m => new MovieResponse(m)).ToList(),
                result.total
            ));
        }
    }
}