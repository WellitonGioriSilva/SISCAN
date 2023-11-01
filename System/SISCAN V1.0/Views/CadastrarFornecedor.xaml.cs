using SISCAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SISCAN.Models;
using SISCAN.Views;
using SISCAN.Helpers;
using Newtonsoft.Json;
using System.Net.Http;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarFornecedor.xam
    /// </summary>
    public partial class CadastrarFornecedor : Page
    {
        public string estado;
        public string cidade;
        public CadastrarFornecedor()
        {
            InitializeComponent();

            MaskCNPJ mascarador = new MaskCNPJ(tbCnpj);
            MaskTelefone mascaradorTel = new MaskTelefone(tbTelefone);
            MaskCEP mascaradorCEP = new MaskCEP(tbCep);
            tbBairro.IsEnabled = false;
            tbRua.IsEnabled = false;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbRua.Text != "" && tbBairro.Text != "")
                {
                    if (ValidacaoCPFeCNPJ.ValidateCNPJ(tbCnpj.Text) == "Erro")
                    {
                        MessageBox.Show("Cpf digitado é inválido!");
                    }
                    else
                    {
                        //Setando informações na tabela fornecedor
                        Fornecedor fornecedor = new Fornecedor();
                        fornecedor.RazaoSocial = tbRazaoSocial.Text;
                        fornecedor.Cnpj = tbCnpj.Text;
                        fornecedor.Bairro = tbBairro.Text;
                        fornecedor.Rua = tbRua.Text;
                        fornecedor.NomeFantasia = tbFantasia.Text;
                        fornecedor.Telefone = tbTelefone.Text;
                        fornecedor.InscricaoEstadual = tbInscricaoEstadual.Text;
                        fornecedor.Responsavel = tbResponsavel.Text;
                        fornecedor.cidade = cidade;
                        fornecedor.estado = estado;

                        //Inserindo os Dados           
                        FornecedorDAO fornecedorDAO = new FornecedorDAO();
                        fornecedorDAO.Insert(fornecedor);
                        MessageBox.Show(fornecedorDAO.mensagem);

                        Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Aguarde à consulta ao cep!");
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }
        private void Clear()
        {
            tbRazaoSocial.Clear();
            tbCnpj.Clear();
            tbBairro.Clear();
            tbRua.Clear();
            tbFantasia.Clear();
            tbTelefone.Clear();
            tbInscricaoEstadual.Clear();
            tbResponsavel.Clear();
            tbCep.Clear();
        }

        private void tbCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Clear();
                MessageBox.Show("Campos limpos com sucesso!");
            }
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarFornecedor());
        }

        private void tbCep_LostFocus(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        public async void Buscar()
        {
            string cep = tbCep.Text;
            if (!string.IsNullOrEmpty(cep))
            {
                string url = $"https://viacep.com.br/ws/{cep}/json/";

                using (HttpClient client = new HttpClient())
                {
                    if (cep.Length == 9)
                    {
                        try
                        {
                            string response = await client.GetStringAsync(url);
                            var endereco = JsonConvert.DeserializeObject<Endereco>(response);

                            if (endereco != null)
                            {
                                tbRua.Text = endereco.Logradouro;
                                tbBairro.Text = endereco.Bairro;
                                cidade = endereco.Localidade;
                                estado = endereco.Uf;
                            }
                            else
                            {
                                MessageBox.Show("CEP não encontrado.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao buscar CEP: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira um CEP válido.");
            }
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(tbCep.Text != "")
            {
                Buscar() ;
            }
        }

        private void tbCep_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoSemMascara = new string(tbCep.Text.Where(char.IsDigit).ToArray());
            if (textoSemMascara.Length >= 7)
            {
                Buscar();
            }
        }
    }
}
