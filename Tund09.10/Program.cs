using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tund09._10.Models;

namespace Tund09._10
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Food> groceries = new List<Food>
            {
                new Food("õun", 1.7),
                new Food("leib", 1.2),
                new Food("sai", 1.4),
                new Food("juust", 2)
            };

            ShoppingCart newCart = new ShoppingCart();

            while (true)
            {
                Console.WriteLine("Vali toit ostukorvi lisamiseks");
                int a = 1;
                foreach (var item in groceries)
                {
                    Console.WriteLine($"{a} - {item.Name}");
                    a++;
                }
                Console.WriteLine($"{a} - lõpeta sisestamine");
                int input = int.Parse(Console.ReadLine());
                if (input == a)
                {
                    break;
                }
                int b = 1;
                foreach (var item in groceries)
                {
                    if (input == b)
                    {

                        newCart.Sum = newCart.Sum + item.Price; 
                        newCart.Items.Add(item);

                    }
                    b++;
                }
                

            }
            
            

            using (var db = new ShopDbContext())
            {
                db.ShoppingCarts.Add(newCart);
                db.SaveChanges();

                var carts = db.ShoppingCarts;
                foreach (var item in carts)
                {
                    
                    Console.WriteLine($"Ostukorv loodi: {item.DateCreated}");
                    foreach (var food in item.Items)
                    {
                        Console.WriteLine($"Nimi: {food.Name} Hind: {food.Price}");
                    }
                    Console.WriteLine("Summa: " + item.Sum);
                }
            }
            Console.ReadKey();
        }
    }
}
