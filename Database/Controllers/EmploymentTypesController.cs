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
    public class EmploymentTypes: ODataController
    {
        private readonly VacanciesContext db = new VacanciesContext();

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(db.EmploymentTypes);
        }

        [EnableQuery]
        public IActionResult Get(string id)
        {
            return Ok(db.EmploymentTypes.Find(id));
        }

        public IActionResult Delete(string id)
        {
            var type = db.EmploymentTypes.Find(id);
            if (type == null)
            {
                return NotFound();
            }

            db.EmploymentTypes.Remove(type);
            db.SaveChanges();
            return NoContent();
        }
        
        [EnableQuery]
        public IActionResult Post([FromBody] EmploymentType EmploymentType)
        {
            EntityState state;
            if (db.EmploymentTypes.Any(d => d.Id == EmploymentType.Id))
            {
                state = EntityState.Modified;
            }
            else
            {
                state = EntityState.Added;
            }

            db.Entry(EmploymentType).State = state;
            db.SaveChanges();
//            if (state == EntityState.Added)
//            {
                return Created(EmploymentType);
//            }
//            else
//            {
//                return Updated(EmploymentType);
//            }
        }
    }
}