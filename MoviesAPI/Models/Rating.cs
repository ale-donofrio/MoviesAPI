﻿using System.ComponentModel.DataAnnotations;

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

        public override bool Equals(object obj)
        {
            bool areEqual = false;

            if (obj != null)
            {
                Rating rating = obj as Rating;
                if (rating != null)
                    areEqual = rating.Id == Id &&
                            rating.UserId == UserId &&
                            rating.MovieId == MovieId;
            }
            return areEqual;
        }

        public override int GetHashCode()
        {
            return (Id.GetHashCode() + UserId.GetHashCode() + MovieId.GetHashCode()) / 3;
        }
    }
}