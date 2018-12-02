using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace Database.Controllers
{
    public class VacanciesController : ODataController
    {
        private readonly VacanciesContext db = new VacanciesContext();

        [EnableQuery]
        public IQueryable<Vacancy> Get()
        {
            return db.Vacancies.AsQueryable();
        }

        [EnableQuery]
        public IActionResult Get([FromODataUri] string id)
        {
            return Ok(db.Vacancies.Find(id));
        }

        public IActionResult Delete(string id)
        {
            var vacancy = db.Vacancies.Find(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            db.Vacancies.Remove(vacancy);
            db.SaveChanges();
            return NoContent();
        }
        
        [EnableQuery]
        public IActionResult Post([FromBody] Vacancy vacancy)
        {
            var state = EntityState.Unchanged;
            if (db.Vacancies.Any(d => d.Id == vacancy.Id))
            {
                state = EntityState.Modified;
            }
            else
            {
                state = EntityState.Added;
            }

            if (vacancy.Employer != null && !db.Employers.Any(e => e.Id == vacancy.Employer.Id))
            {
                db.Entry(vacancy.Employer).State = EntityState.Added;
            }

            
            if (vacancy.Type != null && !db.EmploymentTypes.Any(e => e.Id == vacancy.Type.Id))
            {
                db.Entry(vacancy.Type).State = EntityState.Added;
            }

            db.Entry(vacancy).State = state;
            db.SaveChanges();
                return Created(vacancy);
           
        }
    }
}