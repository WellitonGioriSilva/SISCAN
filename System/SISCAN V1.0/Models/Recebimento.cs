using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    class Recebimento
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Hora { get; set; }
        public Caixa Caixa { get; set; }
        public Venda Venda { get; set; }
        public FormaPagamento FormaPagamento { get; set;}
    }
}
