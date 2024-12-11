using System.Text;
using Restaurant.Service;
using Restaurant.Core.Enums;
namespace Restaurant.App
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Blue;
            string input = "";
            string input2 = "";
            Console.WriteLine("Salam hörmətli istifadəçi");
            var context = new Restaurant.DataAccess.Data.RestaurantDB();


            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1: Menu üzərində əməliyyat");
                Console.WriteLine("2: Sifarişlər üzərində əməliyyat");
                Console.WriteLine("0: Sistemdən çıxmaq");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        do
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
                            input2 = Console.ReadLine();

                            int id;
                            MenuServices menuServices = new MenuServices(context);
                            switch (input2)
                            {
                                case "1":


                                    MenuCase1(menuServices);

                                    break;
                                case "2":
                                    Console.WriteLine("Dəyişiklik etmək istədiyiniz itemin İD-sini qeyd edin!");
                                    id = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Nə düzəliş etmək istəyirsiniz?");
                                    Console.WriteLine("1.Ad");
                                    Console.WriteLine("2.Qiymət");
                                    string case2Input = Console.ReadLine();
                                    await menuServices.EditOnMenuItem(id, case2Input);
                                    break;
                                case "3":
                                    Console.WriteLine("Silmək istədiyiniz itemin İD-sini qeyd edin!");
                                    id = int.Parse(Console.ReadLine());
                                    await menuServices.RemoveMenuItem(id);
                                    break;
                                case "4":
                                    menuServices.ShowAllMenuItems();
                                    break;
                                case "5":
                                    Console.WriteLine("item lərini görmək istədiyiniz kateqoriyanı seçin!");
                                    Console.WriteLine("");
                                    Console.WriteLine("Kateqoriyalar");

                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("1.Salad");
                                    Console.WriteLine("2.Soup");
                                    Console.WriteLine("3.Kebab");
                                    Console.WriteLine("4.Sides");
                                    Console.WriteLine("5.Pizza");
                                    Console.WriteLine("6.Burger");
                                    Console.WriteLine("7.Azerbaijani_dish");
                                    Console.WriteLine("8.Drinks");
                                    Console.WriteLine("9.Desserts");
                                    int categoryNo= int.Parse(Console.ReadLine());
                                    menuServices.ShowAllMenuItemsByCategory(categoryNo);

                                    break;
                                case "6":
                                    break;
                                case "7":
                                    break;
                            }
                        } while (input2 != "0");

                        break;
                    case "2":
                        Console.WriteLine("1: Yeni sifariş əlavə et");
                        Console.WriteLine("2: Sifarişi ləğv et");
                        Console.WriteLine("3: Bütün sifarişləri göstər");
                        Console.WriteLine("4: Verilən tarix aralığına görə sifarişləri göstər");
                        Console.WriteLine("5: Verilən məbləğ aralığına görə sifarişləri göstər");
                        Console.WriteLine("6: Verilmiş bir tarixdə olan sifarişlərini göstər");
                        Console.WriteLine("7: Verilmiş nömrəyə görə sifarişin məlumatların göstər");
                        Console.WriteLine("0: Ana Menyuya qayıt");
                        Console.WriteLine("");
                        input2 = Console.ReadLine();


                        switch (input2)
                        {
                            case "1":
                                break;
                            case "2":
                                break;
                            case "3":
                                break;
                            case "4":
                                break;
                            case "5":
                                break;
                            case "6":
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



        }

        private static async void MenuCase1(MenuServices menuServices)
        {
            Console.WriteLine("Item adı: ");
            string name = Console.ReadLine();

            Console.WriteLine("Qiymət: ");
            float price = float.Parse(Console.ReadLine());

            Console.WriteLine("Kateqoriyalar");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1.Salad");
            Console.WriteLine("2.Soup");
            Console.WriteLine("3.Kebab");
            Console.WriteLine("4.Sides");
            Console.WriteLine("5.Pizza");
            Console.WriteLine("6.Burger");
            Console.WriteLine("7.Azerbaijani_dish");
            Console.WriteLine("8.Drinks");
            Console.WriteLine("9.Desserts");
            try
            {
                

                int inp = int.Parse(Console.ReadLine());
                menuServices.CreateMenuItem(name, price, inp);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}