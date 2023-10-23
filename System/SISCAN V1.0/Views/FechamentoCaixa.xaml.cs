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
        public FechamentoCaixa()
        {
            InitializeComponent();
        }

        private void btFechar_Click(object sender, RoutedEventArgs e)
        {
            CaixaDAO caixaDAO = new CaixaDAO();
            Caixa caixa = new Caixa();
            RecebimentoDAO recebimentoDAO = new RecebimentoDAO();
            PagamentoDAO pagamentoDAO = new PagamentoDAO();
            caixa.id = caixaDAO.GetById();
            double valor = (caixaDAO.valorIni + recebimentoDAO.ValorTotal(caixa.id)) - pagamentoDAO.ValorTotal(caixa.id);
            caixa.ValorFinal = valor;

            caixaDAO.CaixaFechamento(caixa);
            MessageBox.Show(caixaDAO.result);
        }
    }
}
