using System;
using System.Collections.Generic;
using System.Text;

namespace InstituteDemo.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
