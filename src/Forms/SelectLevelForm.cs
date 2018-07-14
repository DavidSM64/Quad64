using Quad64.src;
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
                level_list.Items.Add(entry.Key + " (0x" + entry.Value.ToString("X2") + ")");
            }

            foreach (ushort entry in rom.extra_levelIDs)
            {
                level_list.Items.Add("[EXT] Extra Level (0x" + entry.ToString("X2") + ")");
            }

            //comboBox1.Items.Add("Custom ID value");
            level_list.SelectedIndex = rom.getLevelIndexById(levelID);

            BackColor = Theme.DEFAULT_BACKGROUND;
            level_list.BackColor = Theme.DEFAULT_DROPDOWNLIST_BACKGROUND;
            level_list.ForeColor = Theme.DEFAULT_DROPDOWNLIST_TEXT;
            cancel_button.BackColor = Theme.DEFAULT_BUTTON_BACKGROUND;
            cancel_button.ForeColor = Theme.DEFAULT_BUTTON_TEXT;
            load_button.BackColor = Theme.DEFAULT_BUTTON_BACKGROUND;
            load_button.ForeColor = Theme.DEFAULT_BUTTON_TEXT;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            levelID = ROM.Instance.getLevelIdFromIndex(level_list.SelectedIndex);
            changeLevel = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
