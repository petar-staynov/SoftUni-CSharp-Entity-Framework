using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Models.Enums;
using P01_BillsPaymentSystem.Initializer;

namespace P01_BillsPaymentSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new BillsPaymentSystemContext())
            {
//                context.Database.EnsureDeleted();
//                context.Database.EnsureCreated();
//                Initialize.Seed(context);

                User user = GetUser(context);
                GetInfo(user);

                PayBills(user, 300);
            }
        }

        private static void GetInfo(User user)
        {
            if (user == null)
            {
                return;
            }

            Console.WriteLine($"User: {user.FirstName} {user.LastName}");
            Console.WriteLine($"Bank Accounts:");

            var bankAccounts = user.PaymentMethods.Where(pm => pm.BankAccount != null).Select(pm => pm.BankAccount)
                .ToArray();

            foreach (var bankAccount in bankAccounts)
            {
                Console.WriteLine($"-- ID: {bankAccount.BankAccountId}");
                Console.WriteLine($"--- Balance: {bankAccount.Balance:f2}");
                Console.WriteLine($"--- SWIFT: {bankAccount.SwiftCode}");
            }

            Console.WriteLine("Credit Cards:");
            var creditCards = user.PaymentMethods.Where(pm => pm.CreditCard != null).Select(pm => pm.CreditCard)
                .ToArray();

            foreach (CreditCard creditCard in creditCards)
            {
                Console.WriteLine($"-- ID: {creditCard.CreditCardId}");
                Console.WriteLine($"--- Limit: {creditCard.Limit:f2}");
                Console.WriteLine($"--- Money Owed: {creditCard.MoneyOwed:f2}");
                Console.WriteLine($"--- Limit Left: {creditCard.LimitLeft:f2}");
                Console.WriteLine($"--- Expiration Date: {creditCard.ExpirationDate}");
            }
        }

        private static User GetUser(BillsPaymentSystemContext context)
        {
            int userid = int.Parse(Console.ReadLine());

            var user = context.Users
                .Where(u => u.UserId == userid)
                .Include(u => u.PaymentMethods)
                .ThenInclude(pm => pm.BankAccount)
                .Include(u => u.PaymentMethods)
                .ThenInclude(pm => pm.CreditCard)
                .FirstOrDefault();

            if (user == null)
            {
                Console.WriteLine($"User with id {userid} not found!");
                return null;
            }

            return user;
        }

        private static void PayBills(User user, decimal amount)
        {
            var bankAccountaTotalMoney =
                user.PaymentMethods.Where(pm => pm.BankAccount != null).Sum(x => x.BankAccount.Balance);
            var creditCardsTotalMoney =
                user.PaymentMethods.Where(pm => pm.CreditCard != null).Sum(x => x.CreditCard.LimitLeft);

            var totlaAmount = bankAccountaTotalMoney + creditCardsTotalMoney;

            if (totlaAmount >= amount)
            {
                var bankAccounts = user.PaymentMethods.Where(pm => pm.BankAccount != null).Select(x => x.BankAccount)
                    .OrderBy(x => x.BankAccountId).ToArray();
                foreach (BankAccount bankAccount in bankAccounts)
                {
                    if (bankAccount.Balance >= amount)
                    {
                        bankAccount.Withdraw(amount);
                        amount = 0;
                    }
                    else
                    {
                        amount -= bankAccount.Balance;
                        bankAccount.Withdraw(bankAccount.Balance);
                    }

                    if (amount == 0) return;
                }

                var creditCards = user.PaymentMethods.Where(pm => pm.CreditCard != null).Select(x => x.CreditCard)
                    .OrderBy(x => x.CreditCardId).ToArray();

                foreach (CreditCard creditCard in creditCards)
                {
                    if (creditCard.LimitLeft >= amount)
                    {
                        creditCard.Withdraw(amount);
                        amount = 0;
                    }
                    else
                    {
                        amount -= creditCard.LimitLeft;
                        creditCard.Withdraw(creditCard.LimitLeft);
                    }
                    if (amount == 0) return;

                }
            }
        }
    }
}