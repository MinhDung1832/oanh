using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class CartModel
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal LastPrice { get; set; }
        public decimal Quantity { get; set; }
        public string CategoryName { get; set; }
        public string SizeName { get; set; }
        public string Url { get; set; }
        public decimal ToTal { get; set; }
    }

    public class update{
        public int ID { get; set; }
        public int Quantity { get; set; }
    }
    public class delete{
        public int ID { get; set; }
    }
    public class AddToCart{
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}