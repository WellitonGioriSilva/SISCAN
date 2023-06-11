using SISCAN.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SISCAN.Models
{
    class FuncionarioDAO
    {
        private static Conexao conn;

        public FuncionarioDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Funcionario funcionario)
        {
            try
            {
                //var cidadeId = new CidadeDAO().Insert(cliente.Cidade);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Funcionario (nome_func, bairro_func, rua_func, cpf_func, numero_func, sexo_func, id_cid_fk, id_fun_fk) " +
                    $"VALUES (@nome, @bairro, @rua, @cpf, @numero, @sexo, @id_cid, @id_fun)";

                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@bairro", funcionario.Bairro);
                query.Parameters.AddWithValue("@rua", funcionario.Rua);
                query.Parameters.AddWithValue("@cpf", funcionario.Cpf);
                query.Parameters.AddWithValue("@numero", funcionario.Numero);
                query.Parameters.AddWithValue("@sexo", funcionario.Sexo);
                query.Parameters.AddWithValue("@id_fun", funcionario.Funcao.Id);
                query.Parameters.AddWithValue("@id_cid", funcionario.Cidade.ID);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
