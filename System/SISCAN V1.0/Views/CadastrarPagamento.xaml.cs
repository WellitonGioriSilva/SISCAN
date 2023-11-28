using MySqlX.XDevAPI;
using SISCAN.Helpers;
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

namespace SISCAN.Views
{
    /// <summary>
    /// Interação lógica para CadastrarPagamento.xam
    /// </summary>
    public partial class CadastrarPagamento : Page
    {
        int parcelas = 0;
        int parcelasSelecionadas = 0;
        int idCaixa;
        public CadastrarPagamento(int idCai)
        {
            InitializeComponent();

            DadosCbForm();
            DadosCbDesp();
            idCaixa = idCai;

            dtpData.Visibility = Visibility.Collapsed;
            borderDtp.Visibility = Visibility.Collapsed;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idCaixa != 0)
                {
                    if (cbDespesa.SelectedItem is Despesa selectedItemDespesa)
                    {
                        //Setando informações na tabela cliente
                        Pagamento pagamento = new Pagamento();
                        Despesa despesa = new Despesa();

                        //          
                        pagamento.Caixa = new Caixa();
                        pagamento.Caixa.id = idCaixa;
                        pagamento.FormaPagamento = new FormaPagamento();
                        if (cbFormapag.SelectedItem is FormaPagamento selectedItemForm)
                        {
                            pagamento.FormaPagamento.Id = selectedItemForm.Id;
                        }
                        pagamento.Despesa = new Despesa();
                        if (cbDespesa.SelectedItem is Despesa selectedItemDesp)
                        {
                            pagamento.Despesa.Id = selectedItemDesp.Id;
                            string valorTexto = tbValor.Text.Replace("R$", "").Replace(" ", "");

                            pagamento.Valor = Convert.ToDouble(valorTexto);

                            //MessageBox.Show(pagamento.Despesa.Id.ToString());
                            despesa.Data = dtpData.SelectedDate;
                        }

                        //Inserindo os Dados           
                        PagamentoDAO pagamentoDAO = new PagamentoDAO();
                        pagamentoDAO.Insert(pagamento, despesa, parcelas);
                        MessageBox.Show(pagamentoDAO.mensagem);
                        if (pagamentoDAO.condicao == true)
                        {
                            Clear();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum caixa aberto no sistema!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void Clear()
        {
            tbValor.Clear();
            cbFormapag.SelectedIndex = -1;
            cbDespesa.SelectedIndex = -1;
        }

        private void DadosCbForm()
        {
            FormaPagamentoDAO formaPagamentoDAO = new FormaPagamentoDAO();
            cbFormapag.ItemsSource = formaPagamentoDAO.List();
            cbFormapag.DisplayMemberPath = "Nome";
        }
        
        private void DadosCbDesp()
        {
            DespesaDAO despesaDAO = new DespesaDAO();
            cbDespesa.ItemsSource = despesaDAO.ListPag();
            cbDespesa.DisplayMemberPath = "Nome";
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
            fmFrame.NavigationService.Navigate(new ListarPagamento());
        }

        private void cbDespesa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbDespesa.SelectedItem is Despesa selectedItemDesp)
            {
                parcelas = selectedItemDesp.Parcelas;
                cbParcelas.Items.Clear();

                for (int i = 1; i <= parcelas; i++)
                {
                    cbParcelas.Items.Add(i);
                }
            }
        }

        private void cbParcelas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDespesa.SelectedItem is Despesa selectedItemDesp)
            {
                parcelasSelecionadas = Convert.ToInt32(cbParcelas.SelectedValue.ToString());
                tbValor.Text = (selectedItemDesp.ValorParcela * parcelasSelecionadas).ToString("C");
                if (cbParcelas.SelectedValue.ToString() != parcelas.ToString())
                {
                    dtpData.Visibility = Visibility.Visible;
                    borderDtp.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
