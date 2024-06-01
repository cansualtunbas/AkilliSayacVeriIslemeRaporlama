using AkilliSayac.Services.Report.Settings;
using AkilliSayac.Shared.Messages;
using MassTransit;
using MongoDB.Driver;

namespace AkilliSayac.Services.Report.Consumers
{
    public class CounterChangedEventConsumer : IConsumer<CounterChangedEvent>
    {
        private readonly IMongoCollection<Models.Report> _reportCollection;
        public CounterChangedEventConsumer(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

          
            _reportCollection = database.GetCollection<Models.Report>(databaseSettings.ReportCollectionName);
        }
        public async Task Consume(ConsumeContext<CounterChangedEvent> context)
        {
            var reports = await _reportCollection.Find<Models.Report>(x => x.Counter.Id.ToString() == context.Message.CounterId).ToListAsync();
            foreach (var item in reports)
            {
                var result = await _reportCollection.FindOneAndReplaceAsync(x => x.Id == item.Id, new Models.Report { Counter = new Models.Counter { CurrentValue = context.Message.UpdatedCurrentValue, SerialNumber=context.Message.UpdatedSerialNumber ,LatestIndex=context.Message.UpdatedLatestIndex ,VoltageValue=context.Message.UpdatedVoltageValue } });
            }
            
        }
    }
}
