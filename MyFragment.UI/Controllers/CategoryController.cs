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
    [Aut]
    [AutAdmin]
    [Exc]
    public class CategoryController : Controller
    {
        private CategoryManager categoryManager = new CategoryManager();
        // GET: Categories
        public ActionResult Index()
        {
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            if (movie == null)
            {
                return View(categoryManager.List());
            }
            return View(categoryManager.TypeList().ToList().Join(movie.Categories, c => c.Id, p => p.Id, (c, p) => p).ToList());
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            if (movie != null)
            {
                model = categoryManager.Find(I => I.Id == model.Id);
                if (!model.Movies.Contains(movie))
                {
                    model.Movies.Add(movie);
                }
                categoryManager.Save();
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                categoryManager.Insert(model);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(I => I.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model, int? newNameId)
        {
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            Category category = categoryManager.Find(I => I.Id == model.Id);
            if (movie != null)
            {
                if (newNameId != null)
                {
                    Category addedCategory = categoryManager.Find(I => I.Id == newNameId.Value);
                    category.Movies.Remove(movie);
                    addedCategory.Movies.Add(movie);
                    categoryManager.Save();
                    return RedirectToAction("Index");
                }
            }
            if (ModelState.IsValid)
            {

                category.Value = model.Value;
                category.CategoryState = model.CategoryState;
                categoryManager.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(I => I.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryManager.Find(I => I.Id == id);
            Movie movie = CurrentSession.Get<Movie>("selectedMovie");
            if (movie != null)
            {
                movie.Categories.Remove(category);
                categoryManager.Save();
                return RedirectToAction("Index");
            }
            categoryManager.Delete(category);
            return RedirectToAction("Index");
        }
    }
}
