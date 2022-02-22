using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.Contracts.Dtos
{
    public class UserDto:BaseDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<OrderDto> Orders { get; set; }

        public ICollection<RatingDto> Ratings { get; set; }

        public ICollection<UserAddressDto> UserAddresses { get; set; }

        public ICollection<UserRoleDto> UserRoles { get; set; }
    }
}
