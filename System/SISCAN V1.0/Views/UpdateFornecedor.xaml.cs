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
    public partial class UpdateFornecedor : Page
    {
        Fornecedor user = new Fornecedor();
        string cidade;
        string estado;
        public UpdateFornecedor(Fornecedor fornecedor)
        {
            InitializeComponent();
            user = fornecedor;
            ImportDados();
            MaskCNPJ mascarador = new MaskCNPJ(tbCnpj);
            MaskTelefone mascaradorTel = new MaskTelefone(tbTelefone);

        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Id = fornecedor.Id;

                if (tbRazaoSocial.Text != "")
                {
                    fornecedor.RazaoSocial = tbRazaoSocial.Text;
                }
                else
                {
                    fornecedor.RazaoSocial = user.RazaoSocial;
                }
                if (tbCnpj.Text != "")
                {
                    if (ValidacaoCPFeCNPJ.ValidateCNPJ(tbCnpj.Text) == "Erro")
                    {
                        MessageBox.Show("Cpf digitado é inválido!");
                    }
                    else
                    {
                        fornecedor.Cnpj = tbCnpj.Text;
                    }
                }
                else
                {
                    fornecedor.Cnpj = user.Cnpj;
                }
                if (tbBairro.Text != "")
                {
                    fornecedor.Bairro = tbBairro.Text;
                }
                else
                {
                    fornecedor.Bairro = user.Bairro;
                }
                if (tbRua.Text != "")
                {
                    fornecedor.Rua = tbRua.Text;
                }
                else
                {
                    fornecedor.Rua = user.Rua;
                }
                if (tbFantasia.Text != "")
                {
                    fornecedor.NomeFantasia = tbFantasia.Text;
                }
                else
                {
                    fornecedor.NomeFantasia = user.NomeFantasia;
                }
                if (tbTelefone.Text != "")
                {
                    fornecedor.Telefone = tbTelefone.Text;
                }
                else
                {
                    fornecedor.Telefone = user.Telefone;
                }
                if (tbInscricaoEstadual.Text != "")
                {
                    fornecedor.InscricaoEstadual = tbInscricaoEstadual.Text;
                }
                else
                {
                    fornecedor.InscricaoEstadual = user.InscricaoEstadual;
                }
                if (tbResponsavel.Text != "")
                {
                    fornecedor.Responsavel = tbResponsavel.Text;
                }
                else
                {
                    fornecedor.Responsavel = user.Responsavel;
                }
                if(tbCep.Text != "")
                {
                    fornecedor.cidade = cidade;
                    fornecedor.estado = estado;
                }
                else
                {
                    fornecedor.cidade = user.cidade;
                    fornecedor.estado = user.estado;
                }

                //Inserindo os Dados           
                FornecedorDAO fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.Update(fornecedor);

                Clear();
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

        private void ImportDados()
        {
            tbRazaoSocial.Text = "Razão Social: " + user.RazaoSocial;
            tbCnpj.Text = "Cnpj: " + user.Cnpj;
            tbBairro.Text = "Bairro: " + user.Bairro;
            tbRua.Text = "Rua: " + user.Rua;
            tbFantasia.Text = "Fantasia: " + user.NomeFantasia;
            tbTelefone.Text = "Telefone: " + user.Telefone;
            tbInscricaoEstadual.Text = "InscriçãoEstadual: " + user.InscricaoEstadual;
            tbResponsavel.Text = "Responsavel: " + user.Responsavel;
        }

        private async void tbCep_LostFocus(object sender, RoutedEventArgs e)
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
                                estado = endereco.Uf;
                                cidade = endereco.Localidade;
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
            if (tbCep.Text != "")
            {
                Buscar();
            }
        }
    }
}
