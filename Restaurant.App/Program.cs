using System.Text;
using Restaurant.Service.Services;
using Restaurant.App.Controllers;
namespace Restaurant.App
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            try
            {

                Console.OutputEncoding = Encoding.UTF8;
                Console.ForegroundColor = ConsoleColor.Blue;
                string input = "";
                string input2 = "";
                Console.WriteLine("Salam hörmətli istifadəçi");
                var context = new Restaurant.DataAccess.Data.RestaurantDB();
                OrderServices orderServices = new OrderServices(context);
                OrderController orderController = new OrderController(context);
                MenuItemController menuController = new MenuItemController(context);
                do
                {
                    MainMenu();
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            do
                            {
                                MenuForMenuItem();
                                input2 = Console.ReadLine();
                                int id;
                                MenuServices menuServices = new MenuServices(context);
                                switch (input2)
                                {
                                    case "1":
                                        menuController.CreateMenuItemAsync();
                                        break;
                                    case "2":
                                        id = await menuController.EditMenuItemAsync();
                                        break;
                                    case "3":
                                        id = await menuController.RemoveMenuItemAsync();
                                        break;
                                    case "4":
                                        menuController.GetAllMenuItemsAsync();
                                        break;
                                    case "5":
                                        menuController.GetMenuItemsByCategoryAsync();
                                        break;
                                    case "6":
                                        menuController.GetMenuItemsByPriceAsync();
                                        break;
                                    case "7":
                                        menuController.SearchByNameAsync();
                                        break;
                                }
                            } while (input2 != "0");
                            break;
                        case "2":
                            MenuForOrderItem();
                            input2 = Console.ReadLine();


                            switch (input2)
                            {
                                case "1":
                                    await orderController.CreateOrderAsync();
                                    break;
                                case "2":
                                    await orderController.RemoveOrderAsync();
                                    break;
                                case "3":
                                    await orderController.ShowAllOrdersAsync();
                                    break;
                                case "4":
                                    await orderController.ShowOrdersByTimeIntervalAsync();
                                    break;
                                case "5":
                                    await orderController.ShowOrdersByPriceIntervalAsync();
                                    break;
                                case "6":
                                    await orderController.ShowOrderByDateAsync();
                                    break;
                                case "7":

                                    break;
                            }

                            break;
                        case "0":

                            break;

                    }
                }
                while (input != "0");
            }catch(Exception ex)
            {
                ExceptionMessage(ex);
            }
        }

        private static void MenuForOrderItem()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1: Yeni sifariş əlavə et");
            Console.WriteLine("2: Sifarişi ləğv et");
            Console.WriteLine("3: Bütün sifarişləri göstər");
            Console.WriteLine("4: Verilən tarix aralığına görə sifarişləri göstər");
            Console.WriteLine("5: Verilən məbləğ aralığına görə sifarişləri göstər");
            Console.WriteLine("6: Verilmiş bir tarixdə olan sifarişlərini göstər");
            Console.WriteLine("7: Verilmiş nömrəyə görə sifarişin məlumatların göstər");
            Console.WriteLine("0: Ana Menyuya qayıt");
            Console.WriteLine("");
        }
        private static void MenuForMenuItem()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1: Yeni item əlavə et");
            Console.WriteLine("2: Item üzərində düzəliş et");
            Console.WriteLine("3: Item Sil");
            Console.WriteLine("4: Bütün itemləri göstər");
            Console.WriteLine("5: Kategoriyaya görə itemləri göstər");
            Console.WriteLine("6: Qiymət aralığına görə menu itemları göstər");
            Console.WriteLine("7: Menu Itemlər ada görə axtarış et");
            Console.WriteLine("0: Ana Menyuya qayıt");
            Console.WriteLine("");
        }
        private static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1: Menu üzərində əməliyyat");
            Console.WriteLine("2: Sifarişlər üzərində əməliyyat");
            Console.WriteLine("0: Sistemdən çıxmaq");
        }

        private static void ExceptionMessage(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.Clear();
        }




    }
}