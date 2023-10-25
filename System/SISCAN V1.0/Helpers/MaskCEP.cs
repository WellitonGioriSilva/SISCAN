using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SISCAN.Helpers
{
    internal class MaskCEP
    {
        private TextBox textBox;

        public MaskCEP(TextBox textBox)
        {
            this.textBox = textBox;
            this.textBox.PreviewTextInput += TextBox_PreviewTextInput;
            this.textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verifica se a entrada contém apenas números e está no formato correto do CEP
            if (!string.IsNullOrEmpty(e.Text) && (char.IsDigit(e.Text, 0) || (e.Text == "-" && !textBox.Text.Contains("-"))))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Remove caracteres não numéricos e mantém até 8 dígitos
            string textoSemMascara = new string(textBox.Text.Where(char.IsDigit).ToArray());

            if (textoSemMascara.Length > 8)
            {
                textoSemMascara = textoSemMascara.Substring(0, 8);
            }

            string cepMascarado = textoSemMascara;

            if (cepMascarado.Length > 5)
            {
                cepMascarado = cepMascarado.Insert(5, "-");
            }

            textBox.Text = cepMascarado;
            textBox.CaretIndex = cepMascarado.Length;
        }
    }
}
