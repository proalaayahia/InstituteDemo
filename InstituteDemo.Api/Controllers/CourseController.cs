using AutoMapper;
using InstituteDemo.Api.DTOs;
using InstituteDemo.Application.Interfaces.Common;
using InstituteDemo.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InstituteDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("allCourses")]
        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _unitOfWork.courseRepository.GetAllAsync("Students");
            var result = _mapper.Map<IEnumerable<CourseDto>>(courses);
            return result;
        }
        [HttpGet]
        [Route("getCourseById/{id}")]
        public async Task<CourseDto> GetCourseByIdAsyncAsync(int id)
        {
            var course = await _unitOfWork.courseRepository.GetByIdAsync(id);
            var result = _mapper.Map<CourseDto>(course);
            return result;
        }
        [HttpPost("addCourse")]
        public async Task<IActionResult> AddCourseAsync(CourseDto Course)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            var data = _mapper.Map<Course>(Course);
            await _unitOfWork.courseRepository.CreateAsync(data);
            await _unitOfWork.CompletedAsync(CancellationToken.None);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpPut("editCourse")]
        public async Task<IActionResult> EditCourseAsync(CourseDto Course)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            var data = _mapper.Map<Course>(Course);
            _unitOfWork.courseRepository.Update(data);
            await _unitOfWork.CompletedAsync(CancellationToken.None);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("removeCourse/{id}")]
        public async Task<IActionResult> RemoveCourseAsync(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            await _unitOfWork.courseRepository.DeleteAsync(id);
            await _unitOfWork.CompletedAsync(CancellationToken.None);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
