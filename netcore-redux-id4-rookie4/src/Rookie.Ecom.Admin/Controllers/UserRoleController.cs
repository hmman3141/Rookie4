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
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public async Task<ActionResult<UserRoleDto>> CreateAsync([FromBody] UserRoleDto userRoleDto)
        {
            Ensure.Any.IsNotNull(userRoleDto, nameof(UserRoleDto));
            var asset = await _userRoleService.AddAsync(userRoleDto);
            return Created(Endpoints.UserRole, asset);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UserRoleDto userRoleDto)
        {
            Ensure.Any.IsNotNull(userRoleDto, nameof(UserRoleDto));
            await _userRoleService.UpdateAsync(userRoleDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssetAsync([FromRoute] Guid id)
        {
            var userRoleDto = await _userRoleService.GetByIdAsync(id);
            Ensure.Any.IsNotNull(userRoleDto, nameof(UserRoleDto));
            await _userRoleService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<UserRoleDto> GetByIdAsync(Guid id)
            => await _userRoleService.GetByIdAsync(id);

        [HttpGet]
        public async Task<IEnumerable<UserRoleDto>> GetAsync()
            => await _userRoleService.GetAllAsync();

        /*[HttpGet("find")]
        public async Task<PagedResponseModel<UserRoleDto>>
            FindAsync(string name, int page = 1, int limit = 10)
            => await _userRoleService.PagedQueryAsync(name, page, limit);*/
    }
}
