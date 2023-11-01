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
        public string mensagem;
        public bool condicao;
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
                query.CommandText = $"CALL InsertFornecedor(@razaoSocial, @cnpj, @bairro, @rua, @nomeFantasia, @telefone, @inscricaoEstadual, @responsavel, @cidade, @estado)";

                query.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
                query.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                query.Parameters.AddWithValue("@bairro", fornecedor.Bairro);
                query.Parameters.AddWithValue("@rua", fornecedor.Rua);
                query.Parameters.AddWithValue("@nomeFantasia", fornecedor.NomeFantasia);
                query.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                query.Parameters.AddWithValue("@inscricaoEstadual", fornecedor.InscricaoEstadual);
                query.Parameters.AddWithValue("@responsavel", fornecedor.Responsavel);
                query.Parameters.AddWithValue("@cidade", fornecedor.cidade);
                query.Parameters.AddWithValue("@estado", fornecedor.estado);

                MySqlDataReader reader = query.ExecuteReader();
                if (reader.Read())
                {
                    mensagem = reader.GetString(0); // Pega o primeiro campo, que é a string
                    condicao = reader.GetBoolean(1); // Pega o segundo campo, que é o boolean
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
                    query.CommandText = "SELECT * FROM Fornecedor WHERE (visivel_forn = 'Sim');";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Fornecedor WHERE (razao_social_forn LIKE '%{busca}%') AND (visivel_forn = 'Sim');";
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
                        cidade = DAOHelper.GetString(reader, "cidade_forn"),
                        estado = DAOHelper.GetString(reader, "estado_forn"),
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

        public void Update(Fornecedor fornecedor)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Fornecedor SET razao_social_forn = @razao_social, cnpj_forn = @cnpj, bairro_forn = @bairro, rua_forn = @rua, nome_fantasia_forn = @nome_fantasia, telefone_forn = @telefone" +
                    "inscricao_estadual_forn = @incricao_estadual, responsavel_forn = @responsavel, cidade_forn = @cidade, estado_forn = @estado WHERE id_forn = @id";

                query.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
                query.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                query.Parameters.AddWithValue("@bairro", fornecedor.Bairro);
                query.Parameters.AddWithValue("@rua", fornecedor.Rua);
                query.Parameters.AddWithValue("@nomeFantasia", fornecedor.NomeFantasia);
                query.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                query.Parameters.AddWithValue("@inscricaoEstadual", fornecedor.InscricaoEstadual);
                query.Parameters.AddWithValue("@responsavel", fornecedor.Responsavel);
                query.Parameters.AddWithValue("@cidade", fornecedor.cidade);
                query.Parameters.AddWithValue("@estado", fornecedor.cidade);
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
                MessageBox.Show("Erro 3008 : Contate o suporte!");
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
