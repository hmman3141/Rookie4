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

        public ProductDto productDtos { get; set; }
        public RatingDto ratingUser(Guid id) => ratingService.GetByAsync(x =>x.Id == id,"User").Result;
        public double rating =0;
        public void OnGet(Guid product)
        {
            productDtos = productService.GetByAsync(x => x.Id == product, "ProductPictures,Ratings").Result;
            var ratingCount = ratingService.GetAllByAsync(x => x.ProductID == product, "Product").Result.Count();
            if(ratingCount != 0)
            {
                rating = ratingService.GetAllByAsync(x => x.ProductID == product, "Product").Result.Average(x => x.Rate);
            }
        }
        private RatingDto ratingDto = new RatingDto();
        public async Task<IActionResult> OnPostRatingAsync(string comment, int rating, string product, string user)
        {
            ratingDto.CreatedDate = DateTime.Now;
            ratingDto.UpdatedDate = DateTime.Now;
            ratingDto.Pubished = true;
            ratingDto.ProductID = Guid.Parse(product);
            if(user != "")
            {
                ratingDto.UserID = Guid.Parse(user);
            }
            ratingDto.Rate = rating;
            ratingDto.Comment = comment;
            await ratingService.AddAsync(ratingDto);
            return Redirect("/ProductDetail?product=" + product);
        }
    }
}
