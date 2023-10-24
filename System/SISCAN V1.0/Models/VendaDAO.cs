using MySql.Data.MySqlClient;
using SISCAN.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SISCAN.Models
{

    internal class VendaDAO
    {
        private static Conexao conn;
        public VendaDAO()
        {
            conn = new Conexao();
        }
        public void Insert(Venda venda, List<VendaProduto> vendaProduto, Recebimento recebimento)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"CALL InsertVenda(@valor, @id_fk, @idCaixa, @idFormPag, @idCliente, @result)";

                query.Parameters.AddWithValue("@valor", venda.Valor);
                query.Parameters.AddWithValue("@id_fk", venda.Funcionario.Id);
                query.Parameters.AddWithValue("@idCliente", venda.Cliente.Id);
                query.Parameters.AddWithValue("@idCaixa", recebimento.Caixa.id);
                query.Parameters.AddWithValue("@idFormPag", recebimento.FormaPagamento.Id);
                query.Parameters.Add(new MySqlParameter("@result", MySqlDbType.VarChar));
                query.Parameters["@result"].Direction = System.Data.ParameterDirection.Output;

                query.ExecuteNonQuery();

                string resultado = (string)query.Parameters["@result"].Value;
                MessageBox.Show(resultado);

                query.CommandText = $"CALL InsertVendaProduto(@quantidade, @id_fk_prod, @result1)";

                foreach (VendaProduto vendaProd in vendaProduto)
                {
                    query.Parameters.Clear();
                    query.Parameters.AddWithValue("@quantidade", vendaProd.Quantidade);
                    query.Parameters.AddWithValue("@id_fk_prod", vendaProd.Produto.Id);
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
