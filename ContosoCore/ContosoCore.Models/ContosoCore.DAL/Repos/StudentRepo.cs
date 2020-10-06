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
    public class StudentRepo : RepoBase<Student>, IStudentRepo
    {
        public StudentRepo(DbContextOptions<ContosoCoreContext> options):base(options)
        {

        }

        public override IEnumerable<Student> GetAll() => Table.OrderBy(x=> x.FirstMidName);

        public IQueryable<Student> ObtenerEstudiantes() => Table;
    }
}
