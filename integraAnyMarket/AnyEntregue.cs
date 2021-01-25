using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{

    public class AnyEntregue
    {
        public string status { get; set; }
        public TrackingEntregue tracking { get; set; }
    }

    public class TrackingEntregue
    {
        public String deliveredDate { get; set; }
    }

}
