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
    /// Interação lógica para ListarDespesa.xam
    /// </summary>
    public partial class ListarDespesa : Page
    {
        public string textBusca;
        Despesa selectedItem;
        public ListarDespesa()
        {
            InitializeComponent();
            CarregarLista();
        }

        public void CarregarLista()
        {
            DespesaDAO despesaDAO = new DespesaDAO();

            dgvList.ItemsSource = despesaDAO.List(textBusca);
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

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                Despesa selectedItem = (Despesa)this.dgvList.SelectedItem;
                DespesaDAO despesaDAO = new DespesaDAO();

                despesaDAO.Delete(selectedItem);

                CarregarLista();
            }
        }

        private void btAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                selectedItem = (Despesa)this.dgvList.SelectedItem;
                fmFrame.Visibility = Visibility.Visible;
                fmFrame.NavigationService.Navigate(new UpdateDespesa(selectedItem));
            }
            else
            {
                MessageBox.Show("Selecione uma função antes de atualizar!");
            }
        }
    }
}
