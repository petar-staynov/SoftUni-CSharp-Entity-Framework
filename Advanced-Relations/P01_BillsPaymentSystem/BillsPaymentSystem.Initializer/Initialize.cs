using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Initializer
{
    public class Initialize
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            InsertUsers(context);
            InsertCreditCards(context);
            InsertBankAccounts(context);
            InsertPaymentMethods(context);
        }

        private static void InsertUsers(BillsPaymentSystemContext context)
        {
            var users = UsersInitializer.GetUsers();

            foreach (User user in users)
            {
                if (IsValid(user))
                {
                    context.Users.Add(user);
                }
            }

            context.SaveChanges();
        }

        private static void InsertCreditCards(BillsPaymentSystemContext context)
        {
            var creditCards = CreditCardInitializer.GetCreditCards();
            foreach (CreditCard creditCard in creditCards)
            {
                if (IsValid(creditCard))
                {
                    context.CreditCards.Add(creditCard);
                }
            }

            context.SaveChanges();
        }

        private static void InsertBankAccounts(BillsPaymentSystemContext context)
        {
            var bankAccounts = BankAccountInitializer.GetBankAccounts();

            foreach (BankAccount bankAccount in bankAccounts)
            {
                if (IsValid(bankAccount))
                {
                    context.BankAccounts.Add(bankAccount);
                }
            }

            context.SaveChanges();
        }

        private static void InsertPaymentMethods(BillsPaymentSystemContext context)
        {
            var paymentMethods = PaymentMethodInitializer.GetpaymentMethods();

            foreach (PaymentMethod paymentMethod in paymentMethods)
            {
                if (IsValid(paymentMethod))
                {
                    context.PaymentMethods.Add(paymentMethod);
                }
            }

            context.SaveChanges();
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}