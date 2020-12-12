using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Homework1.Models;
using Omu.ValueInjecter;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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
            return db.Course.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseModelById(int id)
        {
            return db.Course.Find(id);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<Course> PostCourseModel(Course model)
        {
            db.Course.Add(model);
            model.DateModified = DateTime.Now;
            db.SaveChanges();
            return Created("/api/Course/"+model.CourseId, model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult PutCourseModel(int id, CourseUpdateModel model)
        {
            var c = db.Course.Find(id);
            c.InjectFrom(model);
            model.DateModified = DateTime.Now;
            db.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourseModelById(int id)
        {
            var c = db.Course.Find(id);
            db.Course.Remove(c);
            db.SaveChanges();
            return null;
        }

        // GET: api/Courses/CourseStudentCount
        [HttpGet("CourseStudentCount")]
        public async Task<ActionResult<IEnumerable<VwCourseStudentCount>>> GetCourseStudentCount()
        {
            return await db.VwCourseStudentCount.ToListAsync();
            //return await db.VwCourseStudentCount.FromSqlInterpolated($@"SELECT * FROM vwCourseStudentCount").ToListAsync();
        }


        // GET: api/Courses/CourseStudents
        [HttpGet("CourseStudents")]
        public async Task<ActionResult<IEnumerable<VwCourseStudents>>> GetCourseStudents()
        {
            return await db.VwCourseStudents.ToListAsync();
        }
    }
}