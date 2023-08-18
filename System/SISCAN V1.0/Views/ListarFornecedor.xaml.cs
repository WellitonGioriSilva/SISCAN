﻿using SISCAN.Models;
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
    }
}