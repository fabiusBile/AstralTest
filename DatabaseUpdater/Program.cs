using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using AstralTest;
using Newtonsoft.Json;

namespace DatabaseUpdater
{
    class Program
    {
        //hh.ru требует имя приложения в юзер-агенте
        const string hhUserAgent = "AstralTest/1.0 (dock234@yandex.ru)";

        static void Main(string[] args)
        {
            var apiClient = new HHApi.Client(new HttpClient());
            
            var dbServiceClient = new DatabaseService(new Uri("http://localhost:5000/odata/"));
            
            
            try
            {
                // Получаем с сайта id 50 актуальных вакансий
                var vacancyLinks = apiClient.GetVacanciesAsync(50, hhUserAgent).Result.Items;

                // По id получаем подробную информацию о вакансиях
                var vacancies = vacancyLinks.Select(vv => apiClient.GetVacancyAsync(vv.Id, hhUserAgent).Result)
                    .ToList();

                foreach (var vacancy in vacancies)
                {
                    var dbVacancy = VacancyFactory.ConvertHHVacancyToDB(vacancy);
                    dbServiceClient.AddToVacancies(dbVacancy);
                    if (dbVacancy.Employer != null)
                        dbServiceClient.AddToEmployers(dbVacancy.Employer);
                    if (dbVacancy.Type!=null)
                        dbServiceClient.AddToEmploymentTypes(dbVacancy.Type);
                  //  DBServiceClient.AttachLink();
                }
                
                dbServiceClient.SaveChangesAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла ошибка: {e.Message} {e.InnerException} {e.StackTrace}");
            }
        }
    }
}