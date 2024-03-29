﻿using SISCAN.Formularios;
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
    /// Interação lógica para ListarFornecedor.xam
    /// </summary>
    public partial class ListarFornecedor : Page
    {
        public string textBusca;
        Fornecedor selectedItem;
        public ListarFornecedor()
        {
            InitializeComponent();
            CarregarLista();
        }

        public void CarregarLista()
        {
            FornecedorDAO fornecedorDAO = new FornecedorDAO();

            dgvList.ItemsSource = fornecedorDAO.List(textBusca);
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (tbRazao.Text != "")
            {
                textBusca = tbRazao.Text;
                CarregarLista();
            }
            else
            {
                MessageBox.Show("Insira um lote antes de buscar");
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                Fornecedor selectedItem = (Fornecedor)this.dgvList.SelectedItem;
                FornecedorDAO fornecedorDAO = new FornecedorDAO();

                fornecedorDAO.Delete(selectedItem);

                CarregarLista();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                selectedItem = (Fornecedor)this.dgvList.SelectedItem;
                fmFrame.Visibility = Visibility.Visible;
                fmFrame.NavigationService.Navigate(new UpdateFornecedor(selectedItem));
            }
            else
            {
                MessageBox.Show("Selecione uma função antes de atualizar!");
            }
        }
    }
}
