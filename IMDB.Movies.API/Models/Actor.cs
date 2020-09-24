namespace IMDB.Movies.API.Models
{
    public class Actor
    {
        protected Actor() { }

        public Actor(string name)
        {
            Name = name;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}