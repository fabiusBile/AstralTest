using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Database.Models;

namespace DatabaseUpdater
{
    /// <summary>
    /// Фабрика, конвертирующая объекты с HH для хранения в базе
    /// </summary>
    public static class VacancyFactory
    {
        /// <summary>
        /// Конвертирует вакансии
        /// </summary>
        /// <param name="hhVacancy">Вакансия с HH</param>
        /// <returns></returns>
        public static Vacancy ConvertHHTypeToDB(HHApi.Vacancy hhVacancy)
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
                vacancy.EmployerId = hhVacancy.Employer.Id;
            }

            if (hhVacancy.Employment != null)
            {
                vacancy.Type = new EmploymentType()
                {
                    Id = hhVacancy.Employment.Id,
                    Name = hhVacancy.Employment.Name
                };
                vacancy.TypeId = hhVacancy.Employment.Id;
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

        /// <summary>
        /// Конвертирует работодателей
        /// </summary>
        /// <param name="hhEmployer"></param>
        /// <returns></returns>
        public static Employer ConvertHHTypeToDB(HHApi.Employer hhEmployer)
        {
            return new Employer()
            {
                Id = hhEmployer.Id,
                Name = hhEmployer.Name
            };
        }

        /// <summary>
        /// Конвертирует виды занятости
        /// </summary>
        /// <param name="hhEmployment"></param>
        /// <returns></returns>
        public static EmploymentType ConvertHHTypeToDB(HHApi.Employment hhEmployment)
        {
            return new EmploymentType()
            {
                Id = hhEmployment.Id,
                Name = hhEmployment.Name
            };
        }
    }
}