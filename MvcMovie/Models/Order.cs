using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace MvcMovie.Models
{
    public class Order
    {

        public int OrderId { get; set; }
        public string Total { get; set; }
        public DateTime orderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } 
    }
}