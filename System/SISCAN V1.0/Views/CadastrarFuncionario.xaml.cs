using MySqlX.XDevAPI;
using Newtonsoft.Json;
using SISCAN.Helpers;
using SISCAN.Models;
using SISCAN.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        string cidade;
        string estado;
        public CadastrarFuncionario()
        {
            InitializeComponent();
            DadosCb();
            MaskCPF mascarador = new MaskCPF(tbCpf);
            MaskCEP mascaradorCep = new MaskCEP(tbCep);
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidacaoCPFeCNPJ.ValidateCPF(tbCpf.Text) == "Erro")
                {
                    MessageBox.Show("Cpf digitado é inválido!");
                }
                else
                {
                    Buscar();
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
                    //Inserindo os Dados           
                    FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
                    funcionarioDAO.Insert(funcionario);
                    MessageBox.Show(funcionarioDAO.mensagem);
                    Clear();
                }
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
            cbFuncao.SelectedIndex = -1;
            tbCep.Clear();
        }

        private void DadosCb()
        {

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

        private void tbCep_LostFocus(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        public async void Buscar()
        {
            string cep = tbCep.Text;
            if (!string.IsNullOrEmpty(cep))
            {
                string url = $"https://viacep.com.br/ws/{cep}/json/";

                using (HttpClient client = new HttpClient())
                {
                    if (cep.Length == 9)
                    {
                        try
                        {
                            string response = await client.GetStringAsync(url);
                            var endereco = JsonConvert.DeserializeObject<Endereco>(response);

                            if (endereco != null)
                            {
                                tbRua.Text = endereco.Logradouro;
                                tbBairro.Text = endereco.Bairro;
                                cidade = endereco.Localidade;
                                estado = endereco.Uf;
                            }
                            else
                            {
                                MessageBox.Show("CEP não encontrado.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao buscar CEP: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira um CEP válido.");
            }
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(tbCep.Text != "")
            {
                Buscar();
            }
        }
    }
}
