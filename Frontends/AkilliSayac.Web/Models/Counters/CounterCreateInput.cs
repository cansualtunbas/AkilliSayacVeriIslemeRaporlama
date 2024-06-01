using System.ComponentModel.DataAnnotations;

namespace AkilliSayac.Web.Models.Counters
{
    public class CounterCreateInput
    {
        [Display(Name = "Seri Numarası")]
        public string SerialNumber { get; set; }
        [Display(Name = "Ölçüm Zamanı")]
        public DateTime MeasurementTime { get; set; }
        [Display(Name = "Son Endeks Bilgisi")]
        public string LatestIndex { get; set; }
        [Display(Name = "Voltaj Değeri")]
        public string VoltageValue { get; set; }
        [Display(Name = "Akım Değeri")]
        public string CurrentValue { get; set; }
        public string? Id { get; set; }
   
       
        public string? UserId { get; set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }


    }
}
