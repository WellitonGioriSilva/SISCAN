using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SISCAN.Helpers
{
    internal class MaskCNPJ
    {
        private TextBox textBox;

        public MaskCNPJ(TextBox textBox)
        {
            this.textBox = textBox;
            this.textBox.PreviewTextInput += TextBox_PreviewTextInput;
            this.textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Impede a entrada de caracteres não numéricos
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoSemMascara = new string(textBox.Text.Where(char.IsDigit).ToArray());

            if (textoSemMascara.Length > 14)
            {
                // Se o CNPJ digitado for muito longo, limite-o a 14 dígitos
                textoSemMascara = textoSemMascara.Substring(0, 14);
            }

            string cnpjMascarado = string.Empty;
            for (int i = 0; i < textoSemMascara.Length; i++)
            {
                if (i == 2 || i == 5)
                {
                    cnpjMascarado += ".";
                }
                else if (i == 8)
                {
                    cnpjMascarado += "/";
                }
                else if (i == 12)
                {
                    cnpjMascarado += "-";
                }
                cnpjMascarado += textoSemMascara[i];
            }

            textBox.Text = cnpjMascarado;
            textBox.CaretIndex = cnpjMascarado.Length;
        }
    }
}
