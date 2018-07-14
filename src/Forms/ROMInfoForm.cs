using Quad64.src;
using System;
using System.Windows.Forms;

namespace Quad64
{
    public partial class ROMInfoForm : Form
    {
        public ROMInfoForm()
        {
            InitializeComponent();
            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            table.Padding = new Padding(0, 0, vertScrollWidth, 0);
            ROM rom = ROM.Instance;
            addNewRow("File Name", rom.getROMFileName());
            addNewRow("Internal Name", rom.getInternalName());
            addNewRow("Region", rom.getRegionText());
            addNewRow("Endianness", rom.getEndianText());
            addNewRow("Size (MB)", (rom.Bytes.Length / 1024 / 1024).ToString());

            BackColor = Theme.DEFAULT_BACKGROUND;
            label1.ForeColor = Theme.DEFAULT_TEXT;
        }

        private void addNewRow(string rowName, string rowData)
        {
            table.Controls.Add(newLabel(rowName), 0, table.Controls.Count/2);
            table.Controls.Add(newLabel(rowData), 1, table.Controls.Count / 2);
        }

        private TextBox newLabel(string text)
        {
            TextBox label = new TextBox();
            label.Text = text;
            label.Font = new System.Drawing.Font("Times new roman", 10);
            label.TextAlign = HorizontalAlignment.Center;
            label.AutoSize = false;
            label.Dock = DockStyle.Fill;
            label.ReadOnly = true;
            label.BorderStyle = 0;
            label.BackColor = this.BackColor;
            label.TabStop = false;
            label.BackColor = Theme.DEFAULT_BACKGROUND;
            label.ForeColor = Theme.DEFAULT_TEXT;
            return label;
        }
        
    }
}
