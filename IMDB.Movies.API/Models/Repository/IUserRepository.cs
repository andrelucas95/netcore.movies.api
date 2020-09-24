using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Movies.API.Models.Repository
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> Get(Guid id);
        Task<(IList<User> users, int total)> List(string role, bool active, int pageSize, int index);
        Task Save();
    }
}