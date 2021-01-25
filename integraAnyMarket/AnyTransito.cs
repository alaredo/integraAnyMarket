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
        public string date { get; set; }
        public string shippedDate { get; set; }
        public string estimateDate { get; set; }
        public string carrier { get; set; }
        public string number { get; set; }
        public string url { get; set; }
    }

}
