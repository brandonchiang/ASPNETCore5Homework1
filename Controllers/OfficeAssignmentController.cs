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
    public class OfficeAssignmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public OfficeAssignmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<OfficeAssignment>> GetOfficeAssignments()
        {
            return db.OfficeAssignments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<OfficeAssignment> GetOfficeAssignmentById(int id)
        {
            return db.OfficeAssignments.Find(id);
        }

        [HttpPost("")]
        public ActionResult<OfficeAssignment> PostOfficeAssignment(OfficeAssignment model)
        {
            db.Add(model);
            db.SaveChanges();
            return Created("/api/OfficeAssignment/" + model.InstructorId,model);
        }

        [HttpPut("{id}")]
        public IActionResult PutOfficeAssignment(int id, OfficeAssignment model)
        {
            var c = db.OfficeAssignments.Find(id);
            c.InjectFrom(c);
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<OfficeAssignment> DeleteOfficeAssignmentById(int id)
        {
            var c = db.OfficeAssignments.Find(id);
            db.OfficeAssignments.Remove(c);
            db.SaveChanges();
            return null;
        }
    }
}