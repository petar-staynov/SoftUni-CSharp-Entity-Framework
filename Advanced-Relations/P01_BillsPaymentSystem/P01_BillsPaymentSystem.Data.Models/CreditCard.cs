using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public decimal Limit { get; set; }
        public decimal MoneyOwed { get; set; }

        [NotMapped]
        public decimal LimitLeft => Limit - MoneyOwed;


        public DateTime ExpirationDate { get; set; }

        public void Deposit(decimal ammount)
        {
            if (ammount > 0)
            {
                this.MoneyOwed -= ammount;
            }
        }

        public void Withdraw(decimal ammount)
        {
            if (this.LimitLeft - ammount >= 0)
            {
                this.MoneyOwed += ammount;
            }
        }
    }
}
