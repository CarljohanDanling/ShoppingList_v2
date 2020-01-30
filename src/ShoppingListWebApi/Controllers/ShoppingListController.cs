using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Model;
using ShoppingListWebApi.Service;

namespace ShoppingListWebApi.Controllers
{
    [Route("api/shoppinglist")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IRepository _repository;

        public ShoppingListController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/shoppinglist
        // Get all shopping lists from database.
        [HttpGet]
        public async Task<ActionResult<List<ShoppingList>>> GetAllShoppingListsAsync() 
        {
            var shoppingList = await _repository.GetAllShoppingListsAsync();

            if (shoppingList != null)
            {
                return shoppingList;
            }

            return BadRequest();
        }

        // GET: /api/shoppinglist/5
        // GET information about shopping list and all related items
        [HttpGet("{shoppingListId}")]
        public async Task<ActionResult<ShoppingList>> GetShoppingListWithRelatedItemsAsync(int shoppingListId)
        {
            var shoppingList = await _repository.GetShoppingListWithRelatedItemsAsync(shoppingListId);

            if (shoppingList != null)
            {
                return shoppingList;
            }

            return BadRequest();
        }

        // POST: api/shoppinglist
        // POST shopping lists to database.
        [HttpPost]
        public async Task<IActionResult> InsertShoppingListAsync([FromBody] ShoppingList shoppingList)
        {
            try
            {
               await _repository.InsertShoppingListAsync(shoppingList);
            }

            catch
            {
                return StatusCode(500, "(Unable to add shoppinglist)");
            }

            return Ok("Successfully added shoppinglist");
        }

        // PUT: api/shoppinglist/5
        // PUT to update/change the shopping list in database.
        [HttpPut("{shoppingListId}")]
        public async Task<IActionResult> UpdateShoppingListAsync(int shoppingListId, [FromBody] ShoppingList values)
        {
            try
            {
                await _repository.UpdateShoppingListAsync(shoppingListId, values);
            }

            catch
            {
                return StatusCode(500, "(Unable to update shoppinglist)");
            }

            return Ok("Successfully updated shoppinglist");
        }


        // DELETE: api/shoppinglist/5
        // Deletes the actual shopping list
        [HttpDelete("{shoppingListId}")]
        public async Task<IActionResult> DeleteShoppingListAsync(int shoppingListId)
        {
            try
            {
                await _repository.DeleteShoppingListAsync(shoppingListId);
            }

            catch
            {
                return StatusCode(500, "(Unable to delete shoppinglist)");
            }

            return Ok("Successfully deleted shoppinglist");
        }
    }
}