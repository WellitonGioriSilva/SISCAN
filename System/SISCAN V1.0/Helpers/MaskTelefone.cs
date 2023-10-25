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
            // Impede a entrada de caracteres não numéricos
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textoSemMascara = new string(textBox.Text.Where(char.IsDigit).ToArray());

            if (textoSemMascara.Length > 11)
            {
                // Se o número de telefone digitado for muito longo, limite-o a 11 dígitos
                textoSemMascara = textoSemMascara.Substring(0, 11);
            }

            string telefoneMascarado = string.Empty;
            if (textoSemMascara.Length >= 2)
            {
                telefoneMascarado += "(" + textoSemMascara.Substring(0, 2) + ")";
                if (textoSemMascara.Length > 2)
                {
                    telefoneMascarado += " ";
                }
            }

            if (textoSemMascara.Length >= 7)
            {
                telefoneMascarado += textoSemMascara.Substring(2, 4) + "-";
            }

            if (textoSemMascara.Length > 6)
            {
                telefoneMascarado += textoSemMascara.Substring(6);
            }

            textBox.Text = telefoneMascarado;
            textBox.CaretIndex = telefoneMascarado.Length;
        }
    }
}
