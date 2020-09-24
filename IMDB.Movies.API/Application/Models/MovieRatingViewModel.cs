using System.ComponentModel.DataAnnotations;

namespace IMDB.Movies.API.Application.Models
{
    public class MovieRatingViewModel
    {
        [Required]
        [Range(0, 4, ErrorMessage = "Sua avaliação deve estrar entre 0 a 4")]
        public int Rating { get; set; }
    }
}