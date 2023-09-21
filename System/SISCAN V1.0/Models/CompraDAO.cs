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

        public void Insert(Compra compra, List<CompraProduto> compraProduto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"CALL InsertCompra(@valor, @id_fk, @result)";

                query.Parameters.AddWithValue("@valor", compra.Valor);
                query.Parameters.AddWithValue("@id", compra.Fornecedor.Id);
                query.Parameters.Add(new MySqlParameter("@result", MySqlDbType.VarChar));
                query.Parameters["@result"].Direction = System.Data.ParameterDirection.Output;
                
                query.ExecuteNonQuery();

                string resultado = (string)query.Parameters["result"].Value;
                MessageBox.Show(resultado);

                query.CommandText = $"CALL InsertCompraProduto(@quantidade, @id_fk_prod, @lote, @validade, @result1)";

                foreach (CompraProduto compraProd in compraProduto)
                {
                    query.Parameters.Clear();
                    query.Parameters.AddWithValue("@quantidade", compraProd.Quantidade);
                    query.Parameters.AddWithValue("@id_fk_prod", compraProd.Produto.Id);
                    query.Parameters.Add(new MySqlParameter("@result1", MySqlDbType.VarChar));
                    query.Parameters["@result1"].Direction = System.Data.ParameterDirection.Output;

                    query.ExecuteNonQuery();

                    resultado = (string)query.Parameters["@result1"].Value;

                    if (resultado != "0")
                    {
                        MessageBox.Show(resultado);
                    }
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
