using MyFragment.Business.Abstract;
using MyFragment.Entities.Entity;
using MyFragment.Entities.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Business.Manager
{
    public class CategoryManager : ManagerBase<Category>, IManager
    {
        private static CategoryManager _categoryManager;

        public static CategoryManager CreateAsSingleton()
        {
            if (_categoryManager == null)
            {
                _categoryManager = new CategoryManager();
            }
            return _categoryManager;
        }

        public IQueryable<Category> ImdbList()
        {
            return QueryableList(I => I.CategoryState == CategoryState.ImdbPoint);
        }
        public IQueryable<Category> YearList()
        {
            return QueryableList(I => I.CategoryState == CategoryState.MovieYear);
        }
        public IQueryable<Category> TypeList()
        {
            return QueryableList(I => I.CategoryState == CategoryState.MovieType);
        }
        public List<Movie> GetMultipleSelectMovies(int? nameId, int? yearId, int? imdbId)
        {
            if (nameId != null && yearId != null && imdbId != null)
            {
                return TypeList().FirstOrDefault(I => I.Id == nameId).Movies.Join(YearList().FirstOrDefault(I => I.Id == yearId).Movies, c => c.Id, p => p.Id, (c, p) => p).Join(ImdbList().FirstOrDefault(I => I.Id == imdbId).Movies, c => c.Id, p => p.Id, (c, p) => p).ToList();
            }
            else if (nameId != null && yearId != null)
            {
                return TypeList().FirstOrDefault(I => I.Id == nameId).Movies.Join(YearList().FirstOrDefault(I => I.Id == yearId).Movies, c => c.Id, p => p.Id, (c, p) => p).ToList();
            }
            else if (nameId != null && imdbId != null)
            {
                return TypeList().FirstOrDefault(I => I.Id == nameId).Movies.Join(ImdbList().FirstOrDefault(I => I.Id == imdbId).Movies, c => c.Id, p => p.Id, (c, p) => p).ToList();
            }
            else if (yearId != null && imdbId != null)
            {
                return YearList().FirstOrDefault(I => I.Id == yearId).Movies.Join(ImdbList().FirstOrDefault(I => I.Id == imdbId).Movies, c => c.Id, p => p.Id, (c, p) => p).ToList();
            }
            else if (nameId != null)
            {
                return TypeList().FirstOrDefault(I => I.Id == nameId).Movies.ToList();
            }
            else if (yearId != null)
            {
                return YearList().FirstOrDefault(I => I.Id == yearId).Movies.ToList();
            }
            else if (imdbId != null)
            {
                return ImdbList().FirstOrDefault(I => I.Id == imdbId).Movies.ToList();
            }

            return new List<Movie>();

        }
    }
}
