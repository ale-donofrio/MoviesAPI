using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace MoviesAPI.Models.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public List<string> Genres { get; set; }

        private double? _averageRating;
        public double? AverageRating
        {
            get
            {
                return _averageRating;
            }
            set
            {
                if (value.HasValue)
                    value = Math.Round(value.Value, 1);
                _averageRating = value;
            }
        }

        public MovieDTO() { }

        public MovieDTO(Movie m)
        {
            Id = m.Id;
            Title = m.Title;
            RunningTime = m.RunningTime;
            YearOfRelease = m.YearOfRelease;
            Genres = m.Genres.Select(g => g.Name).ToList();
        }
    }
}