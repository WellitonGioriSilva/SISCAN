using MySql.Data.MySqlClient;
using SISCAN.Database;
using SISCAN.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace SISCAN.Models
{
    internal class ProdutoDAO
    {
        private static Conexao conn;
        public string mensagem;
        public bool condicao;
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
                    query.CommandText = "SELECT * FROM Produto WHERE visivel_prod = 'Sim';";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Produto WHERE (nome_prod LIKE '%{busca}%') AND (visivel_prod = 'Sim');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Produto()
                    {
                        Id = reader.GetInt32("id_prod"),
                        Nome = DAOHelper.GetString(reader, "nome_prod"),
                        Marca = DAOHelper.GetString(reader, "marca_prod"),
                        Tipo = DAOHelper.GetString(reader, "tipo_prod"),
                        Valor = reader.GetInt32("valor_com_prod"),
                        ValorVen = DAOHelper.GetDouble(reader, "valor_ven_prod")
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
                query.CommandText = $"CALL InsertProduto(@nome, @marca, @tipo, @valor, @result)";

                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@marca", produto.Marca);
                query.Parameters.AddWithValue("@tipo", produto.Tipo);
                query.Parameters.AddWithValue("@valor", produto.Valor);

                query.ExecuteNonQuery();

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

        public void Update(Produto produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Produto SET nome_prod = @nome, marca_prod = @marca, tipo_prod = @tipo, valor_com_prod = @valor WHERE id_prod = @id";

                query.Parameters.AddWithValue("@id", produto.Id);
                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@marca", produto.Marca);
                query.Parameters.AddWithValue("@tipo", produto.Tipo);
                query.Parameters.AddWithValue("@valor", produto.Valor);

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

        public void Delete(Produto produto)
        {

            MessageBoxResult resultado = MessageBox.Show("Deseja deletar esse dado?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = $"UPDATE Produto SET visivel_prod = 'Nao' WHERE id_prod = @id;";

                    query.Parameters.AddWithValue("@id", produto.Id);

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

        public Produto ListLucro()
        {
            try
            {
                Produto produto = new Produto();
                var query = conn.Query();

                query.CommandText = $"SELECT Produto.id_prod, Produto.nome_prod, SUM(venda_produto.quantidade_vend_prod) AS total_vendido, Produto.valor_com_prod, Produto.valor_ven_prod, (SUM(venda_produto.quantidade_vend_prod) * Produto.valor_ven_prod - SUM(venda_produto.quantidade_vend_prod) * Produto.valor_com_prod) AS lucro_mensal FROM Produto JOIN Venda_produto venda_produto ON Produto.id_prod = venda_produto.id_prod_fk JOIN Venda venda ON venda_produto.id_vend_fk = venda.id_vend WHERE MONTH(venda.data_vend) = MONTH(CURRENT_DATE()) AND YEAR(venda.data_vend) = YEAR(CURRENT_DATE()) GROUP BY Produto.id_prod, Produto.nome_prod, Produto.valor_com_prod, Produto.valor_ven_prod ORDER BY lucro_mensal DESC LIMIT 1;";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    produto.Nome = DAOHelper.GetString(reader, "nome_prod");
                }

                return produto;
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
