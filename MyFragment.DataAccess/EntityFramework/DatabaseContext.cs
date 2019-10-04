using MyFragment.Entities;
using MyFragment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.DataAccess.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }

    }
}
