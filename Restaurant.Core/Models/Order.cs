using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public float Total_Price { get; set; }  

        
    }
}
