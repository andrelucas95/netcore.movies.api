using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IMDB.Movies.API.Models;

namespace IMDB.Movies.API.Application.Models
{
    public class MovieViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string DirectorName { get; set; }

        [Required]
        public List<string> Actors { get; set; }

        public Movie Map()
        {
            return new Movie(Name, Gender, DirectorName, Actors.Select(a => new Actor(a)).ToList());
        }
    }
}