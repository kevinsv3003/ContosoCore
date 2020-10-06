using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCore.DAL.Repos.Interfaces;
using ContosoCore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoCore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentRepo Repo;

        public StudentController(IStudentRepo repo)
        {
            Repo = repo;
        }

        // GET: api/Student
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Repo.GetAll());
        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var student = Repo.Find(id);
            if (student != null)
                return Ok(student);
            return NotFound();
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult Post([FromBody] Student estudiante)
        {
            if (estudiante != null)
                Repo.Add(estudiante);
            return Created(HttpContext.Request.Host + Request.Path + "/" + estudiante.Id, estudiante);
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student estudiante)
        {
            if (id > 0)
            {
                var estu = Repo.Find(id);
                if (estu != null)
                {
                    estu.FirstMidName = estudiante.FirstMidName;
                    estu.LastName = estudiante.LastName;
                    estu.EmrollmentDate = estudiante.EmrollmentDate;
                    estu.UsuarioModificacion = estudiante.UsuarioModificacion;
                    estu.FechaModificacion = DateTime.Now;
                    Repo.Update(estu);

                    return Created(HttpContext.Request.Host + Request.Path + "/" + estudiante.Id, estudiante);
                }
                
            }
            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var estu = Repo.Find(id);

            if (estu != null)
            {
                Repo.Delete(estu);
            }
            return BadRequest();
        }
    }
}
