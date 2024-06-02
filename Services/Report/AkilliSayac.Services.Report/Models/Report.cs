using AkilliSayac.Shared.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AkilliSayac.Services.Report.Models
{
    public class Report
    {
        [BsonId]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime RequestedDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }
        public string CounterId { get; set; }
        [BsonIgnore]
        public Counter Counter { get; set; }
    }

   
}
