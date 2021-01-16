using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{

    public class AnyTransito
    {
        public string order_id { get; set; }
        public string status { get; set; }
        public Tracking tracking { get; set; }
    }

    public class Tracking
    {
        public DateTime date { get; set; }
        public DateTime shippedDate { get; set; }
        public DateTime estimateDate { get; set; }
        public string carrier { get; set; }
        public string number { get; set; }
        public string url { get; set; }
    }

}
