using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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
using SISCAN.Formularios;
using SISCAN.Models;
using SISCAN.Views;

namespace SISCAN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Verificacao();
        }

        private void btLogar_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        public void Verificacao()
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            usuarioDAO.Login(null, null);

            if(usuarioDAO.count == 0)
            {
                fmCadastro.NavigationService.Navigate(new CadastrarUsuario());
            }
            else
            {
                btVoltar.Visibility = Visibility.Collapsed;
            }
        }

        public void Login()
        {
            string user = tbUser.Text;
            string senha = new System.Net.NetworkCredential(string.Empty, tbSenha.SecurePassword).Password;
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            usuarioDAO.Login(user, senha);

            if (usuarioDAO.count == 1)
            {
                FormMenu form = new FormMenu(this);
                form.Show();
            }
            else
            {
                MessageBox.Show("Usuáiro ou Senha incorretos!");
            }
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            fmCadastro.Visibility = Visibility.Collapsed;
            btVoltar.Visibility = Visibility.Collapsed;
        }
    }
}
