using SISCAN.Database;
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
                query.CommandText = $"INSERT INTO Fornecedor (razao_social_forn, cnpj_forn, bairro_forn, rua_forn, nome_fantasia_forn, telefone_forn, inscricao_estadual_forn, responsavel_forn, id_cid_fk) " +
                    $"VALUES (@razaoSocial, @cnpj, @bairro, @rua, @nomeFantasia, @telefone, @inscricaoEstadual, @responsavel, @id_cid)";

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
