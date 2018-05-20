namespace MoviesAPI.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesAPI.Models.MoviesAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoviesAPI.Models.MoviesAPIContext context)
        {
            //  This method will be called after migrating to the latest version.
            
            Genre drama = new Genre { Id = 1, Name = "Drama" };
            Genre history = new Genre { Id = 2, Name = "History" };
            Genre biography = new Genre { Id = 3, Name = "Biography" };
            Genre fantasy = new Genre { Id = 4, Name = "Fantasy" };
            Genre sport = new Genre { Id = 5, Name = "Sport" };
            Genre adventure = new Genre { Id = 6, Name = "Adventure" };
            Genre action = new Genre { Id = 7, Name = "Action" };

            context.Genres.AddOrUpdate(
                  g => g.Id,
                  drama,
                  history,
                  biography,
                  fantasy,
                  sport);

            User user1 = new User { Id = 1, UserName = "aaaaaa" };
            User user2 = new User { Id = 2, UserName = "bbbbb" };
            User user3 = new User { Id = 3, UserName = "cccc" };
            User user4 = new User { Id = 4, UserName = "dddddddd" };

            context.Users.AddOrUpdate(
                  u => u.Id,
                  user1,
                  user2,
                  user3,
                  user4);

            Movie m1 = new Movie { Id = 1, Title = "A Beautiful Mind", RunningTime = 135, YearOfRelease = 2001 };
            Movie m2 = new Movie { Id = 2, Title = "The Imitation Game", RunningTime = 114, YearOfRelease = 2014 };
            Movie m3 = new Movie { Id = 3, Title = "Rocky", RunningTime = 120, YearOfRelease = 1976 };
            Movie m4 = new Movie { Id = 4, Title = "Big Fish", RunningTime = 125, YearOfRelease = 2003 };
            Movie m5 = new Movie { Id = 5, Title = "Gladiator", RunningTime = 155, YearOfRelease = 2000 };

            m1.Genres.Add(biography);
            m1.Genres.Add(drama);
            m2.Genres.Add(biography);
            m2.Genres.Add(drama);
            m2.Genres.Add(history);
            m3.Genres.Add(drama);
            m3.Genres.Add(sport);
            m4.Genres.Add(adventure);
            m4.Genres.Add(drama);
            m4.Genres.Add(fantasy);
            m5.Genres.Add(adventure);
            m5.Genres.Add(drama);
            m5.Genres.Add(action);

            context.Movies.AddOrUpdate(
                  m => m.Id,
                  m1, m2, m3, m4, m5);

            context.Ratings.AddOrUpdate(
                  r => r.Id,
                  new Rating { Id = 1, MovieId = 1, UserId = 1, Score = 4 },
                  new Rating { Id = 2, MovieId = 1, UserId = 2, Score = 5 },
                  new Rating { Id = 3, MovieId = 2, UserId = 3, Score = 3 },
                  new Rating { Id = 4, MovieId = 3, UserId = 4, Score = 2 },
                  new Rating { Id = 5, MovieId = 4, UserId = 4, Score = 5 },
                  new Rating { Id = 6, MovieId = 5, UserId = 4, Score = 4 },
                  new Rating { Id = 7, MovieId = 5, UserId = 3, Score = 4 });
        }
    }
}
