using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShoppingListWebApp.Model;

namespace ShoppingListWebApp.Pages.Shopper
{
    public class BrowseListsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public BrowseListsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public List<ShoppingList> ShoppingList { get; set; }

        public async Task OnGet()
        {
            var response = await _httpClient.GetAsync("api/shoppinglist");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ShoppingList = JsonConvert.DeserializeObject<List<ShoppingList>>(content);
            }
        }

        public async Task<IActionResult> OnGetDeleteList(int shoppingListId)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:61122/api/shoppinglist/{shoppingListId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("BrowseLists");
            }
            return BadRequest("Opps, something went wrong when deleting...");
        }
    }
}