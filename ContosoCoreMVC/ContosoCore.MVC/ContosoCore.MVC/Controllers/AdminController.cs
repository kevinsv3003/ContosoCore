using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ContosoCore.MVC.Models;

namespace ContosoCore.MVC.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;

        public AdminController(UserManager<AppUser> usrMgr, IPasswordHasher<AppUser> passHas)
        {
            userManager = usrMgr;
            passwordHasher = passHas;
        }


        // GET: Admin
        public ActionResult Index()
        {
            return View(userManager.Users);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateModel usuario)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    AppUser user = new AppUser();
                    user.UserName = usuario.Name;
                    user.Email = usuario.Email;

                    IdentityResult result = await userManager.CreateAsync(user, usuario.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("",error.Description);
                        }
                    }
                }
                return View(usuario);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }            
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            try
            {
                // TODO: Add update logic here
                AppUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Email = email;
                    user.PasswordHash = passwordHasher.HashPassword(user, password);

                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorFromResult(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuario no se encontró!");
                }
                return View("Index",user);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                // TODO: Add delete logic here
                AppUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        AddErrorFromResult(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuario no encontrado!");
                }
                return View("Index",userManager.Users);
            }
            catch
            {
                return View();
            }
        }

        private void AddErrorFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}