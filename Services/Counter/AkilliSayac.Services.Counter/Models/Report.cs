using MongoDB.Bson.Serialization.Attributes;

namespace AkilliSayac.Services.Counter.Models
{
    public class Report
    {
        [BsonId]
        public string CounterId { get; set; }
    }
}
