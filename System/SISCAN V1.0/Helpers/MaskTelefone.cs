using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SISCAN.Helpers
{
    internal class MaskTelefone
    {
        private TextBox textBox;

        public MaskTelefone(TextBox textBox)
        {
            this.textBox = textBox;
            this.textBox.PreviewTextInput += TextBox_PreviewTextInput;
            this.textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permite apenas a entrada de caracteres numéricos e o caractere de adição (+)
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != "+")
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoSemMascara = new string(textBox.Text.Where(char.IsDigit).ToArray());

            if (textoSemMascara.Length > 11)
            {
                // Se o número de telefone digitado for muito longo, limite-o a 15 dígitos
                textoSemMascara = textoSemMascara.Substring(0, 11);
            }

            string telefoneMascarado = string.Empty;
            for (int i = 0; i < textoSemMascara.Length; i++)
            {
                if (i == 0)
                {
                    telefoneMascarado += "(";
                }
                else if (i == 2)
                {
                    telefoneMascarado += ") ";
                }
                else if (i == 7)
                {
                    telefoneMascarado += "-";
                }
                telefoneMascarado += textoSemMascara[i];
            }

            textBox.Text = telefoneMascarado;
            textBox.CaretIndex = telefoneMascarado.Length;
        }
    }
}
