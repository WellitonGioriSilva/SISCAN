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
    internal class DespesaDAO
    {
        private static Conexao conn;

        public DespesaDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Despesa despesa)
        {
            try
            {
                var query = conn.Query();
                //query.CommandText = $"INSERT INTO Despesa (nome_desp, parcelas_desp, valor_desp, data_desp, status_desp, vencimento_desp, id_comp_fk)" +
                    //$"VALUES (@nome, @parcelas, @valor, @data, @status, @vencimento, @id_comp)";
                
                query.CommandText = $"INSERT INTO Despesa (nome_desp, parcelas_desp, valor_desp, data_desp, status_desp, vencimento_desp)" +
                    $"VALUES (@nome, @parcelas, @valor, @data, @status, @vencimento)";

                query.Parameters.AddWithValue("@nome", despesa.Nome);
                query.Parameters.AddWithValue("@parcelas", despesa.Parcelas);
                query.Parameters.AddWithValue("@valor", despesa.Valor);
                query.Parameters.AddWithValue("@status", despesa.Status);
                query.Parameters.AddWithValue("@data", despesa.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@vencimento", despesa.Vencimento?.ToString("yyyy-MM-dd"));
                //query.Parameters.AddWithValue("@id_comp", despesa.Compra.Id);

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

        public List<Despesa> List(string busca)
        {
            try
            {
                List<Despesa> list = new List<Despesa>();

                var query = conn.Query();

                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Despesa LEFT JOIN Compra ON Despesa.id_com_fk = Compra.id_com;";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Despesa, Compra WHERE (Despesa.id_com_fk = Compra.id_com) AND (nome_desp LIKE '%{busca}%');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Despesa()
                    {
                        Id = reader.GetInt32("id_desp"),
                        Nome = DAOHelper.GetString(reader, "nome_desp"),
                        Parcelas = reader.GetInt32("parcelas_desp"),
                        Valor = DAOHelper.GetDouble(reader, "valor_desp"),
                        Status = DAOHelper.GetString(reader, "status_desp"),
                        Data = DAOHelper.GetDateTime(reader, "data_desp"),
                        Vencimento = DAOHelper.GetDateTime(reader, "vencimento_desp"),
                        //Estado = DAOHelper.IsNull(reader, "id_est_fk") ? null : new Estado() { Id = reader.GetInt32("id_est"), Nome = reader.GetString("nome_est") }
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
