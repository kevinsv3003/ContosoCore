using ContosoCore.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ContosoCore.MVC.Controllers
{
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;

        public RoleAdminController(RoleManager<IdentityRole> role, UserManager<AppUser> user)
        {
            roleManager = role;
            userManager = user;
        }


        // GET: RoleAdmin
        public ActionResult Index()
        {
            return View(roleManager.Roles);
        }

        // GET: RoleAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }

        // GET: RoleAdmin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<AppUser> Members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();

            foreach (var item in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(item, role.Name) ? Members : nonMembers;
                list.Add(item);
            }
            return View(new RoleEditModel {
                Role = role,
                Members = Members,
                NonMembers = nonMembers
            
            });
        }

        // POST: RoleAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (var item in model.IdsToAdd ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(item);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                foreach (var item in model.IdsToDelete ?? new string[] { })
                {
                    AppUser user = await userManager.FindByIdAsync(item);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return await Edit(model.RoleId);
        }

        // GET: RoleAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleAdmin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found!");
            }
            return View("Index", roleManager.Roles);
        }
    }
}