﻿using Microsoft.IdentityModel.Tokens;
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

    #region Create,Edit,Remove Methods
    public async Task CreateMenuItemAsync(string name, float price, int categoryNo)
    {
        try
        {
            PriceCheck(price);
            NameCheck(name);

            Category category = CategoryCatalogSelection(categoryNo);

            var newMenuItem = new MenuItem
            {
                Name = name,
                Price = price,
                Category = category
            };
            await _context.MenuItems.AddAsync(newMenuItem);




            SuccessfullMessage();
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex);
            throw;
        }

    }
    public async Task EditOnMenuItemAsync(int id, string input)
    {

        MenuItem menuItem = await _context.MenuItems.FindAsync(id);
        try
        {

            if (menuItem != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");


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
                    PriceCheck(newPrice);
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
            ExceptionMessage(ex);
            return;
        }
        SuccessfullMessage();

        await _context.SaveChangesAsync();
    }
    public async Task RemoveMenuItemAsync(int id)
    {
        try
        {
            MenuItem menuItem = await _context.MenuItems.FindAsync(id);
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
        catch (Exception ex) { ExceptionMessage(ex); }
    }
    #endregion

    #region Show Methods
    public async Task<List<MenuItem>> GetAllMenuItemsAsync()
    {
        List<MenuItem> menuItemsList = new List<MenuItem>();

        try
        {
            foreach (MenuItem menuItem in _context.MenuItems)
            {
                menuItemsList.Add(menuItem);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex);
        }

        return menuItemsList;
    }

    public async Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int categoryNo)
    {
        List<MenuItem> menuItems = new List<MenuItem>();
        try
        {
            Category category = CategoryCatalogSelection(categoryNo);
            

            int cnt = 1;
            foreach (var menuItem in _context.MenuItems)
            {

                if (_context.MenuItems.Find(menuItem.Id).Category == category)
                {
                    menuItems.Add(menuItem);
                }


            }
            Console.ForegroundColor = ConsoleColor.Red;
            if (cnt == 1) Console.WriteLine("Bu kataloqa uyğun məhsul yoxdur");
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex);
        }

        return menuItems;

    }



    public async Task<List<MenuItem>> GetMenuItemsByPriceAsync(float min, float max)
    {
        List<MenuItem> menuItems = new List<MenuItem>();
        try
        {
            int cnt = 1;

            if (max <= min) throw new InvalidSalaryException("maximum qiymət minimum qiymətdən böyük olmalıdır");
            foreach (var menuItem in _context.MenuItems)
            {

                if (menuItem.Price > min && menuItem.Price < max)
                {
                    menuItems.Add(menuItem);

                }
            }
            
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex);
        }
        return menuItems;

    } 
    #endregion  
    public async Task<List<MenuItem>> SearchByNameAsync(string text)
    {
        List<MenuItem> menuItems = new List<MenuItem>();
        try
        {
            int cnt = 1;
            foreach (var menuItem in _context.MenuItems)
            {
                if (menuItem.Name.Contains(text) && !text.IsNullOrEmpty())
                {
                    menuItems.Add(menuItem);
                }
            }
            
        }
        catch (Exception ex)
        {

            ExceptionMessage(ex);
        }
        return menuItems;
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
    private void SuccessfullMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Uğurlu proses");
        Console.ForegroundColor = ConsoleColor.Blue;


        for (int i = 0; i < 4; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.Clear();
    }
    private void NameCheck(string name)
    {
        
            if (_context.MenuItems.Any(m => m.Name == name) || string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new InvalidNameException("Eyni adlı item yaradıla bilməz vəya boş dəyər ola bilməz!");

            }
    
    }
    private void PriceCheck(float price)
    {


        
            if (price <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new InvalidSalaryException("Qiymət sıfır vəya sıfırdan kiçik ola bilməz!");

            }
        
        
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

