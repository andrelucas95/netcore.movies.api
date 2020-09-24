using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Movies.API.Application.Models
{
    public class AccessTokenDTO : IActionResult
    {
        public string AccessToken { get; set; }
        public IList<string> Roles { get; set; }
        public string UserName { get; set; }

        public AccessTokenDTO(string token, IList<string> roles, string userName) 
        {
            AccessToken = token;
            Roles = roles;
            UserName = userName;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}