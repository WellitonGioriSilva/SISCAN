using MySql.Data.MySqlClient;
using SISCAN.Database;
using SISCAN.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISCAN.Database;

namespace SISCAN.Models
{
    class FormaPagamentoDAO
    {
        private static Conexao conn;

        public FormaPagamentoDAO()
        {
            conn = new Conexao();
        }

        public List<FormaPagamento> List()
        {
            try
            {
                List<FormaPagamento> list = new List<FormaPagamento>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM Forma_Pagamento;";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new FormaPagamento()
                    {
                        Id = reader.GetInt32("id_form_pag"),
                        Nome = DAOHelper.GetString(reader, "nome_form_pag")
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
