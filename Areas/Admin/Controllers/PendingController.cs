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
    public class PendingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PendingController(ApplicationDbContext context, UserManager<ApplicationUser> manager, IWebHostEnvironment webHostEnvironment)
        {
            _manager = manager;
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            var tables = new RecipesViewModel
            {
                Recipes = _context.Recipes.Where(v => v.Status == "Pending").ToList(),
                Users = _manager.Users
            };
            return View(tables);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangeStatus(int id, bool approved)
        {
            var recipe = _context.Recipes.Where(v => v.RecipeID == id).FirstOrDefault();
            if (recipe != null)
            {
                if (approved == true)
                {
                    recipe.Status = "Approved";
                    _context.Update(recipe);
                    _context.SaveChanges();
                    return Json("Recipe approved");
                }
                else
                {
                    recipe.Status = "Rejected";
                    _context.Update(recipe);
                    _context.SaveChanges();
                    return Json("Recipe rejected");
                }
            }
            else
            {
                return Json("No changes");
            }
        }
    }
}
