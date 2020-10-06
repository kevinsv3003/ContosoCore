using System;
using System.Collections.Generic;
using System.Text;
using ContosoCore.Models.Entities.Base;

namespace ContosoCore.Models.Entities
{
    public class Course:EntityBase
    {
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
