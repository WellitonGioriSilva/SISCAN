using MySqlX.XDevAPI;
using SISCAN.Models;
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

namespace SISCAN.Views
{
    /// <summary>
    /// Interação lógica para CadastrarUsuario.xam
    /// </summary>
    public partial class CadastrarUsuario : Page
    {
        public int contador = 0;
        public CadastrarUsuario()
        {
            InitializeComponent();

            DadosCb();
            Verificacao();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (tbSenha.Text == tbSenhaN.Text)
            {
                try
                {
                    //Setando informações na tabela cliente
                    Usuario usuario = new Usuario();
                    usuario.UsuarioNome = tbUsuario.Text;
                    usuario.Senha = tbSenha.Text;
                    usuario.Funcionario = new Funcionario();
                    usuario.Funcionario.Id = 0;
                    if (cbFuncionario.SelectedIndex == -1)
                    {
                        usuario.Funcionario.Id = 0;
                    }
                    if (cbFuncionario.SelectedItem is Funcionario selectedItem)
                    {
                        usuario.Funcionario.Id = selectedItem.Id;
                        usuario.Acesso = selectedItem.Acesso;
                    }
                    

                    //Inserindo os Dados           
                    UsuarioDAO usuarioDAO = new UsuarioDAO();
                    usuarioDAO.Insert(usuario, contador);
                    MessageBox.Show(usuarioDAO.mensagem);
                    if (usuarioDAO.condicao == true)
                    {
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro 3008 : Contate o suporte");
                }
            }
            else
            {
                MessageBox.Show("As senhas são diferentes!");
            }
        }

        private void Clear()
        {
            tbUsuario.Clear();
            tbSenha.Clear();
            tbSenhaN.Clear();
            cbFuncionario.SelectedIndex = -1;
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
        private void DadosCb()
        {
            FuncionarioDAO funcDAO = new FuncionarioDAO();
            cbFuncionario.ItemsSource = funcDAO.List(null);
            cbFuncionario.DisplayMemberPath = "Nome";
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            fmFrame.Visibility = Visibility.Visible;
            fmFrame.NavigationService.Navigate(new ListarUsuario());
        }

        public void Verificacao()
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            usuarioDAO.Login(null, null);
            contador = usuarioDAO.count;
        }
    }
}
