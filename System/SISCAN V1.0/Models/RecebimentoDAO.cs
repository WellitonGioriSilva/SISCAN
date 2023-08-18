﻿using MySql.Data.MySqlClient;
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

        public List<Recebimento> List(string busca)
        {
            try
            {
                List<Recebimento> list = new List<Recebimento>();

                var query = conn.Query();

                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Recebimento LEFT JOIN Venda ON Recebimento.id_vend_fk = Venda.id_vend INNER JOIN Caixa ON Recebimento.id_cai_fk = Caixa.id_cai INNER JOIN Forma_Pagamento ON Recebimento.id_form_pag_fk = Forma_Pagamento.id_form_pag;";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Recebimento, Caixa, Venda, Forma_Pagamento WHERE (Recebimento.id_cai_fk = Caixa.id_cai) AND (Recebimento.id_vend_fk = Venda.id_vend) AND (Recebimento.id_form_pag_fk = Forma_Pagamento.id_form_pag) AND (data_pag LIKE '%{busca}%');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Recebimento()
                    {
                        Id = reader.GetInt32("id_pag"),
                        Valor = DAOHelper.GetDouble(reader, "valor_pag"),
                        Data = DAOHelper.GetDateTime(reader, "data_pag"),
                        Hora = reader.GetTimeSpan("hora_pag"),
                        Caixa = DAOHelper.IsNull(reader, "id_cai_fk") ? null : new Caixa() { id = reader.GetInt32("id_cai"), Data = DAOHelper.GetDateTime(reader, "data_cai") },
                        Venda = DAOHelper.IsNull(reader, "id_vend_fk") ? null : new Venda() { Id = reader.GetInt32("id_desp"), Data = DAOHelper.GetDateTime(reader, "nome_desp") },
                        FormaPagamento = DAOHelper.IsNull(reader, "id_form_pag_fk") ? null : new FormaPagamento() { Id = reader.GetInt32("id_form_pag"), Nome = reader.GetString("nome_form_pag") }
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
    }
}