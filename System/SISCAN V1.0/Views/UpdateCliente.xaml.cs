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

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarCliente.xam
    /// </summary>
    public partial class UpdateCliente : Page
    {
        Cliente user = new Cliente();
        public UpdateCliente(Cliente cliente)
        {
            InitializeComponent();
            user = cliente;
            DadosCb();
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
                if (tbCpf.Text != "")
                {
                    cliente.Cpf = tbCpf.Text;
                }
                else
                if (tbEmail.Text != "")
                {
                    cliente.Email = tbEmail.Text;
                }
                else
                if (cbSexo.Text != "")
                {
                    cliente.Sexo = cbSexo.SelectionBoxItem.ToString();
                }
                else
                if (dtpData.Text != "")
                {
                    cliente.DataNascimento = dtpData.SelectedDate;
                }
                else
                if (tbRua.Text != "")
                {
                    cliente.Rua = tbRua.Text;
                }
                else
                if (tbBairro.Text != "")
                {
                    cliente.Bairro = tbBairro.Text;
                }
                else
                if (tbNumero.Text != "")
                {
                    cliente.Numero = Convert.ToInt16(tbNumero.Text);
                }
                else
                {
                    cliente.Cidade = new Cidade();
                    cliente.Cidade.ID = cbCidade.SelectedIndex + 1;
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
            cbCidade.SelectedIndex = -1;
        }

        private void DadosCb()
        {
            CidadeDAO cidDAO = new CidadeDAO();
            cbCidade.ItemsSource = cidDAO.List();
            cbCidade.DisplayMemberPath = "Nome";
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            //fmFrame.Visibility = Visibility.Visible;
            //fmFrame.NavigationService.Navigate(new ListarCliente());
        }

        private void ImportDados()
        {
            tbNome.Text = "Nome: " + user.Nome;
            tbCpf.Text = "Cpf: " + user.Cpf;
            tbEmail.Text = "Email: " + user.Email;
            cbSexo.Text = "Sexo: " + user.Sexo;
            dtpData.Text = "Data: " + user.DataNascimento;
            tbRua.Text = "Rua: " + user.Rua;
            tbBairro.Text = "Bairro: " + user.Bairro;
            tbNumero.Text = "Numero: " + user.Numero;
        }

    }
}
