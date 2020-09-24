using System.Threading.Tasks;
using IMDB.Movies.API.Data;
using Microsoft.AspNetCore.Identity;

namespace IMDB.Movies.API.Application.Services
{
    public class IdentityInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            ApplicationDbContext context, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            CreateRole(Roles.ADMINISTRATOR);
            CreateRole(Roles.USER);
        }

        private void CreateRole(string role)
        {
            var roleExists = _roleManager.RoleExistsAsync(role).Result;
            
            if (!roleExists)
            {
                var result = _roleManager.CreateAsync(new IdentityRole(role)).Result;
            }
        }
    }
}