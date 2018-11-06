using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using XmlProcessingExercise.App.Dto;
using XmlProcessingExercise.App.Dto.Export;
using XmlProcessingExercise.Data;
using XmlProcessingExercise.Models;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;


namespace XmlProcessingExercise.App
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var context = new XmlProcessingExerciseContext();
            //ReadUsersXml();
            //ReadProductsXml();
            //ReadCategoriesXml();
            //GenerateCategoriesProducts(new XmlProcessingExerciseContext());

            //ProductsInRange(context);
            //SoldProducts(context);
            CategoriesByProductCount(context);
        }

        public static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return result;
        }


        public static void ReadUsersXml()
        {
            var xmlString = File.ReadAllText("../../../xml/users.xml");

            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            var deserializer = (UserDto[]) serializer.Deserialize(new StringReader(xmlString));

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<XmlProcessingExerciseProfile>(); });
            var mapper = mapperConfig.CreateMapper();

            //var users = new List<User>();

            foreach (UserDto userDto in deserializer)
            {
                if (IsValid(userDto) == false)
                {
                    Console.WriteLine("Invalid user data");
                    continue;
                }

                var user = mapper.Map<User>(userDto);

                //users.Add(user);

                var context = new XmlProcessingExerciseContext();
                context.Add(user);
                context.SaveChanges();
            }

            Console.WriteLine("Users added successfully!");
        }

        public static void ReadProductsXml()
        {
            var xmlString = File.ReadAllText("../../../xml/products.xml");

            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            var deserializer = (ProductDto[]) serializer.Deserialize(new StringReader(xmlString));

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<XmlProcessingExerciseProfile>(); });
            var mapper = mapperConfig.CreateMapper();

            //var products = new List<User>();

            int counter = 0;
            foreach (ProductDto productDto in deserializer)
            {
                if (IsValid(productDto) == false)
                {
                    Console.WriteLine("Invalid product data");
                    continue;
                }

                var product = mapper.Map<Product>(productDto);

                int? buyerId = new Random().Next(1, 30);
                int sellerId = new Random().Next(31, 56);

                if (counter == 4)
                {
                    buyerId = null;
                    counter = 0;
                }

                counter++;

                product.BuyerId = buyerId;
                product.SellerId = sellerId;

                var context = new XmlProcessingExerciseContext();
                context.Add(product);
                context.SaveChanges();
            }

            Console.WriteLine("Products added successfully!");
        }

        public static void ReadCategoriesXml()
        {
            var xmlString = File.ReadAllText("../../../xml/categories.xml");

            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            var deserializer = (CategoryDto[]) serializer.Deserialize(new StringReader(xmlString));

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<XmlProcessingExerciseProfile>(); });
            var mapper = mapperConfig.CreateMapper();

            //var categories = new List<Category>();

            foreach (CategoryDto categoryDto in deserializer)
            {
                if (IsValid(categoryDto) == false)
                {
                    Console.WriteLine("Invalid category data");
                    continue;
                }

                var category = mapper.Map<Category>(categoryDto);

                var context = new XmlProcessingExerciseContext();
                context.Add(category);
                context.SaveChanges();
            }

            Console.WriteLine("Categories added successfully!");
        }

        public static void GenerateCategoriesProducts(XmlProcessingExerciseContext context)
        {
            int productsCount = context.Products.Count();
            int categoriesCount = context.Categories.Count();

            var categoryProducts = new List<CategoryProduct>();

            for (int productId = 1; productId <= productsCount; productId++)
            {
                var categoryId = new Random().Next(1, categoriesCount + 1);

                var categoryProduct = new CategoryProduct()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                };
                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
            Console.WriteLine("Category-Products generated succesfully");
        }

        public static void ProductsInRange(XmlProcessingExerciseContext context)
        {
            var result = context.Products
                .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.BuyerId != null)
                .OrderBy(p => p.Price)
                .Select(p => new ProductDto_P01
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName ?? p.Buyer.LastName
                })
                .ToArray();

            StringBuilder xmlString = new StringBuilder();
            string outputPath = "../../../output/products-in-range.xml";

            var xmlNamespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty,});
            var serializer = new XmlSerializer(typeof(ProductDto_P01[]), new XmlRootAttribute("products"));
            serializer.Serialize(new StringWriter(xmlString), result, xmlNamespaces);

            File.WriteAllText(outputPath, xmlString.ToString());
            Console.WriteLine("Data exported to xml succesfully!");
        }

        public static void SoldProducts(XmlProcessingExerciseContext context)
        {
            var result = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new UserDto_P02
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ProductsSold = u.ProductsSold.Select(p => new ProductDto_P02
                    {
                        Name = p.Name,
                        Price = p.Price
                    }).ToArray()
                }).ToArray();

            StringBuilder xmlString = new StringBuilder();
            string outputPath = "../../../output/users-sold-products.xml";

            var xmlNamespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty,});
            var serializer = new XmlSerializer(typeof(UserDto_P02[]), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(xmlString), result, xmlNamespaces);

            File.WriteAllText(outputPath, xmlString.ToString());
            Console.WriteLine("Data exported to xml succesfully!");
        }

        public static void CategoriesByProductCount(XmlProcessingExerciseContext context)
        {
            var result = context.Categories
                .OrderBy(c => c.CategoryProducts.Count)
                .Select(c => new CategoryDto_P03
                {
                    Name = c.Name,
                    ProductsCount = c.CategoryProducts.Count,
                    TotalRevenue = c.CategoryProducts.Sum(s => s.Product.Price),
                    AveragePrice = c.CategoryProducts
                        .Select(x => x.Product.Price).DefaultIfEmpty(0).Average()
                }).ToArray();


            StringBuilder xmlString = new StringBuilder();
            string outputPath = "../../../output/categories-by-products.xml";
            string xmlRoot = "categories";

            var xmlNamespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty,});
            var serializer = new XmlSerializer(typeof(CategoryDto_P03[]), new XmlRootAttribute(xmlRoot));
            serializer.Serialize(new StringWriter(xmlString), result, xmlNamespaces);

            File.WriteAllText(outputPath, xmlString.ToString());
            Console.WriteLine("Data exported to xml succesfully!");
        }

        public static void UsersAndProducts(XmlProcessingExerciseContext context)
        {
//            var result = context.Users
//                .Where(u => u.ProductsSold.Count > 0)
//                .OrderByDescending(u=>u.ProductsSold.Count)
//                .ThenBy(u=>u.LastName)
//                };

//            var users = new UsersDtoP04
//            {
//                Count = context.Users.Count(),
//                Users = context.Users.Select(u => new UserDtoP04
//                {
//                    FirstName = u.FirstName,
//                    LastName = u.LastName,
//                    Age = u.Age,
//                    SoldProducts = u.ProductsSold.Select(sp => new ProductsDtoP04
//                    {
//                        Count = u.ProductsSold.Count(),
//                        ProductDtoCollection = u.ProductsSold.Select(pdc => new ProductDtoP04
//                        {
//                            Name = pdc.Name,
//                            Price = pdc.Price
//                        }).ToArray()
//                    }).ToArray()
//
//                }).ToArray()
//            };
        }
    }
}