using ShoppingListWebApi.Database;
using ShoppingListWebApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListWebApiTests.Mocks
{
    public class RepositoryMock : IRepository
    {
        public void UpdateShoppingList(ShoppingList shoppingList) { }

        public void DeleteShoppingList(int shoppingListId) { }

        public List<ShoppingList> MockShoppingListResult { get; set; }

        public List<Item> MockItemResult { get; set; }

        public List<ShoppingList> GetAllShoppingLists()
        {
            return MockShoppingListResult;
        }

        public List<ShoppingList> GetDetailedInformationOfSpecificShoppingList(int shoppingListId)
        {
            return MockShoppingListResult;
        }

        public int InsertShoppingList(ShoppingList shoppingList)
        {
            return MockShoppingListResult[0].ShoppingListId;
        }

        public void InsertItem(Item item) { }
    }
}
