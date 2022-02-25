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
    public class UserRoleService : IUserRoleService
    {
        private readonly IBaseRepository<UserRole> _baseRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IBaseRepository<UserRole> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<UserRoleDto> AddAsync(UserRoleDto userRoleDto)
        {
            var userRole = _mapper.Map<UserRole>(userRoleDto);
            var item = await _baseRepository.AddAsync(userRole);
            return _mapper.Map<UserRoleDto>(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(UserRoleDto userRoleDto)
        {
            var userRole = _mapper.Map<UserRole>(userRoleDto);
            await _baseRepository.UpdateAsync(userRole);
        }

        public async Task<IEnumerable<UserRoleDto>> GetAllAsync()
        {
            var UserRoles = await _baseRepository.GetAllAsync();
            return _mapper.Map<List<UserRoleDto>>(UserRoles);
        }

        public async Task<UserRoleDto> GetByIdAsync(Guid id)
        {
            // map roles and users: collection (roleid, userid)
            // upsert: delete, update, insert
            // input vs db
            // input-y vs db-no => insert
            // input-n vs db-yes => delete
            // input-y vs db-y => update
            // unique, distinct, no-duplicate
            var userRole = await _baseRepository.GetByIdAsync(id);
            return _mapper.Map<UserRoleDto>(userRole);
        }

        /* public async Task<UserRoleDto> GetByNameAsync(string name)
        {
            var UserRole = await _baseRepository.GetByAsync(x => x.UserRoleName == name);
            return _mapper.Map<UserRoleDto>(UserRole);
        }

        public async Task<PagedResponseModel<UserRoleDto>> PagedQueryAsync(string name, int page, int limit)
        {
            var query = _baseRepository.Entities;

            query = query.Where(x => string.IsNullOrEmpty(name) || x.UserRoleName.Contains(name));

            query = query.OrderBy(x => x.UserRoleName);

            var assets = await query
                .AsNoTracking()
                .PaginateAsync(page, limit);

            return new PagedResponseModel<UserRoleDto>
            {
                CurrentPage = assets.CurrentPage,
                TotalPages = assets.TotalPages,
                TotalItems = assets.TotalItems,
                Items = _mapper.Map<IEnumerable<UserRoleDto>>(assets.Items)
            };
        }*/

    }
}
