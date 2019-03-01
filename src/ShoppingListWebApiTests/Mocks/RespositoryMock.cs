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

        public List<ShoppingList> GetAllShoppingLists()
        {
            return MockResult;
        }

        public List<ShoppingList> InsertShoppingListAndReturnsIt(ShoppingList shoppingList)
        {
            return MockResult;
        }
    }
}
