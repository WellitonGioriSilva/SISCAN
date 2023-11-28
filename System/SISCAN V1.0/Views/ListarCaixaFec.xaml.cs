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
    /// Interação lógica para ListarCaixa.xam
    /// </summary>
    public partial class ListarCaixaFec : Page
    {
        public string textBusca;
        Caixa selectedItem;
        public ListarCaixaFec()
        {
            InitializeComponent();
            CarregarLista();
        }

        public void CarregarLista()
        {
            CaixaDAO caixaDAO = new CaixaDAO();

            dgvList.ItemsSource = caixaDAO.ListFec(textBusca);
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

        private void btExtrato_Click(object sender, RoutedEventArgs e)
        {
            if(tbNum.Text != "")
            {
               CaixaDAO caixaDAO = new CaixaDAO();
               caixaDAO.Extrato(Convert.ToInt32(tbNum.Text));
            }
            else
            {
                MessageBox.Show("Digite o número do caixa!");
            }
        }

        private void btAtualizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvList.SelectedItem != null)
            {
                selectedItem = (Caixa)this.dgvList.SelectedItem;
                fmFrame.Visibility = Visibility.Visible;
                fmFrame.NavigationService.Navigate(new UpdateCaixa(selectedItem));
            }
            else
            {
                MessageBox.Show("Selecione uma caixa antes de atualizar!");
            }
        }
    }
}
