using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst.Models
{
    public class OutputModel
    {
        public string City { get; set; }
        public List<Service> Services { get; set; }
        public decimal Total { get; set; }
                
    }
}
