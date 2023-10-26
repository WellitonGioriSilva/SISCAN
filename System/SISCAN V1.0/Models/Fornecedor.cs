using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string NomeFantasia { get; set; }
        public string Telefone { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Responsavel { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
    }
}
