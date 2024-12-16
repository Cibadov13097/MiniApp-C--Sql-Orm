using Restaurant.Core.Models;
using Restaurant.DataAccess.Data;

namespace Restaurant.Service.Services
{
    public class OrderServices
    {

        #region Fields and Constructors
        private readonly RestaurantDB _context;
        public OrderServices(RestaurantDB context)
        {
            _context = context;
        }

        #endregion

        #region Create and Remove methods
        public async Task CreateOrderAsync()
        {

            bool retry = true;
            try
            {
                Order order = new Order();
                order.Date = DateTime.Now;
                List<OrderItem> items = new List<OrderItem>();
                bool check = true;
                order.OrderItems = items;
                Console.WriteLine("Sifarişə məhsulları əlavə edin!");


                int id = 0;

                while (check)
                {
                    bool tryInput = true;
                    while (tryInput)
                    {
                        Console.WriteLine("Məhsulun nömrəsini qeyd edin!");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            var product = _context.MenuItems.Find(id);

                            if (product is not null)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine($"{product.Name} məhsulunu sifarişə əlavə edirsiniz!");
                                tryInput = false;

                                Console.WriteLine("Məhsulun sayını qeyd edin!");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                int count = int.Parse(Console.ReadLine());
                                Console.ForegroundColor = ConsoleColor.Blue;

                                var OrderItem = new OrderItem
                                {
                                    MenuItemId = id,
                                    Count = count,
                                    OrderId = order.Id,

                                };
                                items.Add(OrderItem);
                                _context.AddAsync(OrderItem);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Bu nömrədə məhsul yoxdur, yenidən daxil edin!");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Databazada olan məhsullar görsət (Yes/No)");
                                string input = Console.ReadLine();
                                switch (input)
                                {
                                    case "yes":
                                    case "Yes":
                                    case "YES":
                                        Console.WriteLine();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Məhsullar");
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        foreach (var item in _context.MenuItems)
                                        {
                                            Console.WriteLine(item.Id + "-> " + item.Name);
                                        }
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        break;
                                    case "No":
                                    case "no":
                                    case "NO":
                                        break;
                                }
                                Console.WriteLine(" ");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Yanlış format! Xahiş olunur yalnız rəqəm daxil edin!");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        }
                    }
                YesNo:
                    Green();
                    Console.WriteLine("sifarişə digər məhsul əlavə edirsiniz? (Yes/No)");
                    Magenta();
                    string YesNo = Console.ReadLine();
                    Blue();
                    switch (YesNo)
                    {
                        case "Yes":
                        case "yes":
                        case "YES":
                            check = true;
                            tryInput = false;
                            break;
                        case "No":
                        case "NO":
                        case "no":
                            check = false;
                            break;
                        default:
                            Console.WriteLine("sifarişə digər məhsul əlavə edirsiniz? (Yes/No)");
                            goto YesNo;


                    }
                }
                float sum = 0;
                foreach (var item in items)
                {
                    var menuItem = await _context.MenuItems.FindAsync(item.MenuItemId);
                    if (menuItem != null)
                    {
                        sum += item.Count * menuItem.Price;
                    }
                }
                order.TotalAmount = sum;
                if (check == false)
                {
                    _context.AddAsync(order);
                    await _context.SaveChangesAsync();
                }
            }

            catch (Exception ex)
            {
                ExceptionMessage(ex);
            }
        }
        public async Task RemoveOrderAsync(int id)
        {
            try
            {
                Order order = await _context.Orders.FindAsync(id);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
            }
        }
        #endregion

