using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListWebApi.Models
{
    public class ShoppingList
    {
        public int ShoppingListId { get; set; }
        public string Name { get; set; }
        public double BudgetSum { get; set; }
    }
}
