using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AkilliSayac.Services.Counter.Models
{
    public class Counter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string SerialNumber { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime MeasurementTime { get; set; }

        public string LatestIndex { get; set; }
        public string VoltageValue { get; set; }
        public string CurrentValue { get; set; }

        public string UserId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdateTime { get; set; }


    }
}
