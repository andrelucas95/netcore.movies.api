using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IMDB.Movies.API.Application.Extensions
{
    public static class ClaimsExtensions
    {
        public static string Id(this IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type == "UserId").Value;
        }
    }
}