using MySqlX.XDevAPI;
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
    /// Interação lógica para CadastrarFuncionario.xam
    /// </summary>
    public partial class CadastrarFuncionario : Page
    {
        public CadastrarFuncionario()
        {
            InitializeComponent();
            DadosCb();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela funcionário
                Funcionario funcionario = new Funcionario();
                funcionario.Nome = tbNome.Text;
                funcionario.Cpf = tbCpf.Text;
                funcionario.Bairro = tbBairro.Text;
                funcionario.Sexo = cbSexo.SelectionBoxItem.ToString();
                funcionario.Numero = Convert.ToInt16(tbNumero.Text);
                funcionario.Rua = tbRua.Text;
                funcionario.Funcao = new Funcao();
                if (cbFuncao.SelectedItem is Funcao selectedItem)
                {
                    funcionario.Funcao.Id = selectedItem.Id;
                    funcionario.Acesso = selectedItem.Acesso;
                }
                funcionario.Cidade = new Cidade();
                if (cbCidade.SelectedItem is Cidade selectedItemCid)
                {
                    funcionario.Cidade.ID = selectedItemCid.ID;
                }
                //Inserindo os Dados           
                FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
                funcionarioDAO.Insert(funcionario);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void Clear()
        {
            tbNome.Clear();
            tbCpf.Clear();
            tbRua.Clear();
            tbBairro.Clear();
            tbNumero.Clear();
            cbSexo.SelectedIndex = -1;
            cbCidade.SelectedIndex = -1;
            cbFuncao.SelectedIndex = -1;
        }

        private void DadosCb()
        {
            //Setando dados no combobox de cidade
            CidadeDAO cidDAO = new CidadeDAO();
            cbCidade.ItemsSource = cidDAO.List();
            cbCidade.DisplayMemberPath = "Nome";

            //Setando dados no combobox de função
            FuncaoDAO funDAO = new FuncaoDAO();
            cbFuncao.ItemsSource = funDAO.List(null);
            cbFuncao.DisplayMemberPath = "Nome";
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

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarFuncionario());
        }
    }
}
