using AkilliSayac.Services.Report.Dtos;
using AkilliSayac.Shared.Dtos;

namespace AkilliSayac.Services.Report.Services
{
    public interface IReportService
    {
        Task<Response<List<ReportDto>>> GetAllAsync();

        Task<Response<ReportDto>> GetByIdAsync(string id);
        Task<Response<ReportDto>> GetBySerialNumberAsync(string serialNumber);

        Task<Response<ReportDto>> CreateAsync(ReportDto reportDto);
        Task<Response<NoContent>> UpdateAsync(ReportDto reportDto);


    }
}
