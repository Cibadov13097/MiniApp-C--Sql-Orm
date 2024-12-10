using System.Text;
using Restaurant.Service;
using Restaurant.Core.Enums;
namespace Restaurant.App
{
    public class Program
    {

        public static void Main(string[] args)
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
                Console.WriteLine("3: Sistemdən çıxmaq");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        do
                        {
                            Console.ForegroundColor= ConsoleColor.Blue;
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


                            MenuServices menuServices = new MenuServices(context);
                            switch (input2)
                            {
                                case "1":
                                    MenuCase1(menuServices);
                                    
                                    break;
                                case "2":
                                    Console.WriteLine("Nə düzəliş etmək istəyirsiniz?");
                                    Console.WriteLine("1.Ad");
                                    Console.WriteLine("2.Qiymət");
                                    string case2Input = Console.ReadLine();
                                    menuServices.EditOnMenuItem(1, case2Input);
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

        private static void MenuCase1(MenuServices menuServices)
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
            bool check = true;
            do
            {
                Console.WriteLine("Kateqoriyanı seçin (1-9): ");
                if (!int.TryParse(Console.ReadLine(), out int inp) || inp < 1 || inp > 9)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Zəhmət olmasa düzgün seçim edin (1-9) aralığında");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    
                }

                Category category = inp switch
                {
                    1 => Category.Salad,
                    2 => Category.Soup,
                    3 => Category.Kebab,
                    4 => Category.Sides,
                    5 => Category.Pizza,
                    6 => Category.Burger,
                    7 => Category.Azerbaijani_dish,
                    8 => Category.Drinks,
                    9 => Category.Desserts,
                    _ => throw new InvalidOperationException("Yanlış catalog seçimi")
                };

                menuServices.CreateMenuItem(name, price, category);
                 check = false;

            } while (check);

        }
    }
}