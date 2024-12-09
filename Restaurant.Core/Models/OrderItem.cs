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
        public int Count { get; set; }

        public int MenuItemId { get; set; }
        public int OrderId { get; set; }
        public MenuItem MenuItem { get; set; }

        public Order Order { get; set; }
    }
}
