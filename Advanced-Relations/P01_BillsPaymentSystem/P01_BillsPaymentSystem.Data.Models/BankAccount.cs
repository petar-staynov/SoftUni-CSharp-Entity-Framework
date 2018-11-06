using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using P01_BillsPaymentSystem.Data.Models.Attributes;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        [Key] public int BankAccountId { get; set; }

        public decimal Balance { get; set; }

        [Required] [MaxLength(50)] public string BankName { get; set; }

        [Required]
        [MaxLength(20)]
        [NonUnicodeAttributes]
        public string SwiftCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Deposit(decimal ammount)
        {
            if (ammount > 0)
            {
                this.Balance += ammount;
            }
        }

        public void Withdraw(decimal ammount)
        {
            if (this.Balance - ammount >= 0)
            {
                this.Balance -= ammount;
            }
        }
    }
}