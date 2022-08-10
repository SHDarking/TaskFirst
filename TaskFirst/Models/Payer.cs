using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst.Models
{
    public class Payer
    {
        public string Name { get; private set; }
        public decimal Payment { get; private set; }
        public DateOnly Date { get; private set; }
        public long AccountNumber { get; private set; }

        public Payer(string name, decimal payment, DateOnly date, long accountNumber)
        {
            Name = name;
            Payment = payment;
            Date = date;
            AccountNumber = accountNumber;
        }
    }
}
