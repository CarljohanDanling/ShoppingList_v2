using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using ShoppingListWebApi.Model;
using ShoppingListWebApi.Service;

namespace ShoppingListWebApi.Controllers
{
    [Route("api/shoppinglist/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IRepository _repository;

        public ItemController(IRepository repository)
        {
            _repository = repository;
        }

        // POST: /api/shoppinglist/item/13
        // POST an item into a shoppinglist
        [HttpPost("{shoppingListId}")]
        public async Task<IActionResult> InsertItemAsync(int shoppingListId, [FromBody] Item values)
        {
            try
            {
                await _repository.InsertItemAsync(shoppingListId, values);
            }

            catch
            {
                return StatusCode(500, "Unable to add item");
            }

            return Ok("Successfully added item");
        }

        // PATCH: api/shoppinglist/item/1
        // PATCH to update/change an item
        [HttpPatch("{itemId}")]
        public async Task<IActionResult> UpdateItemAsync(int itemId, [FromBody]JsonPatchDocument<Item> item)
        {
            try
            {
                await _repository.UpdateItemAsync(itemId, item);
            }

            catch
            {
                return StatusCode(500, "Unable to update the item");
            }

            return Ok("Successfully updated the item");
        }

        // DELETE: /api/shoppinglist/item/1
        // DELETES an item from database
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItemAsync(int itemId)
        {
            try
            {
                await _repository.DeleteItemAsync(itemId);
            }

            catch
            {
                return StatusCode(500, "Unable to delete item");
            }

            return Ok("Successfully deleted item");
        }
    }
}
