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
    internal class CompraDAO
    {
        private static Conexao conn;

        public CompraDAO()
        {
            conn = new Conexao();
        }

        public List<Compra> List(string busca)
        {
            try
            {
                List<Compra> list = new List<Compra>();

                var query = conn.Query();
                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Compra;";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Compra WHERE (id_com LIKE '%{busca}%');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Compra()
                    {
                        Id = reader.GetInt32("id_com"),
                        Valor = DAOHelper.GetDouble(reader, "valor_com"),
                        Data = DAOHelper.GetDateTime(reader, "data_com")
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
        public void Insert(Compra compra)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Compra (valor_cai, data_com)" +
                    $"VALUES (@valor, data)";

                query.Parameters.AddWithValue("@valor", compra.Valor);
                query.Parameters.AddWithValue("@data", compra.Data?.ToString("yyyy-MM-dd"));

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
