using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListWebApi.Database
{
    public class Repository
    {
        // ConnectionString towards local Database
        private string connectionStringLocal = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=ShoppingList;Integrated Security=True";

        //public List<ShoppingList> GetAllShoppingLists()
        //{
        //    string sql = "SELECT * FROM ShoppingList";

        //    using (var connection = new SqlConnection(connectionStringLocal))
        //    {
        //        return connection.Query<ShoppingList>(sql).ToList();
        //    }
        //}
    }
}
