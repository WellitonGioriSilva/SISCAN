using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    public class VendaProduto
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public Produto Produto { get; set; }
    }
}
