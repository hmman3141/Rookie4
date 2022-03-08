using Rookie.Ecom.Contracts;
using Rookie.Ecom.Contracts.Dtos;
using Rookie.Ecom.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.Business.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<PagedResponseModel<ProductDto>> PagedQueryAsync(Expression<Func<Product, bool>> filter, int page, int limit, string includeProperties = "");

        Task<ProductDto> GetByIdAsync(Guid id);

        Task<IEnumerable<ProductDto>> GetAllByAsync(Expression<Func<Product, bool>> filter, string includeProperties = "");

        Task<ProductDto> GetByAsync(Expression<Func<Product, bool>> filter, string includeProperties = "");

        Task<ProductDto> AddAsync(ProductDto productDto);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(ProductDto productDto);
    }
}
