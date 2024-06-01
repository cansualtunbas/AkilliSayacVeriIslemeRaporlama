

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AkilliSayac.Services.Counter.Dtos
{
    public class CounterDto
    {
     
        public string? Id { get; set; }

        public string? SerialNumber { get; set; }
        public DateTime? MeasurementTime { get; set; }

        public string? LatestIndex { get; set; }
        public string? VoltageValue { get; set; }
        public string? CurrentValue { get; set; }

        public string? UserId { get; set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
