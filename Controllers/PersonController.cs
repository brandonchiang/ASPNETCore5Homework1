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
    public class PersonController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public PersonController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return db.People.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return db.People.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Person> PostPerson(Person model)
        {
            db.People.Add(model);
            db.SaveChanges();
            return Created("/api/Person/"+model.Id,model);
        }

        [HttpPut("{id}")]
        public IActionResult PutPerson(int id, Person model)
        {
            var c = db.People.Find(id);
            c.InjectFrom(model);
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePersonById(int id)
        {
            var c = db.People.Find(id);
            db.People.Remove(c);
            db.SaveChanges();
            return null;
        }
    }
}