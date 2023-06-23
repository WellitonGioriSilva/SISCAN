using SISCAN.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SISCAN.Models
{
    internal class PagamentoDAO
    {
        private static Conexao conn;

        public PagamentoDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Pagamento pagamento)
        {
            try
            {
                //var cidadeId = new CidadeDAO().Insert(cliente.Cidade);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Pagamento (data_pag, valor_pag, hora_pag, id_cai_fk, id_form_pag_fk, id_desp_fk) " +
                    $"VALUES (@data, @valor, @hora, @id_cai, @id_form_pag, @id_desp)";

                query.Parameters.AddWithValue("@data", pagamento.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", pagamento.Valor);
                query.Parameters.AddWithValue("@hora", pagamento.Hora);
                query.Parameters.AddWithValue("@id_cai", pagamento.Caixa.id);
                query.Parameters.AddWithValue("@id_form_pag", pagamento.FormaPagamento.Id);
                query.Parameters.AddWithValue("@id_desp", pagamento.Despesa.Id);

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
