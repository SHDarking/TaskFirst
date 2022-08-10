using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst.Models
{
    public class Service
    {
        public string Name { get; private set; }
        public List<Payer> Payers { get; private set; }
        public decimal Total { get; private set; }

        public Service(string name, List<Payer> payers, decimal total)
        {
            Name = name;
            Payers = payers;
            Total = total;
        }
    }
}
