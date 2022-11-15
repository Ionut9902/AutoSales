using System;
using System.Collections.Generic;

namespace AutoSales.Models.DBObjects
{
    public partial class User
    {
        public User()
        {
            Favourites = new HashSet<Favourite>();
            Messages = new HashSet<Message>();
            Posts = new HashSet<Post>();
        }

        public Guid IdUser { get; set; }
        public string Name { get; set; } = null!;
        public string YearOfBirth { get; set; } = null!;
        public int? NumberOfPosts { get; set; }
        public DateTime FirstRegistered { get; set; }
        public string EmailAddress { get; set; } = null!;

        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
