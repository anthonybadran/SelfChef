using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfChef.Data;
using SelfChef.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public RecipesController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _manager = manager;
            _context = context;
        }

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
        public IActionResult AddOrEdit(int id, Recipes recipe)
        {
            if (id == 0)
            {
                recipe.ModifiedDate = @DateTime.Now;
                _context.Add(recipe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                recipe.ModifiedDate = @DateTime.Now;
                _context.Update(recipe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
