using IMDB.Movies.API.Application.Services;
using IMDB.Movies.API.Data.Repository;
using IMDB.Movies.API.Models.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB.Movies.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<JwtService>();
            services.AddTransient<IdentityInitializer>();
            
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}