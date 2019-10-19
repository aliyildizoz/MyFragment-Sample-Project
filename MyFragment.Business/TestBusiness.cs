using MyFragment.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyFragment.Business.Manager;
using MyFragment.Entities.Entity;

namespace MyFragment.Business
{
    public class TestBusiness
    {
        public TestBusiness()
        {
            DatabaseContext context = new DatabaseContext();
            List<Movie> movies = context.Movies.ToList();
        }
    }
}
