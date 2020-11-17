using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integraAnyMarket
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
    }

    public class Brand
    {
        public int id { get; set; }
        public string name { get; set; }
        public string reducedName { get; set; }
        public string partnerId { get; set; }
    }

    public class Nbm
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Origin
    {
        public int id { get; set; }
        public string description { get; set; }
    }


    public class Characteristic
    {
        public int index { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Image
    {
        public int id { get; set; }
        public int index { get; set; }
        public bool main { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
        public string lowResolutionUrl { get; set; }
        public string standardUrl { get; set; }
        public string status { get; set; }
    }

    
    public class Produto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Category category { get; set; }
        public Brand brand { get; set; }
        public Nbm nbm { get; set; }
        public Origin origin { get; set; }
        public string model { get; set; }
        public int warrantyTime { get; set; }
        public string warrantyText { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public double weight { get; set; }
        public int length { get; set; }
        public double priceFactor { get; set; }
        public bool calculatedPrice { get; set; }
        public string definitionPriceScope { get; set; }
        public bool hasVariations { get; set; }
        public List<Characteristic> characteristics { get; set; }
        public List<Image> images { get; set; }
        public List<Sku> skus { get; set; }
        public bool allowAutomaticSkuMarketplaceCreation { get; set; }
    }

    public class Content
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string partnerId { get; set; }
        public string definitionPriceScope { get; set; }
    }

    
    public class RootObject
    {
        public List<Link> links { get; set; }
        public List<Content> content { get; set; }
        public Page page { get; set; }
    }

    public class RootProduto
    {
        public List<Link> links { get; set; }
        public List<Produto> produtos { get; set; }
        public Page page { get; set; }
    }

    
    public class SetStock
    {
        public string idOrigem { get; set; }
        public string partnerId { get; set; }
        public string quantity { get; set; }
    }

}