using System.Collections.Generic;

namespace WEB.Models
{
    //public class Product_Category
    //{
    //    public List<Product> LstProduct { get; set; }
    //    public List<Category> LstCategory { get; set; }
    //}

    public class objCombox
    {
        public string ID { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string Level { set; get; }
        public string Link { set; get; }
    }

    public class OBJ
    {
        public string Product { get; set; }
        public string Product_color { get; set; }
        public string Product_size { get; set; }
    }

    public class ProColor
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Pro_ID { get; set; }
        public string Color_ID { get; set; }
    }

    public class ProSize
    {
        public string ID { get; set; }
        public string Pro_ID { get; set; }
        public string Size_ID { get; set; }
    }

    public class Pro
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Pro_ID { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string ChatLieu { get; set; }
        public string Status { get; set; }
        public string Detail { get; set; }
        public string IdGroupPro { get; set; }
        public string SpTon { get; set; }
        public string Count { get; set; }
        public string View { get; set; }
        public string Like { get; set; }
        public string IdCategory { get; set; }
        public string CreateDate { get; set; }
        public string Index { get; set; }
        public string Priority { get; set; }
        public string Keyword { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Ord { get; set; }
        public string Active { get; set; }
        public string IdColor { get; set; }
        public string Images { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string OldPrice { get; set; }
        public string Modified_Date { get; set; }
        public string Insurance { get; set; }
        public string Advantages { get; set; }
        public string PriceInfo { get; set; }
    }
}