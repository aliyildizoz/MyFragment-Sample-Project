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
    public class ActorController : Controller
    {
        private ActorManager actorManager = ManagerSingleton.CreateAsSingleton<ActorManager>();

        // GET: Actors
        [AutAdmin]
        public ActionResult Index()
        {
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            if (movie == null)
            {
                return View(actorManager.List());
            }
            return View(movie.Actors);
        }

        // GET: Actors/Create
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
        public ActionResult Create(Actor model, HttpPostedFileBase ImagePath)
        {
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            CurrentSession.Remove("notNormalCreate");
            if (model.Id != 0)
            {
                Actor actor = actorManager.Find(I => I.Id == model.Id);
                movie.Actors.Add(actor);
                actorManager.Save();
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                actorManager.Insert(model);
                Actor actor = actorManager.Find(I => I.Name == model.Name && I.Surname == model.Surname);
                if (ImagePath != null)
                {
                    actor.ImagePath = $"{actor.ImagePath}.{ImagePath.ContentType.Split('/')[1]}";
                    ImagePath.SaveAs(Server.MapPath($"~/img/ActorPhotos/{actor.ImagePath}"));
                }
                else
                {
                    actor.ImagePath = "defaultPhoto.png";
                }
                actorManager.Save();
                if (movie != null)
                {
                    actor.Movies.Add(movie);
                    actorManager.Attach(actor);
                    actorManager.Save();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Actors/Edit/5
        [AutAdmin]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = actorManager.Find(I => I.Id == id.Value);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutAdmin]
        public ActionResult Edit(Actor model, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                Actor actor = actorManager.Find(I => I.Id == model.Id);
                string imagePath = actor.ImagePath;
                actor.ImagePath = model.ImagePath;
                if (ImagePath != null)
                {
                    if (imagePath != "defaultPhoto.png")
                    {
                        System.IO.File.Delete(Server.MapPath($"~/img/ActorPhotos/{imagePath}"));
                    }
                    actor.ImagePath = $"actor_{model.Id}.{ImagePath.ContentType.Split('/')[1]}";
                    ImagePath.SaveAs(Server.MapPath($"~/img/ActorPhotos/{actor.ImagePath}"));
                }
                actor.Name = model.Name;
                actor.Surname = model.Surname;
                int resut = actorManager.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Actors/Delete/5
        [AutAdmin]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = actorManager.Find(I => I.Id == id.Value);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutAdmin]
        public ActionResult DeleteConfirmed(int id)
        {
            Actor actor = actorManager.Find(I => I.Id == id);
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            if (movie != null)
            {
                movie.Actors.Remove(actor);
                actorManager.Save();
                return RedirectToAction("Index");
            }

            actorManager.Delete(actor);
            if (actor.ImagePath != "defaultPhoto.png")
            {
                System.IO.File.Delete(Server.MapPath($"~/img/ActorPhotos/{actor.ImagePath}"));
            }
            return RedirectToAction("Index");
        }

        public ActionResult ShowActor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Actor actor = actorManager.Find(I => I.Id == id.Value);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }
    }
}
