using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace Database.Models
{
    public class Employer
    {
        [Key]
        public string Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }

        public IEnumerable<Vacancy> Vacancies { get; set; }
    }
}