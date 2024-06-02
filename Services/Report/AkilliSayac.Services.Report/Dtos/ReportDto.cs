using AkilliSayac.Services.Report.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using AkilliSayac.Shared.Enums;

namespace AkilliSayac.Services.Report.Dtos
{
    public class ReportDto
    {
      
        public string Id { get; set; }
        public DateTime RequestedDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public string CounterId { get; set; }
     
        public Counter Counter { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
