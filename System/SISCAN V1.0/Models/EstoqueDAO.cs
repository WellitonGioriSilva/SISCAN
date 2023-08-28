using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
    internal class EstoqueDAO
    {
        private static Conexao conn;
        public EstoqueDAO()
        {
            conn = new Conexao();
        }
        public void Insert(Estoque estoque)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Estoque (visivel_est, lote_est, quantidade_est, validade_est, id_prod_fk)" +
                    $"VALUES ('Sim', @lote, @quantidade, @validade, @id_prod)";

                query.Parameters.AddWithValue("@lote", estoque.Lote);
                query.Parameters.AddWithValue("@quantidade", estoque.Quantidade);
                query.Parameters.AddWithValue("@validade", estoque.Validade);
                query.Parameters.AddWithValue("@id_prod", estoque.Produto.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir  os dados, verifique e tente novamente!");
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

        public List<Estoque> List(string busca)
        {
            try
            {
                List<Estoque> list = new List<Estoque>();

                var query = conn.Query();

                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Estoque LEFT JOIN Produto ON Estoque.id_prod_fk = Produto.id_prod WHERE visivel_cai = 'Sim';";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Estoque, Produto WHERE (Estoque.id_prod_fk = Produto.id_prod) AND (lote_est LIKE '%{busca}%') AND (visivel_est = 'Sim');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Estoque()
                    {
                        Id = reader.GetInt32("id_est"),
                        Lote = DAOHelper.GetString(reader, "lote_est"),
                        Quantidade = reader.GetInt32("quantidade_est"),
                        Validade = DAOHelper.GetDateTime(reader, "validade_est"),
                        Produto = DAOHelper.IsNull(reader, "id_prod_fk") ? null : new Produto() { Id = reader.GetInt32("id_prod"), Nome = reader.GetString("nome_prod"), Marca = reader.GetString("marca_prod"), Tipo = reader.GetString("tipo_prod") }
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

        public void Delete(Estoque estoque)
        {

            MessageBoxResult resultado = MessageBox.Show("Deseja deletar esse dado?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = $"UPDATE Estoque SET visivel_est = 'Nao' WHERE id_est = @id;";

                    query.Parameters.AddWithValue("@id", estoque.Id);

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
