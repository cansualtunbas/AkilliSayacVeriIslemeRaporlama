using System.ComponentModel.DataAnnotations;

namespace AkilliSayac.Web.Models.Counters
{
    public class CounterViewModel
    {

        public string Id { get; set; }

        public string SerialNumber { get; set; }
        public DateTime MeasurementTime { get; set; }

        public string LatestIndex { get; set; }
        public string VoltageValue { get; set; }
        public string CurrentValue { get; set; }

    }
}
