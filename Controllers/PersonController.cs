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
            return db.Person.Where(x => x.IsDeleted == false).ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<Person> GetPersonById(int id)
        {
            var c = db.Person.Find(id);
            if (c != null && c?.IsDeleted == false)
                return c;
            else
                return NotFound();
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public ActionResult<Person> PostPerson(Person model)
        {
            db.Person.Add(model);
            model.DateModified = DateTime.Now;

            db.SaveChanges();
            return Created("/api/Person/"+model.Id,model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult PutPerson(int id, Person model)
        {
            var c = db.Person.Find(id);
            if (c == null)
                return NotFound();

            model.DateModified = DateTime.Now;
            c.InjectFrom(model);

            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePersonById(int id)
        {
            var c = db.Person.Find(id);
            db.Person.Remove(c);
            db.SaveChanges();
            return null;
        }
    }
}