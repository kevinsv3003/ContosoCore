using System;
using System.Collections.Generic;
using System.Text;
using ContosoCore.DAL.Repos.Base;
using ContosoCore.Models.Entities;
using System.Linq;

namespace ContosoCore.DAL.Repos.Interfaces
{
    public interface IEnrollmentRepo
    {
        IQueryable<Enrollment> ObenerInscripciones();
    }
}
