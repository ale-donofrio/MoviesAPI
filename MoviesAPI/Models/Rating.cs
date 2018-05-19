using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [Required]
        public short Score { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}