using IMDB.Movies.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDB.Movies.API.Data.Mappings
{
    public class MovieMapping : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired(); 

            builder.Property(m => m.Gender)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.DirectorName)
                .HasMaxLength(30)
                .IsRequired();
            
            builder
                .HasMany(m => m.Actors)
                .WithOne(a => a.Movie)
                .HasForeignKey(a => a.MovieId);
        }
    }
}