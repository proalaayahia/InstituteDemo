using System.Collections.Generic;

namespace InstituteDemo.Api.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public ICollection<CourseDto> Courses { get; set; }
    }
}
