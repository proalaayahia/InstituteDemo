using System;
using System.Collections.Generic;
using System.Text;

namespace InstituteDemo.Domain.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public Student Students { get; set; }
    }
}
