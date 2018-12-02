using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private static readonly string ServiceHost = Environment.GetEnvironmentVariable("DB_SERVICE");
        private static readonly string ServicePort = Environment.GetEnvironmentVariable("DB_SERVICE_PORT");

        private readonly DatabaseService _dbServiceClient = new DatabaseService(new Uri($"http://{ServiceHost}:{ServicePort}/odata/"));
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Vacancy> Get(string id)
        {
            var a = _dbServiceClient.Vacancies.Where(d => d.Id == id).ToListAsync().Result.First();

            return a;
        }

    }
}