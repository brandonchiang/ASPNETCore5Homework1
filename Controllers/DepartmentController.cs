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
    public class DepartmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public DepartmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return db.Departments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            return db.Departments.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Department> PostDepartment(Department model)
        {
            db.Add(model);
            db.SaveChanges();
            return Created("/api/Department/"+model.DepartmentId,model);
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department model)
        {
            var c = db.Departments.Find(id);
            c.InjectFrom(c);
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartmentById(int id)
        {
            var c = db.Departments.Find(id);
            db.Departments.Remove(c);
            db.SaveChanges();
            return null;
        }
    }
}
