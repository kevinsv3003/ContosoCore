using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ContosoCore.DAL.EF;
using ContosoCore.DAL.Repos.Base;
using ContosoCore.DAL.Repos.Interfaces;
using ContosoCore.Models.Entities;
using System.Linq;

namespace ContosoCore.DAL.Repos
{
    public class CourseRepo : RepoBase<Course>, ICourseRepo
    {
        public CourseRepo(DbContextOptions<ContosoCoreContext> options) : base(options)
        {

        }
        public override IEnumerable<Course> GetAll() => Table.OrderBy(x => x.Title);

        public IQueryable<Course> ObtenerCursos() => Table;
    }
}
