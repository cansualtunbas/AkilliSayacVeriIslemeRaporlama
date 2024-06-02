using AkilliSayac.Shared.Classes;
using AkilliSayac.Shared.Messages;
using AkilliSayac.Web.Services.Interfaces;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.ComponentModel;
using System.Data;
using System.IO;
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
            return View(await _reportService.GetAllReportAsync());
        }

        public async Task<IActionResult> CreateReport(string Id)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.ReportChangedEventQueueName}"));
            var result = await _reportService.GetByReportId(Id);


            var reportChangedEvent = new ReportChangedEvent
            {
                ReportId = result.Id,
                ReportStatus = Shared.Enums.ReportStatus.Tamamlandı
            };
            await sendEndpoint.Send<ReportChangedEvent>(reportChangedEvent);

            //await Export(Id);
            return RedirectToAction("Index", "Report");
        }

        public async Task<IActionResult> Export(string Id)
        {
            var result = await _reportService.GetByReportId(Id);
            var counterList = await _counterService.GetAllCounterAsync();

            counterList.Where(x => x.SerialNumber == result.CounterSerialNumber).OrderByDescending(x => x.MeasurementTime).ToList();
            //MemoryStream memStream;

            //using (var package = new ExcelPackage())
            //{
            //    var worksheet = package.Workbook.Worksheets.Add("New Sheet");

            //    worksheet.Cells[1, 1].Value = "ID";
            //    worksheet.Cells[1, 2].Value = "Name";
            //    worksheet.Cells[2, 1].Value = "1";
            //    worksheet.Cells[2, 2].Value = "One";
            //    worksheet.Cells[3, 1].Value = "2";
            //    worksheet.Cells[3, 2].Value = "Two";

            //    memStream = new MemoryStream(package.GetAsByteArray());
            //}
            return Ok();

        }

        public MemoryStream Download()
        {
            MemoryStream memStream;
            ExcelPackage.LicenseContext  = OfficeOpenXml.LicenseContext.Commercial;
            using (var package = new ExcelPackage())
            {
               
                var worksheet = package.Workbook.Worksheets.Add("New Sheet");

                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[2, 1].Value = "1";
                worksheet.Cells[2, 2].Value = "One";
                worksheet.Cells[3, 1].Value = "2";
                worksheet.Cells[3, 2].Value = "Two";

                memStream = new MemoryStream(package.GetAsByteArray());
            }

            return memStream;
        }

        public FileStreamResult Download2()
        {
            var memStream = Download();
            return File(memStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

 
    }
}
