using System;

namespace ProductAPI.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}