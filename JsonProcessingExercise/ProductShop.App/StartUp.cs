using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProductShop.App
{
    using AutoMapper;
    using Data;
    using Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<ProductShopProfile>(); });
            var mapper = config.CreateMapper();

            var context = new ProductShopContext();

            //ImportUsers(context);
            //ImportProducts(context);
            //ImportCategories(context);
            //GenerateCategoryProducts(context);

            //ProductsInRange(context);
            //SuccessfullySoldProducts(context);
            //CategoriesByProductCount(context);
            //UsersAndProducts(context);
        }

        public static void ImportUsers(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/users.json");
            var deserializer = JsonConvert.DeserializeObject<User[]>(jsonString);


            List<User> users = new List<User>();

            foreach (User user in deserializer)
            {
                if (IsValid(user) == false)
                {
                    Console.WriteLine("Invalid user data");
                    continue;
                }

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();
            Console.WriteLine("users added sucessfully");
        }

        public static void ImportProducts(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../Json/products.json");
            var deserializer = JsonConvert.DeserializeObject<Product[]>(jsonString);

            List<Product> products = new List<Product>();
            foreach (Product product in deserializer)
            {
                if (IsValid(product) == false)
                {
                    Console.WriteLine("Invalid product data");
                    continue;
                }

                int usersCount = context.Users.Count();
                int sellerId = new Random().Next(1, usersCount / 2);
                int buyerId = new Random().Next(usersCount / 2, usersCount + 1);
                int randomNum = new Random().Next(1, 5);

                product.SellerId = sellerId;
                product.BuyerId = buyerId;


                if (randomNum == 2)
                {
                    product.BuyerId = null;
                }

                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();
            Console.WriteLine("Products added succesfully");
        }

        public static void ImportCategories(ProductShopContext context)
        {
            var jsonString = File.ReadAllText("../../../json/categories.json");
            var desiarlizer = JsonConvert.DeserializeObject<Category[]>(jsonString);

            List<Category> categories = new List<Category>();
            foreach (Category category in desiarlizer)
            {
                if (IsValid(category) == false)
                {
                    Console.WriteLine("Invalid category data");
                    continue;
                }

                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
            Console.WriteLine("Categories imported succesfully");
        }

        public static void GenerateCategoryProducts(ProductShopContext context)
        {
            int categoriesCount = context.Categories.Count();

            int productsCounts = context.Products.Count();

            List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
            for (int productId = 1; productId <= productsCounts; productId++)
            {
                int categoryid = new Random().Next(1, categoriesCount + 1);

                var categoryProduct = new CategoryProduct
                {
                    CategoryId = categoryid,
                    ProductId = productId
                };
                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
            Console.WriteLine("Category-Products generated succesfully");
        }

        public static void ProductsInRange(ProductShopContext context)
        {
            var result = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName}" ?? $"{p.Seller.LastName}"
                }).ToArray();

            var jsonSerializer = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText("../../../output/products-in-range.json", jsonSerializer);
        }

        public static void SuccessfullySoldProducts(ProductShopContext context)
        {
            var result = context.Users
                .Where(u => u.ProductsSold.Count > 0 && u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Where(p => p.BuyerId != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName,
                        }).ToArray()
                }).ToArray();

            var jsonSerializer = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("../../../output/users-sold-products.json", jsonSerializer);
        }

        public static void CategoriesByProductCount(ProductShopContext context)
        {
            var result = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    totalRevenue = c.CategoryProducts.Sum(p => p.Product.Price),
                    averagePrice = c.CategoryProducts.Select(p => p.Product.Price).DefaultIfEmpty(0).Average()
                }).ToArray();

            var jsonSerializer = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("../../../output/categories-by-products.json", jsonSerializer);
        }

        public static void UsersAndProducts(ProductShopContext context)
        {
            int usersCout = context.Users.Count();
            var result = context.Users
                .Where(u => u.ProductsSold.Count > 0 && u.ProductsSold.Any(p => p.Buyer != null))
                .OrderByDescending(u => u.ProductsSold.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Count,
                        products = u.ProductsSold.Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        }).ToArray()
                    }
                }).ToArray();

            var jsonSerializer = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("../../../output/users-and-products.json", jsonSerializer);
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}