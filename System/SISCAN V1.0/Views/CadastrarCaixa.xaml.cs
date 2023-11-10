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
using MessagePack;
using Google.Protobuf.Reflection;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarCliente.xam
    /// </summary>
    public partial class CadastrarCaixa : Page
    {
        public Funcionario funcionario = new Funcionario();
        public CadastrarCaixa(Funcionario func)
        {
            InitializeComponent();
            funcionario = func;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Verificando caixa aberto
                CaixaDAO caixaDAO = new CaixaDAO();
                if(caixaDAO.GetById() == 0)
                {
                    //Setando informações na tabela cliente
                    Caixa caixa = new Caixa();
                    caixa.ValorIncial = Convert.ToInt32(tbValorInicial.Text);

                    //Inserindo os Dados           
                    caixaDAO.Insert(caixa, funcionario.Id);
                    MessageBox.Show(caixaDAO.mensagem);

                    if (caixaDAO.condicao == true)
                    {
                        Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Já existe um caixa aberto no sistema!");
                }
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
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarCaixa());
        }
    }
}
