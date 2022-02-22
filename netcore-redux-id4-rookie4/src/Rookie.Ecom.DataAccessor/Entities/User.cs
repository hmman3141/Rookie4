using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.DataAccessor.Entities
{
    public class User:BaseEntity
    {
        [StringLength(maximumLength:100)]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 100)]
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
