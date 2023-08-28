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
    /// Interação lógica para ListarPagamento.xam
    /// </summary>
    public partial class ListarPagamento : Page
    {
        public string textBusca;
        Pagamento selectedItem;
        public ListarPagamento()
        {
            InitializeComponent();
            CarregarLista();
        }

        public void CarregarLista()
        {
            PagamentoDAO pagamentoDAO = new PagamentoDAO();

            dgvList.ItemsSource = pagamentoDAO.List(textBusca);
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (tbData.Text != "")
            {
                textBusca = tbData.Text;
                CarregarLista();
            }
            else
            {
                MessageBox.Show("Insira uma data antes de buscar");
            }

        }

        private void btAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                selectedItem = (Pagamento)this.dgvList.SelectedItem;
                fmFrame.Visibility = Visibility.Visible;
                fmFrame.NavigationService.Navigate(new UpdatePagamento(selectedItem));
            }
            else
            {
                MessageBox.Show("Selecione um pagamento antes de atualizar!");
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                Pagamento selectedItem = (Pagamento)this.dgvList.SelectedItem;
                PagamentoDAO pagamentoDAO = new PagamentoDAO();

                pagamentoDAO.Delete(selectedItem);

                CarregarLista();
            }
        }
    }
}
