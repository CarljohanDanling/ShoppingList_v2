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
        // Connection String towards local Database
        private string connectionStringLocal = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=ShoppingList;Integrated Security=True";

        // Gets all the shopping lists in database.
        public List<ShoppingList> GetAllShoppingLists()
        {
            string sqlGet = "SELECT * FROM ShoppingList SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(connectionStringLocal))
            {
                return connection.Query<ShoppingList>(sqlGet).ToList();
            }
        }

        // Insert a new shopping list into database.
        public int InsertShoppingList(ShoppingList shoppingList)
        {
            string sqlInsert = "INSERT INTO ShoppingList (Name, BudgetSum) Values (@Name, @BudgetSum); SELECT CAST(SCOPE_IDENTITY() as int)";
            
            using (var connection = new SqlConnection(connectionStringLocal))
            {
                var shoppingListID = connection.ExecuteScalar<int>(sqlInsert, new { shoppingList.Name, shoppingList.BudgetSum });
                return shoppingListID;
            }
        }

        // Gets detailed information about specific shopping list in database.
        public List<ShoppingList> GetDetailedInformationOfSpecificShoppingList(int shoppingListId)
        {
            string sqlGet = "SELECT * FROM ShoppingList WHERE ShoppingListId = @shoppingListId";

            using (var connection = new SqlConnection(connectionStringLocal))
            {
                return connection.Query<ShoppingList>(sqlGet, new { ShoppingListId = shoppingListId }).ToList();
            }
        }

        // Updates only the shopping list's Name and BudgetSum.
        public void UpdateShoppingList(ShoppingList shoppingList)
        {
            string sqlUpdate = "UPDATE ShoppingList SET Name = @Name, BudgetSum = @BudgetSum WHERE ID = @ID;";

            using (var connection = new SqlConnection(connectionStringLocal))
            {
                connection.Execute(sqlUpdate, new { shoppingList.ShoppingListId, shoppingList.Name, shoppingList.BudgetSum });
            }
        }

        // Deletes the actual shopping list from database.
        public void DeleteShoppingList(int shoppingListId)
        {
            string sqlDelete = "DELETE FROM ShoppingList WHERE ShoppingListId = @shoppingListId;";

            using (var connection = new SqlConnection(connectionStringLocal))
            {
                connection.Execute(sqlDelete, new { ShoppingListId = shoppingListId});
            }
        }

        // Insert an shopping list's item into database
        public void InsertItem(Item item)
        {
            string sqlInsert = "INSERT INTO Item (Name, Price, Quantity, ShoppingListId) Values (@Name, @Price, @Quantity, @ShoppingListId)";

            using (var connection = new SqlConnection(connectionStringLocal))
            {
                connection.ExecuteScalar<Item>(sqlInsert, new { item.Name, item.Price, item.Quantity, item.ShoppingListId });
            }
        }

        // Get all items related to specific shopping list
        public List<Item> GetAllItemsRelatedToSpecificShoppingList(int shoppingListId)
        {
            string sqlGet = "SELECT * FROM Item WHERE ShoppingListId = @shoppingListId";

            using (var connection = new SqlConnection(connectionStringLocal))
            {
                return connection.Query<Item>(sqlGet, new { ShoppingListId = shoppingListId }).ToList();
            }
        }
    }
}
