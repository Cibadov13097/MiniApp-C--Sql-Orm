using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Enums;
using Restaurant.Core.Models;
using Restaurant.DataAccess.Data;
using Restaurant.Service.Exceptions;

public class MenuServices
{
    private readonly RestaurantDB _context;
    public MenuServices(RestaurantDB context)
    {
        _context = context;
    }

    public async Task CreateMenuItem(string name, float price, int categoryNo)
    {
        try
        {
            SalaryCheck(price);
            NameCheck(name);

            Category category = CategoryCatalogSelection(categoryNo);

            var newMenuItem = new MenuItem
            {
                Name = name,
                Price = price,
                Category = category
            };
            _context.MenuItems.Add(newMenuItem);






        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        SuccessfullMessage();
        await _context.SaveChangesAsync();
    }



    public async Task EditOnMenuItem(int id, string input)
    {

        MenuItem menuItem = await _context.MenuItems.FindAsync(id);
        try
        {

            if (menuItem != null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("");
                Console.WriteLine($"{menuItem.Name} adlı item üzərində dəyişiklik edirsiniz!!! ");

                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else throw new DoesNotExistException("Bu Id-li Item mövcud deyil!");




            switch (input)
            {

                case "1":
                    Console.WriteLine("");
                    Console.WriteLine("İtemin yeni adını təyin edin!");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    string newName = Console.ReadLine();
                    NameCheck(newName);
                    menuItem.Name = newName;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                case "2":
                    Console.WriteLine("");
                    Console.WriteLine("İtemin yeni qiymətini təyin edin!");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    float newPrice = float.Parse(Console.ReadLine());
                    SalaryCheck(newPrice);
                    menuItem.Price = newPrice;
                    break;
                default:
                    Console.WriteLine("");
                    Console.WriteLine("Yanlış seçim 1 vəya 2 seçməlisiniz!");

                    return;


            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            return;
        }
        SuccessfullMessage();

        await _context.SaveChangesAsync();
    }
    public async Task RemoveMenuItem(int id)
    {
        try
        {
            MenuItem menuItem = _context.MenuItems.Find(id);
            if (menuItem != null)
            {
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{menuItem.Name} adlı İtemi silmək istəyirsiniz? (Yes/No)");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string YesNo = Console.ReadLine();
                    switch (YesNo)
                    {
                        case "Yes":
                        case "yes":
                            _context.MenuItems.Remove(menuItem);
                            break;
                        case "No":
                        case "no":
                            return;
                        default:
                            Console.WriteLine("Yanlış seçim etdiniz! Zəhmət olmasa, yalnız \"Yes\" və ya \"No\" daxil edin.\r\n");
                            break;
                    }
                    SuccessfullMessage();
                    await _context.SaveChangesAsync();

                }
            }
            else throw new DoesNotExistException("Bu Id-li Item mövcud deyil!");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
    public async Task ShowAllMenuItems()
    {
        try
        {
            int cnt = 1;
            foreach (MenuItem menuItem in _context.MenuItems)
            {

                
                Console.WriteLine("");
                Console.WriteLine($"ITEM {cnt++}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"ID:{_context.MenuItems.Find(menuItem.Id).Id}");
                Console.WriteLine($"Ad:{_context.MenuItems.Find(menuItem.Id).Name}");
                Console.WriteLine($"Qiymət:{_context.MenuItems.Find(menuItem.Id).Price}");
                Console.WriteLine($"Kateqoriya:{_context.MenuItems.Find(menuItem.Id).Category}");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Blue;


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public async Task ShowAllMenuItemsByCategory(int categoryNo)
    {

        try
        {
            Category category = CategoryCatalogSelection(categoryNo);

            int cnt = 1;
            foreach (var menuItem in _context.MenuItems)
            {

                if (_context.MenuItems.Find(menuItem.Id).Category == category)
                {


                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("");
                    Console.WriteLine($"ITEM {cnt++}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID:{_context.MenuItems.Find(menuItem.Id).Id}");
                    Console.WriteLine($"Ad:{_context.MenuItems.Find(menuItem.Id).Name}");
                    Console.WriteLine($"Qiymət:{_context.MenuItems.Find(menuItem.Id).Price}");
                    Console.WriteLine($"Kateqoriya:{_context.MenuItems.Find(menuItem.Id).Category}");
                    Console.WriteLine("");
                   
                   

                }


            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }

    //subMethods
    private void NameCheck(string name)
    {
        if (_context.MenuItems.Any(m => m.Name == name) || string.IsNullOrWhiteSpace(name))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            throw new InvalidNameException("Eyni adlı item yaradıla bilməz vəya boş dəyər ola bilməz!");

        }
    }

    private static void SalaryCheck(float price)
    {
        if (price <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            throw new InvalidSalaryException("Qiymət sıfır vəya sıfırdan kiçik ola bilməz!");

        }
    }

    private static void SuccessfullMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Uğurlu proses");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Blue;
    }
    private static Category CategoryCatalogSelection(int categoryNo)
    {
        return categoryNo switch
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
    }
}


