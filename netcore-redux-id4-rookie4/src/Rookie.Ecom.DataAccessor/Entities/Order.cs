using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.DataAccessor.Entities
{
    public class Order: BaseEntity
    {
        public int TotalAmount { get; set; }
        
        [Required]
        [StringLength(maximumLength:100)]
        public string ShippingAddress { get; set; }

        public Guid? UserID { get; set; }

        public User User { get; set; }

        public Status Status { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public enum Status { Processing,Preparing,Delivering,Delivered,Cancel}

}
