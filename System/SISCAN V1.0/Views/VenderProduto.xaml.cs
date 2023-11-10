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
        
        List<VendaProduto> listVendaProduto = new List<VendaProduto>();

        Recebimento recebimento = new Recebimento();

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

                //vendaProduto = new VendaProduto();
                //vendaProduto.Produto = new Produto();
                //vendaProduto.Venda = new Venda();
                //recebimento.Caixa = new Caixa();
                //vendaProduto.Venda.Funcionario = new Funcionario();
                //venda.Funcionario = new Funcionario();

                if (idCaixa > 0)
                {
                    if (cbFormaPag.SelectedItem is FormaPagamento selectedItemPag)
                    {
                        //Atribuindo a o cb o objeto Produto
                        if (cbProduto.SelectedItem is Produto selectedItemProd)
                        {
                            if (cbCliente.SelectedItem is Cliente selectedItemCli)
                            {

                                recebimento.Caixa.id = idCaixa;
                                recebimento.FormaPagamento.Id = selectedItemPag.Id;

                                VendaProduto vendaProduto = new VendaProduto();

                                //Atribuindo valores aos objetos
                                vendaProduto.Produto = selectedItemProd;
                                vendaProduto.Quantidade = Convert.ToInt32(tbQuantidade.Text);

                                vendaProduto.Produto.ValorVen = selectedItemProd.ValorVen;
                                //vendaProduto.Venda.Valor = vendaProduto.Produto.ValorVen * Convert.ToDouble(tbQuantidade.Text);
                                valorTotal += vendaProduto.Produto.ValorVen * Convert.ToDouble(tbQuantidade.Text);

                                lbValorTotal.Content = $"Valor Total: {valorTotal.ToString("C")}";

                                //Atribuindo aos lists e objtos os respectivos valores
                                //vendaProduto.Venda.Funcionario.Nome = funcionario.Nome;
                               
                                venda.Funcionario = funcionario;
                                venda.Valor = valorTotal;
                                venda.Cliente = selectedItemCli;
                                venda.Items.Add(vendaProduto);

                                dgvList.Items.Add(vendaProduto);
                                listVendaProduto.Add(vendaProduto);

                                ClearAdd();
                            }
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
                MessageBox.Show(vendaDAO.mensagem);
                if (vendaDAO.condicao == true)
                {
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
                //if (item is VendaProduto)
                //{
                //    var rowData = (VendaProduto)item;

                //    var valor = rowData.Valor;

                //    valorTotal += valor;
                //}
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
            cbCliente.SelectedIndex = -1;
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
            
            ClienteDAO clienteDAO = new ClienteDAO();
            cbCliente.ItemsSource = clienteDAO.List(null);
            cbCliente.DisplayMemberPath = "Nome";
            
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
