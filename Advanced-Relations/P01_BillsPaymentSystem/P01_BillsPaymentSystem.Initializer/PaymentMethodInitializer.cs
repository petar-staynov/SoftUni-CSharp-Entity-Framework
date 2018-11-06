using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Models.Enums;

namespace P01_BillsPaymentSystem.Initializer
{
    public class PaymentMethodInitializer
    {
        public static PaymentMethod[] GetpaymentMethods()
        {
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod() {UserId = 1, BankAccountId = 1, Type = PaymentType.BankAccount},
                new PaymentMethod() {UserId = 1, CreditCardId = 1, Type = PaymentType.CreditCard},
                new PaymentMethod() {UserId = 2, BankAccountId = 1, Type = PaymentType.BankAccount},
                new PaymentMethod() {UserId = 3, BankAccountId = 3, Type = PaymentType.BankAccount},
                new PaymentMethod() {UserId = 3, CreditCardId = 2, Type = PaymentType.CreditCard},
                new PaymentMethod() {UserId = 4, BankAccountId = 4, Type = PaymentType.BankAccount},
                new PaymentMethod() {UserId = 5, CreditCardId = 3, Type = PaymentType.CreditCard},
                new PaymentMethod() {UserId = 5, CreditCardId = 4, Type = PaymentType.CreditCard},
                new PaymentMethod() {UserId = 6, BankAccountId = 5, Type = PaymentType.BankAccount},
                new PaymentMethod() {UserId = 7, BankAccountId = 6, Type = PaymentType.BankAccount},
                new PaymentMethod() {UserId = 7, CreditCardId = 5, Type = PaymentType.CreditCard},
                new PaymentMethod() {UserId = 8, CreditCardId = 6, Type = PaymentType.CreditCard},
                new PaymentMethod() {UserId = 9, BankAccountId = 7, Type = PaymentType.BankAccount},
                new PaymentMethod() {UserId = 9, CreditCardId = 8, Type = PaymentType.CreditCard},

            };
            return paymentMethods;
        }
    }
}
