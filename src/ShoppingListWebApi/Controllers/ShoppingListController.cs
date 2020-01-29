using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListWebApi.Controllers
{
    [Route("api/shoppinglist")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly ShoppingListContext _context;

        public ShoppingListController(ShoppingListContext context)
        {
            _context = context;
        }

        // GET: api/shoppinglist
        // Get all shopping lists from database.
        [HttpGet]
        public async Task<List<ShoppingList>> GetAllShoppingLists()
        {
            return await _context.ShoppingList.AsNoTracking().ToListAsync();
        }

        // GET: /api/shoppinglist/5
        // GET information about shopping list and all related items
        [HttpGet("{shoppingListId}")]
        public async Task<ActionResult<ShoppingList>> GetShoppingListWithRelatedItems(int shoppingListId)
        {
            var shoppingList = await _context.ShoppingList.Include(s => s.Items).FirstOrDefaultAsync(s => s.ShoppingListId == shoppingListId);
            if (shoppingList == null)
            {
                return BadRequest();
            }

            return shoppingList;
        }

        // POST: api/shoppinglist
        // POST shopping lists to database.
        [HttpPost]
        public async Task<IActionResult> InsertShoppingList([FromBody] ShoppingList shoppingList)
        {
            try
            {
                await _context.ShoppingList.AddAsync(shoppingList);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "(Unable to add shopping list)");
            }

            return Ok("Shopping list added");
        }


        // PUT: api/shoppinglist/5
        // PUT to update/change the shopping list in database.
        [HttpPut("{shoppingListId}")]
        public IActionResult UpdateShoppingList(int shoppingListId, [FromBody] ShoppingList values)
        {
            try
            {
                var shoppingListToUpdate = _context.ShoppingList.First(s => s.ShoppingListId == shoppingListId);
                shoppingListToUpdate.Name = values.Name;
                shoppingListToUpdate.BudgetSum = values.BudgetSum;

                _context.ShoppingList.Update(shoppingListToUpdate);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "(Unable to update shopping list)");
            }
            return Ok(values);
        }


        // DELETE: api/shoppinglist/5
        // Deletes the actual shopping list
        [HttpDelete("{shoppingListId}")]
        public async Task<IActionResult> DeleteShoppingList(int shoppingListId)
        {
            try
            {
                _context.ShoppingList.Remove(new ShoppingList() { ShoppingListId = shoppingListId });
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "(Unable to delete shopping list)");
            }
            return Ok("Successfully deleted shopping list");
        }
    }
}
