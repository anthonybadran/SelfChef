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
    public class IngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IngredientsController(ApplicationDbContext context, UserManager<ApplicationUser> manager, IWebHostEnvironment webHostEnvironment)
        {
            _manager = manager;
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string term, List<string> include, List<string> exclude)
        {
            var recipes = from m in _context.Recipes
                          select m;
            string[] included;
            string[] excluded;
            if (!String.IsNullOrEmpty(term))
            {
                recipes = recipes.Where(s => s.Description.Contains(term));
            }
            if (include[1] != null)
            {
                included = include[1].Split(',');
                foreach (var ingredient in included)
                {
                    if (ingredient != null)
                    {
                        recipes = recipes.Where(s => s.Ingredients.Contains(ingredient));
                    }
                }
                ViewBag.include = include[1];
            }
            if (exclude[1] != null)
            {
                excluded = exclude[1].Split(',');
                foreach (var ingredient in excluded)
                {
                    if (ingredient != null)
                    {
                        recipes = recipes.Where(s => !s.Ingredients.Contains(ingredient));
                    }
                }
                ViewBag.exclude = exclude[1];
            }
            var recipes1 = recipes.ToList();
            ViewBag.term = term;
            return View(recipes.ToList());
        }
    }
}
