using AkilliSayac.Web.Models.Counters;
using AkilliSayac.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AkilliSayac.Web.Controllers
{
    [Authorize]
    public class CounterController : Controller
    {
        private readonly ICounterService _counterService;
        public CounterController(ICounterService counterService)
        {
            _counterService = counterService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _counterService.GetAllCounterAsync());
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CounterCreateInput counterCreateInput)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            counterCreateInput.Id = Guid.NewGuid().ToString();

            await _counterService.CreateCounterAsync(counterCreateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var counter = await _counterService.GetByCounterId(id);

            if (counter == null)
            {
                RedirectToAction(nameof(Index));
            }

            CounterCreateInput counterUpdate = new()
            {
                Id = counter.Id,
                SerialNumber = counter.SerialNumber,
                MeasurementTime = counter.MeasurementTime,
                LatestIndex = counter.LatestIndex,
                VoltageValue = counter.VoltageValue,
                CurrentValue = counter.CurrentValue

            };

            return View(counterUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CounterCreateInput counterCreate)
        {
            var counters = await _counterService.GetAllCounterAsync();
           
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _counterService.UpdateCounterAsync(counterCreate);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _counterService.DeleteCounterAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
