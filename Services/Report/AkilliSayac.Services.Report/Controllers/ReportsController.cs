using AkilliSayac.Services.Report.Dtos;
using AkilliSayac.Services.Report.Services;
using AkilliSayac.Shared.ControllerBases;
using AkilliSayac.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkilliSayac.Services.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : CustomBaseController
    {
        private readonly IReportService _reportService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public ReportsController(IReportService reportService, ISharedIdentityService sharedIdentityService)
        {
            _reportService= reportService;
            _sharedIdentityService= sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _reportService.GetAllAsync();

            return CreateActionResultInstance(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _reportService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpGet("getbySerialNumber/{serialNumber}")]
      
        public async Task<IActionResult> GetBySerialNumber(string serialNumber)
        {
            var response = await _reportService.GetBySerialNumberAsync(serialNumber);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReportDto reportDto)
        {
            
            var response = await _reportService.CreateAsync(reportDto);

            return CreateActionResultInstance(response);
        }
    }
}
