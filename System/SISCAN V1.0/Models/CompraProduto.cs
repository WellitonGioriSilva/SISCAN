﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    public class CompraProduto
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public Produto Produto { get; set; }
    }
}
