using AkilliSayac.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkilliSayac.Shared.Messages
{
    public class ReportChangedEvent
    {
        public string ReportId { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
