using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AkilliSayac.Services.Report.Models
{
    public class Counter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; }
        
        public string? SerialNumber { get; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime MeasurementTime { get; }

        public string? LatestIndex { get;}
        public string? VoltageValue { get; }
        public string? CurrentValue { get; }

        public string? UserId { get; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? CreatedTime { get; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? UpdateTime { get; }


    }
}
