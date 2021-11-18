using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfChef.Data;
using SelfChef.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipesController(ApplicationDbContext context, UserManager<ApplicationUser> manager, IWebHostEnvironment webHostEnvironment)
        {
            _manager = manager;
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult AddOrEdit(int? id)
        {
            if (id == null)
            {
                ViewBag.edit = 0;
                return View(new Recipes());
            }
            else
            {
                var recipe = _context.Recipes.Find(id);
                if (recipe == null)
                {
                    return NotFound();
                }

                ViewBag.edit = 1;
                return View(recipe);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddOrEdit(int id, Recipes recipe, List<string> categories, List<string> cuisines)
        {
            var user = await _manager.GetUserAsync(HttpContext.User);
            recipe.Category = "";
            foreach (string category in categories)
            {
                recipe.Category += category + Environment.NewLine;
            }
            recipe.Cuisine = "";
            foreach (string cuisine in cuisines)
            {
                recipe.Cuisine += cuisine + Environment.NewLine;
            }
            recipe.Author = user.UserName;
            recipe.ModifiedDate = @DateTime.Now;
            if (recipe.Picture != null)
            {
                var pathSplit = recipe.Picture.Split("recipes/");
                recipe.PictureName = pathSplit[1];
            }
            if (id == 0)
            {
                _context.Add(recipe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                _context.Update(recipe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            fileName = fileName + DateTime.UtcNow.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/img/recipes/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Json(path);
        }

        public IActionResult DeleteImage(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Json("Picture successfully removed.");
            }
            return Json("Picture doesn't exist");
                
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var reviews = _context.RecipeReviews.Where(r => r.RecipeID == id).ToList();
                int sumRatings = 0;
                double sumReviews = 0;
                int avgRatings = 0;
                ViewBag.sumReviews = sumReviews;
                foreach (var review in reviews)
                {
                    sumRatings += review.Rating;
                }
                if (reviews.Count() != 0)
                {
                    avgRatings = sumRatings / reviews.Count();
                    ViewBag.sumReviews = reviews.Count();
                }
                ViewBag.avgRatings = avgRatings;
                var recipe = _context.Recipes.Find(id);
                return View(recipe);
            }
        }

        public IActionResult ReviewsSummary(int id)
        {
            var reviews = _context.RecipeReviews.Where(r => r.RecipeID == id).ToList();
            int sumRatings = 0;
            double sumReviews = 0;
            int avgRatings = 0;
            ViewBag.sumReviews = sumReviews;
            foreach (var review in reviews)
            {
                sumRatings += review.Rating;
            }
            if (reviews.Count() != 0)
            {
                avgRatings = sumRatings / reviews.Count();
                ViewBag.sumReviews = reviews.Count();
            }
            ViewBag.avgRatings = avgRatings;
            var recipe = _context.Recipes.Find(id);
            return View(recipe);
        }
    }
}
