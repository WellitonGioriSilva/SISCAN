﻿using SISCAN.Views;
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
using System.Windows.Shapes;
using SISCAN.Models;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Lógica interna para FormMenu.xaml
    /// </summary>
    public partial class FormMenu : Window
    {
        Usuario usuario;
        CaixaDAO caixaDAO = new CaixaDAO();
        public FormMenu(MainWindow main, Usuario usu)
        {
            InitializeComponent();

            main.Close();

            Frame.NavigationService.Navigate(new Menu());

            usuario = usu;

            lbUserName.Content = usuario.UsuarioNome;

            Acesso();
        }

        private void btCliente_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Cliente";
            Frame.NavigationService.Navigate(new CadastrarCliente());
        }

        private void btDespesa_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Buscar Despesa";
            Frame.NavigationService.Navigate(new ListarDespesa());
        }

        private void btFuncao_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Função";
            Frame.NavigationService.Navigate(new CadastrarFuncao());
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente voltar ao menu?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                lbTitulo.Content = "Menu";
                Frame.NavigationService.Navigate(new Menu());
            }
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente sair do sistema?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }           
        }

        private void btFornecedor_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Fornecedor";
            Frame.NavigationService.Navigate(new CadastrarFornecedor());
        }

        private void btProduto_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Produto";
            Frame.NavigationService.Navigate(new CadastrarProduto());
        }

        private void btFuncionario_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Funcionário";
            Frame.NavigationService.Navigate(new CadastrarFuncionario());
        }

        private void btCaixa_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Caixa";
            Frame.NavigationService.Navigate(new CadastrarCaixa(usuario.Funcionario));
        }

        private void btEstoque_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Buscar Estoque";
            Frame.NavigationService.Navigate(new ListarEstoque());
        }

        private void btPagamento_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Pagar Despesa";
            caixaDAO = new CaixaDAO();
            Frame.NavigationService.Navigate(new CadastrarPagamento(caixaDAO.GetById()));
        }

        private void btUser_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Cadastrar Usuário";
            Frame.NavigationService.Navigate(new CadastrarUsuario());
        }

        private void Acesso()
        {
            btUser.Visibility = Visibility.Collapsed;
            btProduto.Visibility = Visibility.Collapsed;
            btCompra.Visibility = Visibility.Collapsed;
            btFornecedor.Visibility = Visibility.Collapsed;
            btFuncao.Visibility = Visibility.Collapsed;
            btFuncionario.Visibility = Visibility.Collapsed;
            btEstoque.Visibility = Visibility.Collapsed;
            btPagamento.Visibility = Visibility.Collapsed;
            btDespesa.Visibility = Visibility.Collapsed;
            btCaixa.Visibility = Visibility.Collapsed;
            btVenda.Visibility = Visibility.Collapsed;
            btCliente.Visibility = Visibility.Collapsed;
            btRecebimento.Visibility = Visibility.Collapsed;

            if (usuario.Acesso == 1)
            {
                btCaixa.Visibility = Visibility.Visible;
                btVenda.Visibility = Visibility.Visible;
                btCliente.Visibility = Visibility.Visible;
                btRecebimento.Visibility = Visibility.Visible;
            }
            
            if (usuario.Acesso == 2)
            {
                btProduto.Visibility = Visibility.Visible;
                btCompra.Visibility = Visibility.Visible;
                btFornecedor.Visibility = Visibility.Visible;
                btEstoque.Visibility = Visibility.Visible;
                btPagamento.Visibility = Visibility.Visible;
                btDespesa.Visibility = Visibility.Visible;
                btCaixa.Visibility = Visibility.Visible;
            }

            if (usuario.Acesso == 3)
            {
                btFuncao.Visibility = Visibility.Visible;
                btFuncionario.Visibility = Visibility.Visible;
                btUser.Visibility = Visibility.Visible;
            }
            
            if (usuario.Acesso == 4)
            {
                btFuncao.Visibility = Visibility.Visible;
                btFuncionario.Visibility = Visibility.Visible;
                btUser.Visibility = Visibility.Visible;
                btProduto.Visibility = Visibility.Visible;
                btCompra.Visibility = Visibility.Visible;
                btFornecedor.Visibility = Visibility.Visible;
                btEstoque.Visibility = Visibility.Visible;
                btPagamento.Visibility = Visibility.Visible;
                btDespesa.Visibility = Visibility.Visible;
                btCaixa.Visibility = Visibility.Visible;
                btCaixa.Visibility = Visibility.Visible;
                btVenda.Visibility = Visibility.Visible;
                btCliente.Visibility = Visibility.Visible;
                btRecebimento.Visibility = Visibility.Visible;
            }
        }

        private void btVenda_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Vender Produto";
            caixaDAO = new CaixaDAO();
            Frame.NavigationService.Navigate(new VenderProduto(usuario.Funcionario, caixaDAO.GetById()));
        }

        private void btCompra_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Comprar Produto";
            Frame.NavigationService.Navigate(new ComprarProduto());
        }

        private void btFechar_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Fechar Caixa";
            Frame.NavigationService.Navigate(new FechamentoCaixa());
        }

        private void btRecebimento_Click(object sender, RoutedEventArgs e)
        {
            lbTitulo.Content = "Recebimentos";
            Frame.NavigationService.Navigate(new ListarRecebimento());
        }
    }
}
