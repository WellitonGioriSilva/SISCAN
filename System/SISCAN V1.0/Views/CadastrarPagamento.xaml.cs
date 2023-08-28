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
        public CadastrarPagamento()
        {
            InitializeComponent();

            DadosCbForm();
            DadosCbCai();
            DadosCbDesp();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela cliente
                Pagamento pagamento = new Pagamento();
                pagamento.Valor = Convert.ToDouble(tbValor.Text);
                pagamento.Hora = DAOHelper.DateTimeToTimeSpan(tmHora.SelectedTime);
                pagamento.Data = dtpData.SelectedDate;
                pagamento.Caixa = new Caixa();
                if (cbCaixa.SelectedItem is Caixa selectedItemCai)
                {
                    pagamento.Caixa.id = selectedItemCai.id;
                }
                pagamento.FormaPagamento = new FormaPagamento();
                if (cbFormapag.SelectedItem is FormaPagamento selectedItemForm)
                {
                    pagamento.FormaPagamento.Id = selectedItemForm.Id;
                }
                pagamento.Despesa = new Despesa();
                if (cbDespesa.SelectedItem is Despesa selectedItemDesp)
                {
                    pagamento.Despesa.Id = selectedItemDesp.Id;
                }
                //Inserindo os Dados           
                PagamentoDAO pagamentoDAO = new PagamentoDAO();
                pagamentoDAO.Insert(pagamento);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void Clear()
        {
            tbValor.Clear();
            cbCaixa.SelectedIndex = -1;
            cbFormapag.SelectedIndex = -1;
            cbDespesa.SelectedIndex = -1;
        }

        private void DadosCbForm()
        {
            FormaPagamentoDAO formaPagamentoDAO = new FormaPagamentoDAO();
            cbFormapag.ItemsSource = formaPagamentoDAO.List();
            cbFormapag.DisplayMemberPath = "Nome";
        }

        private void DadosCbCai()
        {
            CaixaDAO caixaDAO = new CaixaDAO();
            cbCaixa.ItemsSource = caixaDAO.List(null);
            cbCaixa.DisplayMemberPath = "Data";
        }
        
        private void DadosCbDesp()
        {
            DespesaDAO despesaDAO = new DespesaDAO();
            cbDespesa.ItemsSource = despesaDAO.List(null);
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
    }
}
