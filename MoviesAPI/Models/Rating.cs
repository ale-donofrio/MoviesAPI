﻿namespace MoviesAPI.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public short Score { get; set; }
    }
}