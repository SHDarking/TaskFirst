﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFirst.Models
{
    public class Service
    {
        public string Name { get;  set; }
        public List<Payer> Payers { get;  set; }
        public decimal Total { get;  set; }

    }
}
