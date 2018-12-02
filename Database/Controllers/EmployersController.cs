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
    public class EmployersController : ODataController
    {
        private readonly VacanciesContext db = new VacanciesContext();

        [EnableQuery]
        public async Task<IActionResult>  Get()
        {
            return  Ok(db.Employers);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await db.Employers.FirstOrDefaultAsync(i => i.Id == id));
        }

        public IActionResult Delete(string id)
        {
            var employer = db.Employers.Find(id);
            if (employer == null)
            {
                return NotFound();
            }

            db.Employers.Remove(employer);
            db.SaveChanges();
            return NoContent();
        }

        [EnableQuery]
        public IActionResult Post([FromBody] Employer employer)
        {
            var state = EntityState.Unchanged;
            if (db.Employers.Any(d => d.Id == employer.Id))
            {
                state = EntityState.Modified;
            }
            else
            {
                state = EntityState.Added;
            }


            db.Entry(employer).State = state;
            db.SaveChanges();
            return Created(employer);
        }
    }
}