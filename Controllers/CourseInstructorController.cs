using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Homework1.Models;

namespace ASPNETCore5Homework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInstructorController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseInstructorController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstructors()
        {
            return new List<CourseInstructor> { };
        }

        [HttpGet("{id}")]
        public ActionResult<CourseInstructor> GetCourseInstructorById(int id)
        {
            return null;
        }

        [HttpPost("")]
        public ActionResult<CourseInstructor> PostCourseInstructor(CourseInstructor model)
        {
            return null;
        }

        [HttpPut("{id}")]
        public IActionResult PutCourseInstructor(int id, CourseInstructor model)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<CourseInstructor> DeleteCourseInstructorById(int id)
        {
            return null;
        }
    }
}