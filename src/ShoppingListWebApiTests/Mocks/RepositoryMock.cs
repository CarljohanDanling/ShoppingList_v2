using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Model;
using ShoppingListWebApi.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingListWebApiTests.Mocks
{
    public class RepositoryMock
    {
        public Task<List<ShoppingList>> MockShoppingListResult { get; set; }

        public Task<List<ShoppingList>> GetAllShoppingListsAsync()
            => MockShoppingListResult;

        //public Task<ActionResult<ShoppingList>> GetShoppingListWithRelatedItemsAsync(int shoppingListId)
        //{

        //}

        //public Task InsertShoppingListAsync(ShoppingList shoppingList)
        //{

        //}

        //public Task UpdateShoppingListAsync(int shoppingListId, ShoppingList values)
        //{

        //}

        //public Task DeleteShoppingListAsync(int shoppingListId)
        //{

        //}

        //public Task InsertItemAsync(int shoppingListId, Item values)
        //{

        //}

        //public Task UpdateItemAsync(int itemId, JsonPatchDocument<Item> item)
        //{

        //}

        //public Task DeleteItemAsync(int itemId)
        //{

        //}

        

        

        


        


        
    }
}
