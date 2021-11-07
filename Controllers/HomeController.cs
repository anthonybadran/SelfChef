using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SelfChef.Data;
using SelfChef.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(string searchString, string category, string cuisine)
        {
            var recipes = from m in _context.Recipes
                         select m;
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
            return View(recipes.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
