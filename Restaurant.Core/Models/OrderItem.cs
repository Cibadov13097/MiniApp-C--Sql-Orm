using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public int MenuItem { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }    
        public MenuItem menuitem {get;set;}
        

    }
}
