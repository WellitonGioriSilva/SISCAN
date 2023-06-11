using MySql.Data.MySqlClient;
using SISCAN.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SISCAN.Helpers;

namespace SISCAN.Models
{
    class CidadeDAO
    {
        private static Conexao conn;

        public CidadeDAO()
        {
            conn = new Conexao();
        }

        public List<Cidade> List()
        {
            try
            {
                List<Cidade> list = new List<Cidade>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM Cidade, Estado WHERE(Estado.id_est = Cidade.id_est_fk);";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Cidade()
                    {
                        ID = reader.GetInt32("id_cid"),
                        Nome = DAOHelper.GetString(reader, "nome_cid")
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
