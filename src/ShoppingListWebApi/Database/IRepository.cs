using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingListWebApi.Models;

namespace ShoppingListWebApi.Database
{
    public interface IRepository
    {
        List<ShoppingList> GetAllShoppingLists();
        int InsertShoppingList(ShoppingList shoppingList);
    }
}
