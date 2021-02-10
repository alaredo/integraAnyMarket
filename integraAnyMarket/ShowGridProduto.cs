using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{
    
    public class FieldsProduto
    {
        public Int32 IdProduto { get; set; }
        public string PartnerId { get; set; }
        public Int32 IdSku { get; set; }

        public string Product { get; set; }
        public String Title { get; set; }
        public Double Price { get; set; }
        public Double PriceFactor { get; set; }
        public Double Cost { get; set; }
        public Int32 Amount { get; set; }

       
    }
}
