using SISCAN.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SISCAN.Models
{
    class ClienteDAO
    {
        private static Conexao conn;

        public ClienteDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Cliente cliente)
        {
            try
            {
                //var cidadeId = new CidadeDAO().Insert(cliente.Cidade);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Cliente (nome_cli, cpf_cli, email_cli, sexo_cli, data_nascimento_cli, rua_cli, bairro_cli, numero_cli, id_cid_fk) " +
                    $"VALUES (@nome, @cpf, @email, @sexo, @data_nasc, @rua, @bairro, @numero, @id_cid)";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.Cpf);
                query.Parameters.AddWithValue("@email", cliente.Email);
                query.Parameters.AddWithValue("@sexo", cliente.Sexo);
                query.Parameters.AddWithValue("@data_nasc", cliente.DataNascimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@rua", cliente.Rua);
                query.Parameters.AddWithValue("@bairro", cliente.Bairro);
                query.Parameters.AddWithValue("@numero", cliente.Numero);
                query.Parameters.AddWithValue("@id_cid", cliente.Cidade.ID);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
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
