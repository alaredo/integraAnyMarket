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
        string token_sandBox = "L31103086G1570648571245R-250576705";
        string token_oficial = "259025663L259027832E1628947780001C153563578000100O1.I";
        string token;

        string baseUrl_sandBox = "http://sandbox-api.anymarket.com.br/v2/";
        string baseUrl_oficial = "http://api.anymarket.com.br/v2/";
        string baseUrl;

        public AnyMarket()
        {
            token = token_sandBox;
            baseUrl = baseUrl_sandBox;

          //  token = token_oficial;
          //  baseUrl = baseUrl_oficial;

        }

        public RootProduto GetProdutos()
        {
            RootProduto root = new RootProduto();
            var url = baseUrl + "products?limit=30";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Headers.Add("gumgaToken", token);
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


        public AnyFeed GetFeed()
        {
            AnyFeed feed = new AnyFeed();
            var url = baseUrl + "orders/feeds?status=PAID_WAITING_SHIP";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Headers.Add("gumgaToken", token);
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "GET";
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultFeed = streamReader.ReadToEnd();
                    String result = "{\"lstFeed\": " + Convert.ToString(resultFeed) + "}";
                    // result = result.Replace("content", "produtos");
                    feed = JsonConvert.DeserializeObject<AnyFeed>(result);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set(ex.Message);
            }
            return feed;
        }

        public void PutFeed(string feedId, string tokenFeed)
        {
            var url = $"{baseUrl}orders/feeds/{feedId}?gumgaToken={token}";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"token\": \"" + tokenFeed + "\"}";

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
                Log.Set($"Erro Saldo Produto: {ex.Message}");
            }
        }

        public void PutXML(string idOrder, string xml)
        {
            var url = $"{baseUrl}orders/{idOrder}?gumgaToken={token}";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{{" + xml + "}}";

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
                Log.Set($"Erro Saldo Produto: {ex.Message}");
            }
        }


        public RootOrder GetPedido(string Id)
        {
            string Url = $"{baseUrl}orders/"+Id+"?status=PAID_WAITING_SHIP";
            return GetPedidos(Url, true);
        }

        public RootOrder GetPedidos(Boolean isFeed)
        {
            string Url = $"{baseUrl}orders?status=PAID_WAITING_SHIP&offset=5";
            return GetPedidos(Url, isFeed);
        }

        public RootOrder GetPedidos(string url, Boolean isFeed)
        {
            RootOrder root = new RootOrder();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Headers.Add("gumgaToken", token); 
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
                    if (!isFeed)
                        root = JsonConvert.DeserializeObject<RootOrder>(result);
                    else
                    {
                        Order order = JsonConvert.DeserializeObject<Order>(result);
                        root.orders = new List<Order>();
                        root.orders.Add(order);
                    }

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
                    RootOrder rootOrder = GetPedidos(nextUrl, isFeed);
                    nextUrl = "";
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return root;
        }

        private List<RootOrder> GetPed()
        {
            List<RootOrder> lstPages = new List<RootOrder>();
            RootOrder root = new RootOrder();
            var url = baseUrl + "orders?status=PAID_WAITING_SHIP?limit=30&page=1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Headers.Add("gumgaToken", token); 
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
                            RootOrder rootOrder = GetPedidos(l.href, false);
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

        public bool SetStock(List<SetStock> lstSetStock)
        {
            bool ret = true;
            var url = $"{baseUrl}stocks?gumgaToken={token}";
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
                ret = false;
                string msg = ex.Message;
                Log.Set($"Erro Saldo Produto: {ex.Message}");
            }
            return ret;
        }

        public void SetPrice(List<SetStock> lstSetStock)
        {
            var url = $"{baseUrl}stock/?gumgaToken={token}";
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
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Log.Set($"Erro Saldo Produto: {ex.Message}");
            }
        }

        public bool SetFaturado(string id_order, AnyFaturados faturado)
        {
            bool ret = true;
            //foreach (SetStock setStock in lstSetStock)
            //{
            var url = $"{baseUrl}orders/{id_order}?gumgaToken={token}";
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
                ret = false;
                string msg = ex.Message;
                Log.Set($"Erro Set Faturado: {ex.Message}");
            }
            return ret;
        }

        public bool SetEnviado(string id_order, AnyTransito transito)
        {
            bool ret = true;
            var url = $"{baseUrl}orders/{id_order}?gumgaToken={token}";
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
                ret = false;
                string msg = ex.Message;
                Log.Set($"Erro Set Transito: {ex.Message}");
            }
            return ret;
        }

        public bool SetEntregue(string id_order, AnyEntregue entregue)
        {
            bool ret = true;
            var url = $"{baseUrl}orders/{id_order}?gumgaToken={token}";
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
                ret = false;
                string msg = ex.Message;
                Log.Set($"Erro Set Entregue: {ex.Message}");
            }
            return ret;
        }

        public void SetConcluido(string id_order, AnyConcluido concluido)
        {
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
        }
    }
}
