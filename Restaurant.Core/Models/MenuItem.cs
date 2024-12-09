using Restaurant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public Category Category { get; set; }

        public List<OrderItem> OrderItems { get; set; }

    }
}
