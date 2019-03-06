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
        // POST shopping lists to database and returns it.
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult InsertShoppingListToDatabase([FromBody] ShoppingList values)
        {
            ShoppingList shoppingListObject;
            try
            {
                shoppingListObject = new ShoppingList()
                {
                    Name = values.Name,
                    BudgetSum = values.BudgetSum
                };
                shoppingListObject.ID = repo.InsertShoppingList(shoppingListObject);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message + "(Unable to add shopping list)");
            }
            return Ok(shoppingListObject);
        }

        // PUT api/shoppinglist/5
        // PUT to update/change the shopping list in database.
        [HttpPut("{shoppingListID}")]
        public IActionResult UpdateShoppingList(int shoppingListID, [FromBody] ShoppingList values)
        {
            ShoppingList shoppingListObject;
            try
            {
                shoppingListObject = new ShoppingList()
                {
                    ID = shoppingListID,
                    Name = values.Name,
                    BudgetSum = values.BudgetSum
                };

                repo.UpdateShoppingList(shoppingListObject);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message + "(Unable to update shopping list)");
            }
            return Ok(shoppingListObject);
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
