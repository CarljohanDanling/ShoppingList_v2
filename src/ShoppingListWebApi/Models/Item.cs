using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListWebApi.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public int ShoppingListID { get; set; }
    }
}