        #region Show Methods
        public async Task ShowAllOrdersAsync()
        {
            int num = 1;
            try
            {
                foreach (var order1 in _context.Orders)
                {
                    DarkGreen();
                    Console.WriteLine($"Sifariş {num++}");
                    Cyan();
                    Console.WriteLine($"Tarix {order1.Date}");
                    Console.WriteLine($"Yekun qiymət {order1.TotalAmount}");


                    using (var _newContext = new RestaurantDB())
                    {
                        var orderItems = _newContext.OrderItems.Where(oi => oi.OrderId == order1.Id).ToList();
                        White();
                        Console.WriteLine("   Sifariş məhsulları");
                        foreach (var orderItem in orderItems)
                        {
                            using (var _newContext2 = new RestaurantDB())
                            {
                                List<MenuItem> menuItems = _newContext2.MenuItems.ToList();

                                var menuItem = menuItems.Find(mi => mi.Id == orderItem.MenuItemId);

                                if (menuItem != null)
                                {
                                    Console.WriteLine($"     \u001b[35mAd \u001b[36m{menuItem.Name} \u001b[35mSayı \u001b[36m {orderItem.Count} \u001b[35mQiyməti \u001b[36m{menuItem.Price}");
                                }
                                else
                                {
                                    Console.WriteLine("Məhsul yoxdur!");
                                }
                            }

                        }
                        Console.WriteLine();
                        White();
                        Console.WriteLine("----------------------------------------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
            }

        }
        public async Task ShowOrdersByTimeIntervalAsync(DateTime beginingTime, DateTime endingTime)
        {
            int num = 1;

            try
            {
                foreach (var order1 in _context.Orders)
                {
                    if (order1.Date > beginingTime && order1.Date < endingTime)
                    {
                        DarkGreen();
                        Console.WriteLine($"Sifariş {num++}");
                        Cyan();
                        Console.WriteLine($"Tarix {order1.Date}");
                        Console.WriteLine($"Yekun qiymət {order1.TotalAmount}");
                        using (var _newContext = new RestaurantDB())
                        {
                            var orderItems = _newContext.OrderItems.Where(oi => oi.OrderId == order1.Id).ToList();
                            White();
                            Console.WriteLine("   Sifariş məhsulları");
                            foreach (var orderItem in orderItems)
                            {
                                using (var _newContext2 = new RestaurantDB())
                                {
                                    List<MenuItem> menuItems = _newContext2.MenuItems.ToList();

                                    var menuItem = menuItems.Find(mi => mi.Id == orderItem.MenuItemId);

                                    if (menuItem != null)
                                    {
                                        Console.WriteLine($"     \u001b[35mAd \u001b[36m{menuItem.Name} \u001b[35mSayı \u001b[36m {orderItem.Count} \u001b[35mQiyməti \u001b[36m{menuItem.Price}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Məhsul yoxdur!");
                                    }
                                }
                            }
                            Console.WriteLine();
                            White();
                            Console.WriteLine("----------------------------------------------------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
            }
        }
        public async Task ShowOrderByDateAsync(DateTime date)
        {
            int num = 1;
            try
            {

                foreach (var order1 in _context.Orders)
                {
                    if (order1.Date.Date == date.Date)
                    {
                        DarkGreen();
                        Console.WriteLine($"Sifariş {num++}");
                        Cyan();
                        Console.WriteLine($"Tarix {order1.Date}");
                        Console.WriteLine($"Yekun qiymət {order1.TotalAmount}");

                        using (var _newContext = new RestaurantDB())
                        {
                            var orderItems = _newContext.OrderItems.Where(oi => oi.OrderId == order1.Id).ToList();
                            White();
                            Console.WriteLine("   Sifariş məhsulları");
                            foreach (var orderItem in orderItems)
                            {
                                using (var _newContext2 = new RestaurantDB())
                                {
                                    List<MenuItem> menuItems = _newContext2.MenuItems.ToList();

                                    var menuItem = menuItems.Find(mi => mi.Id == orderItem.MenuItemId);

                                    if (menuItem != null)
                                    {
                                        Console.WriteLine($"     \u001b[35mAd \u001b[36m{menuItem.Name} \u001b[35mSayı \u001b[36m{orderItem.Count} \u001b[35mQiyməti \u001b[36m{menuItem.Price}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Məhsul yoxdur!");
                                    }
                                }
                            }
                            Console.WriteLine();
                            White();
                            Console.WriteLine("----------------------------------------------------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
            }
        }
        public async Task ShowOrderByTotalPriceAsync(float minPrice, float maxPrice)
        {
            int num = 1;
            try
            {
                foreach (var order1 in _context.Orders)
                {
                    if (order1.TotalAmount > minPrice && order1.TotalAmount < maxPrice)
                    {
                        DarkGreen();
                        Console.WriteLine($"Order {num++}");
                        Cyan();
                        Console.WriteLine($"Date {order1.Date}");
                        Console.WriteLine($"Total Price {order1.TotalAmount}");


                        using (var _newContext = new RestaurantDB())
                        {
                            var orderItems = _newContext.OrderItems.Where(oi => oi.OrderId == order1.Id).ToList();
                            White();
                            Console.WriteLine("   Sifariş məhsulları");
                            foreach (var orderItem in orderItems)
                            {
                                using (var _newContext2 = new RestaurantDB())
                                {

                                    List<MenuItem> menuItems = _newContext2.MenuItems.ToList();

                                    var menuItem = menuItems.Find(mi => mi.Id == orderItem.MenuItemId);

                                    if (menuItem != null)
                                    {
                                        Console.WriteLine($"     \u001b[35mAd \u001b[36m{menuItem.Name} \u001b[35mSayı \u001b[36m {orderItem.Count} \u001b[35mQiyməti \u001b[36m{menuItem.Price}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Məhsul yoxdur!");
                                    }
                                }
                            }
                            Console.WriteLine();
                            White();
                            Console.WriteLine("----------------------------------------------------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
            }
        }
        public async Task ShowOrderByIdAsync(int orderId)
        {
            int num = 1;
            try
            {

                foreach (var order1 in _context.Orders)
                {
                    if (order1.Id == orderId)
                    {
                        DarkGreen();
                        Console.WriteLine($"Sifariş {orderId}");
                        Cyan();
                        Console.WriteLine($"Tarix {order1.Date}");
                        Console.WriteLine($"Yekun qiymət {order1.TotalAmount}");

                        using (var _newContext = new RestaurantDB())
                        {
                            var orderItems = _newContext.OrderItems.Where(oi => oi.OrderId == order1.Id).ToList();
                            White();
                            Console.WriteLine("   Sifariş məhsulları");
                            foreach (var orderItem in orderItems)
                            {
                                using (var _newContext2 = new RestaurantDB())
                                {
                                    List<MenuItem> menuItems = _newContext2.MenuItems.ToList();

                                    var menuItem = menuItems.Find(mi => mi.Id == orderItem.MenuItemId);

                                    if (menuItem != null)
                                    {
                                        Console.WriteLine($"     \u001b[35mAd \u001b[36m{menuItem.Name} \u001b[35mSayı \u001b[36m{orderItem.Count} \u001b[35mQiyməti \u001b[36m{menuItem.Price}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Məhsul yoxdur!");
                                    }
                                }
                            }
                            Console.WriteLine();
                            White();
                            Console.WriteLine("----------------------------------------------------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
            }
        }
        #endregion


        #region Colors
        private void Green()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        private void DarkGreen()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        private void Cyan()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        private void White()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        private void Magenta()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        private void Red()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        private void Blue()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        } 
        #endregion
        private void ExceptionMessage(Exception ex)
        {
            Red();
            Console.WriteLine(ex.Message);
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.Clear();
        }

    }
}
