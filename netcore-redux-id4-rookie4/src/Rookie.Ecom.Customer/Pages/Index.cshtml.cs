using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.Customer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public IndexModel(ILogger<IndexModel> logger, IRoleService roleService, IUserRoleService userRoleService)
        {
            _logger = logger;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        public IEnumerable<RoleDto> Roles => _roleService.GetAllAsync().Result;
        public IEnumerable<UserDto> Users(Guid id)
        {

            return _userRoleService.GetByRoleID(id).Result;
        }

        public void OnGet()
        {

        }
    }
}
