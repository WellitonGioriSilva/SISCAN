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
using MaterialDesignThemes.Wpf;
using MySqlX.XDevAPI;
using SISCAN.Models;
using SISCAN.Views;
using SISCAN.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Media.TextFormatting;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarCliente.xam
    /// </summary>
    public partial class CadastrarCliente : Page
    {
        public CadastrarCliente()
        {
            InitializeComponent();
            MaskCPF mascarador = new MaskCPF(tbCpf);
            MaskCEP mascaradorCEP = new MaskCEP(tbCep);
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidacaoCPFeCNPJ.ValidateCPF(tbCpf.Text) == "Erro")
                {
                    MessageBox.Show("Cpf digitado é inválido!");
                }
                else
                {
                    //Setando informações na tabela cliente
                    Cliente cliente = new Cliente();
                    cliente.Nome = tbNome.Text;
                    cliente.Cpf = tbCpf.Text;
                    cliente.Email = tbEmail.Text;
                    cliente.Sexo = cbSexo.SelectionBoxItem.ToString();
                    cliente.DataNascimento = dtpData.SelectedDate;
                    cliente.Rua = tbRua.Text;
                    cliente.Bairro = tbBairro.Text;
                    cliente.Numero = Convert.ToInt16(tbNumero.Text);

                    //Inserindo os Dados           
                    ClienteDAO clienteDAO = new ClienteDAO();
                    clienteDAO.Insert(cliente);
                    MessageBox.Show(clienteDAO.mensagem);
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Clear();
                MessageBox.Show("Campos limpos com sucesso!");
            }
        }

        private void Clear()
        {
            tbNome.Clear();
            tbCpf.Clear();
            tbEmail.Clear();
            tbRua.Clear();
            tbBairro.Clear();
            tbNumero.Clear();
            cbSexo.SelectedIndex = -1;
            tbCep.Clear();
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarCliente());
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
                Buscar();
            }
        }
    }
}
