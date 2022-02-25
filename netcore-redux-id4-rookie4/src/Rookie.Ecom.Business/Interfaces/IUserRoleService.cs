using Rookie.Ecom.Contracts;
using Rookie.Ecom.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.Business.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDto>> GetAllAsync();

        // Task<PagedResponseModel<UserRoleDto>> PagedQueryAsync(string name, int page, int limit);

        Task<UserRoleDto> GetByIdAsync(Guid id);

        // Task<UserRoleDto> GetByNameAsync(string name);

        Task<UserRoleDto> AddAsync(UserRoleDto userRoleDto);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(UserRoleDto userRoleDto);
    }
}
