using ShoppingListWebApi.Database;
using ShoppingListWebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListWebApiTests.Mocks
{
    public class RepositoryMock : IRepository
    {
        // Mocking for shopping list.
        public List<ShoppingList> MockShoppingListResult { get; set; }

        public List<ShoppingList> GetAllShoppingLists()
        {
            return MockShoppingListResult;
        }

        public List<ShoppingList> GetShoppingListWithRelatedItems(int shoppingListId)
        {
            return MockShoppingListResult;
        }

        public void InsertShoppingList(ShoppingList shoppingList)
        {

        }

        public void DeleteShoppingList(int shoppingListId) { }
        public void UpdateShoppingList(ShoppingList shoppingList) { }

        // Mocking for item.

        public Item MockOneItemResult { get; set; }
        public void InsertItem(Item item) { }
        public Item GetOneItem(int itemId)
        {
            return MockOneItemResult;
        }
        public void UpdateItem(Item item) { }

        public void DeleteItem(int itemId) { }
        public List<ShoppingList> GetDetailedInformationOfSpecificShoppingList(int shoppingListId)
        {
            return MockShoppingListResult;
        }

        public List<Item> MockItemResult { get; set; }
        public List<Item> GetAllItemsRelatedToSpecificShoppingList(int shoppingListId)
        {
            return MockItemResult;
        }
    }
}
