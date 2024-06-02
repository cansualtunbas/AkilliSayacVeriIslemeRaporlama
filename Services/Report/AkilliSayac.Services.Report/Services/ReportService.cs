using AkilliSayac.Services.Report.Dtos;
using AkilliSayac.Services.Report.Models;
using AkilliSayac.Services.Report.Settings;
using AkilliSayac.Shared.Dtos;
using AkilliSayac.Shared.Enums;
using AkilliSayac.Shared.Messages;
using AutoMapper;
using MongoDB.Driver;
using static MassTransit.Logging.OperationName;
using Mass = MassTransit;

namespace AkilliSayac.Services.Report.Services
{
    public class ReportService : IReportService
    {
        private readonly IMongoCollection<Models.Counter> _counterCollection;
        private readonly IMongoCollection<Models.Report> _reportCollection;
        private readonly IMapper _mapper;
        //private readonly Mass.IPublishEndpoint _publishEndpoint;
        public ReportService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _counterCollection = database.GetCollection<Models.Counter>(databaseSettings.CounterCollectionName);
            _reportCollection = database.GetCollection<Models.Report>(databaseSettings.ReportCollectionName);

            _mapper = mapper;
            //_publishEndpoint = publishEndpoint;
        }

        public async Task<Response<ReportDto>> CreateAsync(ReportDto reportDto)
        {
            var newReport = _mapper.Map<Models.Report>(reportDto);

            newReport.CreatedTime = DateTime.Now;
            await _reportCollection.InsertOneAsync(newReport);
            //rabbitmq için
            //await _publishEndpoint.Publish<ReportChangedEvent>(new ReportChangedEvent { ReportStatus = ReportStatus.Completed });
            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(newReport), 200);
        }

        public async Task<Response<List<ReportDto>>> GetAllAsync()
        {
            var reports = await _reportCollection.Find(x => true).ToListAsync();
            if (!reports.Any())
            {
                reports = new List<Models.Report>();
            }
        
           
            return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(reports), 200);
        }

        public async Task<Response<ReportDto>> GetByIdAsync(string id)
        {
            var report = await _reportCollection.Find<Models.Report>(x => x.Id == id).FirstOrDefaultAsync();

            if (report == null)
            {
                return Response<ReportDto>.Fail("Report not found", 404);
            }
            report.Counter = await _counterCollection.Find<Models.Counter>(x => x.SerialNumber == report.CounterSerialNumber).FirstAsync();

            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }

        public async Task<Response<ReportDto>> GetBySerialNumberAsync(string serialNumber)
        {
           
            var report = await _reportCollection.Find<Models.Report>(x => x.Counter.SerialNumber == serialNumber).FirstOrDefaultAsync();

            if (report == null)
            {
                return Response<ReportDto>.Fail("Report not found", 404);
            }
            report.Counter = await _counterCollection.Find<Models.Counter>(x => x.SerialNumber == report.CounterSerialNumber).FirstAsync();

            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ReportDto reportDto)
        {
            var updateReport = _mapper.Map<Models.Report>(reportDto);

            var result = await _reportCollection.FindOneAndReplaceAsync(x => x.Id == reportDto.Id, updateReport);

            if (result == null)
            {
                return Response<NoContent>.Fail("Report not found", 404);
            }
            

            return Response<NoContent>.Success(204);
        }
    }
}
