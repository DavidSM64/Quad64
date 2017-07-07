using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64
{
    partial class SelectLevelForm : Form
    {
        public ushort levelID = 0x10;
        public bool changeLevel = false;
        public SelectLevelForm(ushort levelID)
        {
            InitializeComponent();
            this.levelID = levelID;
            ROM rom = ROM.Instance;
            //comboBox1.Item
            foreach (KeyValuePair<string, ushort> entry in rom.levelIDs)
            {
                comboBox1.Items.Add(entry.Key + " (0x" + entry.Value.ToString("X2") + ")");
            }
            //comboBox1.Items.Add("Custom ID value");
            comboBox1.SelectedIndex = rom.getLevelIndexById(levelID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            levelID = ROM.Instance.getLevelIdFromIndex(comboBox1.SelectedIndex);
            changeLevel = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
