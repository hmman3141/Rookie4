using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Contracts;
using Rookie.Ecom.Contracts.Dtos;

namespace Rookie.Ecom.Customer.Pages
{
    public class ProductModel : PageModel
    {
        private readonly ILogger<ProductModel> _logger;
        private readonly IProductService _productService;
        private readonly IProductPictureService _productPictureService;
        private readonly ICategoryService _categoryService;

        public ProductModel(ILogger<ProductModel> logger, IProductService productService, IProductPictureService productPictureService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _productPictureService = productPictureService;
            _categoryService = categoryService;
        }


        public string productName = "";
        public string categoryName = "";
        public int limitPage;
        public int currentPage;
        public PagedResponseModel<ProductDto> products;
        public IEnumerable<CategoryDto> categories => _categoryService.GetAllAsync().Result;
        
        public void OnGet(String product, String category, int minvalue = 0, int maxvalue = 0, int limit = 9, int curpage = 1)
        {
            productName = product;
            categoryName = category;
            limitPage = limit;
            currentPage = curpage;

            minvalue = Math.Abs(minvalue);
            maxvalue = Math.Abs(maxvalue);
            if (minvalue > maxvalue && maxvalue != 0)
            {
                var temp = maxvalue;
                maxvalue = minvalue;
                minvalue = temp;
            }

            products = _productService.PagedQueryAsync(product, curpage, limit, category, minvalue, maxvalue, "Category,ProductPictures").Result;
            
        }
    }
}
