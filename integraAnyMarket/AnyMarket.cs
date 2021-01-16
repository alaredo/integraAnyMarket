using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{
    public class AnyMarket
    {
        /*public RootObject GetCategories()
        {
            RootObject root = new RootObject();
            var url = "http://sandbox-api.anymarket.com.br/v2/categories";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Headers.Add("gumgaToken", "L31103086G1570648571245R-250576705");
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "GET";
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    
                    root = JsonConvert.DeserializeObject<RootObject>(result);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return root;
        }
        */

        public RootProduto GetProdutos()
        {
            RootProduto root = new RootProduto();
            var url = "http://sandbox-api.anymarket.com.br/v2/products?limit=30";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Headers.Add("gumgaToken", "L31103086G1570648571245R-250576705");
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "GET";
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    result = result.Replace("content", "produtos");
                    root = JsonConvert.DeserializeObject<RootProduto>(result);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;

            }
            return root;
        }

        public List<RootOrder> lstPages = new List<RootOrder>();
        public RootOrder GetPedidos(string url)
        {
            
            RootOrder root = new RootOrder();
            // var url = "http://sandbox-api.anymarket.com.br/v2/orders?limit=30";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Headers.Add("gumgaToken", "259025663L259027832E1628947780001C153563578000100O1.I"); //"L31103086G1570648571245R-250576705");
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "GET";

            string nextUrl = "";
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    result = result.Replace("content", "orders");
                    root = JsonConvert.DeserializeObject<RootOrder>(result);

                    lstPages.Add(root);
                    if (root.orders != null)  {
                        if (root.orders.Count() == 5)
                        {
                            foreach (Link l in root.links)
                            {
                                if (l.rel == "next")
                                {
                                    nextUrl = l.href;

                                }
                            }
                        }
                    } else
                    {
                        nextUrl = "";
                    }
                }
                if ( nextUrl != "")
                {
                    RootOrder rootOrder = GetPedidos(nextUrl);
                    nextUrl = "";
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return root;
        }

        public List<RootOrder> GetPedidos()
        {
            List<RootOrder> lstPages = new List<RootOrder>();
            RootOrder root = new RootOrder();
           // var url = "http://sandbox-api.anymarket.com.br/v2/orders?limit=30";
            var url = "http://api.anymarket.com.br/v2/orders?status=INVOICED?limit=30&page=1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Headers.Add("gumgaToken", "259025663L259027832E1628947780001C153563578000100O1.I"); //"L31103086G1570648571245R-250576705");
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "GET";
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    result = result.Replace("content", "orders");
                    root = JsonConvert.DeserializeObject<RootOrder>(result);

                    lstPages.Add(root);
                    foreach (Link l in root.links) {
                        if (l.rel == "Next")
                        {
                            RootOrder rootOrder = GetPedidos(l.href);
                            lstPages.Add(rootOrder);
                        }
                    }

                    
                    
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return lstPages;
        }

        public void SetStock(List<SetStock> lstSetStock)
        {
            //foreach (SetStock setStock in lstSetStock)
            //{
                var url = "http://sandbox-api.anymarket.com.br/v2/stocks?gumgaToken=L31103086G1570648571245R-250576705";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
   
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(lstSetStock);

                    streamWriter.Write(json);
                }
                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                } catch ( Exception ex)
                {
                    string msg = ex.Message;
                    Log.Set($"Erro Saldo Produto: {ex.Message}");
                }
            //}
        }

        public void SetFaturado(string id_order, AnyFaturados faturado)
        {
            //foreach (SetStock setStock in lstSetStock)
            //{
            var url = "http://sandbox-api.anymarket.com.br/v2/orders/id_order?gumgaToken=L31103086G1570648571245R-250576705";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(faturado);

                streamWriter.Write(json);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Log.Set($"Erro Set Faturado: {ex.Message}");
            }
            //}
        }

        public void SetEnviado(string id_order, AnyTransito transito)
        {
            //foreach (SetStock setStock in lstSetStock)
            //{
            var url = "http://sandbox-api.anymarket.com.br/v2/orders/id_order?gumgaToken=L31103086G1570648571245R-250576705";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(transito);

                streamWriter.Write(json);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Log.Set($"Erro Set Transito: {ex.Message}");
            }
            //}
        }

        public void SetEntregue(string id_order, AnyEntregue entregue)
        {
            //foreach (SetStock setStock in lstSetStock)
            //{
            var url = "http://sandbox-api.anymarket.com.br/v2/orders/id_order?gumgaToken=L31103086G1570648571245R-250576705";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(entregue);

                streamWriter.Write(json);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Log.Set($"Erro Set Entregue: {ex.Message}");
            }
            //}
        }

        public void SetConcluido(string id_order, AnyConcluido concluido)
        {
            //foreach (SetStock setStock in lstSetStock)
            //{
            var url = "http://sandbox-api.anymarket.com.br/v2/orders/id_order?gumgaToken=L31103086G1570648571245R-250576705";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(concluido);

                streamWriter.Write(json);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Log.Set($"Erro Set Concluido: {ex.Message}");
            }
            //}
        }
    }
}
