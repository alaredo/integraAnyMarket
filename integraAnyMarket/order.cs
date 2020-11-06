using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{
        public class Order
        {
            public int id { get; set; }
            public string accountName { get; set; }
            public string marketPlaceId { get; set; }
            public string marketPlaceNumber { get; set; }
            public string partnerId { get; set; }
            public string marketPlace { get; set; }
            public string subChannel { get; set; }
            public string subChannelNormalized { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime paymentDate { get; set; }
            public DateTime cancelDate { get; set; }
            public string shippingOptionId { get; set; }
            public string transmissionStatus { get; set; }
            public string status { get; set; }
            public string marketPlaceStatus { get; set; }
            public string marketplaceStatusComplement { get; set; }
            public bool fulfillment { get; set; }
            public int discount { get; set; }
            public int freight { get; set; }
            public int interestValue { get; set; }
            public int gross { get; set; }
            public int total { get; set; }
            public string marketPlaceUrl { get; set; }
            public string marketPlaceShipmentStatus { get; set; }
            public Invoice invoice { get; set; }
            public Shipping shipping { get; set; }
            public Billingaddress billingAddress { get; set; }
            public Anymarketaddress anymarketAddress { get; set; }
            public Buyer buyer { get; set; }
            public Payment[] payments { get; set; }
            public Item[] items { get; set; }
            public Tracking tracking { get; set; }
            public string deliverStatus { get; set; }
            public int idAccount { get; set; }
            public Pickup pickup { get; set; }
            public Metadata metadata { get; set; }
        }

        public class Invoice
        {
            public string accessKey { get; set; }
            public string series { get; set; }
            public string number { get; set; }
            public DateTime date { get; set; }
            public string cfop { get; set; }
            public string companyStateTaxId { get; set; }
            public string linkNfe { get; set; }
            public string invoiceLink { get; set; }
            public string extraDescription { get; set; }
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
            public string comment { get; set; }
            public string reference { get; set; }
            public string zipCode { get; set; }
            public string receiverName { get; set; }
            public DateTime promisedShippingTime { get; set; }
        }

        public class Billingaddress
        {
            public string city { get; set; }
            public string state { get; set; }
            public string stateNameNormalized { get; set; }
            public string country { get; set; }
            public string countryAcronymNormalized { get; set; }
            public string countryNameNormalized { get; set; }
            public string street { get; set; }
            public string number { get; set; }
            public string neighborhood { get; set; }
            public string comment { get; set; }
            public string reference { get; set; }
            public string zipCode { get; set; }
        }

        public class Anymarketaddress
        {
            public string state { get; set; }
            public string city { get; set; }
            public string zipCode { get; set; }
            public string neighborhood { get; set; }
            public string address { get; set; }
            public string street { get; set; }
            public string number { get; set; }
            public string comment { get; set; }
            public string reference { get; set; }
            public string receiverName { get; set; }
            public string promisedShippingTime { get; set; }
        }

        public class Buyer
        {
            public string marketPlaceId { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string document { get; set; }
            public string documentType { get; set; }
            public string phone { get; set; }
            public string cellPhone { get; set; }
            public string documentNumberNormalized { get; set; }
        }

        public class Tracking
        {
            public string url { get; set; }
            public string number { get; set; }
            public string carrier { get; set; }
            public DateTime date { get; set; }
            public DateTime deliveredDate { get; set; }
            public DateTime estimateDate { get; set; }
            public DateTime shippedDate { get; set; }
        }

        public class Pickup
        {
            public int id { get; set; }
            public string description { get; set; }
            public int partnerId { get; set; }
            public string marketplaceId { get; set; }
            public string receiverName { get; set; }
        }

        public class Metadata
        {
            public string numberofpackages { get; set; }
        }

        public class Payment
        {
            public string method { get; set; }
            public string status { get; set; }
            public int value { get; set; }
            public int installments { get; set; }
            public string marketplaceId { get; set; }
            public int gatewayFee { get; set; }
            public int marketplaceFee { get; set; }
            public string paymentMethodNormalized { get; set; }
            public string paymentDetailNormalized { get; set; }
        }

    public class Item
    {
        public Product product { get; set; }
        public Sku sku { get; set; }
        public int amount { get; set; }
        public int unit { get; set; }
        public int gross { get; set; }
        public int total { get; set; }
        public int discount { get; set; }
        public Shipping1[] shippings { get; set; }
        public Stock[] stocks { get; set; }
        public string marketPlaceId { get; set; }
        public string orderItemId { get; set; }
        public string idInMarketPlace { get; set; }
        public string officialStoreId { get; set; }
        public string officialStoreName { get; set; }
        public string listingType { get; set; }

        public string cd_produto {get; set; }
        }

        public class Product
        {
            public int id { get; set; }
            public string title { get; set; }
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
            public int idLocalStock { get; set; }
            public string stockName { get; set; }
            public int amount { get; set; }
        }

}
