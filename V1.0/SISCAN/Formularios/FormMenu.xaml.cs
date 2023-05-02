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
using System.Windows.Shapes;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Lógica interna para FormMenu.xaml
    /// </summary>
    public partial class FormMenu : Window
    {
        public FormMenu(MainWindow main)
        {
            InitializeComponent();

            main.Close();

            Frame.NavigationService.Navigate(new Menu());
        }

        private void btCliente_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Cliente";
            Frame.NavigationService.Navigate(new CadastrarCliente());
        }

        private void btDespesa_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Despesa";
            Frame.NavigationService.Navigate(new CadastrarDespesa());
        }

        private void btRecebimento_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Receber Recebimento";
            Frame.NavigationService.Navigate(new CadastrarRecebimento());
        }

        private void tbFuncao_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Função";
            Frame.NavigationService.Navigate(new CadastrarFuncao());
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Menu";
            Frame.NavigationService.Navigate(new Menu());
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbFornecedor_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Fornecedor";
            Frame.NavigationService.Navigate(new CadastrarFornecedor());
        }

        private void btProduto_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Produto";
            Frame.NavigationService.Navigate(new CadastrarProduto());
        }

        private void btFuncionario_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Funcionário";
            Frame.NavigationService.Navigate(new CadastrarFuncionario());
        }
    }
}
