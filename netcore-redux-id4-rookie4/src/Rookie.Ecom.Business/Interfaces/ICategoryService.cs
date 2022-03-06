using Rookie.Ecom.Contracts;
using Rookie.Ecom.Contracts.Dtos;
using Rookie.Ecom.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rookie.Ecom.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<PagedResponseModel<CategoryDto>> PagedQueryAsync(string name, int page, int limit);

        Task<CategoryDto> GetByIdAsync(Guid id);

        Task<IEnumerable<CategoryDto>> GetAllByAsync(Expression<Func<Category, bool>> filter, string includeProperties = "");

        Task<CategoryDto> GetByNameAsync(string name);

        Task<CategoryDto> AddAsync(CategoryDto categoryDto);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(CategoryDto categoryDto);
    }
}
