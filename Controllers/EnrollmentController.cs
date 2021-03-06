﻿using System;
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
    public class EnrollmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public EnrollmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Enrollment>> GetEnrollments()
        {
            return db.Enrollment.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Enrollment> GetEnrollmentById(int id)
        {
            return db.Enrollment.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Enrollment> PostEnrollment(Enrollment model)
        {
            db.Enrollment.Add(model);
            db.SaveChanges();
            return Created("/api/Enrollment/"+model.EnrollmentId,model);
        }

        [HttpPut("{id}")]
        public IActionResult PutEnrollment(int id, Enrollment model)
        {
            var c = db.Enrollment.Find(id);
            c.InjectFrom(model);
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Enrollment> DeleteEnrollmentById(int id)
        {
            var c = db.Enrollment.Find(id);
            db.Enrollment.Remove(c);
            db.SaveChanges();
            return null;
        }
    }
}