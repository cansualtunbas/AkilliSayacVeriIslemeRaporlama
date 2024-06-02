using AkilliSayac.Shared.Classes;
using AkilliSayac.Shared.Messages;
using AkilliSayac.Web.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkilliSayac.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ReportController(IReportService reportService, ISendEndpointProvider sendEndpointProvider)
        {
            _reportService = reportService;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _reportService.GetAllReportAsync());
        }

        public async Task<IActionResult> CreateReport(string Id)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.ReportChangedEventQueueName}"));
            var result=await _reportService.GetByReportId(Id);


            var reportChangedEvent = new ReportChangedEvent
            {
                ReportId = result.Id,
                ReportStatus = Shared.Enums.ReportStatus.Completed
            };
            await sendEndpoint.Send<ReportChangedEvent>(reportChangedEvent);


            return RedirectToAction("Index", "Report");
        }
    }
}
