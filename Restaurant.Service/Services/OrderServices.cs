
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Restaurant.Core.Models;
using Restaurant.DataAccess.Data;
using Restaurant.Service.Exceptions;

namespace Restaurant.Service.Services
{
    public class OrderServices
    {
        private readonly RestaurantDB _context;
        public OrderServices(RestaurantDB context)
        {
            _context = context;
        }
        public async Task CreateOrder()
        {
           
            Order order = new Order();
            order.Date = DateTime.Now;
            List<OrderItem> items = new List<OrderItem>();
            bool check = true;
            order.OrderItems = items;
            Console.WriteLine("Sifarişə məhsulları əlavə edin");
            while (check)
            {
                
                Console.WriteLine("Məhsulun nömrəsi");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine(_context.MenuItems.Find(id).Name+" məhsulun sifarişə elavə edirsiniz!");
                Console.WriteLine("Məhsulun sayını qeyd edin!");
                int count = int.Parse(Console.ReadLine());

                var OrderItem = new OrderItem
                {
                    MenuItemId = id,
                    Count = count,
                    OrderId = order.Id,

                };
                items.Add(OrderItem);
                _context.AddAsync(OrderItem);

                Console.WriteLine("sifarişə digər məhsul əlavə edirsiniz? (Yes/No)");
                string YesNo = Console.ReadLine();
                switch (YesNo)
                {
                    case "Yes":
                    case "yes":
                    case "YES":
                        break;
                    case "No":
                    case "NO":
                    case "no":
                        check = false;
                        break;
                }

            }
            float sum = 0;
            foreach (var item in items)
            {
                sum = sum + (item.Count * _context.MenuItems.Find(item.MenuItemId).Price);
            }
            order.TotalAmount = sum;




            _context.AddAsync(order);
            await _context.SaveChangesAsync();

        }
    }
}
