using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListWebApp.Model
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required]
        [Display(Name = "Name of item")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price >= 0")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Range(0, double.MaxValue, ErrorMessage = "Quantity >= 0")]
        public double Quantity { get; set; }
    }
}
