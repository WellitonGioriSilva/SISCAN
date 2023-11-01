using MySql.Data.MySqlClient;
using SISCAN.Database;
using SISCAN.Helpers;
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
        //public List<Cidade> listCid = new List<Cidade>();
        public string mensagem;
        public bool condicao;

        public ClienteDAO()
        {
            conn = new Conexao();
        }
        public List<Cliente> List(string busca)
        {
            try
            {
                List<Cliente> listCli = new List<Cliente>();

                var query = conn.Query();

                if(busca == null)
                {
                    query.CommandText = "SELECT * FROM Cliente WHERE (visivel_cli = 'Sim');";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Cliente WHERE (nome_cli LIKE '%{busca}%') AND (visivel_cli = 'Sim');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listCli.Add(new Cliente()
                    {
                        Id = reader.GetInt32("id_cli"),
                        Nome = DAOHelper.GetString(reader, "nome_cli"),
                        Cpf = DAOHelper.GetString(reader, "cpf_cli"),
                        Email = DAOHelper.GetString(reader, "email_cli"),
                        Sexo = DAOHelper.GetString(reader, "sexo_cli"),
                        DataNascimento = DAOHelper.GetDateTime(reader, "data_nascimento_cli"),
                        Rua = DAOHelper.GetString(reader, "rua_cli"),
                        Bairro = DAOHelper.GetString(reader, "bairro_cli"),
                        Numero = reader.GetInt32("numero_cli"),
                        cidade = DAOHelper.GetString(reader, "cidade_cli"),
                        estado = DAOHelper.GetString(reader, "estado_cli"),
                    }) ;                    
                }

                return listCli;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Insert(Cliente cliente)
        {
            try
            {
                //var cidadeId = new CidadeDAO().Insert(cliente.Cidade);

                var query = conn.Query();
                query.CommandText = $"CALL InsertCliente(@nome, @cpf, @email, @sexo, @data_nasc, @rua, @bairro, @numero, @cidade, @estado)";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.Cpf);
                query.Parameters.AddWithValue("@email", cliente.Email);
                query.Parameters.AddWithValue("@sexo", cliente.Sexo);
                query.Parameters.AddWithValue("@data_nasc", cliente.DataNascimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@rua", cliente.Rua);
                query.Parameters.AddWithValue("@bairro", cliente.Bairro);
                query.Parameters.AddWithValue("@numero", cliente.Numero);
                query.Parameters.AddWithValue("@cidade", cliente.cidade);
                query.Parameters.AddWithValue("@estado", cliente.estado);

                MySqlDataReader reader = query.ExecuteReader();
                if (reader.Read())
                {
                    mensagem = reader.GetString(0); // Pega o primeiro campo, que é a string
                    condicao = reader.GetBoolean(1); // Pega o segundo campo, que é o boolean
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

        public void Update(Cliente cliente)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Cliente SET nome_cli = @nome, cpf_cli = @cpf, email_cli = @email, sexo_cli = @sexo, data_nascimento_cli = @data_nascimento, " +
                    "rua_cli = @rua, bairro_cli = @bairro, numero_cli = @numero, cidade_cli = @cidade, estado_cli = @estado WHERE id_cli = @id";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.Cpf);
                query.Parameters.AddWithValue("@email", cliente.Email);
                query.Parameters.AddWithValue("@sexo", cliente.Sexo);
                query.Parameters.AddWithValue("@data_nasc", cliente.DataNascimento?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@rua", cliente.Rua);
                query.Parameters.AddWithValue("@bairro", cliente.Bairro);
                query.Parameters.AddWithValue("@numero", cliente.Numero);
                query.Parameters.AddWithValue("@cidade", cliente.cidade);
                query.Parameters.AddWithValue("@estado", cliente.cidade);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
                else
                {
                    MessageBox.Show("Dados atualizados com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte!");
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(Cliente cliente)
        {

            MessageBoxResult resultado = MessageBox.Show("Deseja deletar esse dado?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = $"UPDATE Cliente SET visivel_cli = 'Nao' WHERE id_cli = @id;";

                    query.Parameters.AddWithValue("@id", cliente.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao deletar os dados, verifique e tente novamente");
                    }
                    else
                    {
                        MessageBox.Show("Dados deletados com sucesso!");
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
}
