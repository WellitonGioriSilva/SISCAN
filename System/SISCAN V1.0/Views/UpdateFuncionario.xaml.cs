using Newtonsoft.Json;
using SISCAN.Helpers;
using SISCAN.Models;
using SISCAN.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class UpdateFuncionario : Page
    {
        Funcionario funcionario = new Funcionario();
        string cidade;
        string estado;
        public UpdateFuncionario(Funcionario func)
        {
            InitializeComponent();
            DadosCb();
            funcionario = func;
            ImportDados();
            MaskCPF mascarador = new MaskCPF(tbCpf);
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Funcionario func = new Funcionario();

                func.Id = funcionario.Id;
                if (tbNome.Text != "")
                {
                    func.Nome = tbNome.Text;
                }
                else
                {
                    func.Nome = funcionario.Nome;
                }
                if (tbBairro.Text != "")
                {
                    func.Bairro = tbBairro.Text;
                }
                else
                {
                    func.Bairro = funcionario.Bairro;
                }
                if (tbCpf.Text != "")
                {
                    if (ValidacaoCPFeCNPJ.ValidateCPF(tbCpf.Text) == "Erro")
                    {
                        MessageBox.Show("Cpf digitado é inválido!");
                    }
                    else
                    {
                        func.Cpf = tbCpf.Text;
                    }
                }
                else
                {
                    func.Cpf = funcionario.Cpf;
                }
                if (tbRua.Text != "")
                {
                    func.Rua = tbRua.Text;
                }
                else
                {
                    func.Rua = funcionario.Rua;
                }
                if (tbNumero.Text != "")
                {
                    func.Numero = Convert.ToInt32(tbNumero.Text);
                }
                else
                {
                    func.Numero = funcionario.Numero;
                }
                if (cbFuncao.SelectedIndex != -1)
                {
                    func.Funcao = new Funcao();
                    func.Funcao.Id = cbFuncao.SelectedIndex + 1;
                }
                else
                {
                    func.Funcao = new Funcao();
                    func.Funcao.Id = funcionario.Funcao.Id;
                }
                if (cbSexo.SelectedIndex != -1)
                {
                    func.Sexo = cbSexo.SelectionBoxItem.ToString();
                }
                else
                {
                    func.Sexo = funcionario.Sexo;
                }
                if(tbCep.Text != "")
                {
                    func.cidade = cidade;
                    func.estado = estado;
                }
                else
                {
                    func.cidade = funcionario.cidade; 
                    func.estado = funcionario.estado;
                }


                //Inserindo os Dados           
                FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
                funcionarioDAO.Update(func);

                Clear();
            }
            catch (Exception ex)
            {

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
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar a atualização?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

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

        private void ImportDados()
        {
            lbNome.Content = "Nome: " + funcionario.Nome;
            lbBairro.Content = "Bairro: " + funcionario.Bairro;
            lbRua.Content = "Rua: " + funcionario.Rua;
            lbCpf.Content = "Cpf: " + funcionario.Cpf;
            lbNumero.Content = "Número: " + funcionario.Numero;
            lbSexo.Content = "Sexo: " + funcionario.Sexo;
            lbFuncao.Content = "Função: " + funcionario.Funcao.Nome;
        }

        private async void tbCep_LostFocus(object sender, RoutedEventArgs e)
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
                                estado = endereco.Uf;
                                cidade = endereco.Localidade;
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
            if (tbCep.Text != "")
            {
                Buscar();
            }
        }
    }
}
