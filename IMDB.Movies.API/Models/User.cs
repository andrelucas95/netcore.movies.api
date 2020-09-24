using System;

namespace IMDB.Movies.API.Models
{
    public class User
    {
        protected User() { }

        public User(Guid id, string name, string role)
        {
            Id = id;
            Name = name;
            Active = true;
            Role = role;
        }

        public void Deactive()
        {
            Active = false;
        }

        public void Activate()
        {
            Active = true;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool Active { get; private set; }
        public string Role { get; private set; }
    }
}