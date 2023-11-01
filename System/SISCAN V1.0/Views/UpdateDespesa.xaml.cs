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
    public partial class UpdateDespesa : Page
    {
        Despesa user = new Despesa();
        public UpdateDespesa(Despesa despesa)
        {
            InitializeComponent();
            user = despesa;
            ImportDados();
            DadosCbCom();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Despesa despesa = new Despesa();
                despesa.Id = despesa.Id;

                if (tbNome.Text != "")
                {
                    despesa.Nome = tbNome.Text;
                }
                else
                {
                    despesa.Nome = user.Nome;
                }
                if (tbParcelas.Text != "")
                {
                    despesa.Parcelas = Convert.ToInt32(tbParcelas.Text);
                }
                else
                {
                    despesa.Parcelas = user.Parcelas;
                }
                if (tbValor.Text != "")
                {
                    despesa.Valor = Convert.ToInt32(tbValor.Text);
                }
                else
                {
                    despesa.Valor = user.Valor;
                }
                if (cbStatus.Text != "")
                {
                    despesa.Status = cbStatus.SelectionBoxItem.ToString();
                }
                else
                {
                    despesa.Status = user.Status;
                }
                if (dtpData.Text != "")
                {
                    despesa.Data = dtpData.SelectedDate;
                }
                else
                {
                    despesa.Data = user.Data;
                }
                if (dtpVencimento.Text != "")
                {
                    despesa.Vencimento = dtpVencimento.SelectedDate;
                }
                else
                {
                    despesa.Vencimento = user.Vencimento;
                }
                if (cbStatus.SelectedIndex != 1)
                {
                    despesa.Compra = new Compra();
                    despesa.Compra.Id = cbStatus.SelectedIndex - 1;
                }
                else
                {
                    despesa.Compra.Id = user.Compra.Id;
                }
                //Inserindo os Dados           
                DespesaDAO despesaDAO = new DespesaDAO();
                despesaDAO.Update(despesa);

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

        private void DadosCbCom()
        {
            //CompraDAO compraDAO = new CompraDAO();
            //cbStatus.ItemsSource = compraDAO.List(null);
            //cbStatus.DisplayMemberPath = "Data";
        }
        private void ImportDados()
        {
            tbNome.Text = "Nome: " + user.Nome;
            tbParcelas.Text = "Parcelas: " + user.Parcelas;
            tbValor.Text = "Valor: " + user.Valor;
            cbStatus.Text = "Status: " + user.Status;
            dtpData.Text = "Data: " + user.Data;
            dtpVencimento.Text = "Vencimento: " + user.Vencimento;
        }
    }
}
