using Newtonsoft.Json;
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

        public List<PedidoProcessado> lstProcessados = new List<PedidoProcessado>();
        public void processaPedido()
        {
            AnyMarket anyMarket = new AnyMarket();
            AnyFeed anyFeed = anyMarket.GetFeed();

            foreach( Feed f in anyFeed.lstFeed)
            {
                RootOrder rootOrder = anyMarket.GetPedido(Convert.ToString(f.id));
                if (rootOrder.orders != null)
                {
                    foreach (Order o in rootOrder.orders)
                    {
                        Db db = new Db();
                        db.ProcessaPedido(o);
                    }
                }
            }
        }

        public void processa()
        {
            AnyMarket anyMarket = new AnyMarket();
            anyMarket.lstPages = new List<RootOrder>();
            anyMarket.GetPedidos(false);

            List<RootOrder> lstPages = anyMarket.lstPages;
            foreach (RootOrder r in lstPages)
            {
                if (r.orders != null)
                {
                    foreach (Order o in r.orders)
                    {
                        Db db = new Db();
                        db.ProcessaPedido(o);
                    }
                }
            }
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
