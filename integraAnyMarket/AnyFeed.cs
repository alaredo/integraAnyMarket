using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{


    public class AnyFeed
    {
        public List<Feed> lstFeed { get; set; }
    }

    public class Feed
    {
        public int id { get; set; }
        public string token { get; set; }
    }


}
