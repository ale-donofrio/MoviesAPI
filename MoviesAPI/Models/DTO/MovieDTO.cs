using System.Linq;

namespace MoviesAPI.Models.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public string[] Genres { get; set; }
        public double? AverageRating { get; set; }

        public MovieDTO() { }

        public MovieDTO(Movie m)
        {
            Id = m.Id;
            Title = m.Title;
            RunningTime = m.RunningTime;
            YearOfRelease = m.YearOfRelease;
            Genres = m.Genres.Select(g => g.Name).ToArray();
        }
    }
}