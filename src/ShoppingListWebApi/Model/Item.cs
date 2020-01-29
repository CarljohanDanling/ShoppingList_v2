using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListWebApi.Model
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Quantity { get; set; }
        public int? ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
    }
}
