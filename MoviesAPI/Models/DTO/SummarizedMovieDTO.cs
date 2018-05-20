using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAPI.Models.DTO
{
    public class SummarizedMovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }

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
    }
}