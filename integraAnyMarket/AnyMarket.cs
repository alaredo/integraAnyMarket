using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{
    public class AnyMarket
    {
        public RootObject GetCategories()
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
        public RootOrder GetPedidos()
        {
            RootOrder root = new RootOrder();
            var url = "http://sandbox-api.anymarket.com.br/v2/orders?limit=30";
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
                    root = JsonConvert.DeserializeObject<RootOrder>(result);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return root;
        }
    }
}
