using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListWebApp.Model
{
    public class ShoppingList
    {
        [Display(Name = "Shoppinglist Id")]
        public int ShoppingListId { get; set; }

        [Required]
        [Display(Name = "Shoppinglist name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Budget")]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be 1 or greater")]
        public double BudgetSum { get; set; }

        public List<Item> Items { get; set; }
    }
}
