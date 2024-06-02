using AkilliSayac.Web.Models.Counters;
using AkilliSayac.Web.Models.Reports;

namespace AkilliSayac.Web.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<ReportViewModel>> GetAllReportAsync();
        Task<ReportViewModel> GetByReportId(string id);
        Task<bool> UpdateReportAsync(ReportViewModel reportCreate);
    }
}
