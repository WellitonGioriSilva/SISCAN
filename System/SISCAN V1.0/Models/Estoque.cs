using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    internal class Estoque
    {
        public int Id { get; set; }
        public string Lote { get; set; }
        public string Quantidade { get; set; }
        public DateTime? Validade { get; set; }
        public Produto Produto { get; set; }
    }
}
