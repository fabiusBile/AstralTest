using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Vacancy
    {
        [Key]
        public string Id { get; set; }

        [StringLength(255)] public string Name { get; set; }
        public int? Salary { get; set; }
        [StringLength(63)] public string ContactPerson { get; set; }
        [StringLength(12)] public string PhoneNumber { get; set; }
        [StringLength(1000)] public string Description { get; set; }
        
        public Employer Employer { get; set; }
        public string EmployerId { get; set; }
        public EmploymentType Type { get; set; }
        public string TypeId { get; set; }
    }
}