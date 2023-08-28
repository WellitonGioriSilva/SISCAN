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
    /// Interação lógica para ListarCliente.xam
    /// </summary>
    public partial class ListarCliente : Page
    {
        public string textBusca;
        public ListarCliente()
        {
            InitializeComponent();
            CarregarLista();
        }

        public void CarregarLista()
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            dgvList.ItemsSource = clienteDAO.List(textBusca);
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if(tbNome.Text != "")
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
                Cliente selectedItem = (Cliente)this.dgvList.SelectedItem;
                ClienteDAO clienteDAO = new ClienteDAO();

                clienteDAO.Delete(selectedItem);

                CarregarLista();
            }
        }
    }
}
