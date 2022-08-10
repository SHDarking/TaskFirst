using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace TaskFirst.Models
{
    
    public class InputModel
    {
        [Index(0)]
        public string FirstName { get; set; }
        [Index(1)]
        [NullValues]
        public string? LastName { get; set; }
        [Index(2)]
        public string Address { get; set; }
        [Index(3)]
        public decimal Payment { get; set; }
        [Index(4)]
        public DateTime Date { get; set; }
        [Index(5)]
        public long AccountNumber { get; set; }
        [Index(6)]
        public string Service { get; set; }

        
    }
}
