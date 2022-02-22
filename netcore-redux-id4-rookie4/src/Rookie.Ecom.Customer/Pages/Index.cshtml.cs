using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.Customer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICategoryService _categoryService;

        public IndexModel(ILogger<IndexModel> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public IEnumerable<CategoryDto> GetAsync => _categoryService.GetAllAsync().Result;

        public void OnGet()
        {

        }
    }
}
