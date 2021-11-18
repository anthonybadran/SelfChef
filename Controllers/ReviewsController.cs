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
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReviewsController(ApplicationDbContext context, UserManager<ApplicationUser> manager, IWebHostEnvironment webHostEnvironment)
        {
            _manager = manager;
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize]
        public async Task<IActionResult> AddOrEdit(int? recipe, int id = 0)
        {
            if (id == 0)
            {
                var user = await _manager.GetUserAsync(HttpContext.User);
                ViewBag.recipe = recipe;
                if (user != null)
                    ViewBag.author = user.Id;
                return View(new RecipeReviews());
            }
            else
            {
                var review = await _context.RecipeReviews.FindAsync(id);
                if (review == null)
                {
                    return NotFound();
                }
                var user = await _manager.GetUserAsync(HttpContext.User);
                ViewBag.recipe = recipe;
                if (user != null)
                    ViewBag.author = user.Id;
                return View(review);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddOrEdit(int id, RecipeReviews review)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    review.ModifiedDate = @DateTime.Now;
                    _context.Add(review);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    review.ModifiedDate = DateTime.Now;
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                var tables = new ReviewsViewModel
                {
                    Reviews = _context.RecipeReviews.Where(s => s.RecipeID == id).ToList(),
                    Users = _manager.Users,
                };
                return Json(new { isValid = true });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", review) });
        }

        public IActionResult ViewReviews(int id)
        {
            var tables = new ReviewsViewModel
            {
                Reviews = _context.RecipeReviews.Where(s => s.RecipeID == id).ToList(),
                Users = _manager.Users,
                ReviewVotes = _context.ReviewVotes.ToList()
            };
            return View(tables);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var review = _context.RecipeReviews.Find(id);
            _context.RecipeReviews.Remove(review);
            _context.SaveChanges();
            return Json("Review successfuly deleted");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Helpful(int id, string user, int recipe)
        {
            if (user == null)
            {
                return RedirectToAction("Details", "Recipes", new { id = recipe });
            }
            else
            {
                var vote = _context.ReviewVotes.Where(v => v.AuthorID == user && v.ReviewID == id).FirstOrDefault();
                if (vote == null)
                {
                    vote = new ReviewVotes();
                    vote.AuthorID = user;
                    vote.ReviewID = id;
                    vote.Vote = 1;
                    _context.Add(vote);
                    _context.SaveChanges();
                    return Json("Review successfully marked as helpful");
                }
                else
                {
                    _context.ReviewVotes.Remove(vote);
                    _context.SaveChanges();
                    return Json("Review successfully marked as NOT helpful");
                }
                
            }
        }
    }
}
