﻿using MySqlX.XDevAPI;
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

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para Cadastrar_Recebimento.xam
    /// </summary>
    public partial class CadastrarRecebimento : Page
    {
        public CadastrarRecebimento()
        {
            InitializeComponent();

            DadosCbForm();
            DadosCbCai();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela cliente
                Recebimento recebimento = new Recebimento();
                recebimento.Valor = Convert.ToDouble(tbValor.Text);
                recebimento.Hora = tmHora.SelectedTime;
                recebimento.Data = dtpData.SelectedDate;
                recebimento.Caixa = new Caixa();
                recebimento.Caixa.id = cbCaixa.SelectedIndex + 1;
                recebimento.FormaPagamento = new FormaPagamento();
                recebimento.FormaPagamento.Id = cbFormaPagamento.SelectedIndex + 1;

                //Inserindo os Dados           
                RecebimentoDAO recebimentoDAO = new RecebimentoDAO();
                recebimentoDAO.Insert(recebimento);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
                MessageBox.Show(ex.Message);
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
            cbCaixa.ItemsSource = caixaDAO.List();
            cbCaixa.DisplayMemberPath = "Data";
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
    }
}
