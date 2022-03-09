using Microsoft.AspNetCore.Authorization;
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
        private readonly ICategoryService categoryService;
        private readonly IOrderDetailService orderDetailService;
        private readonly IProductService productService;

        public IndexModel(ILogger<IndexModel> logger, ICategoryService _categoryService, IOrderDetailService _orderDetailService, IProductService _productService)
        {
            _logger = logger;
            categoryService = _categoryService;
            orderDetailService = _orderDetailService;
            productService = _productService;
        }

        public IEnumerable<CategoryDto> categoryDtos => categoryService.GetAllAsync().Result;

        public IEnumerable<ProductDto> productDtos;

        public void OnGet()
        {
            var temp = orderDetailService.GetAllByAsync(x => 1 == 1,"Product").Result
                .GroupBy(x => x.ProductID)
                .Select(x => new { Product = x.FirstOrDefault().Product, Count = x.Count()})
                .OrderBy(x=>x.Count)
                .Take(5);

            Guid[] temp1 = new Guid[5];

            int count = 0;

            foreach (var test in temp)
            {
                temp1[count++] = test.Product.Id;
            }

            productDtos = productService.GetAllByAsync(x => temp1.Contains(x.Id),"ProductPictures").Result;
        }
    }
}
