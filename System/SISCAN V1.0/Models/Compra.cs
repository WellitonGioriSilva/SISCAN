using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime? Data { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
