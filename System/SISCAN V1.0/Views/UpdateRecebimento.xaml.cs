using MySqlX.XDevAPI;
using SISCAN.Models;
using SISCAN.Views;
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
using SISCAN.Helpers;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para Cadastrar_Recebimento.xam
    /// </summary>
    public partial class UpdateRecebimento : Page
    {
        Recebimento recebimento = new Recebimento();
        public UpdateRecebimento(Recebimento receb)
        {
            InitializeComponent();

            DadosCbForm();
            DadosCbCai();
            recebimento = receb;
            ImportDados();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Recebimento receb = new Recebimento();

                receb.Id = recebimento.Id;
                if (tbValor.Text != "")
                {
                    receb.Valor = Convert.ToDouble(tbValor.Text);
                }
                else
                {
                    receb.Valor = recebimento.Valor;
                }
                if (dtpData.Text != "")
                {
                    receb.Data = dtpData.DisplayDate;
                }
                else
                {
                    receb.Data = recebimento.Data;
                }
                if (tmHora.Text != "")
                {
                    receb.Hora = DAOHelper.DateTimeToTimeSpan(tmHora.SelectedTime);
                }
                else
                {
                    receb.Hora = recebimento.Hora;
                }
                if (cbFormaPagamento.SelectedIndex != -1)
                {
                    receb.FormaPagamento = new FormaPagamento();
                    receb.FormaPagamento.Id = cbFormaPagamento.SelectedIndex + 1;
                }
                else
                {
                    receb.FormaPagamento = new FormaPagamento();
                    receb.FormaPagamento.Id = recebimento.FormaPagamento.Id;
                }
                if (cbCaixa.SelectedIndex != -1)
                {
                    receb.Caixa = new Caixa();
                    receb.Caixa.id = cbCaixa.SelectedIndex + 1;
                }
                else
                {
                    receb.Caixa = new Caixa();
                    receb.Caixa.id = recebimento.Caixa.id;
                }

                //Inserindo os Dados           
                RecebimentoDAO recebimentoDAO = new RecebimentoDAO();
                recebimentoDAO.Update(receb);

                Clear();
            }
            catch (Exception ex)
            {

            }
        }

        private void Clear()
        {
            tbValor.Clear();
            cbCaixa.SelectedIndex = -1;
            cbFormaPagamento.SelectedIndex = -1;
        }

        private void DadosCbForm()
        {
            FormaPagamentoDAO formaPagamentoDAO = new FormaPagamentoDAO();
            cbFormaPagamento.ItemsSource = formaPagamentoDAO.List();
            cbFormaPagamento.DisplayMemberPath = "Nome";
        }

        private void DadosCbCai()
        {
            CaixaDAO caixaDAO = new CaixaDAO();
            cbCaixa.ItemsSource = caixaDAO.List(null);
            cbCaixa.DisplayMemberPath = "Data";
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar a atualização?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Clear();
                MessageBox.Show("Campos limpos com sucesso!");
            }
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarRecebimento());
        }

        private void ImportDados()
        {
            lbFormaDePagamento.Content = "Forma de Pagametno: " + recebimento.FormaPagamento.Nome;
            lbCaixa.Content = "Caixa: " + recebimento.Caixa.id;
            lbHoraDeAbertura.Content = "Hora de Abertura: " + recebimento.Hora;
            lbValor.Content = "Valor: " + recebimento.Valor;
            lbData.Content = "Data: " + recebimento.Data;
        }
    }
}
