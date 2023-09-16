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
using MySqlX.XDevAPI;
using SISCAN.Models;
using SISCAN.Views;
using SISCAN.Helpers;
using System.Globalization;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarCliente.xam
    /// </summary>
    public partial class VenderProduto : Page
    {
        int quantidade;
        Venda venda = new Venda();
        VendaProduto vendaProduto = new VendaProduto();
        List<VendaProduto> listVendaProduto = new List<VendaProduto>();
        double valorTotal;
        public VenderProduto()
        {
            InitializeComponent();
            DadosCb();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

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
            
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            //fmFrame.Visibility = Visibility.Visible;
            //fmFrame.NavigationService.Navigate(new ListarCaixa());
        }

        private void DadosCb()
        {
            FuncionarioDAO funcDAO = new FuncionarioDAO();
            cbFuncionario.ItemsSource = funcDAO.List(null);
            cbFuncionario.DisplayMemberPath = "Nome";
            
            ProdutoDAO prodDAO = new ProdutoDAO();
            cbProduto.ItemsSource = prodDAO.List(null);
            cbProduto.DisplayMemberPath = "Nome";
        }

        private void btQuant_Click(object sender, RoutedEventArgs e)
        {
            quantidade += 1;
            tbQuantidade.Text = quantidade.ToString();
        }

        private void tbQuantidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            quantidade = Convert.ToInt32(tbQuantidade.Text);
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if((tbQuantidade != null) && (cbFuncionario.SelectedIndex != -1) && (cbProduto.SelectedIndex != -1))
            {
                vendaProduto.Produto = new Produto();
                vendaProduto.Venda = new Venda();
                vendaProduto.Venda.Funcionario = new Funcionario();
                if (cbProduto.SelectedItem is Produto selectedItemProd)
                {
                    vendaProduto.Produto.Nome = selectedItemProd.Nome;
                    vendaProduto.Produto.ValorVen = selectedItemProd.ValorVen;
                    vendaProduto.Venda.Valor = vendaProduto.Produto.ValorVen * Convert.ToDouble(tbQuantidade.Text);
                    valorTotal += vendaProduto.Venda.Valor;
                    CultureInfo cultura = new CultureInfo("pt-BR");
                    lbValorTotal.Content = $"Valor Total: {valorTotal.ToString("C")}";
                    if (cbFuncionario.SelectedItem is Funcionario selectedItemFunc)
                    {
                        vendaProduto.Venda.Funcionario.Nome = selectedItemFunc.Nome;
                        dgvList.Items.Add(vendaProduto);
                    }
                }
            }
        }
    }
}
