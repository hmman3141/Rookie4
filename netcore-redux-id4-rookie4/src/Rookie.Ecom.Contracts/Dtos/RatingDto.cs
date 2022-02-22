using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.Contracts.Dtos
{
    public class RatingDto
    {
        public Guid? UserID { get; set; }
        public UserDto User { get; set; }

        public Guid? ProductID { get; set; }
        public ProductDto Product { get; set; }

        public int Rate { get; set; }

        public string Comment { get; set; }
    }
}
