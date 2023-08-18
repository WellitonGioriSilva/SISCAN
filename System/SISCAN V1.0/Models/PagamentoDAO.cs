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

        public List<Pagamento> List(string busca)
        {
            try
            {
                List<Pagamento> list = new List<Pagamento>();

                var query = conn.Query();

                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Pagamento LEFT JOIN Despesa ON Pagamento.id_desp_fk = Despesa.id_desp INNER JOIN Caixa ON Pagamento.id_cai_fk = Caixa.id_cai INNER JOIN Forma_Pagamento ON Pagamento.id_form_pag_fk = Forma_Pagamento.id_form_pag;";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Pagamento, Caixa, Despesa, Forma_Pagamento WHERE (Pagamento.id_cai_fk = Caixa.id_cai) AND (Pagamento.id_desp_fk = Despesa.id_desp) AND (Pagamento.id_form_pag_fk = Forma_Pagamento.id_form_pag) AND (data_pag LIKE '%{busca}%');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Pagamento()
                    {
                        Id = reader.GetInt32("id_pag"),
                        Valor = DAOHelper.GetDouble(reader, "valor_pag"),
                        Data = DAOHelper.GetDateTime(reader, "data_pag"),
                        Hora = reader.GetTimeSpan("hora_pag"),
                        Caixa = DAOHelper.IsNull(reader, "id_cai_fk") ? null : new Caixa() { id = reader.GetInt32("id_cai"), Data = DAOHelper.GetDateTime(reader, "data_cai") },
                        Despesa = DAOHelper.IsNull(reader, "id_desp_fk") ? null : new Despesa() { Id = reader.GetInt32("id_desp"), Nome = reader.GetString("nome_desp") },
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