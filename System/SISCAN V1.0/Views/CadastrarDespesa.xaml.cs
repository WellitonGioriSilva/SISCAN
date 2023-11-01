using SISCAN.Models;
using SISCAN.Views;
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

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarDespesa.xam
    /// </summary>
    public partial class CadastrarDespesa : Page
    {
        public CadastrarDespesa()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela cliente
                Despesa despesa = new Despesa();
                despesa.Nome = tbNome.Text;
                despesa.Parcelas = Convert.ToInt32(tbParcelas.Text);
                despesa.Valor = Convert.ToInt32(tbValor.Text);
                despesa.Status = cbStatus.SelectionBoxItem.ToString();
                despesa.Data = dtpData.SelectedDate;
                despesa.Vencimento = dtpVencimento.SelectedDate;
                //despesa.Compra.Id = 1;

                //Inserindo os Dados           
                DespesaDAO despesaDAO = new DespesaDAO();
                despesaDAO.Insert(despesa);
                MessageBox.Show(despesaDAO.mensagem);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Clear();
                MessageBox.Show("Campos limpos com sucesso!");
            }
        }

        private void Clear()
        {
            tbNome.Clear();
            tbParcelas.Clear();
            tbValor.Clear();
            cbStatus.SelectedIndex = -1;
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarDespesa());
        }
    }
}
