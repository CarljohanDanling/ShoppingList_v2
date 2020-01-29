using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShoppingListWebApp.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace ShoppingListWebApp.Pages.Shopper
{
    public class ViewSelectedListModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ViewSelectedListModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public Item Item { get; set; }
        public ShoppingList ShoppingList { get; set; }

        public async Task<IActionResult> OnGet(int shoppingListId)
        {
            var request = await _httpClient.GetAsync($"http://localhost:61122/api/shoppinglist/{shoppingListId}");

            if (request.IsSuccessStatusCode)
            {
                var response = await request.Content.ReadAsStringAsync();
                ShoppingList = JsonConvert.DeserializeObject<ShoppingList>(response);
                return Page();
            }
            return RedirectToPage("/Shopper/ErrorMessage");
        }

        public async Task<IActionResult> OnPostCreateItem(Item item, int shoppingListId)
        {
            if (ModelState.IsValid)
            {
                var request = await _httpClient.PostAsJsonAsync($"http://localhost:61122/api/shoppinglist/item/{shoppingListId}", item);

                if (request.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Shopper/PostRedirectGet", new { id = shoppingListId });
                }
            }
            return RedirectToPage("/Shopper/ErrorMessage");
        }

        public async Task<IActionResult> OnPostBudgetHandler(Item item, int shoppingListId, int itemId)
        {
            JsonPatchDocument<Item> patchDoc = new JsonPatchDocument<Item>();

            patchDoc.Replace(i => i.Name, item.Name);
            patchDoc.Replace(i => i.Price, item.Price);
            patchDoc.Replace(i => i.Quantity, item.Quantity);

            var itemToUpdateJson = JsonConvert.SerializeObject(patchDoc);

            await _httpClient.PatchAsync($"http://localhost:61122/api/shoppinglist/item/{itemId}",
                new StringContent
                    (
                        itemToUpdateJson,
                        System.Text.Encoding.Unicode,
                        "application/json")
                    );

            return RedirectToPage("/Shopper/PostRedirectGet", new { id = shoppingListId });
        }

        public async Task<IActionResult> OnGetDeleteItem(int itemId, int shoppingListId)
        {
            var request = await _httpClient.DeleteAsync($"http://localhost:61122/api/shoppinglist/item/{itemId}");

            if (request.IsSuccessStatusCode)
            {
                return RedirectToPage("/Shopper/PostRedirectGet", new { id = shoppingListId });
            }

            return RedirectToPage("/Shopper/ErrorMessage");
        }
    }
}