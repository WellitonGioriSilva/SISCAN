using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SISCAN.Helpers
{
    internal class MaskCPF
    {
        private TextBox textBox;

        public MaskCPF(TextBox textBox)
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
                // Se o CPF digitado for muito longo, limite-o a 11 dígitos
                textoSemMascara = textoSemMascara.Substring(0, 11);
            }

            string cpfMascarado = string.Empty;
            for (int i = 0; i < textoSemMascara.Length; i++)
            {
                if (i == 3 || i == 6)
                {
                    cpfMascarado += ".";
                }
                else if (i == 9)
                {
                    cpfMascarado += "-";
                }
                cpfMascarado += textoSemMascara[i];
            }

            textBox.Text = cpfMascarado;
            textBox.CaretIndex = cpfMascarado.Length;
        }
    }
}
