using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    [Exc]
    public class DirectorController : Controller
    {
        private DirectorManager directorManager = ManagerSingleton.CreateAsSingleton<DirectorManager>();
        // GET: Directors
        [AutAdmin]
        public ActionResult Index()
        {
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            if (movie == null)
            {
                return View(directorManager.List());
            }
            return View(movie.Directors);
        }

        // GET: Directors/Create
        [AutAdmin]
        public ActionResult Create(bool? selector)
        {
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            CurrentSession.Set("notNormalCreate", false);
            if (selector != null)
            {
                if (selector.Value)
                {
                    CurrentSession.Set("notNormalCreate", false);
                    return View();
                }
            }
            if (movie != null)
            {
                CurrentSession.Set("notNormalCreate", true);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutAdmin]
        public ActionResult Create(Director model, HttpPostedFileBase ImagePath)
        {
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            CurrentSession.Remove("notNormalCreate");
            if (model.Id != 0)
            {
                Director director = directorManager.Find(I => I.Id == model.Id);
                movie.Directors.Add(director);
                directorManager.Save();
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                directorManager.Insert(model);
                Director director = directorManager.Find(I => I.Name == model.Name && I.Surname == model.Surname);
                if (ImagePath != null)
                {
                    director.ImagePath = $"{director.ImagePath}.{ImagePath.ContentType.Split('/')[1]}";
                    ImagePath.SaveAs(Server.MapPath($"~/img/DirectorPhotos/{director.ImagePath}"));
                }
                else
                {
                    director.ImagePath = "defaultPhoto.png";
                }
                directorManager.Save();
                if (movie != null)
                {
                    director.Movies.Add(movie);
                    directorManager.Attach(director);
                    directorManager.Save();
                    return RedirectToAction("Index");
                }
                directorManager.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Directors/Edit/5
        [AutAdmin]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = directorManager.Find(I => I.Id == id.Value);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutAdmin]
        public ActionResult Edit(Director model, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                Director director = directorManager.Find(I => I.Id == model.Id);
                string imagePath = director.ImagePath;
                director.ImagePath = model.ImagePath;
                if (ImagePath != null)
                {
                    if (imagePath != "defaultPhoto.png")
                    {
                        System.IO.File.Delete(Server.MapPath($"~/img/DirectorPhotos/{imagePath}"));
                    }
                    director.ImagePath = $"director_{model.Id}.{ImagePath.ContentType.Split('/')[1]}";
                    ImagePath.SaveAs(Server.MapPath($"~/img/DirectorPhotos/{director.ImagePath}"));
                }
                director.Name = model.Name;
                director.Surname = model.Surname;
                directorManager.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Directors/Delete/5
        [AutAdmin]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = directorManager.Find(I => I.Id == id.Value);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutAdmin]
        public ActionResult DeleteConfirmed(int id)
        {
            Director director = directorManager.Find(I => I.Id == id);
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            if (movie != null)
            {
                movie.Directors.Remove(director);
                directorManager.Save();
                return RedirectToAction("Index");
            }

            directorManager.Delete(director);
            if (director.ImagePath != "defaultPhoto.png")
            {
                System.IO.File.Delete(Server.MapPath($"~/img/DirectorPhotos/{director.ImagePath}"));
            }
            return RedirectToAction("Index");
        }
        public ActionResult ShowDirector(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Director director = directorManager.Find(I => I.Id == id.Value);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }
    }
}
