using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{

    public class AnyFaturados
    {
     //   public string order_id { get; set; }
        public string status { get; set; }
        public Invoice invoice { get; set; }
    }

    public class Invoice
    {
        public string accessKey { get; set; }
        public string series { get; set; }
        public string number { get; set; }
        public string date { get; set; }

        public string cfop { get; set; }
        
        public string companyStateTaxId { get; set; }
        
    }

}
