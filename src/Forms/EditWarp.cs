using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    public partial class EditWarp : Form
    {
        public byte fromID, toLevel, toArea, toID, triggerID;
        public short tX, tY, tZ;
        public bool pressedSaved = false;
        bool isInstantWarp = false;
        List<ushort> levelIds = new List<ushort>();

        public EditWarp(string title, byte fromID, byte toLevel, byte toArea, byte toID)
        {
            this.fromID = fromID;
            this.toLevel = toLevel;
            this.toArea = toArea;
            this.toID = toID;
            isInstantWarp = false;
            InitializeComponent();
            Text = title;
        }

        public EditWarp(byte triggerID, byte toArea, short teleX, short teleY, short teleZ)
        {
            this.triggerID = triggerID;
            this.toArea = toArea;
            tX = teleX;
            tY = teleY;
            tZ = teleZ;
            isInstantWarp = true;
            InitializeComponent();
            Text = "Update Instant Warp";
        }

        private void EditWarp_Load(object sender, EventArgs e)
        {
            if (Globals.useHexadecimal)
            {
                iw_trigger.Hexadecimal = true;
                iw_area.Hexadecimal = true;
                iw_tx.Hexadecimal = true;
                iw_ty.Hexadecimal = true;
                iw_tz.Hexadecimal = true;

                w_areaID.Hexadecimal = true;
                w_warpFrom.Hexadecimal = true;
                w_warpToID.Hexadecimal = true;
            }


            if (!isInstantWarp)
            {
                panel_instWarp_controls.Hide();
                panel_warp_controls.Show();
                w_warpFrom.Value = fromID;
                w_warpToID.Value = toID;
                w_areaID.Value = toArea;
                levelIds.Clear();
                foreach (KeyValuePair<string, ushort> entry in ROM.Instance.levelIDs)
                {
                    w_selectLevel.Items.Add(entry.Key + " (0x" + entry.Value.ToString("X2") + ")");
                    levelIds.Add(entry.Value);
                    if (entry.Value == toLevel)
                    {
                        w_selectLevel.SelectedIndex = w_selectLevel.Items.Count - 1;
                    }
                }
            }
            else
            {
                panel_instWarp_controls.Show();
                panel_warp_controls.Hide();
                iw_trigger.Value = triggerID;
                iw_area.Value = toArea;
                iw_tx.Value = tX;
                iw_ty.Value = tY;
                iw_tz.Value = tZ;
            }

            updateTheme();
        }

        private void updateLabelColor(ref Label label)
        {
            label.ForeColor = Theme.DEFAULT_TEXT;
            label.BackColor = Theme.DEFAULT_PANEL_BACKGROUND;
        }

        private void updateUpDownColor(ref NumericUpDown upDown)
        {
            upDown.ForeColor = Theme.DEFAULT_UPDOWN_TEXT;
            upDown.BackColor = Theme.DEFAULT_UPDOWN_BACKGROUND;
        }

        private void updateTheme()
        {
            BackColor = Theme.DEFAULT_BACKGROUND;
            panel_instWarp_controls.BackColor = Theme.DEFAULT_PANEL_BACKGROUND;
            panel_warp_controls.BackColor = Theme.DEFAULT_PANEL_BACKGROUND;
            w_selectLevel.BackColor = Theme.DEFAULT_DROPDOWNLIST_BACKGROUND;
            w_selectLevel.ForeColor = Theme.DEFAULT_DROPDOWNLIST_TEXT;
            cancelButton.BackColor = Theme.DEFAULT_BUTTON_BACKGROUND;
            cancelButton.ForeColor = Theme.DEFAULT_BUTTON_TEXT;
            selectButton.BackColor = Theme.DEFAULT_BUTTON_BACKGROUND;
            selectButton.ForeColor = Theme.DEFAULT_BUTTON_TEXT;
            updateLabelColor(ref label1);
            updateLabelColor(ref label2);
            updateLabelColor(ref label3);
            updateLabelColor(ref label4);
            updateLabelColor(ref label5);
            updateLabelColor(ref label6);
            updateLabelColor(ref label7);
            updateLabelColor(ref label9);
            updateLabelColor(ref label10);
            updateUpDownColor(ref iw_area);
            updateUpDownColor(ref iw_trigger);
            updateUpDownColor(ref iw_tx);
            updateUpDownColor(ref iw_ty);
            updateUpDownColor(ref iw_tz);
            updateUpDownColor(ref w_warpFrom);
            updateUpDownColor(ref w_warpToID);
            updateUpDownColor(ref w_areaID);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            pressedSaved = false;
            Hide();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (!isInstantWarp)
            {
                fromID = (byte)w_warpFrom.Value;
                toID = (byte)w_warpToID.Value;
                toArea = (byte)w_areaID.Value;
                toLevel = (byte)levelIds[w_selectLevel.SelectedIndex];
            }
            else
            {
                panel_instWarp_controls.Show();
                panel_warp_controls.Hide();
                iw_trigger.Value = triggerID;
                iw_area.Value = toArea;
                iw_tx.Value = tX;
                iw_ty.Value = tY;
                iw_tz.Value = tZ;
            }
            pressedSaved = true;
            Hide();
        }
    }
}
