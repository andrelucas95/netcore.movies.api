using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Movies.API.Models;
using IMDB.Movies.API.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Movies.API.Data.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> Get(int id)
        {
            return await _context.Movies
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<(IList<Movie> movies, int total)> List(string name, string directorName, string gender, List<string> actors, int pageSize, int index)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Actors)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(name));

            if (!string.IsNullOrEmpty(directorName))
                moviesQuery = moviesQuery.Where(m => m.DirectorName.Contains(directorName));

            if (!string.IsNullOrEmpty(gender))
                moviesQuery = moviesQuery.Where(m => m.Gender.Contains(gender));

            if (actors.Any())
                moviesQuery = moviesQuery.Where(m => m.Actors.Any(a => actors.Contains(a.Name)));

            var data = await moviesQuery
                .OrderBy(m => m.RatingCount)
                .ThenBy(m => m.Name)
                .Skip((index - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await moviesQuery.CountAsync();

            return (data, count);
        }       

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}