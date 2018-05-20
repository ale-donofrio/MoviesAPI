using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class User
    {
        public User()
        {
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}