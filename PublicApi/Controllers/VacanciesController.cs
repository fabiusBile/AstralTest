using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Client;
using PublicApi.Models;

namespace PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private static readonly string ServiceHost = Environment.GetEnvironmentVariable("DB_SERVICE");
        private static readonly string ServicePort = Environment.GetEnvironmentVariable("DB_SERVICE_PORT");

        private readonly DatabaseService _dbServiceClient =
            new DatabaseService(new Uri($"http://{ServiceHost}:{ServicePort}/odata/"));


        // GET api/values
        /// <summary>
        /// Ищет вакансии по параметрам
        /// </summary>
        /// <param name="searchParams">Параметры поиска</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacancy>>> Get([FromQuery] VacancySearchModel searchParams)
        {
            if (searchParams == null)
            {
                searchParams = new VacancySearchModel();
            }

            var query = (DataServiceQuery<Vacancy>) _dbServiceClient.Vacancies.Expand(v => v.Employer).Expand(v => v.Type).AsQueryable();
         

            if (!string.IsNullOrEmpty(searchParams.Name))
            {
                query = (DataServiceQuery<Vacancy>) query.Where(v => v.Name.Contains(searchParams.Name));
            }

            if (!string.IsNullOrEmpty(searchParams.Type))
            {
                query = (DataServiceQuery<Vacancy>) query.Where(v => v.TypeId == searchParams.Type);
            }

            if (searchParams.SalaryFrom.HasValue)
            {
                query = (DataServiceQuery<Vacancy>) query.Where(v => v.Salary >= searchParams.SalaryFrom);
            }

            if (searchParams.SalaryTo.HasValue)
            {
                query = (DataServiceQuery<Vacancy>) query.Where(v => v.Salary <= searchParams.SalaryTo);
            }

            if (!string.IsNullOrEmpty(searchParams.Employer))
            {
                query = (DataServiceQuery<Vacancy>) query.Where(v => v.EmployerId != null && v.Employer.Name.Contains(searchParams.Employer));
            }

            query = (DataServiceQuery<Vacancy>) query.Skip(10 * searchParams.Page).Take(10);

            var result = await query.ExecuteAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получает вакансию по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacancy>> Get(string id)
        {
            var result = await _dbServiceClient.Vacancies.ByKey(id)
                .Expand(v => v.Employer)
                .Expand(v => v.Type)
                .GetValueAsync();
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}