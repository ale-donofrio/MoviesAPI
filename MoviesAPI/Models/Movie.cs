using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Movie
    {
        public Movie()
        {
            Genres = new HashSet<Genre>();
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        /// <summary>
        /// Running Time in minutes
        /// </summary>
        public int RunningTime { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}