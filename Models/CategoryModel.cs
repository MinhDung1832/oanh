using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class CategoryModel
    {
        public class CreateCate
        {
            public int CategoryId { get; set; }
            public string Name { get; set; }

        }
        public class DeleteCate
        {
            public int CategoryId { get; set; }
        }
    }
}