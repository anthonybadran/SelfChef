using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfChef.Data;
using SelfChef.Models;
using SelfChef.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FavoritesController(ApplicationDbContext context, UserManager<ApplicationUser> manager, IWebHostEnvironment webHostEnvironment)
        {
            _manager = manager;
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> Index(string user)
        {
            var currentUser = await _manager.GetUserAsync(HttpContext.User);
            if (currentUser.Id != user)
            {
                return NotFound();
            }
            var tables = new FavoritesViewModel
            {
                Favorites = _context.Favorites.Where(v => v.AuthorID == user).ToList(),
                Recipes = _context.Recipes.ToList()
            };
            ViewBag.user = user;
            return View(tables);
        }

        public IActionResult FavoritesList(string user)
        {
            var tables = new FavoritesViewModel
            {
                Favorites = _context.Favorites.Where(v => v.AuthorID == user).ToList(),
                Recipes = _context.Recipes.ToList()
            };
            ViewBag.user = user;
            return View(tables);
        }

        [HttpGet]
        public IActionResult AddToFavorite(int id)
        {
            ViewBag.RecipeID = id;
            return View(_context.Favorites.Where(f => f.RecipeID == id).ToList());
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddToFavorites(int recipe, string user)
        {
            if (user == null)
            {
                return RedirectToAction("Details", "Recipes", new { id = recipe });
            }
            else
            {
                var favorite = _context.Favorites.Where(v => v.AuthorID == user && v.RecipeID == recipe).FirstOrDefault();
                if (favorite == null)
                {
                    favorite = new Favorites();
                    favorite.AuthorID = user;
                    favorite.RecipeID = recipe;
                    _context.Add(favorite);
                    _context.SaveChanges();
                    return Json("Recipe successfully added to favorites");
                }
                else
                {
                    _context.Favorites.Remove(favorite);
                    _context.SaveChanges();
                    return Json("Recipe successfully removed from favorites");
                }

            }
        }
    }
}
