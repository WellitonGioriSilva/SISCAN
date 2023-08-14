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
    /// Interação lógica para ListarRecebimento.xam
    /// </summary>
    public partial class ListarRecebimento : Page
    {
        public string textBusca;
        public ListarRecebimento()
        {
            InitializeComponent();
            CarregarLista();
        }
        public void CarregarLista()
        {
            RecebimentoDAO recebimentoDAO = new RecebimentoDAO();

            dgvList.ItemsSource = recebimentoDAO.List(textBusca);
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

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
