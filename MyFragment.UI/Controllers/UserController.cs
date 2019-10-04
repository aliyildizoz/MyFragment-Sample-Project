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
using MyFragment.Entities.Entity.Enums;
using MyFragment.Entities.ViewModel;
using MyFragment.UI.Filters;
using MyFragment.UI.Models;

namespace MyFragment.UI.Controllers
{
    [Aut]
    [Exc]
    public class UserController : Controller
    {
        private UserManager userManager = ManagerSingleton.CreateAsSingleton<UserManager>();
        private MovieManager movieManager = ManagerSingleton.CreateAsSingleton<MovieManager>();

        // GET: Users
        [AutAdmin]
        public ActionResult Index()
        {
            CurrentSession.Remove("selectedUser");
            return View(userManager.List());
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            TempData["key"] = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutAdmin]
        public ActionResult Create(User user)
        {
            TempData["key"] = true;

            if (ModelState.IsValid)
            {
                Result result = userManager.CreateUser(user);

                if (result.ResultState == ResultState.UsernameEmailAlreadyExists)
                {
                    ModelState.AddModelError("", "Kullanıcı adı ve E-posta adresi kullanılıyor.");
                }
                else if (result.ResultState == ResultState.UsernameAlreadyExists)
                {
                    ModelState.AddModelError("", "Kullanıcı adı kullanılıyor.");
                }
                else if (result.ResultState == ResultState.EmailAlreadyExists)
                {
                    ModelState.AddModelError("", "E-posta adresi kullanılıyor.");
                }
                else if (result.ResultState == ResultState.Error)
                {
                    ModelState.AddModelError("", "HATA!! Kayıt yapılamadı.");
                }
                else if (result.ResultState == ResultState.Success)
                {
                    return RedirectToAction("Index");
                }
                if (ModelState.Count > 0)
                {
                    TempData["key"] = false;
                }
            }

            return View();
        }

        // GET: Users/Edit/5
        [AutAdmin]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userManager.Find(I => I.Id == id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            TempData["key"] = true;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutAdmin]
        public ActionResult Edit(User model, HttpPostedFileBase ImagePath)
        {
            TempData["key"] = true;

            if (ModelState.IsValid)
            {

                if (ImagePath != null)
                {
                    User principal = userManager.Find(I => I.Id == model.Id);
                    if (principal.ImagePath != "defaultPhoto.png")
                    {
                        System.IO.File.Delete(Server.MapPath($"~/img/UserPhotos/{principal.ImagePath}"));
                    }
                    model.ImagePath = $"user_{model.Id}.{ImagePath.ContentType.Split('/')[1]}";
                    ImagePath.SaveAs(Server.MapPath($"~/img/UserPhotos/{model.ImagePath}"));
                    userManager.Save();
                }
                Result result = userManager.EditUser(model);

                if (result.ResultState == ResultState.UsernameEmailAlreadyExists)
                {
                    ModelState.AddModelError("", "Kullanıcı adı ve E-posta adresi kullanılıyor.");
                }
                else if (result.ResultState == ResultState.UsernameAlreadyExists)
                {
                    ModelState.AddModelError("", "Kullanıcı adı kullanılıyor.");
                }
                else if (result.ResultState == ResultState.EmailAlreadyExists)
                {
                    ModelState.AddModelError("", "E-posta adresi kullanılıyor.");
                }
                else if (result.ResultState == ResultState.Error)
                {
                    ModelState.AddModelError("", "HATA!! Kayıt yapılamadı.(Lütfen geçerli bir değişikilik yapınız.)");
                }
                else if (result.ResultState == ResultState.Success)
                {
                    return RedirectToAction("Index");
                }
                if (ModelState.Count > 0)
                {
                    TempData["key"] = false;
                }
            }
            return View(model);
        }

        // GET: Users/Delete/5
        [AutAdmin]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userManager.Find(I => I.Id == id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutAdmin]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userManager.Find(I => I.Id == id);
            userManager.Delete(user);
            if (user.ImagePath != "defaultPhoto.png")
            {
                System.IO.File.Delete(Server.MapPath($"~/img/UserPhotos/{user.ImagePath}"));
            }
            return RedirectToAction("Index");
        }
        public ActionResult UserMovies(int? id)
        {
            CurrentSession.Remove("selectedMovie");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userManager.Find(I => I.Id == id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            CurrentSession.Set("selectedUser", user);
            return View(user.Movies);
        }
        public ActionResult GetModalFragment(int? id)
        {
            return PartialView("_PartialModalFragment", movieManager.Find(I => I.Id == id.Value));
        }
        public ActionResult GetModalSummary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return PartialView("_PartialModalSummary", movieManager.Find(I => I.Id == id.Value));
        }
        public ActionResult GetModalActors(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return PartialView("_PartialModalActors", movieManager.Find(I => I.Id == id.Value).Actors);
        }
        public ActionResult GetModalDirectors(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return PartialView("_PartialModalDirectors", movieManager.Find(I => I.Id == id.Value).Directors);
        }
        public ActionResult GetModalCategories(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return PartialView("_PartialModalCategories", movieManager.Find(I => I.Id == id.Value).Categories);
        }
    }
}
