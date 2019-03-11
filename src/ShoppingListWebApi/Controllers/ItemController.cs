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
    [Route("api/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IRepository repo;

        public ItemController(IRepository repos)
        {
            repo = repos;
        }

        // GET api/shoppinglist/item
        // GET all items from database
        [HttpGet]
        public List<Item> GetAllItemsFromDatabase()
        {
            List<Item> items = repo.GetAllItems();
            return items;
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
