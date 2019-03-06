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

        public int InsertShoppingList(ShoppingList shoppingList)
        {
            string sqlInsert = "INSERT INTO ShoppingList (Name, BudgetSum) Values (@Name, @BudgetSum); SELECT CAST(SCOPE_IDENTITY() as int)";
            
            using (var connection = new SqlConnection(connectionStringLocal))
            {
                var shoppingListID = connection.ExecuteScalar<int>(sqlInsert, new { shoppingList.Name, shoppingList.BudgetSum });
                return shoppingListID;
            }
        }

        public void UpdateShoppingList(ShoppingList shoppingList)
        {
            string sqlUpdate = "UPDATE ShoppingList SET Name = @Name, BudgetSum = @BudgetSum WHERE ID = @ID;";

            using (var connection = new SqlConnection(connectionStringLocal))
            {
                connection.Execute(sqlUpdate, new { shoppingList.ID, shoppingList.Name, shoppingList.BudgetSum });
            }
        }

        //public List<Item> InsertItemAndReturnsIt (Item item)
        //{
        //    string sqlInsert = "INSERT INTO Item (Name, Price, Quantity, ShoppingListID) Values (@Name, @Price, @Quantity, @ShoppingListID); SELECT CAST(SCOPE_IDENTITY() as int)";

        //    using (var connection = new SqlConnection(connectionStringLocal))
        //    {
        //        var itemID = connection.ExecuteScalar<int>(sqlInsert, new { item.Name, item.Price, item.Quantity, item.ShoppingListID });

        //        string sqlReturnSpecificItem = $"SELECT * FROM Item WHERE ID = @ItemID";
        //        return connection.Query<Item>(sqlReturnSpecificItem, new { ItemID = itemID }).ToList();
        //    }
        //}
    }
}
