using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using AstralTest;
using Database.Models;
using Newtonsoft.Json;

namespace DatabaseUpdater
{
    class Program
    {
        //hh.ru требует имя приложения в юзер-агенте
        const string HhUserAgent = "AstralTest/1.0 (dock234@yandex.ru)";

        private static readonly string ServiceHost = Environment.GetEnvironmentVariable("DB_SERVICE");
        private static readonly string ServicePort = Environment.GetEnvironmentVariable("DB_SERVICE_PORT");

        static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now} - Start");

            var apiClient = new HHApi.Client(new HttpClient());

            var dbServiceClient = new DatabaseService(new Uri($"http://{ServiceHost}:{ServicePort}/odata/"));


            try
            {
                Console.WriteLine($"{DateTime.Now} - Start getting vacancies");
                // Получаем с сайта id 50 актуальных вакансий
                var vacancyLinks = apiClient.GetVacanciesAsync(50, HhUserAgent).Result.Items;
                

                // По id получаем подробную информацию о вакансиях
                var vacancies = vacancyLinks.Select(vv => apiClient.GetVacancyAsync(vv.Id, HhUserAgent).Result)
                    .ToList();
                Console.WriteLine($"{DateTime.Now} - Got vacancies");
                
                // Получаем работодателей из вакансий, и записываем их в базу
                var employers = vacancies.Where(v => v.Employer != null).GroupBy(v => v.Employer.Id)
                    .Select(v => v.FirstOrDefault().Employer).ToList();

                foreach (var employer in employers)
                {
                    dbServiceClient.AddToEmployers(VacancyFactory.ConvertHHTypeToDB(employer));
                }

                // Получаем виды занятости из вакансий и записываем их в базу
                var employments = vacancies.Where(v => v.Employment != null).GroupBy(v => v.Employment.Id)
                    .Select(v => v.FirstOrDefault().Employment).ToList();

                foreach (var employment in employments)
                {
                    dbServiceClient.AddToEmploymentTypes(VacancyFactory.ConvertHHTypeToDB(employment));
                }

                // Записываем вакансии в базу

                foreach (var vacancy in vacancies)
                {
                    var dbVacancy = VacancyFactory.ConvertHHTypeToDB(vacancy);
                    dbServiceClient.AddToVacancies(dbVacancy);
                }

                Console.WriteLine($"{DateTime.Now} - Start saving to DB");
                dbServiceClient.SaveChangesAsync().Wait();
                Console.WriteLine($"{DateTime.Now} - Saved to DB");
                Console.WriteLine($"{DateTime.Now} - All Done");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла ошибка: {e.Message} {e.InnerException} {e.StackTrace}");
            }
        }
    }
}