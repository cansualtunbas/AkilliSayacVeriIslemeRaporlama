using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AkilliSayac.Services.Report.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime RequestedDate { get; set; }
        public Status ReportStatus { get; set; }=Status.GettingReady;
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CounterId { get; set; }
        [BsonIgnore]
        public Counter Counter { get; set; }
    }

    public enum Status
    {
        GettingReady = 0,
        Completed = 1
    }
}
