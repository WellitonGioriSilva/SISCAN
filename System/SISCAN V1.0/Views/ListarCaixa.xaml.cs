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
    /// Interação lógica para ListarCaixa.xam
    /// </summary>
    public partial class ListarCaixa : Page
    {
        public string textBusca;
        public ListarCaixa()
        {
            InitializeComponent();
            CarregarLista();
        }

        public void CarregarLista()
        {
            CaixaDAO caixaDAO = new CaixaDAO();

            dgvList.ItemsSource = caixaDAO.List(textBusca);
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (tbNum.Text != "")
            {
                textBusca = tbNum.Text;
                CarregarLista();
            }
            else
            {
                MessageBox.Show("Insira um número antes de buscar");
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                Caixa selectedItem = (Caixa)this.dgvList.SelectedItem;
                CaixaDAO caixaDAO = new CaixaDAO();

                caixaDAO.Delete(selectedItem);

                CarregarLista();
            }
        }
    }
}
