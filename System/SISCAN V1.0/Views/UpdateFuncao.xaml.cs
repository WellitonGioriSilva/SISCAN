using SISCAN.Helpers;
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
    public partial class UpdateFuncao : Page
    {
        string turnoSelect = "";
        Funcao funcao = new Funcao();
        public UpdateFuncao(Funcao func)
        {
            InitializeComponent();
            funcao = func;
            ImportDados();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Funcao func = new Funcao();

                func.Id = funcao.Id;
                if (tbNome.Text != "")
                {
                    func.Nome = tbNome.Text;
                }
                else
                {
                    func.Nome = funcao.Nome;
                }
                if (tbSalario.Text != "")
                {
                    func.Salario = Convert.ToDouble(tbSalario.Text);
                }
                else
                {
                    func.Salario = funcao.Salario;
                }
                if(cbAcesso.SelectedIndex != -1)
                {
                    func.Acesso = Convert.ToInt32(cbAcesso.SelectionBoxItem.ToString());
                }
                else
                {
                    func.Acesso = funcao.Acesso;
                }
                if (turnoSelect != "")
                {
                    func.Turno = turnoSelect;
                }
                else
                {
                    func.Turno = funcao.Turno;
                }

                //Inserindo os Dados           
                FuncaoDAO funcaoDAO = new FuncaoDAO();
                funcaoDAO.Update(func);

                Clear();
            }
            catch (Exception ex)
            {

            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar a atualização?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

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

        private void ImportDados()
        {
            lbNome.Content = "Nome: " + funcao.Nome;
            lbSalario.Content = "Salário: " + funcao.Salario;
            lbAcesso.Content = "Nível de Acesso: " + funcao.Acesso;
            lbTurno.Content = "Turno: " + funcao.Turno;
        }
    } 
}
