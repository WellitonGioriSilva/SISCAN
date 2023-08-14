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
    internal class CaixaDAO
    {
        private static Conexao conn;
        public CaixaDAO()
        {
            conn = new Conexao();
        }
        public List<Caixa> List(string busca)
        {
            try
            {
                List<Caixa> list = new List<Caixa>();

                var query = conn.Query();
                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Caixa;";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Caixa WHERE (id_cai LIKE '%{busca}%');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Caixa()
                    {
                        id = reader.GetInt32("id_cai"),
                        Data = DAOHelper.GetDateTime(reader, "data_cai"),
                        HoraAbertura = reader.GetTimeSpan("hora_abertura_cai"),
                        HoraFechamento = reader.GetTimeSpan("hora_fechamento_cai"),
                        ValorIncial = DAOHelper.GetDouble(reader, "valor_inicial_cai"),
                        ValorFinal = DAOHelper.GetDouble(reader, "valor_final_cai")
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

        public void Insert(Caixa caixa)
        {
            try
            {
                //var caixaId = new CaixaDao().Insert(caixa.Caixa);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Caixa (data_cai, hora_abertura_cai, hora_fechamento_cai, valor_inicial_cai, valor_final_cai)" +
                    $"VALUES (@data, @hora_abertura, @hora_fechamento, @valor_inicial, @valor_final)";

                query.Parameters.AddWithValue("@data", caixa.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@hora_abertura", caixa.HoraAbertura);
                query.Parameters.AddWithValue("@hora_fechamento", caixa.HoraFechamento);
                query.Parameters.AddWithValue("@valor_inicial", caixa.ValorIncial);
                query.Parameters.AddWithValue("@valor_final", caixa.ValorFinal);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente");
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
