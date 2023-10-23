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
        double valorTotal;
        Venda venda = new Venda();
        Recebimento recebimento = new Recebimento();
        VendaProduto vendaProduto = new VendaProduto();
        List<VendaProduto> listVendaProduto = new List<VendaProduto>();
        Funcionario funcionario;
        int idCaixa;
        public VenderProduto(Funcionario func, int idCaix)
        {
            InitializeComponent();
            DadosCb();
            funcionario = func;
            idCaixa = idCaix;
            tbQuantidade.Text = "0";
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((tbQuantidade != null) && (cbFormaPag.SelectedIndex != -1) && (cbProduto.SelectedIndex != -1))
            {
                //Instanciando Objetos
                recebimento = new Recebimento();
                recebimento.FormaPagamento = new FormaPagamento();
                recebimento.Caixa = new Caixa();
                vendaProduto = new VendaProduto();
                vendaProduto.Produto = new Produto();
                vendaProduto.Venda = new Venda();
                recebimento.Caixa = new Caixa();
                vendaProduto.Venda.Funcionario = new Funcionario();
                venda.Funcionario = new Funcionario();

                if(idCaixa > 0)
                {
                    if (cbFormaPag.SelectedItem is FormaPagamento selectedItemPag)
                    {
                        //Atribuindo a o cb o objeto Produto
                        if (cbProduto.SelectedItem is Produto selectedItemProd)
                        {
                            //Atribuindo valores aos objetos
                            vendaProduto.Produto.Id = selectedItemProd.Id;
                            vendaProduto.Produto.Nome = selectedItemProd.Nome;
                            recebimento.Caixa.id = idCaixa;
                            recebimento.FormaPagamento.Id = selectedItemPag.Id;

                            vendaProduto.Quantidade = Convert.ToInt32(tbQuantidade.Text);

                            vendaProduto.Produto.ValorVen = selectedItemProd.ValorVen;
                            vendaProduto.Venda.Valor = vendaProduto.Produto.ValorVen * Convert.ToDouble(tbQuantidade.Text);
                            valorTotal += vendaProduto.Venda.Valor;
                            lbValorTotal.Content = $"Valor Total: {valorTotal.ToString("C")}";

                            //Atribuindo aos lists e objtos os respectivos valores
                            vendaProduto.Venda.Funcionario.Nome = funcionario.Nome;
                            venda.Funcionario.Id = funcionario.Id;
                            venda.Valor = valorTotal;

                            dgvList.Items.Add(vendaProduto);
                            listVendaProduto.Add(vendaProduto);

                            ClearAdd();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum caixa registrado no sistema!");
                }
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VendaDAO vendaDAO = new VendaDAO();
                vendaDAO.Insert(venda, listVendaProduto, recebimento);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar a venda?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Clear();
                MessageBox.Show("Campos limpos com sucesso!");
            }
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.dgvList.SelectedIndex;
            listVendaProduto.RemoveAt(selectedIndex);
            dgvList.Items.RemoveAt(selectedIndex);
            valorTotal = 0;
            foreach (var item in dgvList.Items)
            {
                if (item is VendaProduto)
                {
                    var rowData = (VendaProduto)item;

                    var valor = rowData.Venda.Valor;

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
            cbCaixa.SelectedIndex = -1;
            cbFormaPag.SelectedIndex = -1;
            tbQuantidade.Text = "0";
        }
        
        private void Clear()
        {
            cbProduto.SelectedIndex = -1;
            cbCaixa.SelectedIndex = -1;
            cbFormaPag.SelectedIndex = -1;
            tbQuantidade.Text = "0";
            lbValorTotal.Content = "Valor Total:";
            dgvList.Items.Clear();
        }

        private void DadosCb()
        {            
            ProdutoDAO prodDAO = new ProdutoDAO();
            cbProduto.ItemsSource = prodDAO.List(null);
            cbProduto.DisplayMemberPath = "Nome";
            
            CaixaDAO caixaDAO = new CaixaDAO();
            cbCaixa.ItemsSource = caixaDAO.List(null);
            cbCaixa.DisplayMemberPath = "Data";
            
            FormaPagamentoDAO formaPagamentoDAO = new FormaPagamentoDAO();
            cbFormaPag.ItemsSource = formaPagamentoDAO.List();
            cbFormaPag.DisplayMemberPath = "Nome";
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
