using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListWebApi.Model
{
    public class ShoppingListContext : DbContext
    {
        public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
        : base(options)
        { }

        public DbSet<ShoppingList> ShoppingList { get; set; }
        public DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(i => i.ShoppingList)
                .WithMany(s => s.Items)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
