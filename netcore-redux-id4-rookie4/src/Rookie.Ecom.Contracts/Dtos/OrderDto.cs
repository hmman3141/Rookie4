using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.Contracts.Dtos
{
    public class OrderDto:BaseDto
    {
        public int TotalAmount { get; set; }

        public string ShippingAddress { get; set; }

        public Guid? UserID { get; set; }

        public UserDto User { get; set; }

        public Status Status { get; set; }

        public ICollection<OrderDetailDto> OrderDetails { get; set; }
    }
    public enum Status { Processing, Preparing, Delivering, Delivered, Cancel }
}
