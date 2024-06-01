using AkilliSayac.Web.Models.Counters;

namespace AkilliSayac.Web.Services.Interfaces
{
    public interface ICounterService
    {
        Task<List<CounterViewModel>> GetAllCounterAsync();


        Task<CounterViewModel> GetByCounterId(string id);

        Task<bool> CreateCounterAsync(CounterCreateInput counterCreateInput);

        Task<bool> UpdateCounterAsync(CounterCreateInput counterCreateInput);

        Task<bool> DeleteCounterAsync(string id);
    }
}
