using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    public class Caixa
    {
        public int id {  get; set; }
        public DateTime?  Data { get; set; }
        public TimeSpan? HoraAbertura { get; set; }
        public TimeSpan? HoraFechamento { get; set; }
        public double ValorIncial { get; set;}
        public double ValorFinal { get; set; }
        public Funcionario funcionario { get; set; }
    }
}
