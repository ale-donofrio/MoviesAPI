using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}