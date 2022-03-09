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
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailDto>> GetAllAsync();

        // Task<PagedResponseModel<OrderDetailDto>> PagedQueryAsync(string name, int page, int limit);

        Task<OrderDetailDto> GetByIdAsync(Guid id);

        Task<IEnumerable<OrderDetailDto>> GetAllByAsync(Expression<Func<OrderDetail, bool>> filter, string includeProperties = "");

        Task<OrderDetailDto> AddAsync(OrderDetailDto OrderDetailDto);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(OrderDetailDto OrderDetailDto);
    }
}
