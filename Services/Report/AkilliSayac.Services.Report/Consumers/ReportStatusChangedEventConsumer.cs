using AkilliSayac.Services.Report.Dtos;
using AkilliSayac.Services.Report.Services;
using AkilliSayac.Shared.Messages;
using MassTransit;

namespace AkilliSayac.Services.Report.Consumers
{
    public class ReportStatusChangedEventConsumer : IConsumer<ReportChangedEvent>
    {
        private readonly IReportService _reportService;
        public ReportStatusChangedEventConsumer(IReportService reportService)
        {
            _reportService = reportService;
        }



        public async Task Consume(ConsumeContext<ReportChangedEvent> context)
        {

            var result = await _reportService.GetByIdAsync(context.Message.ReportId);
            result.Data.ReportStatus = Shared.Enums.ReportStatus.Completed;
            var reportDto = new ReportDto
            {
                RequestedDate=result.Data.RequestedDate,
                CounterId=result.Data.CounterId,
                Counter=result.Data.Counter,
                CreatedTime=result.Data.CreatedTime,
                Id = result.Data.Id,
                //değişecek yer
                ReportStatus = context.Message.ReportStatus,
            };

            await _reportService.UpdateAsync(reportDto);
        }
    }
}
