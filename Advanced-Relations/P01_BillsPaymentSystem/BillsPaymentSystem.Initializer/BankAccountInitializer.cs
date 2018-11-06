using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Initializer
{
    public class BankAccountInitializer
    {
        public static BankAccount[] GetBankAccounts()
        {
            BankAccount[] bankAccounts = new BankAccount[]
            {
                new BankAccount() {BankName = "Crazy Bank", SwiftCode = "CRB", Balance = 2320.50m},
                new BankAccount() {BankName = "Lazy Bank", SwiftCode = "LB", Balance = 1220.20m},
                new BankAccount() {BankName = "New Bank", SwiftCode = "NBB", Balance = 3204.03m},
                new BankAccount() {BankName = "Evil Bank", SwiftCode = "EB", Balance = 3120.00m},
                new BankAccount() {BankName = "BR Bank", SwiftCode = "BRB", Balance = 375.00m},
                new BankAccount() {BankName = "Z Bank", SwiftCode = "ZB", Balance = 30.00m},
                new BankAccount() {BankName = "B Bank", SwiftCode = "BB", Balance = 0.00m},
                new BankAccount() {BankName = "NoName Bank", SwiftCode = "NNB", Balance = 234.30m},
                new BankAccount() {BankName = "Medina Bank", SwiftCode = "MB", Balance = 52.31m},
                new BankAccount() {BankName = "Happy Bank", SwiftCode = "HP", Balance = 32320.00m},
                new BankAccount() {BankName = "Poor bank", SwiftCode = "PB", Balance = 3220.90m},
                new BankAccount() {BankName = "Rich Bank", SwiftCode = "RB", Balance = 9120.20m}
            };
            return bankAccounts;
        }
    }
}
