using EnsureThat;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Contracts;
using Rookie.Ecom.Contracts.Constants;
using Rookie.Ecom.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rookie.Ecom.Admin.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductService ProductService, IWebHostEnvironment env)
        {
            _productService = ProductService;
            _env = env;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateAsync([FromBody] ProductDto productDto)
        {
            Ensure.Any.IsNotNull(productDto, nameof(ProductDto));
            var asset = await _productService.AddAsync(productDto);
            return Created(Endpoints.Product, asset);
        }

        [HttpPost("File")]
        public async Task<ActionResult<ProductDto>> CreateFileAsync([FromForm] IFormFile file)
        {
            var div = _env.ContentRootPath;
            using (var fileStream = new FileStream(Path.Combine(div,"ClientApp","public","product", file.FileName), FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] ProductDto productDto)
        {
            Ensure.Any.IsNotNull(productDto, nameof(ProductDto));
            await _productService.UpdateAsync(productDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssetAsync([FromRoute] Guid id)
        {
            var productDto = await _productService.GetByIdAsync(id);
            Ensure.Any.IsNotNull(productDto, nameof(ProductDto));
            await _productService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> GetByIdAsync(Guid id)
            => await _productService.GetByIdAsync(id);

        [HttpGet("all/{id}")]
        public async Task<IEnumerable<ProductDto>> GetAllByIdAsync(Guid id)
            => await _productService.GetAllByAsync(x => x.Id == id);

        [HttpGet("name/{name}")]
        public async Task<ProductDto> GetByNameAsync(string name)
            => await _productService.GetByAsync(x => x.ProductName == name);

        [HttpGet("containname/{name}")]
        public async Task<IEnumerable<ProductDto>> GetByContainNameAsync(string name)
            => await _productService.GetAllByAsync(x => x.ProductName == name || x.ProductName.Contains(name));

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAsync()
            => await _productService.GetAllAsync();

        [HttpGet("find")]
        public async Task<PagedResponseModel<ProductDto>>
            FindAsync(string name, int page = 1, int limit = 10)
            => await _productService.PagedQueryAsync(name, page, limit, null);
    }
}
