using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Contracts;
using Rookie.Ecom.Contracts.Constants;
using Rookie.Ecom.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rookie.Ecom.Admin.Controllers
{
    [Route("api/[controller]")]
    public class UserAddressController : Controller
    {
        private readonly IUserAddressService _userAddressService;
        public UserAddressController(IUserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }

        [HttpPost]
        public async Task<ActionResult<UserAddressDto>> CreateAsync([FromBody] UserAddressDto userAddressDto)
        {
            Ensure.Any.IsNotNull(userAddressDto, nameof(UserAddressDto));
            var asset = await _userAddressService.AddAsync(userAddressDto);
            return Created(Endpoints.UserAddress, asset);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UserAddressDto userAddressDto)
        {
            Ensure.Any.IsNotNull(userAddressDto, nameof(UserAddressDto));
            await _userAddressService.UpdateAsync(userAddressDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssetAsync([FromRoute] Guid id)
        {
            var userAddressDto = await _userAddressService.GetByIdAsync(id);
            Ensure.Any.IsNotNull(userAddressDto, nameof(UserAddressDto));
            await _userAddressService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<UserAddressDto> GetByIdAsync(Guid id)
            => await _userAddressService.GetByIdAsync(id);

        [HttpGet]
        public async Task<IEnumerable<UserAddressDto>> GetAsync()
            => await _userAddressService.GetAllAsync();

        /*[HttpGet("find")]
        public async Task<PagedResponseModel<UserAddressDto>>
            FindAsync(string name, int page = 1, int limit = 10)
            => await _userAddressService.PagedQueryAsync(name, page, limit);*/
    }
}
