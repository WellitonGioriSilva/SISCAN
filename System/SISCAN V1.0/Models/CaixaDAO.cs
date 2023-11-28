using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using SISCAN.Database;
using SISCAN.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection.PortableExecutable;
using Microsoft.Win32;

namespace SISCAN.Models
{
    internal class CaixaDAO
    {
        private static Conexao conn;
        public double valorIni;
        public string mensagem;
        public bool condicao;
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
                    query.CommandText = "SELECT * FROM Caixa WHERE visivel_cai = 'Sim';";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Caixa WHERE (id_cai LIKE '%{busca}%') AND (visivel_cai = 'Sim');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Caixa()
                    {
                        id = reader.GetInt32("id_cai"),
                        Data = DAOHelper.GetDateTime(reader, "data_cai"),
                        HoraAbertura = reader.GetTimeSpan("hora_abertura_cai"),
                        ValorIncial = DAOHelper.GetDouble(reader, "valor_inicial_cai")
                    });
                }

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Caixa> ListFec(string busca)
        {
            try
            {
                List<Caixa> list = new List<Caixa>();

                var query = conn.Query();
                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Caixa WHERE visivel_cai = 'Sim';";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Caixa WHERE (id_cai LIKE '%{busca}%') AND (visivel_cai = 'Sim');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Caixa()
                    {
                        id = reader.GetInt32("id_cai"),
                        Data = DAOHelper.GetDateTime(reader, "data_cai"),
                        HoraAbertura = reader.GetTimeSpan("hora_abertura_cai"),
                        ValorIncial = DAOHelper.GetDouble(reader, "valor_inicial_cai"),
                        HoraFechamento = reader.GetTimeSpan("hora_fechamento_cai"),
                        ValorFinal = DAOHelper.GetDouble(reader, "valor_final_cai")
                    });
                }

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Insert(Caixa caixa, int IdFunc)
        {
            try
            {
                conn = new Conexao();
                //var caixaId = new CaixaDao().Insert(caixa.Caixa);

                var query = conn.Query();
                query.CommandText = $"CALL InsertCaixa(@valor_inicial, @id_func)";

                query.Parameters.AddWithValue("@valor_inicial", caixa.ValorIncial);
                query.Parameters.AddWithValue("@id_func", IdFunc);

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
        }

        public void Update(Caixa caixa)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Caixa SET data_cai = @data, hora_abertura_cai = @hora_abertura, hora_fechamento_cai = @hora_fechamento, " +
                    "valor_inicial_cai = @valor_inicial, valor_final_cai = @valor_final WHERE id_cai = @id";

                query.Parameters.AddWithValue("@id", caixa.id);
                query.Parameters.AddWithValue("@data", caixa.Data);
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
                    MessageBox.Show("Dados atualizados com sucesso!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte!");
            }
        }
        public void Delete(Caixa caixa)
        {

            MessageBoxResult resultado = MessageBox.Show("Deseja deletar esse dado?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = $"UPDATE Caixa SET visivel_cai = 'Nao' WHERE id_cai = @id;";

                    query.Parameters.AddWithValue("@id", caixa.id);

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
            }
        }

        public int GetById()
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"CALL GetByIdCaixa(@valor, @result)";
                query.Parameters.Add(new MySqlParameter("@result", MySqlDbType.VarChar));
                query.Parameters["@result"].Direction = System.Data.ParameterDirection.Output;
                
                query.Parameters.Add(new MySqlParameter("@valor", MySqlDbType.VarChar));
                query.Parameters["@valor"].Direction = System.Data.ParameterDirection.Output;

                query.ExecuteNonQuery();

                valorIni = Convert.ToDouble(query.Parameters["@valor"].Value);

                int id = Convert.ToInt32(query.Parameters["@result"].Value);

                return id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public void CaixaFechamento(Caixa caixa)
        {
            try
            {
                conn = new Conexao();
                var query = conn.Query();
                query.CommandText = $"CALL FechamentoCaixa(@id, @valorFinal)";

                query.Parameters.AddWithValue("@valorFinal", caixa.ValorFinal);
                query.Parameters.AddWithValue("@id", caixa.id);

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
        }

        public void Extrato(int busca)
        {
            try
            {
                // Crie um diálogo de salvamento de arquivo
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Arquivo PDF|*.pdf";
                saveFileDialog.FileName = "extrato.pdf";

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Crie um novo documento PDF
                    Document doc = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));

                    doc.Open();

                    // Adicione a primeira seção com título
                    AddCaixa(doc, "Caixa", busca);

                    // Adicione a segunda seção com título
                    AddVendas(doc, "Vendas", busca);

                    doc.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
        }

        private void AddCaixa(Document doc, string sectionTitle, int busca)
        {
            // Adicione o título da seção
            doc.Add(new Paragraph(sectionTitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));

            var query = conn.Query();
            query.CommandText = $"CALL ExtratoCaixa(@id)";
            query.Parameters.AddWithValue("@id", busca);
            MySqlDataReader reader = query.ExecuteReader();

            // Crie uma tabela no PDF para armazenar os dados
            PdfPTable table = new PdfPTable(reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                table.AddCell(new PdfPCell(new Phrase(reader.GetName(i))));
            }

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    table.AddCell(new PdfPCell(new Phrase(reader[i].ToString())));
                }
            }

            doc.Add(table);

            // Adicione uma quebra de página entre as seções
            doc.NewPage();

            reader.Close();
        }
        private void AddVendas(Document doc, string sectionTitle, int busca)
        {
            // Adicione o título da seção
            doc.Add(new Paragraph(sectionTitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));

            var query = conn.Query();
            query.CommandText = $"CALL ExtratoVendas(@id)";
            query.Parameters.AddWithValue("@id", busca);
            MySqlDataReader reader = query.ExecuteReader();

            // Crie uma tabela no PDF para armazenar os dados
            PdfPTable table = new PdfPTable(reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                table.AddCell(new PdfPCell(new Phrase(reader.GetName(i))));
            }

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    table.AddCell(new PdfPCell(new Phrase(reader[i].ToString())));
                }
            }

            doc.Add(table);

            // Adicione uma quebra de página entre as seções
            doc.NewPage();

            reader.Close();
        }
    }
}
