using System;
using Database.Models;

namespace PublicApi.Models
{
   /// <summary>
   /// Объект для поиска вакансий по параметрам
   /// </summary>
    public class VacancySearchModel
    {
        /// <summary>
        /// Наименование вакансии
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Минимальная зарплата
        /// </summary>
        public int? SalaryFrom { get; set; }
        
        /// <summary>
        /// Максимальная зарплата
        /// </summary>
        public int? SalaryTo { get; set; }
        
        /// <summary>
        /// Вид занятости
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Название компании
        /// </summary>
        public string Employer { get; set; }

        /// <summary>
        /// Страница
        /// </summary>
        public int Page { get; set; } = 1;
    }
}