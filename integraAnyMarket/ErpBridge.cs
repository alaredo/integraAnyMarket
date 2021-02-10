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
                        if (db.ProcessaPedido(o))
                        {
                            anyMarket.PutFeed(f.id.ToString());
                        }
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
                setStock.cost = Convert.ToDecimal( dr["vl_cume"].ToString()) ;
                lstStocks.Add(setStock);
                AnyMarket any = new AnyMarket();
                if (any.SetStock(lstStocks))
                {
                    db.setStock(setStock.idOrigem, "200", "sucesso", "1");
                }
                else
                {
                    db.setStock(dr["id_nfs01"].ToString(), "400", "erro", "2");
                }
            }
        }


    }
}
