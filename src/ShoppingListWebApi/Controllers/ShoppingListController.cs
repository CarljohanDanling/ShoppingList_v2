using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Models;
using ShoppingListWebApi.Database;

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
        public List<ShoppingList> GetAllShoppingLists()
        {
            List<ShoppingList> shoppingList = repo.GetAllShoppingLists();
            return shoppingList;
        }

        // POST api/shoppinglist
        // POST shopping lists to database and returns it.
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult InsertShoppingList([FromBody] ShoppingList values)
        {
            ShoppingList shoppingListObject;
            try
            {
                shoppingListObject = new ShoppingList()
                {
                    Name = values.Name,
                    BudgetSum = values.BudgetSum
                };
                shoppingListObject.ShoppingListId = repo.InsertShoppingList(shoppingListObject);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message + "(Unable to add shopping list)");
            }
            return Ok(shoppingListObject);
        }

        // GET /api/shoppinglist/5
        // GET detailed information about the shopping list (name, budget)
        [HttpGet("{shoppingListId}")]
        public List<ShoppingList> GetDetailedInformationOfSpecificShoppingList(int shoppingListId)
        {
            List<ShoppingList> shoppingList = repo.GetDetailedInformationOfSpecificShoppingList(shoppingListId);
            return shoppingList;
        }

        // PUT api/shoppinglist/5
        // PUT to update/change the shopping list in database.
        [HttpPut("{shoppingListId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateShoppingList(int shoppingListId, [FromBody] ShoppingList values)
        {
            ShoppingList shoppingListObject;
            try
            {
                shoppingListObject = new ShoppingList()
                {
                    ShoppingListId = shoppingListId,
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

        // DELETE api/shoppinglist/5
        // Deletes the actual shopping list
        [HttpDelete("{shoppingListId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult DeleteShoppingList(int shoppingListId)
        {
            try
            {
                repo.DeleteShoppingList(shoppingListId);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message + "(Unable to delete shopping list)");
            }
            return Ok("Successfully deleted shopping list");
        }
    }
}
