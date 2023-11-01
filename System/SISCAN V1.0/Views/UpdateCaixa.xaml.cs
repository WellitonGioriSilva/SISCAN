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
using MySqlX.XDevAPI;
using SISCAN.Models;
using SISCAN.Views;
using SISCAN.Helpers;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarCliente.xam
    /// </summary>
    public partial class UpdateCaixa : Page
    {
        Caixa user = new Caixa();
        public UpdateCaixa(Caixa caixa)
        {
            InitializeComponent();
            user = caixa;
            ImportDados();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Caixa caixa = new Caixa();

                caixa.id = user.id;
                if (tbValorInicial.Text != "")
                {
                    caixa.ValorIncial = Convert.ToInt32(tbValorInicial.Text);
                }
                else
                {
                    caixa.ValorIncial = user.ValorIncial;
                }
                if (tbValorFinal.Text != "")
                {
                    caixa.ValorFinal = Convert.ToInt32(tbValorFinal.Text);
                }
                else
                {
                    caixa.ValorFinal = user.ValorFinal;
                }
                if (dtpData.Text != "")
                {
                    caixa.Data = dtpData.SelectedDate;
                }
                else
                {
                    caixa.Data = user.Data;
                }
                if (tmAbertura.Text != "")
                {
                    DateTime? aber = tmAbertura.SelectedTime;
                }
                else
                {
                    caixa.HoraAbertura = user.HoraAbertura;
                }
                if (tmFechamento.Text != "")
                {
                    DateTime? fec = tmFechamento.SelectedTime;
                }
                else
                {
                    caixa.HoraFechamento = user.HoraFechamento;
                }

                //Inserindo os Dados           
                CaixaDAO caixaDAO = new CaixaDAO();
                caixaDAO.Update(caixa);

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
            tbValorInicial.Clear();
            tbValorFinal.Clear();
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarCaixa());
        }

        private void ImportDados()
        {
            tbValorInicial.Text = "Valor Inicial: " + user.ValorFinal;
            tbValorFinal.Text = "Valor Final: " + user.ValorFinal;
            dtpData.Text = "Data: " + user.Data;
            tmAbertura.Text = "Abertura: " + user.HoraAbertura;
            tmFechamento.Text = "Fechamento: " + user.HoraFechamento;
        }
    }
}
