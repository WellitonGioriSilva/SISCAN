using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SISCAN.Helpers
{
    internal class MaskCPFeCNPJ
    {
        public static void MaskCPF(TextBox textBox)
        {
            int cursorPosition = 0; // Salvar a posição do cursor

            textBox.TextChanged += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    // Remove caracteres não numéricos
                    string cpf = new string(textBox.Text.Where(char.IsDigit).ToArray());

                    // Salvar a posição atual do cursor
                    cursorPosition = textBox.SelectionStart;

                    if (cpf.Length <= 3)
                    {
                        // Insere apenas os primeiros 3 dígitos
                        textBox.Text = cpf;
                    }
                    else if (cpf.Length <= 6)
                    {
                        // Insere pontos após o terceiro dígito
                        textBox.Text = cpf.Insert(3, ".");
                    }
                    else if (cpf.Length <= 9)
                    {
                        // Insere pontos e traço após o sexto dígito
                        textBox.Text = cpf.Insert(3, ".").Insert(7, ".");
                    }
                    else
                    {
                        // Insere todos os pontos e o traço
                        textBox.Text = cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                    }

                    // Definir a posição do cursor de volta para onde estava
                    textBox.SelectionStart = cursorPosition;
                }
            };
        }
    }
}
