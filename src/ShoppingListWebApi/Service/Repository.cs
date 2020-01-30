using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingListWebApi.Service
{
    public class Repository : IRepository
    {
        private readonly ShoppingListContext _context;

        public Repository(ShoppingListContext context)
            => _context = context;

        public async Task<List<ShoppingList>> GetAllShoppingListsAsync()
            => await _context.ShoppingList.AsNoTracking().ToListAsync();

        public async Task<ActionResult<ShoppingList>> GetShoppingListWithRelatedItemsAsync(int shoppingListId)
        {
            return await _context.ShoppingList
                .AsNoTracking()
                .Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.ShoppingListId == shoppingListId);
        }

        public async Task InsertShoppingListAsync(ShoppingList shoppingList)
        {
            await _context.ShoppingList.AddAsync(shoppingList);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShoppingListAsync(int shoppingListId, ShoppingList values)
        {
            var listToUpdate = await _context.ShoppingList.FirstOrDefaultAsync(s => s.ShoppingListId == shoppingListId);

            listToUpdate.Name = values.Name;
            listToUpdate.BudgetSum = values.BudgetSum;

            _context.ShoppingList.Update(listToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShoppingListAsync(int shoppingListId)
        {
            _context.ShoppingList.Remove(new ShoppingList() { ShoppingListId = shoppingListId });
            await _context.SaveChangesAsync();
        }

        /***** ITEM *****/

        public async Task InsertItemAsync(int shoppingListId, Item values)
        {
            values.ShoppingListId = shoppingListId;

            await _context.Item.AddAsync(values);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(int itemId, JsonPatchDocument<Item> item)
        {
            item.ApplyTo(await _context.Item.FindAsync(itemId));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int itemId)
        {
            _context.Item.Remove(new Item() { ItemId = itemId });
            await _context.SaveChangesAsync();
        }
    }
}
