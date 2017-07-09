using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src
{
    class Prompts
    {
        public static DialogResult ShowShouldSaveDialog()
        {
            if (Globals.needToSave)
            {
                DialogResult result = MessageBox.Show("You have unsaved changes, would you like to save the ROM?", "Save ROM?", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    ROM.Instance.saveFileAs(ROM.Instance.Filepath, ROM.Instance.Endian);
                return result;
            }
            return DialogResult.None;
        }

        public static string ShowInputDialog(string text, string title)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
        
    }
}
