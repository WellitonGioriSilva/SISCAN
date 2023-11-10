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
        public string mensagem;
        public bool condicao;
        public CompraDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Compra compra, List<CompraProduto> compraProduto, Despesa despesa, Estoque estoque)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"CALL InsertCompra(@valor, @id_fk, @dataVencimento, @statusDespesa, @parcelas)";

                query.Parameters.AddWithValue("@valor", compra.Valor);
                query.Parameters.AddWithValue("@id_fk", compra.Fornecedor.Id);
                query.Parameters.AddWithValue("@dataVencimento", despesa.Data);
                query.Parameters.AddWithValue("@statusDespesa", despesa.Status);
                query.Parameters.AddWithValue("@parcelas", despesa.Parcelas);

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    mensagem = reader.GetString(0); // Pega o primeiro campo, que é a string
                    condicao = reader.GetBoolean(1); // Pega o segundo campo, que é o boolean
                }

                reader.Close();

                foreach (CompraProduto compraProd in compraProduto)
                {
                    query = conn.Query();
                    query.CommandText = $"CALL InsertCompraProduto(@lote, @quantidade, @id_fk_prod, @validade)";
                    
                    query.Parameters.AddWithValue("@quantidade", compraProd.Quantidade);
                    query.Parameters.AddWithValue("@id_fk_prod", compraProd.Produto.Id);
                    query.Parameters.AddWithValue("@lote", estoque.Lote);
                    query.Parameters.AddWithValue("@validade", estoque.Validade);

                    reader = query.ExecuteReader();
                    if (reader.Read())
                    {
                        mensagem = reader.GetString(0); // Pega o primeiro campo, que é a string
                        condicao = reader.GetBoolean(1); // Pega o segundo campo, que é o boolean
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
