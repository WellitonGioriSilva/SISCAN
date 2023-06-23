using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    internal class Despesa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Parcelas { get; set;}
        public double Valor { get; set; }
        public string Status { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Vencimento { get; set; }
        public Compra Compra { get; set; }
    }
}
