using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Movies.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Movies.API.Application.Models
{
    public class MovieResponse : IActionResult
    {
        public MovieResponse(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            Gender = movie.Gender;
            RatingCount = movie.RatingCount;
            Actors = movie.Actors.Select(a => a.Name).ToList();
            AverageRating = movie.RatingCount > 0 ? (movie.Rating/movie.RatingCount) : 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
        public List<string> Actors { get; set; }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}