using System;
using System.Linq;
using System.Net.Http;

namespace DatabaseUpdater
{
    class Program
    {
        //hh.ru требует имя приложения в юзер-агенте
        const string  hhUserAgent = "AstralTest/1.0 (dock234@yandex.ru)";
        static void Main(string[] args)
        {
            var apiClient = new HHApi.Client(new HttpClient());
            
            // Получаем с сайта id 50 актуальных вакансий
            var vacancyLinks = apiClient.GetVacanciesAsync(50,hhUserAgent).Result.Items;
            
            // По id получаем подробную информацию о вакансиях
            var vacancies = vacancyLinks.Select(vv => apiClient.GetVacancyAsync(vv.Id, hhUserAgent).Result).ToList();
        }
    }
}