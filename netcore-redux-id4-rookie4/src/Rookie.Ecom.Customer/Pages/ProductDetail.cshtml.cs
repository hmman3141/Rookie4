using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Contracts.Dtos;

namespace Rookie.Ecom.Customer.Pages
{
    public class ProductDetailModel : PageModel
    {
        public String name = "";
        private readonly ICategoryService categoryService;
        public ProductDetailModel(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public IEnumerable<CategoryDto> categoryDtos;

        public void OnGet(String product)
        {
            name = product;
            categoryDtos = categoryService.GetAllByAsync(x => 1 == 1).Result;
        }
    }
}
