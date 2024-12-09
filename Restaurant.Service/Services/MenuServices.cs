using System;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task CreateMenuItem(string name, float price, Category category)
    {
        try
        {
           
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new InvalidNameException("Ad boş dəyər ola bilməz!");
                

            }

            if (price <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new InvalidSalaryException("Qiymət sıfır vəya sıfırdan kiçik ola bilməz!");
              
            }

           
            if (_context.MenuItems.Any(m => m.Name == name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new InvalidNameException("Eyni adlı item mövcuddur!");
                
            }

           
            var newMenuItem = new MenuItem
            {
                Name = name,
                Price = price,
                Category = category
            };
            _context.MenuItems.Add(newMenuItem);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uğurlu proses");
            Console.ForegroundColor = ConsoleColor.Blue;
            
            await _context.SaveChangesAsync();

           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }
}
