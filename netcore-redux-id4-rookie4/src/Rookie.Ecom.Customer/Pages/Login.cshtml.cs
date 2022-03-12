using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rookie.Ecom.Customer.Pages
{
    [Authorize]
    public class LoginModel : PageModel
    {
        public IActionResult OnGet(string? returnUrl)
        {
            if(returnUrl == null)
            {
                return Redirect("/");
            }
            else
            {
                return Redirect(returnUrl);
            }
            
        }
    }
}
