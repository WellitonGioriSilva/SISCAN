using MySqlX.XDevAPI;
using SISCAN.Database;
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
                query.CommandText = $"INSERT INTO Estoque (lote_est, quantidade_est, validade_est, id_prod_fk)" +
                    $"VALUES (@lote, @quantidade, @validade, @id_prod)";

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

    }
}
