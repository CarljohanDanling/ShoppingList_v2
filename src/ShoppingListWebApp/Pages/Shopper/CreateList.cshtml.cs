using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingListWebApp.Model;

namespace ShoppingListWebApp.Pages.Shopper
{
    public class CreateListModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateListModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public ShoppingList ShoppingList { get; set; }

        public async Task<IActionResult> OnPost(ShoppingList shoppingList)
        {
            if (ModelState.IsValid)
            {
                var request = await _httpClient.PostAsJsonAsync("http://localhost:61122/api/shoppinglist/", shoppingList);

                if (request.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Shopper/BrowseLists");
                }
            }
            return RedirectToPage();
        }
    }
}