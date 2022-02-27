using System;
using System.Collections.Generic;

namespace Rookie.Ecom.Contracts.Dtos
{
    public class CategoryDto : BaseDto
    {
        public string CategoryName { get; set; }

        public string Desc { get; set; }

        public string ImageUrl { get; set; }

        /*public ICollection<ProductDto> Products { get; set; }*/
    }
}
