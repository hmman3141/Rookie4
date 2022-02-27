using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.Contracts.Dtos
{
    public class OrderDetailDto:BaseDto
    {
        public Guid? OrderID { get; set; }
        /*public OrderDto Order { get; set; }*/

        public Guid? ProductID { get; set; }
        /*public ProductDto Product { get; set; }*/

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
