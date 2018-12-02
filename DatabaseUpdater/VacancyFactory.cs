using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Database.Models;

namespace DatabaseUpdater
{
    public static class VacancyFactory
    {
        
        public static Vacancy ConvertHHVacancyToDB(HHApi.Vacancy hhVacancy)
        {
            var vacancy = new Vacancy()
            {
                Name = hhVacancy.Name,
                Description = hhVacancy.Description,
                Id = hhVacancy.Id,
                ContactPerson = hhVacancy.Contacts?.Name,
            };


            var phoneNumber = hhVacancy.Contacts?.Phones?.FirstOrDefault()?.Number;
            if (phoneNumber != null)
            {
                var phoneCleanerPattern = @"[^\d\+]";
                vacancy.PhoneNumber = Regex.Replace(phoneNumber, phoneCleanerPattern,"");
            }

            if (hhVacancy.Employer != null)
            {
                vacancy.Employer = new Employer()
                {
                    Name = hhVacancy.Employer.Name,
                    Id = hhVacancy.Employer.Id
                };
            }

            if (hhVacancy.Employment != null)
            {
                vacancy.Type = new EmploymentType()
                {
                    Id = hhVacancy.Employment.Id,
                    Name = hhVacancy.Employment.Name
                };
            }

            if (hhVacancy.Salary != null)
            {
                var hSal = hhVacancy.Salary;
                var sals = new List<int>();
                if (hSal.From.HasValue && hSal.From.Value > 0)
                    sals.Add(hSal.From.Value);
                if (hSal.To.HasValue && hSal.To.Value > 0)
                    sals.Add(hSal.To.Value);

                vacancy.Salary = (int) sals.Average();
            }
            return vacancy;
        }
    }
}