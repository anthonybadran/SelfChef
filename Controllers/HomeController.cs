using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SelfChef.ViewModels;
using SelfChef.Data;
using SelfChef.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace SelfChef.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> manager, IWebHostEnvironment webHostEnvironment)
        {
            _manager = manager;
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string id, string searchString, string category, string cuisine)
        {
            var recipes = from m in _context.Recipes.Where(r => r.Private != true && r.Status == "Approved")
                         select m;
            if (id == "Popular")
            {
                var mostPopularId = _context.RecipeReviews
                    .GroupBy(i => i.RecipeID)
                    .OrderByDescending(grp => grp.Count())
                    .Select(grp => grp.Key).ToList();

                List<Recipes> mostPopularRecipes = new List<Recipes>();

                foreach (var recipeId in mostPopularId)
                    mostPopularRecipes.Add(recipes.Where(r => r.RecipeID == recipeId).FirstOrDefault());

                if (mostPopularRecipes.Count() > 10)
                {
                    mostPopularRecipes = mostPopularRecipes.Take(10).ToList();
                }
                ViewBag.popular = "Popular Recipes";
                var table = new RecipesViewModel
                {
                    Recipes = mostPopularRecipes.ToList(),
                    Users = _manager.Users,
                    Reviews = _context.RecipeReviews.ToList()
                };
                return View(table);
            }
            if (!String.IsNullOrEmpty(category))
            {
                recipes = recipes.Where(s => s.Category.Contains(category));
                ViewBag.category = category;
            }
            if (!String.IsNullOrEmpty(cuisine))
            {
                recipes = recipes.Where(s => s.Cuisine.Contains(cuisine));
                ViewBag.cuisine = cuisine;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.Title.Contains(searchString) || s.Description.Contains(searchString));
                ViewBag.query = searchString;
            }
            var tables = new RecipesViewModel
            {
                Recipes = recipes.ToList(),
                Users = _manager.Users,
                Reviews = _context.RecipeReviews.ToList()
            };
            return View(tables);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
