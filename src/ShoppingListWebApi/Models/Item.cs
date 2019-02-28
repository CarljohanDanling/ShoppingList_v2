using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListWebApi.Models
{
    class Item
    {
        private int ItemID { get; set; }
        private string Name { get; set; }
        private double Price { get; set; }
        private double Quantity { get; set; }
        private int ShoppingListID { get; set; }
    }
}
