using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ContosoCore.Models.Entities.Base;

namespace ContosoCore.Models.Entities
{
    public class Student:EntityBase
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstMidName { get; set; }
        public DateTime EmrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
