using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.Business.Interfaces;

namespace Rookie.Ecom.Customer.Pages
{
    [Authorize]
    public class LoginModel : PageModel
    {
        private readonly IUserService userService;

        public LoginModel(IUserService _userService)
        {
            userService = _userService;
        }

        public async Task<IActionResult> OnGet(string? returnUrl)
        {
            
            if (User.IsInRole("User") && userService.GetByIdAsync(Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value)).Result == null)
            {
                await userService.AddAsync(new Contracts.Dtos.UserDto
                {
                    FirstName = User.Claims.Where(x => x.Type == JwtClaimTypes.FamilyName).FirstOrDefault().Value,
                    LastName = User.Claims.Where(x => x.Type == JwtClaimTypes.GivenName).FirstOrDefault().Value,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Pubished = true,
                    CreatorId = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value),
                    Id = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value)
                });
            } 
            

                
            if(returnUrl == null)
            {
                return Redirect("/");
            }
            
            return Redirect(returnUrl);
            
        }
    }
}
