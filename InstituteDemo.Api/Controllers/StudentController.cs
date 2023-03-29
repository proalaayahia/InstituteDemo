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
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StudentController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("allStudents")]
        public async Task<IEnumerable<StudentDto>>? GetAllStudentsAsync()
        {
            var students = await _unitOfWork.studentRepository.GetAllAsync(include:"Courses");
            var result = _mapper.Map<IEnumerable<StudentDto>>(students);
            return result;
        }
        [HttpGet]
        [Route("getStudentById/{id}")]
        public async Task<StudentDto> GetStudentByIdAsyncAsync(int id)
        {
            var student = await _unitOfWork.studentRepository.GetByIdAsync(id);
            var result = _mapper.Map<StudentDto>(student);
            return result;
        }
        [HttpPost("addStudent")]
        public async Task<IActionResult> AddStudentAsync(StudentDto student)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            var data = _mapper.Map<Student>(student);
            await _unitOfWork.studentRepository.CreateAsync(data);
            await _unitOfWork.CompletedAsync(CancellationToken.None);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpPut("editStudent")]
        public async Task<IActionResult> EditStudentAsync(StudentDto student)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            var data = _mapper.Map<Student>(student);
             _unitOfWork.studentRepository.Update(data);
            await _unitOfWork.CompletedAsync(CancellationToken.None);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("removeStudent/{id}")]
        public async Task<IActionResult> RemoveStudentAsync(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            await _unitOfWork.studentRepository.DeleteAsync(id);
            await _unitOfWork.CompletedAsync(CancellationToken.None);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
