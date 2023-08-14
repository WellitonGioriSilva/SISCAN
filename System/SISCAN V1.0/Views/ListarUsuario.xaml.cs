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
    /// Interação lógica para ListarUsuario.xam
    /// </summary>
    public partial class ListarUsuario : Page
    {
        public string textBusca;
        Usuario selectedItemUser;
        public ListarUsuario()
        {
            InitializeComponent();
            CarregarLista();
            btAtualizar.Visibility = Visibility.Visible;
        }

        public void CarregarLista()
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO();

            dgvList.ItemsSource = usuarioDAO.List(textBusca);
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (tbUser.Text != "")
            {
                textBusca = tbUser.Text;
                CarregarLista();
            }
            else
            {
                MessageBox.Show("Insira um usuário antes de buscar");
            }

        }

        private void dgvList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                selectedItemUser = (Usuario)dgvList.SelectedItem;
                MessageBox.Show(selectedItemUser.UsuarioNome + " selecionado!");
            }
        }

        private void btAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                btAtualizar.Visibility = Visibility.Collapsed;
                fmFrame.Visibility = Visibility.Visible;
                fmFrame.NavigationService.Navigate(new UpdateUsuario(selectedItemUser));
            }
            else
            {
                MessageBox.Show("Selecione um usuário antes de atualizar!");
            }
        }
    }
}
