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
    class FuncaoDAO
    {
        private static Conexao conn;

        public FuncaoDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Funcao funcao)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Funcao (visivel_fun, nome_fun, salario_fun, nivel_acess_fun, turno_fun) " +
                    $"VALUES ('Sim',@nome, @salario, @acesso, @turno)";

                query.Parameters.AddWithValue("@nome", funcao.Nome);
                query.Parameters.AddWithValue("@salario", funcao.Salario);
                query.Parameters.AddWithValue("@acesso", funcao.Acesso);
                query.Parameters.AddWithValue("@turno", funcao.Turno);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
                else
                {
                    MessageBox.Show("Dados salvos com sucesso!");
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

        public List<Funcao> List(string busca)
        {
            try
            {
                List<Funcao> list = new List<Funcao>();

                var query = conn.Query();
                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Funcao WHERE visivel_fun = 'Sim';";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Funcao WHERE (nome_fun LIKE '%{busca}%') AND (visivel_fun = 'Sim');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Funcao()
                    {
                        Id = reader.GetInt32("id_fun"),
                        Nome = DAOHelper.GetString(reader, "nome_fun"),
                        Salario = DAOHelper.GetDouble(reader, "salario_fun"),
                        Acesso = reader.GetInt32("nivel_acess_fun"),
                        Turno = DAOHelper.GetString(reader, "turno_fun")
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

        public void Update(Funcao funcao)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Funcao SET nome_fun = @nome, salario_fun = @salario, nivel_acess_fun = @acesso, turno_fun = @turno WHERE id_fun = @id";

                query.Parameters.AddWithValue("@id", funcao.Id);
                query.Parameters.AddWithValue("@nome", funcao.Nome);
                query.Parameters.AddWithValue("@salario", funcao.Salario);
                query.Parameters.AddWithValue("@acesso", funcao.Acesso);
                query.Parameters.AddWithValue("@turno", funcao.Turno);

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

        public void Delete(Funcao funcao)
        {

            MessageBoxResult resultado = MessageBox.Show("Deseja deletar esse dado?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = $"UPDATE Funcao SET visivel_fun = 'Nao' WHERE id_fun = @id;";

                    query.Parameters.AddWithValue("@id", funcao.Id);

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
