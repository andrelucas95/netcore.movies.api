using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Movies.API.Models.Repository
{
    public interface IMovieRepository
    {
        Task Add(Movie movie);
        Task<Movie> Get(int id);
        Task<(IList<Movie> movies, int total)> List(string name, string directorName, string gender, List<string> actors, int pageSize, int index);
        Task Save();
    }
}