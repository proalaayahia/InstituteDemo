using InstituteDemo.UI.Helpers;
using InstituteDemo.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace InstituteDemo.UI.Controllers
{
    public class CourseController : CommonController<CourseModel>
    {
        public CourseController(IOptions<Settings> option) : base(option) { }

        public async Task<IActionResult> Courses()
        {
            try
            {
                var data = await getAllAsync("Course/allCourses");
                return View(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult CreateCourse()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");
            bool result = await postAsync(model, "Course/addCourse");
            if (result)
            {
                var data = await getAllAsync("Course/allCourses");
                return RedirectToAction("Courses", data);
            }
            return PartialView();
        }
        [HttpGet]
        public async Task<IActionResult> EditCourse(int id)
        {
            CourseModel data = await getByIdAsync<CourseModel>(id, "Course/getCourseById");
            if (data != null)
                return View(data);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");
            bool result = await putAsync(model, "Course/editCourse");
            if (result)
            {
                var data = await getAllAsync("Course/allCourses");
                return RedirectToAction("Courses", data);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> RemoveCourse(int id)
        {
            CourseModel data = await getByIdAsync<CourseModel>(id, "Course/getCourseById");
            if (data != null)
                return View(data);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveCourse(CourseModel model)
        {
            var result = await removeAsync(model.CourseId, "Course/removeCourse");
            if (result)
            {
                var data = await getAllAsync("Course/allCourses");
                return RedirectToAction("Courses", data);
            }
            return RedirectToAction("Index");
        }
    }
}
