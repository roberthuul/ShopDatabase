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

                        newCart.addToCart(item);

                    }
                    b++;
                }
                

            }

            
            

            using (var db = new ShopDbContext())
            {
                
                /*
                var cartsWithZeroSum = db.ShoppingCarts.Where(x => x.Sum == 0);

                foreach (var item in cartsWithZeroSum)
                {
                    db.ShoppingCarts.Remove(item);
                }
                db.SaveChanges();

                db.ShoppingCarts.Add(newCart);
                db.SaveChanges();

                var carts = db.ShoppingCarts.Include("Items").OrderByDescending(x => x.DateCreated).ToList();
                */
                var sumFive = db.ShoppingCarts.Where(x => x.Sum > 4.5);
                Console.WriteLine(sumFive);

                foreach (var item in sumFive)
                {
                    foreach (var food in item.Items)
                    {
                        Console.WriteLine(food.Name);
                    }
                }
                db.SaveChanges();

                /*
                foreach (var item in carts)
                {
                    
                    Console.WriteLine($"Ostukorv loodi: {item.DateCreated}");
                    foreach (var food in item.Items)
                    {
                        Console.WriteLine($"Nimi: {food.Name} Hind: {food.Price}");
                    }
                    Console.WriteLine("Summa: " + item.Sum);
                }*/
            }
            Console.ReadKey();
        }
    }
}
