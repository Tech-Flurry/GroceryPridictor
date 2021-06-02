using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryPridictor.Model;
namespace GroceryPridictor.Data
{
    public class DbInitializer
    {
        public static void Initialize(GroceryContext context)
        {
            try
            {
                context.Database.EnsureCreated();

                // Look for any students.
                if (context.User.Any())
                {
                    return;   // DB has been seeded
                }

                //User
                var users = new User[]
                {
            new User{  UserName = "Henry" , Email = "henry@gamil.com", Password= "1234" , Latitude="41234214" , Longitude ="3412341324", Phone="4234112", UserType ="Seller" },
            new User{ UserName = "James" , Email = "james@gamil.com", Password= "1234" , Latitude="41234214" , Longitude ="3412341324", Phone="4234112" ,UserType ="Customer"},
            new User{ UserName = "William" , Email = "william@gamil.com", Password= "1234" , Latitude="41234214" , Longitude ="3412341324", Phone="4234112" ,UserType ="Seller"},

                };
                foreach (User s in users)
                {
                    context.User.Add(s);
                }
                context.SaveChanges();

                //Product
                var products = new Product[]
                {
                new Product{Name="S10",Catagory="Phone",Price=123,Stock=123,StoreId=1,UserId= 1,Link="https://www.daraz.pk/products/samsung-s10-lite-8gb-ram-128gb-rom-4500-mah-battery-prism-white-i136130385-s1296426893.html?spm=a2a0e.searchlist.list.1.50bb5130LSO5Iz&search=1"},
                new Product{Name="S9",Catagory="Phone",Price=123,Stock=13,StoreId=1,UserId= 1,Link="https://www.amazon.com/Samsung-Galaxy-S9-Display-Resistance/dp/B07WVRJQ7V/ref=sr_1_2?crid=2BXW75J7J6OU&dchild=1&keywords=samsung+s9&qid=1622466509&sprefix=samsung+s9%2Caps%2C445&sr=8-2"},
                new Product{Name="Gtx1060",Catagory="GPU",Price=123,Stock=35,StoreId=2, UserId=2,Link="https://www.amazon.com/Asus-Phoenix-PH-GTX1060-3G-GeForce-Graphic/dp/B07VVMGP4G/ref=sr_1_10?dchild=1&keywords=gtx+1060&qid=1622466554&sr=8-10"},
                new Product{Name="Gtx1050ti",Catagory="GPU",Price=123,Stock=35,StoreId=2, UserId=2,Link="https://www.amazon.com/ASUS-Geforce-Phoenix-Graphics-PH-GTX1050TI-4G/dp/B01M360WG6/ref=sr_1_1?dchild=1&keywords=gtx+1050ti&qid=1622530878&sr=8-1"},
                new Product{Name="PS4",Catagory="Console",Price=123,Stock=35,StoreId=3, UserId=3,Link="https://www.amazon.com/PlayStation-Console-Light-System-Greatest-4/dp/B077QT6K94/ref=sr_1_2?dchild=1&keywords=ps4&qid=1622530950&sr=8-2"}

                };
                foreach (Product p in products)
                {
                    context.Product.Add(p);
                }
                context.SaveChanges();

                //Store
                var stores = new Store[]
                {
            new Store{StoreName="ExcelMart",StoreCategoryId=1,UserId=1,Region=1},
            new Store{StoreName="DMart",StoreCategoryId=1,UserId=2,Region=2},
            new Store{StoreName="NewMart",StoreCategoryId=1,UserId=3,Region=3},

                };
                foreach (Store e in stores)
                {
                    context.Store.Add(e);
                }
                context.SaveChanges();

                //StoreCategory
                var StoreCategory = new StoreCategory[]
                {
            new StoreCategory{Category="Electronics"},
            new StoreCategory{Category="Sanitory"},

                };
                foreach (StoreCategory e in StoreCategory)
                {
                    context.StoreCategory.Add(e);
                }
                context.SaveChanges();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } }

}
