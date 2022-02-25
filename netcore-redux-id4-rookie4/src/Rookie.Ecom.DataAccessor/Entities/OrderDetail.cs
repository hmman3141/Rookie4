using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.DataAccessor.Entities
{
    public class OrderDetail:BaseEntity
    {
        public Guid? OrderID { get; set; }
        public Order Order { get; set; }

        public Guid? ProductID { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

    }
}
