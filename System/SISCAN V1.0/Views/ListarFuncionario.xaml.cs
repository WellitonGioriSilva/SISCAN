using SISCAN.Formularios;
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
    /// Interação lógica para ListarFuncionario.xam
    /// </summary>
    public partial class ListarFuncionario : Page
    {
        public string textBusca;
        Funcionario selectedItem;

        public ListarFuncionario()
        {
            InitializeComponent();
            CarregarLista();
        }

        public void CarregarLista()
        {
            FuncionarioDAO funcionarioDAO = new FuncionarioDAO();

            dgvList.ItemsSource = funcionarioDAO.List(textBusca);
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

        private void btAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                selectedItem = (Funcionario)this.dgvList.SelectedItem;
                fmFrame.Visibility = Visibility.Visible;
                fmFrame.NavigationService.Navigate(new UpdateFuncionario(selectedItem));
            }
            else
            {
                MessageBox.Show("Selecione um funcionário antes de atualizar!");
            }
        }
    }
}
