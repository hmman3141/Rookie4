﻿using AutoMapper;
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
    public class ProductPictureService : IProductPictureService
    {
        private readonly IBaseRepository<ProductPicture> _baseRepository;
        private readonly IMapper _mapper;

        public ProductPictureService(IBaseRepository<ProductPicture> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<ProductPictureDto> AddAsync(ProductPictureDto productPictureDto)
        {
            var productPicture = _mapper.Map<ProductPicture>(productPictureDto);
            var item = await _baseRepository.AddAsync(productPicture);
            return _mapper.Map<ProductPictureDto>(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(ProductPictureDto productPictureDto)
        {
            var productPicture = _mapper.Map<ProductPicture>(productPictureDto);
            await _baseRepository.UpdateAsync(productPicture);
        }

        public async Task<IEnumerable<ProductPictureDto>> GetAllAsync()
        {
            var productPictures = await _baseRepository.GetAllAsync();
            return _mapper.Map<List<ProductPictureDto>>(productPictures);
        }

        public async Task<ProductPictureDto> GetByIdAsync(Guid id)
        {
            // map roles and users: collection (roleid, userid)
            // upsert: delete, update, insert
            // input vs db
            // input-y vs db-no => insert
            // input-n vs db-yes => delete
            // input-y vs db-y => update
            // unique, distinct, no-duplicate
            var productPicture = await _baseRepository.GetByIdAsync(id);
            return _mapper.Map<ProductPictureDto>(productPicture);
        }

        public async Task<ProductPictureDto> GetByProductIdAsync(Guid id)
        {
            var query = _baseRepository.Entities;
            query = query.Where(x => x.ProductId == id);
            query = query.OrderBy(x => x.ProductId);
            var assets = await query
                .AsNoTracking()
                .FirstOrDefaultAsync(null);

            return _mapper.Map<ProductPictureDto>(assets);
        }

        public async Task<IEnumerable<ProductPictureDto>> GetAllByProductIdAsync(Guid id)
        {
            var query = _baseRepository.Entities;
            query = query.Where(x => x.ProductId == id);
            query = query.OrderBy(x => x.ProductId);
            var assets = await query
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ProductPictureDto>>(assets);
        }

        /* public async Task<ProductPictureDto> GetByNameAsync(string name)
        {
            var ProductPicture = await _baseRepository.GetByAsync(x => x.ProductPictureName == name);
            return _mapper.Map<ProductPictureDto>(ProductPicture);
        }

        public async Task<PagedResponseModel<ProductPictureDto>> PagedQueryAsync(string name, int page, int limit)
        {
            var query = _baseRepository.Entities;

            query = query.Where(x => string.IsNullOrEmpty(name) || x.ProductPictureName.Contains(name));

            query = query.OrderBy(x => x.ProductPictureName);

            var assets = await query
                .AsNoTracking()
                .PaginateAsync(page, limit);

            return new PagedResponseModel<ProductPictureDto>
            {
                CurrentPage = assets.CurrentPage,
                TotalPages = assets.TotalPages,
                TotalItems = assets.TotalItems,
                Items = _mapper.Map<IEnumerable<ProductPictureDto>>(assets.Items)
            };
        }*/

    }
}
