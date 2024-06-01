using AkilliSayac.Services.Counter.Dtos;
using AkilliSayac.Services.Counter.Settings;
using AkilliSayac.Shared.Dtos;
using AkilliSayac.Shared.Messages;
using AutoMapper;
using MongoDB.Driver;
using static MassTransit.Logging.OperationName;
using Mass = MassTransit;

namespace AkilliSayac.Services.Counter.Services
{
    public class CounterService : ICounterService
    {
        private readonly IMongoCollection<Models.Counter> _counterCollection;
        private readonly IMapper _mapper;
        private readonly Mass.IPublishEndpoint _publishEndpoint;


        public CounterService(IMapper mapper, IDatabaseSettings databaseSettings, Mass.IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _counterCollection = database.GetCollection<Models.Counter>(databaseSettings.CounterCollectionName);

            _mapper = mapper;

            _publishEndpoint = publishEndpoint;
        }
        public async Task<Response<CounterDto>> CreateAsync(CounterDto counter)
        {
            var newCounter=_mapper.Map<Models.Counter>(counter);

            newCounter.CreatedTime = DateTime.Now;

            await _counterCollection.InsertOneAsync(newCounter);

            return Response<CounterDto>.Success(_mapper.Map<CounterDto>(newCounter), 200);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _counterCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("counter not found", 404);
            }
        }

        public async Task<Response<List<CounterDto>>> GetAllAsync()
        {
            var counters = await _counterCollection.Find(x => true).ToListAsync();
            if (counters.Any())
            {
                return Response<List<CounterDto>>.Success(_mapper.Map<List<CounterDto>>(counters), 200);
            }
            else
            {
                return Response<List<CounterDto>>.Fail("Counter not found", 404);
            }
        }


        public async Task<Response<CounterDto>> GetByIdAsync(string id)
        {
            var counter=await _counterCollection.Find<Models.Counter>(x=>x.Id == id).FirstOrDefaultAsync();

            if(counter == null)
            {
                return Response<CounterDto>.Fail("Counter not found", 404);
            }

            return Response<CounterDto>.Success(_mapper.Map<CounterDto>(counter), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CounterDto counter)
        {
            var updateCounter = _mapper.Map<Models.Counter>(counter);

            var result = await _counterCollection.FindOneAndReplaceAsync(x => x.Id == counter.Id, updateCounter);

            if (result == null)
            {
                return Response<NoContent>.Fail("Counter not found", 404);
            }
            //rabbitmq için
            await _publishEndpoint.Publish<CounterChangedEvent>(new CounterChangedEvent { CounterId = counter.Id, UpdatedSerialNumber = counter.SerialNumber, UpdatedLatestIndex = counter.LatestIndex, UpdatedVoltageValue = counter.VoltageValue, UpdatedCurrentValue = counter.CurrentValue });

            return Response<NoContent>.Success(204);
        }
    }
}
