using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{

    public class RootOrder
    {
        public Link[] links { get; set; }
        public Order[] orders { get; set; }
        public Page page { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int number { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class Order
    {
        public int id { get; set; }
        public string accountName { get; set; }
        public string marketPlaceId { get; set; }
        public string marketPlaceNumber { get; set; }
        public string marketPlace { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime paymentDate { get; set; }
        public string transmissionStatus { get; set; }
        public string status { get; set; }
        public string marketPlaceStatus { get; set; }
        public int discount { get; set; }
        public int freight { get; set; }
        public int sellerFreight { get; set; }
        public int interestValue { get; set; }
        public float gross { get; set; }
        public float total { get; set; }
        public Shipping shipping { get; set; }
        public Anymarketaddress anymarketAddress { get; set; }
        public Buyer buyer { get; set; }
        public Item[] items { get; set; }
        public string deliverStatus { get; set; }
        public int idAccount { get; set; }
        public bool fulfillment { get; set; }
        public string subChannel { get; set; }
    }

    public class Shipping
    {
        public string city { get; set; }
        public string state { get; set; }
        public string stateNameNormalized { get; set; }
        public string country { get; set; }
        public string countryAcronymNormalized { get; set; }
        public string countryNameNormalized { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string neighborhood { get; set; }
        public string street { get; set; }
        public string zipCode { get; set; }
        public string receiverName { get; set; }
        public string comment { get; set; }
        public DateTime promisedShippingTime { get; set; }
    }

    public class Anymarketaddress
    {
        public string country { get; set; }
        public string state { get; set; }
        public string stateAcronymNormalized { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public string neighborhood { get; set; }
        public string address { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string receiverName { get; set; }
        public DateTime promisedShippingTime { get; set; }
    }

    public class Buyer
    {
        public string name { get; set; }
        public string email { get; set; }
        public string document { get; set; }
        public string documentType { get; set; }
        public string phone { get; set; }
        public string cellPhone { get; set; }
        public string documentNumberNormalized { get; set; }
    }

    public class Item
    {
        public Product product { get; set; }
        public Sku sku { get; set; }
        public int amount { get; set; }
        public float unit { get; set; }
        public float gross { get; set; }
        public float total { get; set; }
        public int discount { get; set; }
        public Shipping1[] shippings { get; set; }
        public string idInMarketPlace { get; set; }
        public string marketPlaceId { get; set; }
        public int orderItemId { get; set; }
        public Stock[] stocks { get; set; }
        public string cd_produto { get; set; }
    }

    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Sku
    {
        public int id { get; set; }
        public string title { get; set; }
        public string partnerId { get; set; }
        public double price { get; set; }
        public double sellPrice { get; set; }
        public int amount { get; set; }
    }

    public class Shipping1
    {
        public int id { get; set; }
        public string shippingtype { get; set; }
        public string shippingCarrierNormalized { get; set; }
        public string shippingCarrierTypeNormalized { get; set; }
    }

    public class Stock
    {
        public int stockLocalId { get; set; }
        public int amount { get; set; }
        public string stockName { get; set; }
    }

}
