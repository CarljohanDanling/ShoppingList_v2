using ShoppingListWebApi.Database;
using ShoppingListWebApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListWebApiTests.Mocks
{
    public class RespositoryMock : IRepository
    {
        public List<ShoppingList> MockResult { get; set; }
        public ShoppingList MockResultShoppingList { get; set; }

        public List<ShoppingList> GetAllShoppingLists()
        {
            return MockResult;
        }

        public int InsertShoppingList(ShoppingList shoppingList)
        {
            return MockResultShoppingList.ID;
        }
    }
}
