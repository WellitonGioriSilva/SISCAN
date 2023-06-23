using SISCAN.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SISCAN.Models
{
    class RecebimentoDAO
    {
        private static Conexao conn;

        public RecebimentoDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Recebimento recebimento)
        {
            try
            {
                //var cidadeId = new CidadeDAO().Insert(cliente.Cidade);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Recebimento (data_rec, valor_rec, hora_rec, id_cai_fk, id_form_pag_fk) " +
                    $"VALUES (@data, @valor, @hora, @id_cai, @id_form_pag)";

                query.Parameters.AddWithValue("@data", recebimento.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", recebimento.Valor);
                query.Parameters.AddWithValue("@hora", recebimento.Hora);
                query.Parameters.AddWithValue("@id_cai", recebimento.Caixa.id);
                query.Parameters.AddWithValue("@id_form_pag", recebimento.FormaPagamento.Id);

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
