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

        Task<RatingDto> GetByIdAsync(Guid id);

        Task<RatingDto> GetByAsync(Expression<Func<Rating, bool>> filter, string includeProperties = "");

        Task<RatingDto> AddAsync(RatingDto RatingDto);

        Task<IEnumerable<RatingDto>> GetAllByAsync(Expression<Func<Rating, bool>> filter, string includeProperties = "");

        Task DeleteAsync(Guid id);

        Task UpdateAsync(RatingDto RatingDto);
    }
}
