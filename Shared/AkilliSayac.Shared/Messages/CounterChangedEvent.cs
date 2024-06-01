using System;
using System.Collections.Generic;
using System.Text;

namespace AkilliSayac.Shared.Messages
{
    public class CounterChangedEvent
    {
        public string CounterId { get; set; }
        public string UpdatedSerialNumber { get; set; }
        public string UpdatedLatestIndex { get; set; }
        public string UpdatedVoltageValue { get; set; }
        public string UpdatedCurrentValue { get; set; }
    }
}
