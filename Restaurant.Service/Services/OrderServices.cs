
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Core.Enums;
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
            context = _context;
        }
        public async Task CreateOrder() {

            bool check = true;
            while (check) {
                Console.WriteLine("Menu item nömrəsi və sayını qeyd edin!");
                int id = int.Parse(Console.ReadLine());
                int count = int.Parse(Console.ReadLine());
                var OrderItem = new OrderItem
                {
                    Id = id,
                    Count = count,
                };
                OrderItem.OrderId = 1;
                Console.WriteLine("sifarişə digər məhsul əlavə edirsiniz? (Yes/No)");
                string YesNo= Console.ReadLine();
                switch (YesNo) {
                    case "Yes":
                    case "yes":
                    case "YES":
                        break;
                    case "No":
                    case "NO":
                    case "no":
                          check=false;
                        break;
                }
               
            }

            float sum = 0;
            foreach (var item in _context.OrderItems)
            {
                sum=sum+(item.Count*_context.MenuItems.Find(item.Id).Price);
            }

            Console.WriteLine(sum);

        }


    }
}
