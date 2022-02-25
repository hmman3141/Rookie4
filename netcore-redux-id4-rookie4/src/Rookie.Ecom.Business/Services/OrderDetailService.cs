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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IBaseRepository<OrderDetail> _baseRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IBaseRepository<OrderDetail> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<OrderDetailDto> AddAsync(OrderDetailDto orderDetailDto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
            var item = await _baseRepository.AddAsync(orderDetail);
            return _mapper.Map<OrderDetailDto>(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(OrderDetailDto orderDetailDto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
            await _baseRepository.UpdateAsync(orderDetail);
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllAsync()
        {
            var orderDetails = await _baseRepository.GetAllAsync();
            return _mapper.Map<List<OrderDetailDto>>(orderDetails);
        }

        public async Task<OrderDetailDto> GetByIdAsync(Guid id)
        {
            // map roles and users: collection (roleid, userid)
            // upsert: delete, update, insert
            // input vs db
            // input-y vs db-no => insert
            // input-n vs db-yes => delete
            // input-y vs db-y => update
            // unique, distinct, no-duplicate
            var orderDetail = await _baseRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDetailDto>(orderDetail);
        }

        /* public async Task<OrderDetailDto> GetByNameAsync(string name)
        {
            var OrderDetail = await _baseRepository.GetByAsync(x => x.OrderDetailName == name);
            return _mapper.Map<OrderDetailDto>(OrderDetail);
        }

        public async Task<PagedResponseModel<OrderDetailDto>> PagedQueryAsync(string name, int page, int limit)
        {
            var query = _baseRepository.Entities;

            query = query.Where(x => string.IsNullOrEmpty(name) || x.OrderDetailName.Contains(name));

            query = query.OrderBy(x => x.OrderDetailName);

            var assets = await query
                .AsNoTracking()
                .PaginateAsync(page, limit);

            return new PagedResponseModel<OrderDetailDto>
            {
                CurrentPage = assets.CurrentPage,
                TotalPages = assets.TotalPages,
                TotalItems = assets.TotalItems,
                Items = _mapper.Map<IEnumerable<OrderDetailDto>>(assets.Items)
            };
        }*/

    }
}
