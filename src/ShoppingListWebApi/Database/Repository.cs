using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingListWebApi.Models;
using System.Data.SqlClient;
using Dapper;

namespace ShoppingListWebApi.Database
{
    public class Repository : IRepository
    {
        // ConnectionString towards local Database
        private string connectionStringLocal = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=ShoppingList;Integrated Security=True";

        public List<ShoppingList> GetAllShoppingLists()
        {
            string sqlGet = "SELECT * FROM ShoppingList SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(connectionStringLocal))
            {
                return connection.Query<ShoppingList>(sqlGet).ToList();
            }
        }

        public List<ShoppingList> InsertShoppingListAndReturnsIt(ShoppingList shoppingList)
        {
            string sqlInsert = "INSERT INTO ShoppingList (ShoppingListName, BudgetSum) Values (@ShoppingListName, @BudgetSum); SELECT CAST(SCOPE_IDENTITY() as int)";
            
            using (var connection = new SqlConnection(connectionStringLocal))
            {
                var shoppingListID = connection.ExecuteScalar<int>(sqlInsert, new { shoppingList.ShoppingListName, shoppingList.BudgetSum });

                string sqlReturnSpecificShoppingList = $"SELECT * FROM ShoppingList WHERE ID = @ShoppingListID";

                return connection.Query<ShoppingList>(sqlReturnSpecificShoppingList, new { ShoppingListID = shoppingListID }).ToList();
            }
        }
    }
}
