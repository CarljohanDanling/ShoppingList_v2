using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Database;
using ShoppingListWebApi.Models;

namespace ShoppingListWebApi.Controllers
{
    [Route("api/shoppinglist/{shoppingListId}/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IRepository repo;

        public ItemController(IRepository repos)
        {
            repo = repos;
        }

        // GET /api/shoppinglist/5/item
        // GET all items related to specific shopping list
        [HttpGet]
        public List<Item> GetAllItemsRelatedToSpecificShoppingList(int shoppingListId)
        {
            List<Item> items = repo.GetAllItemsRelatedToSpecificShoppingList(shoppingListId);
            return items;
        }

        // POST /api/shoppinglist/5/item
        // POST an item into a shoppinglist
        [HttpPost]
        public IActionResult InsertItem(int shoppingListId, [FromBody] Item values)
        {
            Item itemObject;
            try
            {
                itemObject = new Item()
                {
                    Name = values.Name,
                    Price = values.Price,
                    Quantity = values.Quantity,
                    ShoppingListId = shoppingListId
                };
                repo.InsertItem(itemObject);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message + "(Unable to add item)");
            }
            return Ok("Item added");
        }



        /*
        // GET: api/Item/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Item
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Item/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
