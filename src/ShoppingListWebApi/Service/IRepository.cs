using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingListWebApi.Service
{
    public interface IRepository
    {
        /***** SHOPPINGLIST *****/

        Task<List<ShoppingList>> GetAllShoppingListsAsync();
        Task<ShoppingList> GetShoppingListWithRelatedItemsAsync(int shoppingListId);
        Task InsertShoppingListAsync(ShoppingList shoppingList);
        Task UpdateShoppingListAsync(int shoppingListId, ShoppingList values);
        Task DeleteShoppingListAsync(int shoppingListId);

        /***** ITEM *****/

        Task InsertItemAsync(int shoppingListId, Item values);
        Task UpdateItemAsync(int itemId, JsonPatchDocument<Item> item);
        Task DeleteItemAsync(int itemId);
    }
}
