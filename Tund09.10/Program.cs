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
                db.ShoppingCarts.Add(newCart);
                db.SaveChanges();
                
                var cartsWithZeroSum = db.ShoppingCarts.Where(x => x.Sum == 0);

                foreach (var item in cartsWithZeroSum)
                {
                    db.ShoppingCarts.Remove(item);
                }
                db.SaveChanges();
                
                

                var carts = db.ShoppingCarts.Include("Items").OrderByDescending(x => x.DateCreated).ToList();
                
                var lastItem = db.ShoppingCarts.OrderByDescending(x => x.DateCreated).First();
                Console.WriteLine($"Viimase ostukorvi summa: {lastItem.Sum}, loomise kuupäev: {lastItem.DateCreated}\nSisu:");
                foreach (var item in lastItem.Items)
                {
                    Console.WriteLine(item.Name);
                }
                
                var sumFive = db.ShoppingCarts.Where(x => x.Sum > 5);
               

                foreach (var item in sumFive)
                {
                    Console.WriteLine($"Kuupäev: {item.DateCreated}, summa: {item.Sum}");
                }
                

                var moreThanOne = db.ShoppingCarts.Where(x => x.Items.Count > 1);

                foreach (var item in moreThanOne)
                {
                    Console.WriteLine($"Ostukorv loodi: {item.DateCreated}, seal on {item.Items.Count} asja");
                }
                
                var containsApples = db.ShoppingCarts.Where(x => x.Items.Any(y => y.Name == "õun"));

                foreach (var item in containsApples)
                {
                    Console.WriteLine($"Ostukorv loodi: {item.DateCreated}, tooted:");
                    foreach (var goods in item.Items)
                    {
                        Console.WriteLine(goods.Name);
                    }
                }
                

                var totalNumber = db.ShoppingCarts.Count();
                Console.WriteLine("Ostukorve kokku: " + totalNumber);
                

                var maxSum = db.ShoppingCarts.OrderByDescending(x => x.Sum).First();
                Console.WriteLine("Suurima summaga ostukorv: " + maxSum.Sum);
                

                var cheapestFood = db.Foods.OrderByDescending(x => x.Price).ToList().Last();
                Console.WriteLine("Kõige odavam toit on " + cheapestFood.Name);
                
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
