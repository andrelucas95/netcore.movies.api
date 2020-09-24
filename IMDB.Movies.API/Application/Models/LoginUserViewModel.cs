using System.ComponentModel.DataAnnotations;

namespace IMDB.Movies.API.Application.Models
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Informe o email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; }
    }
}