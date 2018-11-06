using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //                DbInitializer.ResetDatabase(context);

                //string command = Console.ReadLine();
                //Console.WriteLine(GetBooksByAgeRestriction(db, command));


                //Console.WriteLine(GetBooksByAuthor(db, Console.ReadLine()));

                //Console.WriteLine(CountBooks(db, int.Parse(Console.ReadLine())));

                Console.WriteLine(RemoveBooks(db));
                
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToArray();

            context.RemoveRange(books);
            context.SaveChanges();

            return books.Length;
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = (AgeRestriction) Enum.Parse(typeof(AgeRestriction), command, true);

            var books = context.Books
                .Where(x => x.AgeRestriction == ageRestriction)
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToArray();


            return string.Join(Environment.NewLine, books);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold)
                .Where(b => b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:f2}")
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categoriesInput = input.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();

            var books = context.Books
                .Where(b => b.BookCategories.Any(c => categoriesInput.Contains(c.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime inputDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate.Value < inputDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(x => new
                {
                    x.Title,
                    x.EditionType,
                    x.Price
                })
                .ToArray();

            var stringBuilder = new StringBuilder();

            foreach (var bookObj in books)
            {
                stringBuilder.AppendLine($"{bookObj.Title} - {bookObj.EditionType} - ${bookObj.Price:f2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => EF.Functions.Like(a.FirstName, "%" + input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a => a.FullName)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FullName}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => EF.Functions.Like(b.Title.ToLower(), $"%{input.ToLower()}%"))
                .Select(b => new
                {
                    id = b.BookId,
                    Title = b.Title,
                    Author = b.Author.FirstName + " " + b.Author.LastName
                })
                .OrderBy(b => b.Title)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => EF.Functions.Like(b.Author.LastName.ToLower(), $"{input.ToLower()}%"))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    Title = b.Title,
                    Author = b.Author.FirstName + " " + b.Author.LastName
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.Author})");
            }

            return sb.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int booksCount = context.Books
                .Count(b => b.Title.Length > lengthCheck);

            return booksCount;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var result = context.Authors
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    booksCount = x.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(x => x.booksCount)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var resultObj in result)
            {
                sb.AppendLine($"{resultObj.FirstName} {resultObj.LastName} - {resultObj.booksCount}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var result = context.Categories
                .Select(x => new
                {
                    name = x.Name,
                    profits = x.CategoryBooks.Sum(bc => bc.Book.Price * bc.Book.Copies)
                })
                .OrderByDescending(x => x.profits).ThenBy(x => x.name)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var obj in result)
            {
                sb.AppendLine($"{obj.name} ${obj.profits}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var result = context.Categories
                .Select(bc => new
                    {
                        CategoryName = bc.Name,
                        CategoryBooks = bc.CategoryBooks
                            .OrderByDescending(cb => cb.Book.ReleaseDate)
                            .Select(b => new
                            {
                                BookTitle = b.Book.Title,
                                BookDate = b.Book.ReleaseDate.Value.Year
                            })
                            .OrderByDescending(b => b.BookDate)
                            .Take(3)
                    }
                )
                .OrderBy(x => x.CategoryName);

            StringBuilder sb = new StringBuilder();

            foreach (var bookCat in result)
            {
                sb.AppendLine($"--{bookCat.CategoryName}");
                foreach (var book in bookCat.CategoryBooks)
                {
                    sb.AppendLine($"{book.BookTitle} ({book.BookDate})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var booksBefore2010 = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);
            foreach (Book book in booksBefore2010)
            {
                book.Price += 5m;
            }

            context.SaveChanges();
        }
    }
}