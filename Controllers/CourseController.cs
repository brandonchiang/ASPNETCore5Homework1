using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Homework1.Models;
using Omu.ValueInjecter;

namespace ASPNETCore5Homework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourseModels()
        {
            return db.Courses.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseModelById(int id)
        {
            return db.Courses.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Course> PostCourseModel(Course model)
        {
            db.Add(model);
            db.SaveChanges();
            return Created("/api/Coourse/"+model.CourseId,model);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourseModel(int id, CourseUpdateModel model)
        {
            var c = db.Courses.Find(id);
            c.InjectFrom(model);
            db.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourseModelById(int id)
        {
            var c = db.Courses.Find(id);
            db.Courses.Remove(c);
            db.SaveChanges();
            return null;
        }
    }
}