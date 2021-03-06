﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Homework1.Models;
using Omu.ValueInjecter;
using Microsoft.AspNetCore.Http;

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
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstructor()
        {
            return db.CourseInstructor.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CourseInstructor> GetCourseInstructorById(int id)
        {
            return db.CourseInstructor.Find(id);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<CourseInstructor> PostCourseInstructor(CourseInstructor model)
        {
            db.CourseInstructor.Add(model);
            db.SaveChanges();
            return Created("/api/CourseInstructor",model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult PutCourseInstructor(int id, CourseInstructor model)
        {
            var c = db.CourseInstructor.Find(id);
            c.InjectFrom(model);
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<CourseInstructor> DeleteCourseInstructorById(int id)
        {
            var c = db.CourseInstructor.Find(id);
            db.CourseInstructor.Remove(c);
            db.SaveChanges();
            return null;
        }
    }
}