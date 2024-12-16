using Restaurant.Core.Models;
using Restaurant.DataAccess.Data;
using Restaurant.Service.Exceptions;
namespace Restaurant.App.Controllers;

public class MenuItemController
{
    private readonly RestaurantDB _context;
    private readonly MenuServices _menuServices;
    public MenuItemController(RestaurantDB context)
    {
        _context = context;
        _menuServices = new MenuServices(_context);

    }
    public async Task CreateMenuItemAsync()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Item adı: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string name = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Qiymət: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
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

            Console.ForegroundColor = ConsoleColor.Magenta;
            int inp = int.Parse(Console.ReadLine());
            await _menuServices.CreateMenuItemAsync(name, price, inp);

        }
        catch (Exception ex) { ExceptionMessage(ex); }

    }
    public async Task<int> EditMenuItemAsync()
    {

        int id;

    back:
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Məhsulların listini görmək istəyirsiniz? (yes/no)");
        Console.ForegroundColor = ConsoleColor.Magenta;

        string YesNo = Console.ReadLine();
        Console.WriteLine();

        switch (YesNo)
        {
            case "Yes":
            case "yes":
            case "YES":
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (MenuItem mitem in _context.MenuItems)
                {

                    Console.WriteLine($"{mitem.Id} --> {mitem.Name}");
                }
                Console.ForegroundColor = ConsoleColor.Blue;

                break;
            case "No":
            case "NO":
            case "no":
                break;
            default:
                Console.WriteLine("Yes vəya Yox seçin!");
                goto back;
                break;



        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Dəyişiklik etmək istədiyiniz itemin İD-sini qeyd edin!");
        Console.ForegroundColor = ConsoleColor.Magenta;
        id = int.Parse(Console.ReadLine());

        MenuItem menu = _context.MenuItems.Find(id);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("");
        Console.WriteLine($"\u001b[32m{menu.Name} \u001b[37madlı item üzərində dəyişiklik edirsiniz!!! ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Nə düzəliş etmək istəyirsiniz?");
        Console.WriteLine("1.Ad");
        Console.WriteLine("2.Qiymət");
        Console.ForegroundColor = ConsoleColor.Magenta;
        string case2Input = Console.ReadLine();
        await _menuServices.EditOnMenuItemAsync(id, case2Input);


        return id;
    }
    public async Task<int> RemoveMenuItemAsync()
    {
        try
        {
            int id;
            Console.WriteLine("Silmək istədiyiniz itemin İD-sini qeyd edin!");
            Console.ForegroundColor = ConsoleColor.Magenta;
            id = int.Parse(Console.ReadLine());
            await _menuServices.RemoveMenuItemAsync(id);

            return id;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex);
            return 0;
        }

    }


    public async Task GetAllMenuItemsAsync()
    {
        try
        {

            int cnt = 1;
            foreach (var item in await _menuServices.GetAllMenuItemsAsync())
            {
                cnt = await PrintMenuItemAttributes(cnt, item);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex);
        }

    }

    public async Task GetMenuItemsByCategoryAsync()
    {
        try
        {
            int cnt = 1;
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
            int categoryNo = int.Parse(Console.ReadLine());

            foreach (var menuItem in await _menuServices.GetMenuItemsByCategoryAsync(categoryNo))
            {
                cnt = await PrintMenuItemAttributes(cnt, menuItem);
            }
        }
        catch (Exception ex)
        { ExceptionMessage(ex); }
    }
        
    
    public async Task GetMenuItemsByPriceAsync()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Zəhmət olmasa max və min qiymətləri qeyd edin!");
            Console.Write("Max dəyər: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            float max = float.Parse(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Min dəyər: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            float min = float.Parse(Console.ReadLine());
            int cnt = 1;
            foreach (var item in await _menuServices.GetMenuItemsByPriceAsync(min, max))
            {
                cnt = await PrintMenuItemAttributes(cnt, item);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            if (cnt == 1) throw new Exception("Bu qiymət aralığında İtem yoxdur");
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex);
        }

    }
    public async Task SearchByNameAsync()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Axtarış etmək istədiyiniz adı vəya teksti yazın: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            int cnt = 1;
            foreach (var item in await _menuServices.SearchByNameAsync(Console.ReadLine()))
            {
                cnt = await PrintMenuItemAttributes(cnt, item);
            };
            Console.ForegroundColor = ConsoleColor.Red;
            if (cnt == 1) throw new DoesNotExistException("Heçbir Məhsul tapılmadı!");
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex);
        }

    }

    //subMethods
    private void ExceptionMessage(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        for (int i = 0; i < 4; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.Clear();
    }

    private async Task<int> PrintMenuItemAttributes(int cnt, MenuItem menuItem)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("");
        Console.WriteLine($"ITEM {cnt++}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        MenuItem menu = await _context.MenuItems.FindAsync(menuItem.Id);

        Console.WriteLine($"ID:{menu.Id}");
        Console.WriteLine($"Ad:{menu.Name}");
        Console.WriteLine($"Qiymət:{menu.Price}");
        Console.WriteLine($"Kateqoriya:{menu.Category}");
        Console.WriteLine("");
        return cnt;
    }
}
