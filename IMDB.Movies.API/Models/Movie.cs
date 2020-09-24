using System.Collections.Generic;

namespace IMDB.Movies.API.Models
{
    public class Movie
    {
        protected Movie() { }

        public Movie(string name, string gender, string directorName, List<Actor> actors)
        {
            Name = name;
            Gender = gender;
            DirectorName = directorName;
            Rating = 0;
            Actors = actors;
        }

        public void UpdateRating(int rating)
        {
            RatingCount++;
            Rating =+ rating;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Rating { get; private set; }
        public int RatingCount { get; set; }
        public string DirectorName { get; set; }
        public ICollection<Actor> Actors { get; set; }
    }
}