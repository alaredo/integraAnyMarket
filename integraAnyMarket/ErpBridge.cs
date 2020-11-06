using System;
using System.Collections.Generic;
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
            if (order.buyer.documentType == "CPF")
            {
                if (!db.CheckPF(order.buyer.documentNumberNormalized)) {
                    db.InsertPF(    order.buyer.name, 
                                    "", DateTime.Now, 
                                    "", 
                                    order.buyer.documentNumberNormalized, 
                                    null, 
                                    "0",
                                    "", 
                                    "", 
                                    order.buyer.phone, 
                                    order.buyer.cellPhone );

                }
            }
            if (order.buyer.documentType == "CNPJ")
            {
                if (!db.CheckPJ(order.buyer.documentNumberNormalized))
                {
                    //db.insertPJ()
                }
            }
        }
    }
}
