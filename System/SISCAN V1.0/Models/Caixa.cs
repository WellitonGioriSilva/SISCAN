﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    internal class Caixa
    {
        public int id {  get; set; }
        public DateTime?  Data { get; set; }
        public DateTime? HoraAbertura { get; set; }
        public DateTime? HoraFechamento { get; set; }
        public double ValorIncial { get; set;}
        public double ValorFinal { get; set; }
    }
}
