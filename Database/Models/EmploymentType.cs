using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class EmploymentType
    {
        [Key]
        [StringLength(63)]
        public string Id { get; set; }
        [StringLength(63)]
        public string Name { get; set; }
        
        public IEnumerable<Vacancy> Vacancies { get; set; }
    }
}