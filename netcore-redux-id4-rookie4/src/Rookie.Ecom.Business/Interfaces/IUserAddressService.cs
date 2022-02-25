using Rookie.Ecom.Contracts;
using Rookie.Ecom.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.Business.Interfaces
{
    public interface IUserAddressService
    {
        Task<IEnumerable<UserAddressDto>> GetAllAsync();

        // Task<PagedResponseModel<UserAddressDto>> PagedQueryAsync(string name, int page, int limit);

        Task<UserAddressDto> GetByIdAsync(Guid id);

        // Task<UserAddressDto> GetByNameAsync(string name);

        Task<UserAddressDto> AddAsync(UserAddressDto userAddressDto);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(UserAddressDto userAddressDto);
    }
}
