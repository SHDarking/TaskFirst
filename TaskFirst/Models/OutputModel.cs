using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst.Models
{
    public class OutputModel
    {
        public string City { get; private set; }
        public List<Service> Services { get; private set; }
        public decimal Total { get; private set; }

        public OutputModel(string city, List<Service> services, decimal total)
        {
            City = city;
            Services = services;
            Total = total;
        }
    }
}
