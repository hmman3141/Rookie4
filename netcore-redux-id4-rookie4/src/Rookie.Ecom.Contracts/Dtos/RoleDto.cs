using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.Contracts.Dtos
{
    public class RoleDto:BaseDto
    {
        public string RoleName { get; set; }
        public ICollection<UserRoleDto> UserRoles { get; set; }
    }
}
