using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISCAN.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UsuarioNome { get; set; }
        public string Senha { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
