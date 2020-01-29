using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingListWebApp.Pages.Shopper
{
    public class PostRedirectGetModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            return RedirectToPage("/Shopper/ViewSelectedList", new { shoppingListId = id });
        }
    }
}