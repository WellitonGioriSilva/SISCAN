using SISCAN.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SISCAN.Models
{
    internal class UsuarioDAO
    {
        private static Conexao conn;
        public UsuarioDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Usuario usuario) 
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Usuario (usuario_usu, senha_usu, id_func_fk)" +
                    $"VALUES (@usuario, @senha, @id_func)";

                query.Parameters.AddWithValue("@usuario", usuario.UsuarioNome);
                query.Parameters.AddWithValue("@senha", usuario.Senha);
                query.Parameters.AddWithValue("@id_func", usuario.Funcionario.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente");
                }
                else
                {
                    MessageBox.Show("Dados salvos com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
