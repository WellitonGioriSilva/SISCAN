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
            DadosCb();
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
                    cliente.Cidade = new Cidade();
                    if (cbCidade.SelectedItem is Cidade selectedItem)
                    {
                        cliente.Cidade.ID = selectedItem.ID;
                    }

                    //Inserindo os Dados           
                    ClienteDAO clienteDAO = new ClienteDAO();
                    clienteDAO.Insert(cliente);

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
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarCliente());
        }

        private void tbCpf_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaskCPFeCNPJ.MaskCPF(tbCpf);
        }
    }
}
