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
                query.CommandText = $"INSERT INTO Funcao (nome_fun, salario_fun, turno_fun) " +
                    $"VALUES (@nome, @salario, @turno)";

                query.Parameters.AddWithValue("@nome", funcao.Nome);
                query.Parameters.AddWithValue("@salario", funcao.Salario);
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

        public List<Funcao> List()
        {
            try
            {
                List<Funcao> list = new List<Funcao>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM Funcao;";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Funcao()
                    {
                        Id = reader.GetInt32("id_fun"),
                        Nome = DAOHelper.GetString(reader, "nome_fun")
                        //Estado = DAOHelper.IsNull(reader, "id_est_fk") ? null : new Estado() { Id = reader.GetInt32("id_est"), Nome = reader.GetString("nome_est") }
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
    }
}
