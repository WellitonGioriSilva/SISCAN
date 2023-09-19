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
    /// Interação lógica para Compra.xam
    /// </summary>
    public partial class Compra : Page
    {
        int quantidade;
        double valorTotal;
        Compra compra = new Compra();
        CompraProduto compraProduto = new CompraProduto();
        List<CompraProduto> listCompraProduto = new List<CompraProduto>();
        Fornecedor fornecedor;
        public Compra(Fornecedor forn)
        {
            InitializeComponent();
            DadosCb();
            fornecedor = forn;
            tbQuantidade.Text = "0";
        }
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((tbQuantidade != null) && (cbProduto.SelectedIndex != -1))
            {
                //Instanciando Objetos
                compraProduto = new CompraProduto();
                compraProduto.Produto = new Produto();
                compraProduto.Compra = new Compra();
                compraProduto.Compra.Fornecedor = new Fornecedor();
                compra.Fornecedor = new Fornecedor();

                //Atribuindo a o cb o objeto Produto
                if (cbProduto.SelectedItem is Produto selectedItemProd)
                {
                    //Atribuindo valores aos objetos
                    compraProduto.Produto.Id = selectedItemProd.Id;
                    compraProduto.Produto.Nome = selectedItemProd.Nome;

                    compraProduto.Quantidade = Convert.ToInt32(tbQuantidade.Text);

                    compraProduto.Produto.ValorCom = selectedItemProd.ValorCom;
                    compraProduto.Compra.Valor = compraProduto.Produto.ValorCom * Convert.ToDouble(tbQuantidade.Text);
                    valorTotal += compraProduto.Compra.Valor;
                    lbValorTotal.Content = $"Valor Total: {valorTotal.ToString("C")}";

                    //Atribuindo aos lists e objtos os respectivos valores
                    compraProduto.Compra.Fornecedor.Nome = fornecedor.Nome;
                    compra.Fornecedor.Id = fornecedor.Id;
                    compra.Valor = valorTotal;

                    dgvList.Items.Add(compraProduto);
                    listCompraProduto.Add(compraProduto);

                    ClearAdd();
                }
            }
        }
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CompraDAO compraDAO = new CompraDAO();
                compraDAO.Insert(compra, listCompraProduto);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar a compra?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Clear();
                MessageBox.Show("Campos limpos com sucesso!");
            }
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.dgvList.SelectedIndex;
            listCompraProduto.RemoveAt(selectedIndex);
            dgvList.Items.RemoveAt(selectedIndex);
            valorTotal = 0;
            foreach (var item in dgvList.Items)
            {
                if (item is CompraProduto)
                {
                    var rowData = (CompraProduto)item;

                    var valor = rowData.Compra.Valor;

                    valorTotal += valor;
                }
            }
            lbValorTotal.Content = $"Valor Total: {valorTotal.ToString("C")}";
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            //fmFrame.Visibility = Visibility.Visible;
            //fmFrame.NavigationService.Navigate(new ListarCaixa());
        }

        private void ClearAdd()
        {
            cbProduto.SelectedIndex = -1;
            tbQuantidade.Text = "0";
        }

        private void Clear()
        {
            cbProduto.SelectedIndex = -1;
            tbQuantidade.Text = "0";
            lbValorTotal.Content = "Valor Total:";
            dgvList.Items.Clear();
        }

        private void DadosCb()
        {
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
    }
}