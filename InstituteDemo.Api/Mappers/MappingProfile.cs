using InstituteDemo.Api.DTOs;
using InstituteDemo.Domain.Entities;

namespace InstituteDemo.Api.Mappers
{
    public class MappingProfile:AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap().ForMember(p=>p.Students,m=>m.Ignore());
        }
    }
}
