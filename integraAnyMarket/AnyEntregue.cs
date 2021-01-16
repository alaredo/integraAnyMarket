using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{

    public class AnyEntregue
    {
        public string order_id { get; set; }
        public string status { get; set; }
        public TrackingEntregue tracking { get; set; }
    }

    public class TrackingEntregue
    {
        public DateTime date { get; set; }
        public DateTime deliveredDate { get; set; }
        public string carrier { get; set; }
        public string number { get; set; }
        public string url { get; set; }
    }

}
