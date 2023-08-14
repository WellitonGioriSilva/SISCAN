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
    public partial class UpdateUsuario : Page
    {
        Usuario user = new Usuario();
        public UpdateUsuario(Usuario usuario)
        {
            InitializeComponent();
            user = usuario;
            DadosCb();
            ImportDados();

        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (tbSenha.Text == tbSenhaN.Text)
            {
                try
                {                   
                    Usuario usuario = new Usuario();

                    usuario.Id = user.Id;
                    if (tbUsuario.Text != "")
                    {
                        usuario.UsuarioNome = tbUsuario.Text;
                    }
                    else
                    {
                        usuario.UsuarioNome = user.UsuarioNome;
                    }
                    if(tbSenha.Text!= "")
                    {
                        usuario.Senha = tbSenha.Text;
                    }
                    else
                    {
                        usuario.Senha = user.Senha;
                    }
                    if (cbFuncionario.SelectedIndex != -1)
                    {
                        usuario.Funcionario = new Funcionario();
                        usuario.Funcionario.Id = cbFuncionario.SelectedIndex + 1;
                    }
                    else
                    {
                        usuario.Funcionario = new Funcionario();
                        usuario.Funcionario.Id = user.Funcionario.Id;
                    }

                    //Inserindo os Dados           
                    UsuarioDAO usuarioDAO = new UsuarioDAO();
                    usuarioDAO.Update(usuario);

                    Clear();
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
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar a atualização?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

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
        private void ImportDados()
        {
            lbNome.Content = "User: " + user.UsuarioNome;
            lbSenha.Content = "Senha: " + user.Senha;
            lbFuncionario.Content = "Funcionário: " + user.Funcionario.Nome;
        }
    }
}
