using AkilliSayac.Shared.Classes;
using AkilliSayac.Shared.Enums;
using AkilliSayac.Shared.Messages;
using AkilliSayac.Web.Models.Counters;
using AkilliSayac.Web.Models.Reports;
using AkilliSayac.Web.Services.Interfaces;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using static MassTransit.ValidationResultExtensions;

namespace AkilliSayac.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ICounterService _counterService;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ReportController(IReportService reportService, ISendEndpointProvider sendEndpointProvider, ICounterService counterService)
        {
            _reportService = reportService;
            _sendEndpointProvider = sendEndpointProvider;
            _counterService = counterService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _counterService.GetAllCounterAsync();
            List<CounterViewModel> counterViewModels = new List<CounterViewModel>();
            var a = list.GroupBy(x => x.SerialNumber).Select(x => x.First().SerialNumber).ToList();

            foreach (var item in a)
            {
                counterViewModels.Add(list.Where(x => x.SerialNumber == item).First());
            }
            return View(counterViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReport(string serialNumber)
        {
            var guid = Guid.NewGuid();
            var reportViewModel = new ReportViewModel
            {
                Id = guid.ToString(),
                RequestedDate = DateTime.Now,
                ReportStatus = 0,
                CreatedTime = DateTime.Now,
                CounterSerialNumber = serialNumber,
                
               

            };


            var result = await _reportService.CreateReportAsync(reportViewModel);

            var report = await _reportService.GetByReportId(guid.ToString());


            return Ok(report);





        }
        [HttpPost]
        public async Task<IActionResult> DownloadedReport(string Id)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.ReportChangedEventQueueName}"));
            var result = await _reportService.GetByReportId(Id);


            var reportChangedEvent = new ReportChangedEvent
            {
                ReportId = result.Id,
                ReportStatus = Shared.Enums.ReportStatus.Tamamlandı
            };
            await sendEndpoint.Send<ReportChangedEvent>(reportChangedEvent);

            return Ok(result.Id);



        }
        [HttpGet]
        public async Task<List<CounterViewModel>> GetCounterList(string Id)
        {
            var result = await _reportService.GetByReportId(Id);
            var counterList = await _counterService.GetAllCounterAsync();

            counterList=counterList.Where(x => x.SerialNumber == result.CounterSerialNumber).OrderByDescending(x => x.MeasurementTime).ToList();
            return counterList;
        }


    }



}
