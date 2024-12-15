using Restaurant.Service.Services;

namespace Restaurant.App
{
    public class OrderController
    {
        private readonly OrderServices _orderServices;

        public OrderController(Restaurant.DataAccess.Data.RestaurantDB context)
        {
            _orderServices = new OrderServices(context);
        }
        public async Task CreateOrderAsync()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            await _orderServices.CreateOrderAsync();
        }
        public async Task RemoveOrderAsync()
        {
            Console.WriteLine("Silmək istədiyiniz Sifarişin nömrəsini daxil edin: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            await _orderServices.RemoveOrderAsync(int.Parse(Console.ReadLine()));
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        public async Task ShowAllOrdersAsync()
        {
            await _orderServices.ShowAllOrdersAsync();
        }
        public async Task ShowOrdersByTimeIntervalAsync()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("ilk tarix (dd/MM/yyyy): ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string beginningTimestr = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("son tarix (dd/MM/yyyy): ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string endingTimestr = Console.ReadLine();

            if (DateTime.TryParseExact(beginningTimestr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime beginningTime) && (DateTime.TryParseExact(endingTimestr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime endingTime)))
            {
                await _orderServices.ShowOrdersByTimeIntervalAsync(beginningTime, endingTime);
            }
        }
        public async Task ShowOrdersByPriceIntervalAsync()
        {
            Console.WriteLine("Minimum qiymət");
            Console.ForegroundColor = ConsoleColor.Magenta;
            float minPrice = float.Parse(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Maximum qiymət");
            Console.ForegroundColor = ConsoleColor.Magenta;
            float maxPrice = float.Parse(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.Blue;
            _orderServices.ShowOrderByTotalPriceAsync(minPrice, maxPrice);
        }
        public async Task ShowOrderByDateAsync()
        {
            Console.Write("Tarixi daxil edin (dd/MM/yyyy): ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string datestr = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            if (DateTime.TryParseExact(datestr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {

                _orderServices.ShowOrderByDateAsync(date);
            }
        }
        public async Task ShowOrderByIdAsync()
        {
            Console.WriteLine("Sifarişin nömrəsini daxil edin!");
            Console.ForegroundColor = ConsoleColor.Magenta;
            _orderServices.ShowOrderByIdAsync(int.Parse(Console.ReadLine()));
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }
}
