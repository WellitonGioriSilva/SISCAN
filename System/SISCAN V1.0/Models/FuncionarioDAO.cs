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
    class FuncionarioDAO
    {
        private static Conexao conn;
        public string mensagem;
        public bool condicao;
        public FuncionarioDAO()
        {
            conn = new Conexao();
        }

        public List<Funcionario> List(string busca)
        {
            try
            {
                List<Funcionario> list = new List<Funcionario>();

                var query = conn.Query();

                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Funcionario, Funcao WHERE (Funcionario.id_fun_fk = Funcao.id_fun) AND (visivel_func = 'Sim');";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Funcionario, Funcao WHERE (Funcionario.id_fun_fk = Funcao.id_fun) AND (nome_func LIKE '%{busca}%') AND (visivel_func = 'Sim');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Funcionario()
                    {
                        Id = reader.GetInt32("id_func"),
                        Nome = DAOHelper.GetString(reader, "nome_func"),
                        Cpf = DAOHelper.GetString(reader, "cpf_func"),
                        Sexo = DAOHelper.GetString(reader, "sexo_func"),
                        Rua = DAOHelper.GetString(reader, "rua_func"),
                        Bairro = DAOHelper.GetString(reader, "bairro_func"),
                        Numero = reader.GetInt32("numero_func"),
                        Acesso = reader.GetInt32("nivel_acess_func"),
                        cidade = DAOHelper.GetString(reader, "cidade_func"),
                        estado = DAOHelper.GetString(reader, "estado_func"),
                        Funcao = DAOHelper.IsNull(reader, "id_fun_fk") ? null : new Funcao() { Id = reader.GetInt32("id_fun"), Nome = reader.GetString("nome_fun") }
                    });
                }

                return list;
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

        public void Insert(Funcionario funcionario)
        {
            try
            {
                //var cidadeId = new CidadeDAO().Insert(cliente.Cidade);

                var query = conn.Query();

                query.CommandText = $"CALL InsertFuncionario(@nome, @bairro, @rua, @cpf, @numero, @sexo, @cidade, @estado, @id_fun, @acesso)";

                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@bairro", funcionario.Bairro);
                query.Parameters.AddWithValue("@rua", funcionario.Rua);
                query.Parameters.AddWithValue("@cpf", funcionario.Cpf);
                query.Parameters.AddWithValue("@numero", funcionario.Numero);
                query.Parameters.AddWithValue("@sexo", funcionario.Sexo);
                query.Parameters.AddWithValue("@acesso", funcionario.Acesso);
                query.Parameters.AddWithValue("@id_fun", funcionario.Funcao.Id);
                query.Parameters.AddWithValue("@cidade", funcionario.cidade);
                query.Parameters.AddWithValue("@estado", funcionario.estado);

                MySqlDataReader reader = query.ExecuteReader();
                if (reader.Read())
                {
                    mensagem = reader.GetString(0); // Pega o primeiro campo, que é a string
                    condicao = reader.GetBoolean(1); // Pega o segundo campo, que é o boolean
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

        public void Update(Funcionario funcionario)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Funcionario SET nome_func = @nome, bairro_func = @bairro, rua_func = @rua, cpf_func = @cpf, " +
                    "numero_func = @numero, sexo_func = @sexo, cidade_func = @cidade, estado_func = @estado, id_fun_fk = @id_fun WHERE id_func = @id";

                query.Parameters.AddWithValue("@id", funcionario.Id);
                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@rua", funcionario.Rua);
                query.Parameters.AddWithValue("@bairro", funcionario.Bairro);
                query.Parameters.AddWithValue("@numero", funcionario.Numero);
                query.Parameters.AddWithValue("@sexo", funcionario.Sexo);
                query.Parameters.AddWithValue("@cpf", funcionario.Cpf);
                query.Parameters.AddWithValue("@cidade", funcionario.cidade);
                query.Parameters.AddWithValue("@estado", funcionario.cidade);
                query.Parameters.AddWithValue("@id_fun", funcionario.Funcao.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente");
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

        public void Delete(Funcionario funcionario)
        {

            MessageBoxResult resultado = MessageBox.Show("Deseja deletar esse dado?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = $"UPDATE Funcionario SET visivel_func = 'Nao' WHERE id_func = @id;";

                    query.Parameters.AddWithValue("@id", funcionario.Id);

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
