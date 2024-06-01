using AkilliSayac.Services.Counter.Dtos;
using AkilliSayac.Shared.Dtos;

namespace AkilliSayac.Services.Counter.Services
{
    public interface ICounterService
    {
        Task<Response<List<CounterDto>>> GetAllAsync();

        Task<Response<CounterDto>> GetByIdAsync(string id);

        Task<Response<CounterDto>> CreateAsync(CounterDto courseCreateDto);

        Task<Response<NoContent>> UpdateAsync(CounterDto courseUpdateDto);

        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
