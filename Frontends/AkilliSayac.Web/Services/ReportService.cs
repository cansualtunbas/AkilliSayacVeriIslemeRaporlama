using AkilliSayac.Shared.Dtos;
using AkilliSayac.Web.Models;
using AkilliSayac.Web.Models.Counters;
using AkilliSayac.Web.Models.Reports;
using AkilliSayac.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Json;

namespace AkilliSayac.Web.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _client;
        public ReportService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> CreateReportAsync(ReportViewModel reportCreate)
        {

            var response = await _client.PostAsJsonAsync<ReportViewModel>("report", reportCreate);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<ReportViewModel>> GetAllReportAsync()
        {
            var response = await _client.GetAsync("report");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ReportViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<ReportViewModel> GetByReportId(string id)
        {
            var response = await _client.GetAsync($"report/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ReportViewModel>>();


            return responseSuccess.Data;
        }

        public async Task<bool> UpdateReportAsync(ReportViewModel reportCreate)
        {
            var response = await _client.PutAsJsonAsync<ReportViewModel>("report", reportCreate);

            return response.IsSuccessStatusCode;
        }


    }
}
