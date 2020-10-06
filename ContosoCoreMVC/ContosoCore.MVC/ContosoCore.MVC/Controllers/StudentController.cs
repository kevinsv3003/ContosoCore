using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContosoCore.MVC.WebServiceAccess.Base;
using ContosoCore.MVC.WebServiceAccess;
using System.Net.Http;
using Newtonsoft.Json;
using ContosoCore.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ContosoCore.MVC.Controllers
{
    public class StudentController : Controller
    {

        //Primeroooo inyectamos la interfaz en el constructor
        private readonly IWebApiCalls _webApiCalls;

        public StudentController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        // GET: Student
        [Authorize(Roles ="Programadores,Admin")]
        public async Task<ActionResult> Index()
        {
            var student = await _webApiCalls.GetStudentsAll("");
            if (student != null)
            {
                return View(student);
            }
            return NotFound();
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View(new Student());
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Student student)
        {
            try
            {
                // TODO: Add insert logic here
                if(student != null)
                {
                    await _webApiCalls.AddStudent(student);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var student = await _webApiCalls.GetStudent(id);
            if (student != null)
            {
                return View(student);
            }
            return NotFound();
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Student student)
        {
            try
            {
                // TODO: Add update logic here
                if (student != null)
                {
                    await _webApiCalls.UpdateStudent(student);
                    return RedirectToAction(nameof(Index));
                }
                return View(new Student());
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var student = await _webApiCalls.GetStudent(id);
            if (student != null)
            {
                return View(student);
            }
            return NotFound();
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Student student)
        {
            try
            {
                // TODO: Add delete logic here
                if (student != null)
                {
                    await _webApiCalls.DeleteStudent(student.Id);
                    return RedirectToAction(nameof(Index));
                }
                return View(new Student());
            }
            catch
            {
                return View();
            }
        }
    }
}