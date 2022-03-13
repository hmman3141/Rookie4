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
        private readonly IProductService productService;
        private readonly IRatingService ratingService;
        public ProductDetailModel(IProductService _productService, IRatingService _ratingService)
        {
            productService = _productService;
            ratingService = _ratingService;
        }

        public ProductDto productDtos;
        public double rating =0;

        public void OnGet(Guid product)
        {
            productDtos = productService.GetByAsync(x => x.Id == product,"ProductPictures,Ratings").Result;
            var ratingCount = ratingService.GetAllByAsync(x => x.ProductID == product, "Product").Result.Count();
            if(ratingCount != 0)
            {
                rating = ratingService.GetAllByAsync(x => x.ProductID == product, "Product").Result.Average(x => x.Rate);
            }
            /*rating = ratingService.GetAllByAsync(x => x.ProductID == product,"Product").Result.Average(x => x.Rate);*/
        }
    }
}
