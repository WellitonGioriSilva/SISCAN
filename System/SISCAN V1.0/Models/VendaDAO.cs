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
        public string mensagem;
        public bool condicao;
        public VendaDAO()
        {
            conn = new Conexao();

        }
        public void Insert(Venda venda, List<VendaProduto> vendaProdutos, Recebimento recebimento)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"CALL InsertVenda(@valor, @id_fk, @idCaixa, @idFormPag, @idCliente)";

                query.Parameters.AddWithValue("@valor", venda.Valor);
                query.Parameters.AddWithValue("@id_fk", venda.Funcionario.Id);
                query.Parameters.AddWithValue("@idCliente", venda.Cliente.Id);
                query.Parameters.AddWithValue("@idCaixa", recebimento.Caixa.id);
                query.Parameters.AddWithValue("@idFormPag", recebimento.FormaPagamento.Id);
                //query.Parameters.Add(new MySqlParameter("@result", MySqlDbType.VarChar));
                //query.Parameters["@result"].Direction = System.Data.ParameterDirection.Output;

                //query.ExecuteNonQuery();

                MySqlDataReader reader = query.ExecuteReader();
                if (reader.Read())
                {
                    mensagem = reader.GetString(0); // Pega o primeiro campo, que é a string
                    condicao = reader.GetBoolean(1); // Pega o segundo campo, que é o boolean
                }

                reader.Close();

                

                

                foreach (VendaProduto vendaProd in venda.Items)
                {
                   
                    MessageBox.Show(vendaProd.Quantidade.ToString());

                    query = conn.Query();
                    query.CommandText = $"CALL InsertVendaProduto(@quantidade, @id_fk_prod)";

                    query.Parameters.AddWithValue("@quantidade", vendaProd.Quantidade);
                    query.Parameters.AddWithValue("@id_fk_prod", vendaProd.Produto.Id);

                    reader = query.ExecuteReader();

                    //if (reader.Read())
                    //{
                    //    mensagem = reader.GetString(0); // Pega o primeiro campo, que é a string
                    //    condicao = reader.GetBoolean(1); // Pega o segundo campo, que é o boolean
                    //}

                    reader.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
        }
    }
}
