using AkilliSayac.Shared.Enums;
using AkilliSayac.Web.Models.Counters;

namespace AkilliSayac.Web.Models.Reports
{
    public class ReportViewModel
    {
   
        public string Id { get; set; }
        public DateTime RequestedDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CounterSerialNumber { get; set; }
   
        //public CounterCreateInput Counter { get; set; }
    }


}
