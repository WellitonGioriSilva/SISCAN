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
    internal class ProdutoDAO
    {
        private static Conexao conn;
        public ProdutoDAO()
        {
            conn = new Conexao();
        }
        public List<Produto> List(string busca)
        {
            try
            {
                List<Produto> list = new List<Produto>();

                var query = conn.Query();

                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Produto;";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Produto WHERE (nome_prod LIKE '%{busca}%');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Produto()
                    {
                        Id = reader.GetInt32("id_prod"),
                        Nome = DAOHelper.GetString(reader, "nome_prod"),
                        Marca = DAOHelper.GetString(reader, "marca_prod"),
                        Tipo = DAOHelper.GetString(reader, "tipo_prod")
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
        public void Insert(Produto produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Produto (nome_prod, marca_prod, tipo_prod)" +
                    $"VALUES (@nome, @marca, @tipo)";

                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@marca", produto.Marca);
                query.Parameters.AddWithValue("@tipo", produto.Tipo);

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

        public void Update(Produto produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Produto SET nome_prod = @nome, marca_prod = @marca, tipo_prod = @tipo WHERE id_prod = @id";

                query.Parameters.AddWithValue("@id", produto.Id);
                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@marca", produto.Marca);
                query.Parameters.AddWithValue("@tipo", produto.Tipo);

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

    }
}
