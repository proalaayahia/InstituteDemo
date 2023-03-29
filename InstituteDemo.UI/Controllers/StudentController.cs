using InstituteDemo.UI.Helpers;
using InstituteDemo.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace InstituteDemo.UI.Controllers
{
    public class StudentController : CommonController<StudentModel>
    {
        public StudentController(IOptions<Settings> option) : base(option) { }
        public async Task<IActionResult> Students()
        {
            try
            {
                var data = await getAllAsync("Student/allStudents");
                return View(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");
            bool result = await postAsync(model, "Student/addStudent");
            if (result)
            {
                var data = await getAllAsync("Student/allStudents");
                return RedirectToAction("Students", data);
            }
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            StudentModel data = await getByIdAsync<StudentModel>(id, "Student/getStudentById");
            if (data != null)
                return View(data);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");
            bool result = await putAsync(model, "Student/editStudent");
            if (result)
            {
                var data = await getAllAsync("Student/allStudents");
                return RedirectToAction("Students", data);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> RemoveStudent(int id)
        {
            StudentModel data = await getByIdAsync<StudentModel>(id, "Student/getStudentById");
            if (data != null)
                return View(data);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveStudent(StudentModel model)
        {
            var result = await removeAsync(model.Id, "Student/removeStudent");
            if (result)
            {
                var data = await getAllAsync("Student/allStudents");
                return RedirectToAction("Students", data);
            }
            return RedirectToAction("Index");
        }

    }
}
