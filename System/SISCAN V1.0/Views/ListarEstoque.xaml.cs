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
    /// Interação lógica para ListarEstoque.xam
    /// </summary>
    public partial class ListarEstoque : Page
    {
        public string textBusca;
        Estoque selectedItem;
        public ListarEstoque()
        {
            InitializeComponent();
            CarregarLista();
        }

        public void CarregarLista()
        {
            EstoqueDAO estoqueDAO = new EstoqueDAO();

            dgvList.ItemsSource = estoqueDAO.List(textBusca);
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (tbLote.Text != "")
            {
                textBusca = tbLote.Text;
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
                Estoque selectedItem = (Estoque)this.dgvList.SelectedItem;
                EstoqueDAO estoqueDAO = new EstoqueDAO();

                estoqueDAO.Delete(selectedItem);

                CarregarLista();
            }
        }

        private void btAtualizar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
