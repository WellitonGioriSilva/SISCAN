using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Cpf { get; set; }
        public int Numero { get; set; }
        public string Sexo { get; set; }
        public Cidade Cidade { get; set; }
        public Funcao Funcao { get; set; }
    }
}
