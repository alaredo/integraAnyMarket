using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{

    public class AnyFaturados
    {
        public string order_id { get; set; }
        public string status { get; set; }
        public Invoice invoice { get; set; }
    }

    public class Invoice
    {
        public string series { get; set; }
        public string number { get; set; }
        public string accessKey { get; set; }
        public int installments { get; set; }
        public DateTime date { get; set; }
    }

}
