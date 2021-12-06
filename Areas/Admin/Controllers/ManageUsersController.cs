using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfChef.Areas.Admin.Models;
using SelfChef.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ManageUsersController(ApplicationDbContext context, UserManager<ApplicationUser> manager, IWebHostEnvironment webHostEnvironment)
        {
            _manager = manager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _manager.GetUserAsync(HttpContext.User);
            var allUsersExceptCurrentUser = await _manager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
            return View(allUsersExceptCurrentUser);
        }

        public IActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser { UserName = user.Email, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName };
                var result = await _manager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(user);
        }

        public IActionResult Edit(string id)
        {
            var user = _manager.Users.Where(a => a.Id == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(id);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.NormalizedEmail = model.Email.ToUpper();
                user.NormalizedUserName = model.Email.ToUpper();
                user.UserName = model.Email;
                var result = await _manager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return Json("No user provided");
                }

                var user = await _manager.FindByIdAsync(id);
                var rolesForUser = await _manager.GetRolesAsync(user);

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        var result = await _manager.RemoveFromRoleAsync(user, item);
                    }
                }
                await _manager.DeleteAsync(user);

                return Json("User successfully deleted");
            }
            else
            {
                return Json("Cannot delete user");
            }
        }
    }
}
