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
    /// Interação lógica para CadastrarFuncao.xam
    /// </summary>
    public partial class CadastrarFuncao : Page
    {
        string turnoSelect = "";
        public CadastrarFuncao()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela função
                Funcao funcao = new Funcao();
                funcao.Nome = tbNome.Text;
                funcao.Salario = Convert.ToSingle(tbSalario.Text);
                funcao.Acesso = Convert.ToInt32(cbAcesso.SelectionBoxItem.ToString());
                funcao.Turno = turnoSelect;

                //Inserindo os Dados           
                FuncaoDAO funcaoDAO = new FuncaoDAO();
                funcaoDAO.Insert(funcao);
                MessageBox.Show(funcaoDAO.mensagem);
                if (funcaoDAO.condicao == true)
                {
                    Clear();
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
            tbNome.Clear();
            tbSalario.Clear();
            rbMatutino.IsChecked = false;
            rbNoturno.IsChecked = false;
            rbVespertino.IsChecked = false;
            cbAcesso.SelectedIndex = -1;
            turnoSelect = "";
        }

        //Setando dado ao turno
        private void rbMatutino_Checked(object sender, RoutedEventArgs e)
        {
            if (turnoSelect == "")
            {
                turnoSelect = $"Matutino";
            }
            else
            {
                turnoSelect = $"Matutino, {turnoSelect}";
            }
        }

        private void rbVespertino_Checked(object sender, RoutedEventArgs e)
        {
            if (turnoSelect == "")
            {
                turnoSelect = $"Vespertino";
            }
            else
            {
                turnoSelect = $"{turnoSelect}, Vespertino";
            }
        }

        private void rbNoturno_Checked(object sender, RoutedEventArgs e)
        {
            if (turnoSelect == "")
            {
                turnoSelect = $"Noturno";
            }
            else
            {
                turnoSelect = $"{turnoSelect}, Noturno";
            }
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarFuncao());
        }
    }
}
