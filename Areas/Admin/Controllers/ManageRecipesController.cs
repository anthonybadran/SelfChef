using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfChef.Areas.Admin.ViewModels;
using SelfChef.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageRecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ManageRecipesController(ApplicationDbContext context, UserManager<ApplicationUser> manager, IWebHostEnvironment webHostEnvironment)
        {
            _manager = manager;
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string searchString)
        {
            var recipes = from m in _context.Recipes.Where(r => r.Status == "Approved")
                                        select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.Title.Contains(searchString) || s.Description.Contains(searchString));
                ViewBag.query = searchString;
            }
            var tables = new RecipesViewModel
            {
                Recipes = recipes.ToList(),
                Users = _manager.Users
            };
            return View(tables);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _manager.GetUserAsync(HttpContext.User);
            var recipe = await _context.Recipes.FindAsync(id);
            if (user.Id == recipe.Author || await _manager.IsInRoleAsync(user, "Admin"))
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
                var reviews = _context.RecipeReviews.Where(a => a.RecipeID == id).ToList();
                foreach (var review in reviews)
                {
                    _context.RecipeReviews.Remove(review);
                    _context.SaveChanges();

                    var votes = _context.ReviewVotes.Where(a => a.ReviewID == review.ReviewID).ToList();
                    foreach (var vote in votes)
                    {
                        _context.ReviewVotes.Remove(vote);
                        _context.SaveChanges();
                    }
                }
                var favorites = _context.Favorites.Where(a => a.RecipeID == id).ToList();
                foreach (var favorite in favorites)
                {
                    _context.Favorites.Remove(favorite);
                    _context.SaveChanges();
                }
                if (System.IO.File.Exists(recipe.Picture))
                {
                    System.IO.File.Delete(recipe.Picture);
                    return Json("Picture successfully removed.");
                }
                return Json("Recipe successfully deleted.");
            }
            return Json("Not Allowed");
        }
    }
}
