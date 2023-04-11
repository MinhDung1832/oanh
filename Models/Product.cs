using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string LastPrice { get; set; }
        public int Quantity { get; set; }
        public string Url { get; set; }
        public string CategoryName { get; set; }
        public string SizeName { get; set; }
    }
}