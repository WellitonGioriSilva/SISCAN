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
using SISCAN.Models;

namespace SISCAN.Views
{
    /// <summary>
    /// Interação lógica para FechamentoCaixa.xam
    /// </summary>
    public partial class FechamentoCaixa : Page
    {
        CaixaDAO caixaD = new CaixaDAO();
        RecebimentoDAO recebimentoDAO = new RecebimentoDAO();
        PagamentoDAO pagamentoDAO = new PagamentoDAO();
        int id;
        double valor;
        public FechamentoCaixa()
        {
            InitializeComponent();
            id = caixaD.GetById();
            //caixaD.GetById();
            double valorPag = pagamentoDAO.ValorTotal(id);
            double valorRec = recebimentoDAO.ValorTotal(id);
            double valorInic = caixaD.valorIni;
            valor = (valorInic + valorRec) - valorPag;
        }

        private void btFechar_Click(object sender, RoutedEventArgs e)
        {
            CaixaDAO caixaDAO = new CaixaDAO();
            Caixa caixa = new Caixa();

            caixa.id = caixaDAO.GetById();
            caixa.ValorFinal = valor;

            caixaDAO.CaixaFechamento(caixa);
            MessageBox.Show(caixaDAO.mensagem);
            id = caixaD.GetById();
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (id == 0)
            {
                fmFrame.Visibility = Visibility.Visible;
                fmFrame.NavigationService.Navigate(new ListarCaixaFec());
            }
            else
            {
                MessageBox.Show("Feche o caixa antes de consultá-lo!");   
            }
        }
    }
}
