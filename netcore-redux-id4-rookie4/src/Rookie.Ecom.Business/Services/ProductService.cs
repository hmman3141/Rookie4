using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Contracts;
using Rookie.Ecom.Contracts.Dtos;
using Rookie.Ecom.DataAccessor.Entities;
using Rookie.Ecom.DataAccessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.Business.Services
{
    public class ProductService: IProductService
    {
        private readonly IBaseRepository<Product> _baseRepository;
        private readonly IMapper _mapper;

        public ProductService(IBaseRepository<Product> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddAsync(ProductDto ProductDto)
        {
            var product = _mapper.Map<Product>(ProductDto);
            var item = await _baseRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(ProductDto ProductDto)
        {
            var product = _mapper.Map<Product>(ProductDto);
            await _baseRepository.UpdateAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var categories = await _baseRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(categories);
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            // map roles and users: collection (roleid, userid)
            // upsert: delete, update, insert
            // input vs db
            // input-y vs db-no => insert
            // input-n vs db-yes => delete
            // input-y vs db-y => update
            // unique, distinct, no-duplicate
            var product = await _baseRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllByAsync(Expression<Func<Product,bool>> filter, string includeProperties = "")
        {
            var product = await _baseRepository.GetAllByAsync(filter, includeProperties);
            return _mapper.Map<List<ProductDto>>(product);
        }

        public async Task<ProductDto> GetByAsync(Expression<Func<Product, bool>> filter, string includeProperties = "")
        {
            var product = await _baseRepository.GetByAsync(filter, includeProperties);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PagedResponseModel<ProductDto>> PagedQueryAsync(String? product, int page, int limit, String? category, int minvalue = 0, int maxvalue = 0, string includeProperties = "")
        {

            var query = _baseRepository.Entities;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if(!string.IsNullOrEmpty(product))
                query = query.Where(x => x.ProductName == product || x.ProductName.Contains(product));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(x => x.Category.CategoryName == category);

            if (maxvalue > 0)
                query = query.Where(x => x.Price <= maxvalue);

            query = query.Where(x => x.Price >= minvalue);

            query = query.OrderBy(x => x.ProductName);

            var assets = await query
                .AsNoTracking()
                .PaginateAsync(page, limit);

            return new PagedResponseModel<ProductDto>
            {
                CurrentPage = assets.CurrentPage,
                TotalPages = assets.TotalPages,
                TotalItems = assets.TotalItems,
                Items = _mapper.Map<IEnumerable<ProductDto>>(assets.Items)
            };
        }

    }
}
