using Restaurant.DataAccess.Data;
namespace Restaurant.App.Controllers;

public class MenuItemController
{
    private readonly MenuServices _menuServices;
    public MenuItemController(RestaurantDB context)
    {
        _menuServices = new MenuServices(context);
    }
    public async Task CreateMenuItemAsync()
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
            Console.ForegroundColor = ConsoleColor.Magenta;
            int inp = int.Parse(Console.ReadLine());
            await _menuServices.CreateMenuItemAsync(name, price, inp);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
    public async Task<int> EditMenuItemAsync()
    {
        int id;
        Console.WriteLine("Dəyişiklik etmək istədiyiniz itemin İD-sini qeyd edin!");
        id = int.Parse(Console.ReadLine());
        Console.WriteLine("Nə düzəliş etmək istəyirsiniz?");
        Console.WriteLine("1.Ad");
        Console.WriteLine("2.Qiymət");
        Console.ForegroundColor = ConsoleColor.Magenta;
        string case2Input = Console.ReadLine();
        await _menuServices.EditOnMenuItemAsync(id, case2Input);
        System.Threading.Thread.Sleep(2000);

        return id;
    }
    public async Task<int> RemoveMenuItemAsync()
    {
        int id;
        Console.WriteLine("Silmək istədiyiniz itemin İD-sini qeyd edin!");
        Console.ForegroundColor = ConsoleColor.Magenta;
        id = int.Parse(Console.ReadLine());
        await _menuServices.RemoveMenuItemAsync(id);

        return id;
    }
    public async Task ShowAllMenuItemsAsync()
    {
        _menuServices.ShowAllMenuItemsAsync();
    }
    public async Task ShowMenuItemsByCategoryAsync()
    {
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
        await _menuServices.ShowMenuItemsByCategoryAsync(categoryNo);
    }
    public async Task ShowMenuItemsByPriceAsync()
    {
        Console.WriteLine("Zəhmət olmasa max və min qiymətləri qeyd edin!");
        Console.Write("Max dəyər: ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        float max = float.Parse(Console.ReadLine());
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Min dəyər: ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        float min = float.Parse(Console.ReadLine());

        await _menuServices.ShowMenuItemsByPriceAsync(min, max);
    }
    public async Task SearchByNameAsync()
    {
        Console.Write("Axtarış etmək istədiyiniz adı vəya teksti yazın: ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        await _menuServices.SearchByNameAsync(Console.ReadLine());
    }
}
