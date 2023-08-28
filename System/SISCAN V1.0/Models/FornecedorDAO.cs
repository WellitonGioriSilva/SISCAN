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
    class FornecedorDAO
    {
        private static Conexao conn;

        public FornecedorDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Fornecedor fornecedor)
        {
            try
            {
                //var cidadeId = new CidadeDAO().Insert(cliente.Cidade);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Fornecedor (visivel_forn, razao_social_forn, cnpj_forn, bairro_forn, rua_forn, nome_fantasia_forn, telefone_forn, inscricao_estadual_forn, responsavel_forn, id_cid_fk) " +
                    $"VALUES ('Sim', @razaoSocial, @cnpj, @bairro, @rua, @nomeFantasia, @telefone, @inscricaoEstadual, @responsavel, @id_cid)";

                query.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
                query.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                query.Parameters.AddWithValue("@bairro", fornecedor.Bairro);
                query.Parameters.AddWithValue("@rua", fornecedor.Rua);
                query.Parameters.AddWithValue("@nomeFantasia", fornecedor.NomeFantasia);
                query.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                query.Parameters.AddWithValue("@inscricaoEstadual", fornecedor.InscricaoEstadual);
                query.Parameters.AddWithValue("@responsavel", fornecedor.Responsavel);
                query.Parameters.AddWithValue("@id_cid", fornecedor.Cidade.ID);

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

        public List<Fornecedor> List(string busca)
        {
            try
            {
                List<Fornecedor> list = new List<Fornecedor>();

                var query = conn.Query();

                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Fornecedor LEFT JOIN Cidade ON Fornecedor.id_cid_fk = Cidade.id_cid LEFT JOIN Estado ON Cidade.id_est_fk = Estado.id_est WHERE visivel_forn = 'Sim';";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Fornecedor, Cidade, Estado WHERE (Fornecedor.id_cid_fk = Cidade.id_cid) AND (Cidade.id_est_fk = Estado.id_est) AND (razao_social_forn LIKE '%{busca}%') AND (visivel_forn = 'Sim');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Fornecedor()
                    {
                        Id = reader.GetInt32("id_forn"),
                        RazaoSocial = DAOHelper.GetString(reader, "razao_social_forn"),
                        Cnpj = DAOHelper.GetString(reader, "cnpj_forn"),
                        Bairro = DAOHelper.GetString(reader, "bairro_forn"),
                        Rua = DAOHelper.GetString(reader, "rua_forn"),
                        NomeFantasia = DAOHelper.GetString(reader, "nome_fantasia_forn"),
                        Telefone = DAOHelper.GetString(reader, "telefone_forn"),
                        InscricaoEstadual = DAOHelper.GetString(reader, "inscricao_estadual_forn"),
                        Responsavel = DAOHelper.GetString(reader, "responsavel_forn"),
                        Cidade = DAOHelper.IsNull(reader, "id_cid_fk") ? null : new Cidade() { ID = reader.GetInt32("id_cid"), Nome = reader.GetString("nome_cid"), Estado = DAOHelper.IsNull(reader, "id_est_fk") ? null : new Estado() { Id = reader.GetInt32("id_est"), Nome = reader.GetString("nome_est") } }
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

        public void Delete(Fornecedor fornecedor)
        {

            MessageBoxResult resultado = MessageBox.Show("Deseja deletar esse dado?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = $"UPDATE Fornecedor SET visivel_forn = 'Nao' WHERE id_forn = @id;";

                    query.Parameters.AddWithValue("@id", fornecedor.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao deletar os dados, verifique e tente novamente");
                    }
                    else
                    {
                        MessageBox.Show("Dados deletados com sucesso!");
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
}
