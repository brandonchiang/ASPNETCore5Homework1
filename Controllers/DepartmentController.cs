using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Homework1.Models;
using Omu.ValueInjecter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace ASPNETCore5Homework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        private readonly ContosoUniversityContextProcedures sp;
        public DepartmentController(ContosoUniversityContext db, ContosoUniversityContextProcedures sp)
        {
            this.sp = sp;
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return db.Department.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            return db.Department.Find(id);
        }

        //[HttpPost("")]
        //public ActionResult<Department> PostDepartment(Department model)
        //{
        //    db.Department.Add(model);
        //    db.SaveChanges();

        //    //var Name = new SqlParameter("Name", model.Name);
        //    //var Budget = new SqlParameter("Budget",model.Budget);
        //    //var StartDate = new SqlParameter("StartDate", model.StartDate);
        //    //var InstructorID = new SqlParameter("InstructorID", model.InstructorId);
        //    //db.Departments.FromSqlRaw("Execute Department_Insert @Name,@Buget,@StartDate,@InstructorID",Name,Budget,StartDate,InstructorID);

        //    return Created("/api/Department/"+model.DepartmentId,model);
        //}

        [HttpPost("")]
        public async Task<ActionResult<Department>> PostDepartmentSP(Department model)
        {
            await sp.Department_Insert(model.Name, model.Budget, model.StartDate, model.InstructorId);
            return Created("/api/Department/" + model.DepartmentId, model);
        }

        //[HttpPut("{id}")]
        //public IActionResult PutDepartment(int id, Department model)
        //{
        //    var c = db.Department.Find(id);
        //    c.InjectFrom(c);
        //    db.SaveChanges();
        //    return NoContent();
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> PutDepartment(int id, Department model)
        {
            await sp.Department_Update(id,model.Name, model.Budget, model.StartDate, model.InstructorId, model.RowVersion);
            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public ActionResult<Department> DeleteDepartmentById(int id)
        //{
        //    var c = db.Department.Find(id);
        //    db.Department.Remove(c);
        //    db.SaveChanges();
        //    return null;
        //}

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Department>> DeleteDepartmentById(int id)
        {
            var c = db.Department.Find(id);
            if (c == null)
                return NotFound();

            OutputParameter<int> returnValue = new OutputParameter<int>();
            await sp.Department_Delete(id, c.RowVersion, returnValue);
            return Ok(returnValue);
        }
    }
}
