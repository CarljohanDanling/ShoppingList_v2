using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace ShoppingListWebApi.Controllers
{
    [Route("api/shoppinglist/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ShoppingListContext _context;

        public ItemController(ShoppingListContext context)
        {
            _context = context;
        }

        // POST: /api/shoppinglist/item/13
        // POST an item into a shoppinglist
        [HttpPost("{shoppingListId}")]
        public IActionResult InsertItem(int shoppingListId, [FromBody] Item values)
        {
            try
            {
                values.ShoppingListId = shoppingListId;
                _context.Item.Add(values);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "Unable to add item");
            }
            return Ok("Item added");
        }


        // PATCH: api/shoppinglist/item/1
        // PATCH to update/change an item
        [HttpPatch("{itemId}")]
        public async Task<IActionResult> UpdateItem(int itemId, [FromBody]JsonPatchDocument<Item> item)
        {
            try
            {
                item.ApplyTo(await _context.Item.FindAsync(itemId), ModelState);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "Unable to update the item");
            }
            return Ok();
        }

        // DELETE: /api/shoppinglist/item/1
        // DELETES an item from database
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            try
            {
                _context.Item.Remove(new Item() { ItemId = itemId });
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "Unable to delete item");
            }
            return Ok("Successfully deleted item");
        }
    }
}
