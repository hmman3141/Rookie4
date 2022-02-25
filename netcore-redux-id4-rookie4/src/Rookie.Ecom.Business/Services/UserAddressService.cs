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
using System.Threading.Tasks;

namespace Rookie.Ecom.Business.Services
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IBaseRepository<UserAddress> _baseRepository;
        private readonly IMapper _mapper;

        public UserAddressService(IBaseRepository<UserAddress> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<UserAddressDto> AddAsync(UserAddressDto userAddressDto)
        {
            var userAddress = _mapper.Map<UserAddress>(userAddressDto);
            var item = await _baseRepository.AddAsync(userAddress);
            return _mapper.Map<UserAddressDto>(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(UserAddressDto userAddressDto)
        {
            var userAddress = _mapper.Map<UserAddress>(userAddressDto);
            await _baseRepository.UpdateAsync(userAddress);
        }

        public async Task<IEnumerable<UserAddressDto>> GetAllAsync()
        {
            var UserAddresss = await _baseRepository.GetAllAsync();
            return _mapper.Map<List<UserAddressDto>>(UserAddresss);
        }

        public async Task<UserAddressDto> GetByIdAsync(Guid id)
        {
            // map roles and users: collection (roleid, userid)
            // upsert: delete, update, insert
            // input vs db
            // input-y vs db-no => insert
            // input-n vs db-yes => delete
            // input-y vs db-y => update
            // unique, distinct, no-duplicate
            var userAddress = await _baseRepository.GetByIdAsync(id);
            return _mapper.Map<UserAddressDto>(userAddress);
        }

        /* public async Task<UserAddressDto> GetByNameAsync(string name)
        {
            var UserAddress = await _baseRepository.GetByAsync(x => x.UserAddressName == name);
            return _mapper.Map<UserAddressDto>(UserAddress);
        }

        public async Task<PagedResponseModel<UserAddressDto>> PagedQueryAsync(string name, int page, int limit)
        {
            var query = _baseRepository.Entities;

            query = query.Where(x => string.IsNullOrEmpty(name) || x.UserAddressName.Contains(name));

            query = query.OrderBy(x => x.UserAddressName);

            var assets = await query
                .AsNoTracking()
                .PaginateAsync(page, limit);

            return new PagedResponseModel<UserAddressDto>
            {
                CurrentPage = assets.CurrentPage,
                TotalPages = assets.TotalPages,
                TotalItems = assets.TotalItems,
                Items = _mapper.Map<IEnumerable<UserAddressDto>>(assets.Items)
            };
        }*/

    }
}
