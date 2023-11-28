using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using SISCAN.Helpers;
using SISCAN.Models;
using SISCAN.Views;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarCliente.xam
    /// </summary>
    public partial class UpdateCliente : Page
    {
        Cliente user = new Cliente();
        public string cidade;
        public string estado;
        public UpdateCliente(Cliente cliente)
        {
            InitializeComponent();
            user = cliente;
            MaskCPF mascarador = new MaskCPF(tbCpf);
            ImportDados();          
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Id = cliente.Id;

                if (tbNome.Text != "")
                {
                    cliente.Nome = tbNome.Text;
                }
                else
                {
                    cliente.Nome = user.Nome;
                }
                if (tbCpf.Text != "")
                {
                    if (ValidacaoCPFeCNPJ.ValidateCPF(tbCpf.Text) == "Erro")
                    {
                        MessageBox.Show("Cpf digitado é inválido!");
                    }
                    else
                    {
                        cliente.Cpf = tbCpf.Text;
                    }
                }
                else
                {
                    cliente.Cpf = user.Cpf;
                }
                if (tbEmail.Text != "")
                {
                    cliente.Email = tbEmail.Text;
                }
                else
                {
                    cliente.Email = user.Email;
                }
                if (cbSexo.Text != "")
                {
                    cliente.Sexo = cbSexo.SelectionBoxItem.ToString();
                }
                else
                {
                    cliente.Sexo = user.Sexo;
                }
                if (dtpData.Text != "")
                {
                    cliente.DataNascimento = dtpData.SelectedDate;
                }
                else
                {
                    cliente.DataNascimento = user.DataNascimento;
                }
                if (tbRua.Text != "")
                {
                    cliente.Rua = tbRua.Text;
                }
                else
                {
                    cliente.Rua = user.Rua;
                }
                if (tbBairro.Text != "")
                {
                    cliente.Bairro = tbBairro.Text;
                }
                else
                {
                    cliente.Bairro = user.Bairro;
                }
                if (tbNumero.Text != "")
                {
                    cliente.Numero = Convert.ToInt16(tbNumero.Text);
                }
                else
                {
                    cliente.Numero = user.Numero;
                }
                if(tbCep.Text != "")
                {
                    cliente.cidade = cidade;
                    cliente.estado = estado;
                }
                else
                {
                    cliente.cidade = user.cidade;
                    cliente.estado = user.estado;
                }

                //Inserindo os Dados           
                ClienteDAO clienteDAO = new ClienteDAO();
                clienteDAO.Update(cliente);

                Clear();
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
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            //fmFrame.Visibility = Visibility.Visible;
            //fmFrame.NavigationService.Navigate(new ListarCliente());
        }

        private void ImportDados()
        {
            lbNome.Content = "Nome: " + user.Nome;
            lbCpf.Content = "Cpf: " + user.Cpf;
            lbemail.Content = "Email: " + user.Email;
            lbSexo.Content = "Sexo: " + user.Sexo;
            lbData.Content = "Data: " + user.DataNascimento;
            lbrua.Content = "Rua: " + user.Rua;
            lbBairro.Content = "Bairro: " + user.Bairro;
            lbnumero.Content = "Numero: " + user.Numero;
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
            if (tbCep.Text != "")
            {
                Buscar();
            }
        }

        private void tbCep_LostFocus(object sender, RoutedEventArgs e)
        {
            Buscar();
        }
    }
}
