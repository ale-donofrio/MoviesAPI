using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesAPI.Models
{
    public class MoviesAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MoviesAPIContext() : base("name=MoviesAPIContext")
        {
        }

        public System.Data.Entity.DbSet<MoviesAPI.Models.Movie> Movies { get; set; }

        public System.Data.Entity.DbSet<MoviesAPI.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<MoviesAPI.Models.Rating> Ratings { get; set; }

        public System.Data.Entity.DbSet<MoviesAPI.Models.Genre> Genres { get; set; }
    }
}
