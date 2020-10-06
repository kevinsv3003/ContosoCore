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
    public class EnrollmentRepo : RepoBase<Enrollment>, IEnrollmentRepo
    {
        public EnrollmentRepo(DbContextOptions<ContosoCoreContext> options) : base(options)
        {

        }

        public override IEnumerable<Enrollment> GetAll() => Table.OrderBy(x => x.StudentID);

        public IQueryable<Enrollment> ObenerInscripciones() => Table;  

    }
}
