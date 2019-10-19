using MyFragment.Business;
using MyFragment.Business.Abstract;
using MyFragment.Business.Manager;
using MyFragment.Entities.Entity;
using MyFragment.Entities.Entity.Enums;
using MyFragment.Entities.ViewModel;
using MyFragment.UI.Filters;
using MyFragment.UI.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFragment.UI.Controllers
{
    [Exc]
    public class HomeController : Controller
    {

        private MovieManager movieManager = ManagerSingleton.CreateAsSingleton<MovieManager>();
        private UserManager userManager = ManagerSingleton.CreateAsSingleton<UserManager>();
        private CategoryManager categoryManager = new CategoryManager();
        public ActionResult Index(int pageNumber = 1)
        {
            CurrentSession.Remove("selectedMovie");
            CurrentSession.Set("selectClear", true);
            ViewBag.selectedCategory = false;
            TempData["carouselEmbedKeys"] = movieManager.List().Take(5).Select(I => I.EmbedKey).ToList();
            return View(movieManager.List().ToPagedList(pageNumber, 6));
        }
        public ActionResult ByCategory(int? nameId, int? yearId, int? imdbId, int? pageNumber = 1)
        {
            TempData["carouselEmbedKeys"] = movieManager.List().Take(5).Select(I => I.EmbedKey).ToList();
            if (nameId == null && yearId == null && imdbId == null)
            {
                return View("Index", new List<Movie>().ToPagedList(1, 6));
            }
            ViewBag.selectedNameId = nameId;
            ViewBag.selectedYearId = yearId;
            ViewBag.selectedImdbId = imdbId;

            ViewBag.selectedCategory = true;
            return View("Index", categoryManager.GetMultipleSelectMovies(nameId, yearId, imdbId).ToPagedList(pageNumber.Value, 6));
        }

        public ActionResult ShowMovies(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            return View(movieManager.Find(I => I.Id == id));
        }

        [HttpPost]
        public ActionResult GetSearchMovies(string searchText)
        {
            return View("Index", movieManager.QueryableList(I => I.Name.Contains(searchText)).ToList().ToPagedList(1, 6));
        }

        [HttpPost]
        public ActionResult SetLikeState(int movieId, bool liked)
        {
            User user = CurrentSession.user;
            Movie movie = movieManager.Find(I => I.Id == movieId);
            if (liked)
            {
                user.Movies.Add(movie);
            }
            else
            {
                user.Movies.Remove(movie);
            }
            if (userManager.Save() > 0)
            {
                return Json(new { hasError = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hasError = true, errorMessage = "Sunucu ile ilgili bir hata oluştu." }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult About()
        {
            CurrentSession.Remove("selectedMovie");
            return View();
        }
        public ActionResult HasError()
        {

            return View();
        }
        public ActionResult AccessDenied()
        {

            return View();
        }

        #region Profile
        [Aut]
        public ActionResult ShowProfile()
        {
            return View(CurrentSession.user);
        }

        [Aut]
        public ActionResult EditProfile()
        {
            TempData["key"] = true;
            return View(CurrentSession.user);
        }

        [HttpPost]
        [Aut]
        public ActionResult EditProfile(User model, HttpPostedFileBase ImagePath)
        {
            TempData["key"] = true;

            if (ModelState.IsValid)
            {
                User principal = userManager.Find(I => I.Id == model.Id);
                if (ImagePath != null)
                {
                    if (principal.ImagePath != "defaultPhoto.png")
                    {
                        System.IO.File.Delete(Server.MapPath($"~/img/UserPhotos/{principal.ImagePath}"));
                    }
                    model.ImagePath = $"user_{model.Id}.{ImagePath.ContentType.Split('/')[1]}";
                    ImagePath.SaveAs(Server.MapPath($"~/img/UserPhotos/{model.ImagePath}"));
                    userManager.Save();
                }
                else
                {
                    model.ImagePath = principal.ImagePath;
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
                    ModelState.AddModelError("", "HATA!! Kayıt yapılamadı.");
                }
                else if (result.ResultState == ResultState.Success)
                {
                    return RedirectToAction("ShowProfile");
                }
                if (ModelState.Count > 0)
                {
                    TempData["key"] = false;
                }
            }
            return View(model);
        }

        [Aut]
        public ActionResult DeleteProfile()
        {
            return View(CurrentSession.user);
        }

        [HttpPost]
        [Aut]
        public ActionResult DeleteProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            User user = userManager.Find(I => I.Id == id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            userManager.Delete(user);
            userManager.Save();
            CurrentSession.Remove("login");
            return RedirectToAction("Index");
        }
        #endregion

        #region IOR
        public ActionResult Register()
        {
            TempData["key"] = true;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            User user = userManager.ConvertToUser(registerViewModel);
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
                    CurrentSession.Set("login", result.User);
                    return RedirectToAction("Index");

                }
                if (ModelState.Count > 0)
                {
                    TempData["key"] = false;
                }
            }

            return View();
        }
        public ActionResult Login()
        {
            TempData["key"] = true;
            return View();

        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            TempData["key"] = true;
            if (ModelState.IsValid)
            {
                Result result = userManager.Login(new User() { Username = loginViewModel.Username, Password = loginViewModel.Password });
                if (result.ResultState == ResultState.NotFound)
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                }
                else
                {
                    CurrentSession.Set("login", result.User);
                    return RedirectToAction("Index");
                }

                if (ModelState.Count > 0)
                {
                    TempData["key"] = false;
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            CurrentSession.Remove("login");
            return RedirectToAction("Index");
        }
        #endregion

    }
}