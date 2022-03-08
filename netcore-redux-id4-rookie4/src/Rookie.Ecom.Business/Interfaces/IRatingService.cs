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
    public interface IRatingService
    {
        Task<IEnumerable<RatingDto>> GetAllAsync();

        // Task<PagedResponseModel<RatingDto>> PagedQueryAsync(string name, int page, int limit);

        Task<RatingDto> GetByIdAsync(Guid id);

        // Task<RatingDto> GetByNameAsync(string name);

        Task<RatingDto> AddAsync(RatingDto RatingDto);

        Task<IEnumerable<RatingDto>> GetAllByAsync(Expression<Func<Rating, bool>> filter, string includeProperties = "");

        Task DeleteAsync(Guid id);

        Task UpdateAsync(RatingDto RatingDto);
    }
}
