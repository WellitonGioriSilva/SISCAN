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
    /// Interação lógica para ListarProduto.xam
    /// </summary>
    public partial class ListarProduto : Page
    {
        public string textBusca;

        public ListarProduto()
        {
            InitializeComponent();
            CarregarLista();
        }

        public void CarregarLista()
        {
            ProdutoDAO produtoDAO = new ProdutoDAO();

            dgvList.ItemsSource = produtoDAO.List(textBusca);
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (tbNome.Text != "")
            {
                textBusca = tbNome.Text;
                CarregarLista();
            }
            else
            {
                MessageBox.Show("Insira um nome antes de buscar");
            }
        }   
    }
}