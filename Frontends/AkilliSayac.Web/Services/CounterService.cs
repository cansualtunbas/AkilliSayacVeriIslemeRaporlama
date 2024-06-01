using AkilliSayac.Shared.Dtos;
using AkilliSayac.Web.Models.Counters;
using AkilliSayac.Web.Services.Interfaces;

namespace AkilliSayac.Web.Services
{
    public class CounterService : ICounterService
    {
        private readonly HttpClient _client;
        public CounterService(HttpClient client)
        {
            _client = client;
                
        }
        public async Task<bool> CreateCounterAsync(CounterCreateInput counterCreateInput)
        {
            
            
            var response = await _client.PostAsJsonAsync<CounterCreateInput>("counter", counterCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCounterAsync(string id)
        {
            var response = await _client.DeleteAsync($"counter/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CounterViewModel>> GetAllCounterAsync()
        {
            //http:localhost:5000/services/counter/counter
            var response = await _client.GetAsync("counter");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CounterViewModel>>>();
         
            return responseSuccess.Data;
        }

        public async Task<CounterViewModel> GetByCounterId(string id)
        {
            var response = await _client.GetAsync($"counter/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CounterViewModel>>();


            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCounterAsync(CounterCreateInput counterCreateInput)
        {
          

            var response = await _client.PutAsJsonAsync<CounterCreateInput>("counter", counterCreateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
