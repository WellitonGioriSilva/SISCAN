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
    public partial class CadastrarFornecedor : Page
    {
        public CadastrarFornecedor()
        {
            InitializeComponent();
            DadosCb();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidacaoCPFeCNPJ.ValidateCNPJ(tbCnpj.Text) == "Erro")
                {
                    MessageBox.Show("Cpf digitado é inválido!");
                }
                else
                {
                    //Setando informações na tabela fornecedor
                    Fornecedor fornecedor = new Fornecedor();
                    fornecedor.RazaoSocial = tbRazaoSocial.Text;
                    fornecedor.Cnpj = tbCnpj.Text;
                    fornecedor.Bairro = tbBairro.Text;
                    fornecedor.Rua = tbRua.Text;
                    fornecedor.NomeFantasia = tbFantasia.Text;
                    fornecedor.Telefone = tbTelefone.Text;
                    fornecedor.InscricaoEstadual = tbInscricaoEstadual.Text;
                    fornecedor.Responsavel = tbResponsavel.Text;
                    fornecedor.Cidade = new Cidade();
                    if (cbCidade.SelectedItem is Cidade selectedItem)
                    {
                        fornecedor.Cidade.ID = selectedItem.ID;
                    }

                    //Inserindo os Dados           
                    FornecedorDAO fornecedorDAO = new FornecedorDAO();
                    fornecedorDAO.Insert(fornecedor);

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

        private void DadosCb()
        {
            CidadeDAO cidDAO = new CidadeDAO();
            cbCidade.ItemsSource = cidDAO.List();
            cbCidade.DisplayMemberPath = "Nome";
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
    }
}
