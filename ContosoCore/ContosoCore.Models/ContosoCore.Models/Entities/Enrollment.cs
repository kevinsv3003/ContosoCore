using System;
using System.Collections.Generic;
using System.Text;
using ContosoCore.Models.Entities.Base;

namespace ContosoCore.Models.Entities
{
    public enum Grade
    {
        A,B,C,D,F
    }
    public class Enrollment:EntityBase
    {
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
