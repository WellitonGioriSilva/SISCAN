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
    public partial class ComprarProduto : Page
    {
        int quantidade;
        double valorTotal;
        Despesa despesa = new Despesa();
        Estoque estoque = new Estoque();
        Compra compra = new Compra();
        CompraProduto compraProduto = new CompraProduto();
        List<CompraProduto> listCompraProduto = new List<CompraProduto>();
        Fornecedor fornecedor = new Fornecedor();
        public ComprarProduto()
        {
            InitializeComponent();
            DadosCb();
            tbQuantidade.Text = "0";
        }
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            quantidade = Convert.ToInt32(tbQuantidade.Text);

            if ((tbQuantidade.Text != null) && (cbProduto.SelectedIndex != -1))
            {
                if (cbFornecedor.SelectedItem is Fornecedor selectedItemForn)
                {
                    //Instanciando Objetos
                    compraProduto = new CompraProduto();
                    compraProduto.Produto = new Produto();
                    compraProduto.Compra = new Compra();
                    compraProduto.Compra.Fornecedor = new Fornecedor();
                    compra.Fornecedor = new Fornecedor();
                    despesa = new Despesa();
                    estoque = new Estoque();

                    //Atribuindo a o cb o objeto Produto
                    if (cbProduto.SelectedItem is Produto selectedItemProd)
                    {
                        //Atribuindo valores aos objetos
                        compra.Fornecedor.Id = selectedItemForn.Id;

                        compraProduto.Produto.Id = selectedItemProd.Id;
                        compraProduto.Produto.Nome = selectedItemProd.Nome;

                        compraProduto.Quantidade = Convert.ToInt32(tbQuantidade.Text);

                        compraProduto.Produto.Valor = selectedItemProd.Valor;
                        compraProduto.Compra.Valor = compraProduto.Produto.Valor * Convert.ToDouble(tbQuantidade.Text);
                        valorTotal += compraProduto.Compra.Valor;
                        lbValorTotal.Content = $"Valor Total: {valorTotal.ToString("C")}";

                        //Atribuindo aos lists e objtos os respectivos valores
                        compra.Valor = valorTotal;
                        despesa.Valor = valorTotal;
                        despesa.Data = dtpValidadeDesp.SelectedDate;
                        despesa.Parcelas = Convert.ToInt32(tbParcelas.Text);
                        if (tbParcelas.Text != "")
                        {
                            despesa.Status = "Aberta";
                        }
                        else
                        {
                            despesa.Status = "Fechada";
                        }
                        estoque.Validade = dtpValidadeProd.SelectedDate;
                        DateTime dataAtual = DateTime.Now;
                        estoque.Lote = compraProduto.Produto.Nome;

                        dgvList.Items.Add(compraProduto);
                        listCompraProduto.Add(compraProduto);

                        ClearAdd();
                    }
                }
            }
        }
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CompraDAO compraDAO = new CompraDAO();
                compraDAO.Insert(compra, listCompraProduto, despesa, estoque);
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
            cbFornecedor.SelectedIndex = -1;
            tbParcelas.Text = "0";
            tbQuantidade.Text = "0";
            lbValorTotal.Content = "Valor Total:";
            dgvList.Items.Clear();
        }

        private void DadosCb()
        {
            ProdutoDAO prodDAO = new ProdutoDAO();
            cbProduto.ItemsSource = prodDAO.List(null);
            cbProduto.DisplayMemberPath = "Nome";
            
            FornecedorDAO fornecedorDAO = new FornecedorDAO();
            cbFornecedor.ItemsSource = fornecedorDAO.List(null);
            cbFornecedor.DisplayMemberPath = "NomeFantasia";
        }

        private void btQuant_Click(object sender, RoutedEventArgs e)
        {
            quantidade += 1;
            tbQuantidade.Text = quantidade.ToString();
        }

        private void tbQuantidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}