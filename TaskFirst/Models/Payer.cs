﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst.Models
{
    public class Payer
    {
        public string Name { get;  set; }
        public decimal Payment { get;  set; }
        public DateTime Date { get;  set; }
        public long AccountNumber { get;  set; }

    }
}
