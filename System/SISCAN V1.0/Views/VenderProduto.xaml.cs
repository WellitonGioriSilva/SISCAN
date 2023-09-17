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
        VendaProduto vendaProduto = new VendaProduto();
        List<VendaProduto> listVendaProduto = new List<VendaProduto>();
        public VenderProduto()
        {
            InitializeComponent();
            DadosCb();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((tbQuantidade != null) && (cbFuncionario.SelectedIndex != -1) && (cbProduto.SelectedIndex != -1))
            {
                //Instanciando Objetos
                vendaProduto.Produto = new Produto();
                vendaProduto.Venda = new Venda();
                vendaProduto.Venda.Funcionario = new Funcionario();
                venda.Funcionario = new Funcionario();

                //Atribuindo a o cb o objeto Produto
                if (cbProduto.SelectedItem is Produto selectedItemProd)
                {
                    //Atribuindo valores aos objetos
                    vendaProduto.Produto.Id = selectedItemProd.Id;
                    vendaProduto.Produto.Nome = selectedItemProd.Nome;

                    vendaProduto.Quantidade = Convert.ToInt32(tbQuantidade.Text);

                    vendaProduto.Produto.ValorVen = selectedItemProd.ValorVen;
                    vendaProduto.Venda.Valor = vendaProduto.Produto.ValorVen * Convert.ToDouble(tbQuantidade.Text);
                    valorTotal += vendaProduto.Venda.Valor;
                    lbValorTotal.Content = $"Valor Total: {valorTotal.ToString("C")}";

                    //Atribuindo a o cb o objeto Funcionário
                    if (cbFuncionario.SelectedItem is Funcionario selectedItemFunc)
                    {
                        //Atribuindo aos lists e objtos os respectivos valores
                        vendaProduto.Venda.Funcionario.Nome = selectedItemFunc.Nome;
                        venda.Funcionario.Id = selectedItemFunc.Id;
                        venda.Valor = valorTotal;

                        dgvList.Items.Add(vendaProduto);
                        listVendaProduto.Add(vendaProduto);

                        ClearAdd();
                    }
                }
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VendaDAO vendaDAO = new VendaDAO();
                vendaDAO.Insert(venda, listVendaProduto);
                ClearVenda();
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
                //Clear();
                MessageBox.Show("Campos limpos com sucesso!");
            }
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
        
        private void ClearVenda()
        {
            cbProduto.SelectedIndex = -1;
            cbFuncionario.SelectedIndex = -1;
            tbQuantidade.Text = "0";
            lbValorTotal.Content = "Valor Total:";
            dgvList.Items.Clear();
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
    }
}
