using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.DataAccessor.Entities
{
    public class UserRole
    {
        [Key]
        [Column(Order=0)]
        public Guid? UserID { get; set; }
        public User User { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid? RoleID { get; set; }
        public Role Role { get; set; }
    }
}
