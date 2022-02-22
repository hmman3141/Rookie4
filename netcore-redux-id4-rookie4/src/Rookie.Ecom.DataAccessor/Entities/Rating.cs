using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.DataAccessor.Entities
{
    public class Rating
    {
        [Key]
        [Column(Order=0)]
        public Guid? UserID { get; set; }
        public User User { get; set; }

        [Key]
        [Column(Order=1)]
        public Guid? ProductID { get; set; }
        public Product Product { get; set; }

        public int Rate { get; set; }

        [StringLength(maximumLength:500)]
        public string Comment { get; set; }
    }
}
