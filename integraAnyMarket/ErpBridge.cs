using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{
    public class ErpBridge
    {
        public void processaPedido(Order order)
        {
            Db db = new Db();
            db.ProcessaPedido(order);
            
        }


        public void processaEstoque()
        {
            Db db = new Db();
            DataTable dt = db.LoadStock();
            List<SetStock> lstStocks = new List<SetStock>();
            
            foreach( DataRow dr in dt.Rows )
            {
                lstStocks = new List<SetStock>();
                SetStock setStock = new SetStock();
                setStock.idOrigem = dr["id_apisd"].ToString();
                setStock.partnerId = dr["id_sku"].ToString();
                setStock.quantity = dr["qt_prod"].ToString();
                lstStocks.Add(setStock);
                AnyMarket any = new AnyMarket();
                any.SetStock(lstStocks);
            }
            
        }
    }
}
