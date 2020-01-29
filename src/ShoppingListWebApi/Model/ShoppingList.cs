using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListWebApi.Model
{
    public class ShoppingList
    {
        [Key]
        public int ShoppingListId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public double BudgetSum { get; set; }

        public List<Item> Items { get; set; }
    }
}
