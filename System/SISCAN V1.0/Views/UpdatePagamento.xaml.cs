﻿using Org.BouncyCastle.Crypto;
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
    public partial class UpdatePagamento : Page
    {
        Pagamento pagamento = new Pagamento();
        int parcelas = 0;
        int parcelasSelecionadas = 0;
        int idCaixa;
        public UpdatePagamento(Pagamento pag)
        {
            InitializeComponent();

            DadosCbForm();
            DadosCbDesp();
            pagamento = pag;
            ImportDados();
            CaixaDAO caixa = new CaixaDAO();
            idCaixa = caixa.GetById();

            dtpData.Visibility = Visibility.Collapsed;
            borderDtp.Visibility = Visibility.Collapsed;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pagamento pag = new Pagamento();

                pag.Id = pagamento.Id;
                if (tbValor.Text != "")
                {
                    pag.Valor = Convert.ToDouble(tbValor.Text);
                }
                else
                {
                    pag.Valor = pagamento.Valor;
                }
                if (dtpData.Text != "")
                {
                    pag.Data = dtpData.DisplayDate;
                }
                else
                {
                    pag.Data = pagamento.Data;
                }
                if (tmHora.Text != "")
                {
                    pag.Hora = DAOHelper.DateTimeToTimeSpan(tmHora.SelectedTime);
                }
                else
                {
                    pag.Hora = pag.Hora;
                }
                if (cbFormapag.SelectedIndex != -1)
                {
                    pag.FormaPagamento = new FormaPagamento();
                    pag.FormaPagamento.Id = cbFormapag.SelectedIndex + 1;
                }
                else
                {
                    pag.FormaPagamento = new FormaPagamento();
                    pag.FormaPagamento.Id = pagamento.FormaPagamento.Id;
                }
                if (cbDespesa.SelectedIndex != -1)
                {
                    pag.Despesa = new Despesa();
                    pag.Despesa.Id = cbDespesa.SelectedIndex + 1;
                }
                else
                {
                    pag.Despesa = new Despesa();
                    pag.Despesa.Id = pagamento.Despesa.Id;
                }
                pag.Caixa = new Caixa();
                pag.Caixa.id = idCaixa;

                //Inserindo os Dados           
                PagamentoDAO pagamentoDAO = new PagamentoDAO();
                pagamentoDAO.Update(pag);

                Clear();
            }
            catch (Exception ex)
            {

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

        private void ImportDados()
        {
            lbData.Content = "Data: " + pagamento.Data;
            lbValor.Content = "Valor: " + pagamento.Valor;
            lbHoradopagamento.Content = "Hora de Pagamento: " + pagamento.Hora;
            lbFormadepagamento.Content = "Forma de Pagamento: " + pagamento.FormaPagamento.Nome;
            lbDespesa.Content = "Despesa: " + pagamento.Despesa.Nome;
        }

        private void cbDespesa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDespesa.SelectedItem is Despesa selectedItemDesp)
            {
                parcelas = selectedItemDesp.Parcelas;
                cbParcela.Items.Clear();

                for (int i = 1; i <= parcelas; i++)
                {
                    cbParcela.Items.Add(i);
                }
            }
        }

        private void cbParcela_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDespesa.SelectedItem is Despesa selectedItemDesp)
            {
                parcelasSelecionadas = Convert.ToInt32(cbParcela.SelectedValue.ToString());
                tbValor.Text = (selectedItemDesp.ValorParcela * parcelasSelecionadas).ToString("C");
                if (cbParcela.SelectedValue.ToString() != parcelas.ToString())
                {
                    dtpData.Visibility = Visibility.Visible;
                    borderDtp.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
