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

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para Menu.xam
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
            CarregarProduto();
            CarregarLembrete();
        }

        public void CarregarProduto()
        {
            Produto produto = new Produto();
            ProdutoDAO produtoDAO = new ProdutoDAO();
            produto = produtoDAO.ListLucro();

            lbProduto.Text = produto.Nome;
        }

        public void CarregarLembrete()
        {
            Despesa despesa = new Despesa();
            DespesaDAO despesaDAO = new DespesaDAO();
            despesa = despesaDAO.Lembrete();

            lbLembrete.Text = despesa.Nome;
        }
    }
}
