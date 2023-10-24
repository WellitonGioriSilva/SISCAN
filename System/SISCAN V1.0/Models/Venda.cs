using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Hora { get; set; }
        public double Valor { get; set; }
        public Funcionario Funcionario { get; set; }
        public Cliente Cliente { get; set; }
    }
}
