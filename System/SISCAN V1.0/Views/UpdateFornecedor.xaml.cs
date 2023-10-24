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
using SISCAN.Models;
using SISCAN.Views;
using SISCAN.Helpers;

namespace SISCAN.Formularios
{
    /// <summary>
    /// Interação lógica para CadastrarFornecedor.xam
    /// </summary>
    public partial class UpdateFornecedor : Page
    {
        Fornecedor user = new Fornecedor();
        public UpdateFornecedor(Fornecedor fornecedor)
        {
            InitializeComponent();
            user = fornecedor;
            DadosCb();
            ImportDados();

        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Id = fornecedor.Id;

                if (tbRazaoSocial.Text != "")
                {
                    fornecedor.RazaoSocial = tbRazaoSocial.Text;
                }
                else
                if (tbCnpj.Text != "")
                {
                    if (ValidacaoCPFeCNPJ.ValidateCNPJ(tbCnpj.Text) == "Erro")
                    {
                        MessageBox.Show("Cpf digitado é inválido!");
                    }
                    else
                    {
                        fornecedor.Cnpj = tbCnpj.Text;
                    }
                }
                else
                if (tbBairro.Text != "")
                {
                    fornecedor.Bairro = tbBairro.Text;
                }
                else
                if (tbRua.Text != "")
                {
                    fornecedor.Rua = tbRua.Text;

                }
                else
                if (tbFantasia.Text != "")
                {
                    fornecedor.NomeFantasia = tbFantasia.Text;
                }
                else
                if (tbTelefone.Text != "")
                {
                    fornecedor.Telefone = tbTelefone.Text;
                }
                else
                if (tbInscricaoEstadual.Text != "")
                {
                    fornecedor.InscricaoEstadual = tbInscricaoEstadual.Text;
                }
                else
                if (tbResponsavel.Text != "")
                {
                    fornecedor.Responsavel = tbResponsavel.Text;
                }
                else
                if (cbCidade.SelectedIndex != 1)
                {
                    fornecedor.Cidade = new Cidade();
                    fornecedor.Cidade.ID = cbCidade.SelectedIndex + 1;
                }

                //Inserindo os Dados           
                FornecedorDAO fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.Update(fornecedor);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }
        private void Clear()
        {
            tbRazaoSocial.Clear();
            tbCnpj.Clear();
            tbBairro.Clear();
            tbRua.Clear();
            tbFantasia.Clear();
            tbTelefone.Clear();
            tbInscricaoEstadual.Clear();
            tbResponsavel.Clear();
            cbCidade.SelectedIndex = -1;
        }

        private void tbCancelar_Click(object sender, RoutedEventArgs e)
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
            fmFrame.NavigationService.Navigate(new ListarFornecedor());
        }

        private void DadosCb()
        {
            CidadeDAO cidDAO = new CidadeDAO();
            cbCidade.ItemsSource = cidDAO.List();
            cbCidade.DisplayMemberPath = "Nome";
        }

        private void ImportDados()
        {
            tbRazaoSocial.Text = "Razão Social: " + user.RazaoSocial;
            tbCnpj.Text = "Cnpj: " + user.Cnpj;
            tbBairro.Text = "Bairro: " + user.Bairro;
            tbRua.Text = "Rua: " + user.Rua;
            tbFantasia.Text = "Fantasia: " + user.NomeFantasia;
            tbTelefone.Text = "Telefone: " + user.Telefone;
            tbInscricaoEstadual.Text = "InscriçãoEstadual: " + user.InscricaoEstadual;
            tbResponsavel.Text = "Responsavel: " + user.Responsavel;
        }
    }
}
