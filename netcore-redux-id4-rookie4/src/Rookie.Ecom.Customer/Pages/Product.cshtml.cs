using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Rookie.Ecom.Business.Interfaces;
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


        public string temp;
        public IEnumerable<ProductDto> products;
        public IEnumerable<CategoryDto> categories => _categoryService.GetAllAsync().Result;
        public ProductPictureDto productPicture(Guid id) => _productPictureService.GetAllByProductIdAsync(id).Result.FirstOrDefault();
        public void OnGet(String product, String category, int minvalue, int maxvalue)
        {
            products = _productService.GetAllByAsync(x => x.ProductName == product || x.ProductName.Contains(product)).Result;

            if (!String.IsNullOrEmpty(product))
                products = _productService.GetAllByAsync(x => x.ProductName == product || x.ProductName.Contains(product)).Result;
            else
            {
                minvalue = Math.Abs(minvalue);
                maxvalue = Math.Abs(maxvalue);
                if (minvalue > maxvalue && maxvalue != 0)
                {
                    var temp = maxvalue;
                    maxvalue = minvalue;
                    minvalue = temp;
                }

                if (category != null)
                {
                    products = _productService.GetByCateID(_categoryService.GetByNameAsync(category).Result.Id, minvalue, maxvalue).Result;
                }
                else
                {
                    products = _productService.GetByRange(minvalue, maxvalue).Result;
                }
            }
        }
    }
}
