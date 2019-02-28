using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Models;
using ShoppingListWebApi.Database;
using Newtonsoft.Json;

namespace ShoppingListWebApi.Controllers
{
    [Route("api/shoppinglist")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private IRepository repo;

        public ShoppingListController(IRepository repos)
        {
            repo = repos;
        }

        // GET api/shoppinglist
        // Get all shopping lists from database.
        [HttpGet]
        public List<ShoppingList> GetAllShoppingListsFromDatabase()
        {
            List<ShoppingList> shoppingList = repo.GetAllShoppingLists();
            return shoppingList;
        }

        // POST api/shoppinglist
        // POST shopping lists to database and returns the shopping list ID.
        [HttpPost]
        public int InsertShoppingListToDatabase([FromBody] ShoppingList values)
        {
            ShoppingList shoppingList = new ShoppingList()
            {
                ShoppingListName = values.ShoppingListName,
                BudgetSum = values.BudgetSum
            };

            return repo.InsertShoppingList(shoppingList);
        }

        /*
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
