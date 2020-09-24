using System.ComponentModel.DataAnnotations;

namespace IMDB.Movies.API.Application.Models
{
    public class UpdateUserViewModel
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Name { get; set; }
    }
}