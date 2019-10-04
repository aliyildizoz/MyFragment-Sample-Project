using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyFragment.Business.Abstract;
using MyFragment.Business.Manager;
using MyFragment.Entities.Entity;
using MyFragment.UI.Filters;
using MyFragment.UI.Models;

namespace MyFragment.UI.Controllers
{
    [AutAdmin]
    [Exc]
    public class MovieController : Controller
    {
        private MovieManager movieManager = ManagerSingleton.CreateAsSingleton<MovieManager>();

        #region Movie
        // GET: Movie
        public ActionResult Index()
        {
            if (CurrentSession.Get<Movie>("selectedMovie") != null)
            {
                CurrentSession.Remove("selectedMovie");
            }
            return View(movieManager.List());
        }

        public ActionResult GetModalSummary(int? id)
        {
            return PartialView("_PartialModalSummary", movieManager.Find(I => I.Id == id.Value));
        }
        public ActionResult GetModalFragment(int? id)
        {
            return PartialView("_PartialModalFragment", movieManager.Find(I => I.Id == id.Value));
        }

        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieManager.Find(I => I.Id == id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie model, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                movieManager.Insert(model);
                Movie movie = movieManager.Find(I => I.Name == model.Name && I.Summary.Length == model.Summary.Length && I.MovieYear == model.MovieYear);
                if (ImagePath != null)
                {
                    movie.ImagePath = $"{movie.ImagePath}.{ImagePath.ContentType.Split('/')[1]}";
                    ImagePath.SaveAs(Server.MapPath($"~/img/MoviePoster/{movie.ImagePath}"));
                    movieManager.Save();
                }
                else
                {
                    movie.ImagePath = "defaultPoster.png";
                    movieManager.Save();
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieManager.Find(I => I.Id == id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie model, int? yearId, int? imdbId, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid && yearId != null && imdbId != null)
            {
                Movie movie = movieManager.Find(I => I.Id == model.Id);
                if (movie == null)
                {
                    return HttpNotFound();
                }
                if (ImagePath != null)
                {
                    if (movie.ImagePath != "defaultPoster.png")
                    {
                        System.IO.File.Delete(Server.MapPath($"~/img/MoviePoster/{movie.ImagePath}"));
                    }
                    model.ImagePath = $"movie_{model.Id}.{model.ImagePath.Split('.')[1]}";
                    ImagePath.SaveAs(Server.MapPath($"~/img/MoviePoster/{model.ImagePath}"));
                }
                movieManager.Update(model, yearId, imdbId);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieManager.Find(I => I.Id == id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = movieManager.Find(I => I.Id == id);
            movieManager.Delete(movie);

            if (movie.ImagePath != "defaultPoster.png")
            {
                System.IO.File.Delete(Server.MapPath($"~/img/MoviePoster/{movie.ImagePath}"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult MovieActors(int? movieId)
        {
            if (movieId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieManager.Find(I => I.Id == movieId.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            CurrentSession.Set("selectedMovie", movie);
            return RedirectToAction("Index", "Actor");
        }
        public ActionResult DirectorMovies(int? movieId)
        {
            if (movieId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieManager.Find(I => I.Id == movieId.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            CurrentSession.Set("selectedMovie", movie);
            return RedirectToAction("Index", "Director");
        }
        public ActionResult CategoryMovies(int? movieId)
        {
            if (movieId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = movieManager.Find(I => I.Id == movieId.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            CurrentSession.Set("selectedMovie", movie);
            return RedirectToAction("Index", "Category");
        }
        #endregion
    }
}
