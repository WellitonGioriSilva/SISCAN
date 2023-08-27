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
using SISCAN.Formularios;
using SISCAN.Views;
using SISCAN.Helpers;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarProduto.xam
    /// </summary>
    public partial class UpdateProduto : Page
    {
        Produto produto = new Produto();
        public UpdateProduto(Produto prod)
        {
            InitializeComponent();     
            produto = prod;
            ImportDados();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Produto prod = new Produto();

                prod.Id = produto.Id;
                if (tbNome.Text != "")
                {
                    prod.Nome = tbNome.Text;
                }
                else
                {
                    prod.Nome = produto.Nome;
                }
                if (tbMarca.Text != "")
                {
                    prod.Marca = tbMarca.Text;
                }
                else
                {
                    prod.Marca = produto.Marca;
                }
                if (cbTipo.SelectedIndex != -1)
                {
                    prod.Tipo = cbTipo.SelectionBoxItem.ToString();
                }
                else
                {
                    prod.Tipo = produto.Tipo;
                }

                //Inserindo os Dados           
                ProdutoDAO produtoDAO = new ProdutoDAO();
                produtoDAO.Update(prod);

                Clear();
            }
            catch (Exception ex)
            {

            }
        }

        private void Clear()
        {
            tbNome.Clear();
            tbMarca.Clear();
            cbTipo.SelectedIndex = -1;
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

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarProduto());
        }


        private void ImportDados()
        {
            lbNome.Content = "Nome: " + produto.Nome;
            lbTipo.Content = "Tipo: " + produto.Tipo;
            lbMarca.Content = "Marca: " + produto.Marca;
        }
    }
}
