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
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();

        Task<OrderDto> GetByIdAsync(Guid id);

        Task<IEnumerable<OrderDto>> GetAllByAsync(Expression<Func<Order, bool>> filter, string includeProperties = "");

        Task<OrderDto> AddAsync(OrderDto orderDto);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(OrderDto orderDto);
    }
}
