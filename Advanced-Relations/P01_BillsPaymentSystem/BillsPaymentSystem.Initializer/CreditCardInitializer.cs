using System;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Initializer
{
    public class CreditCardInitializer
    {
        public static CreditCard[] GetCreditCards()
        {
            var creditCards = new CreditCard[]
            {
                new CreditCard() {Limit = 3000, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-12)},
                new CreditCard() {Limit = 200, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-1)},
                new CreditCard() {Limit = 1500, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-2)},
                new CreditCard() {Limit = 3200, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-15)},
                new CreditCard() {Limit = 580, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-10)},
                new CreditCard() {Limit = 672, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-19)},
                new CreditCard() {Limit = 347, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-12)},
                new CreditCard() {Limit = 2587, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-75)},
                new CreditCard() {Limit = 956, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-16)},
                new CreditCard() {Limit = 3000, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-47)},
                new CreditCard() {Limit = 8000, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-80)},
                new CreditCard() {Limit = 10000, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-124)},
                new CreditCard() {Limit = 5, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddMonths(-2)},

            };
            return creditCards;
        }
    }
}
