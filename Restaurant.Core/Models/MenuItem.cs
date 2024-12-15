using Restaurant.Core.Enums;

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
