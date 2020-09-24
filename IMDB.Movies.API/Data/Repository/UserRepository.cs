using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Movies.API.Models;
using IMDB.Movies.API.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Movies.API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.ApplicationUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Get(Guid id)
        {
            return await _context.ApplicationUsers
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<(IList<User> users, int total)> List(string role, bool active, int pageSize, int index)
        {
            var data = await _context.ApplicationUsers
                .Where(u => u.Role == role && u.Active == active)
                .OrderBy(u => u.Name)
                .Skip((index - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _context.ApplicationUsers
                .Where(u => u.Role == role && u.Active == active)
                .CountAsync();

            return (data, count);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}