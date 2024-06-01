using AkilliSayac.Services.Counter.Dtos;
using AkilliSayac.Services.Counter.Services;
using AkilliSayac.Shared.ControllerBases;
using AkilliSayac.Shared.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkilliSayac.Services.Counter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : CustomBaseController
    {
        private readonly ICounterService _counterService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public CounterController(ICounterService counterService, ISharedIdentityService sharedIdentityService)
        {
            _counterService = counterService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _counterService.GetAllAsync();

            return CreateActionResultInstance(response);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _counterService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CounterDto counterDto)
        {
            counterDto.UserId = _sharedIdentityService.GetUserId;
            var response = await _counterService.CreateAsync(counterDto);

            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CounterDto counterDto)
        {
            var response = await _counterService.UpdateAsync(counterDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _counterService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }
    }
}
