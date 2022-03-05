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

        Task<PagedResponseModel<ProductDto>> PagedQueryAsync(string name, int page, int limit);

        Task<ProductDto> GetByIdAsync(Guid id);

        Task<IEnumerable<ProductDto>> GetAllByAsync(Expression<Func<Product, bool>> filter);

        Task<IEnumerable<ProductDto>> GetByCateID(Guid id, int minvalue, int maxvalue);

        Task<IEnumerable<ProductDto>> GetByRange(int minvalue, int maxvalue);

        Task<ProductDto> AddAsync(ProductDto productDto);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(ProductDto productDto);
    }
}
