using MyFragment.Business.Abstract;
using MyFragment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Business.Manager
{
    public class MovieManager : ManagerBase<Movie>, IManager
    {
        private static MovieManager _movieManager;
        private CategoryManager categoryManager = new CategoryManager();
        private MovieManager()
        {

        }

        public static MovieManager CreateAsSingleton()
        {
            if (_movieManager == null)
            {
                _movieManager = new MovieManager();
            }
            return _movieManager;
        }

        public override int Insert(Movie entity)
        {
            entity.AddedDate = DateTime.Now;
            Category categoryImdb = categoryManager.ImdbList().ToList().Find(I => I.Id == int.Parse(entity.ImdbPoint));
            Category categoryYear = categoryManager.YearList().ToList().Find(I => I.Id == entity.MovieYear);
            entity.ImdbPoint = categoryImdb.Value;
            entity.MovieYear = int.Parse(categoryYear.Value);
            entity.Categories.Add(categoryImdb);
            entity.Categories.Add(categoryYear);
            base.Insert(entity);

            Movie movie = Find(I => I.Name == entity.Name);
            movie.ImagePath = $"movie_{movie.Id.ToString()}";


            return Save();
        }
        public new int Update(Movie entity, int? yearId, int? imdbId)
        {
            Movie movie = Find(I => I.Id == entity.Id);

            Category categoryImdb = categoryManager.ImdbList().ToList().Find(I => I.Id == imdbId);
            Category categoryYear = categoryManager.YearList().ToList().Find(I => I.Id == yearId);
            entity.ImdbPoint = categoryImdb.Value;
            entity.MovieYear = int.Parse(categoryYear.Value);
            if (entity.ImdbPoint != movie.ImdbPoint)
            {
                movie.Categories.Remove(categoryManager.ImdbList().ToList().Find(I => I.Value == movie.ImdbPoint));
                movie.Categories.Add(categoryImdb);
            }
            if (entity.MovieYear != movie.MovieYear)
            {
                movie.Categories.Remove(categoryManager.YearList().ToList().Find(I => I.Value == movie.MovieYear.ToString()));
                movie.Categories.Add(categoryYear);
            }

            movie.ImagePath = entity.ImagePath;
            movie.ImdbPoint = entity.ImdbPoint;
            movie.Lenght = entity.Lenght;
            movie.Name = entity.Name;
            movie.MovieYear = entity.MovieYear;
            movie.Summary = entity.Summary;
            movie.EmbedKey = entity.EmbedKey;
            Save();

            return 0;
        }

    }
}
