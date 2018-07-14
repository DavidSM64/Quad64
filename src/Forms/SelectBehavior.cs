using Quad64.src.JSON;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    public partial class SelectBehavior : Form
    {
        public bool ClickedSelect { get; set; }
        public uint ReturnBehavior { get; set; }

        public SelectBehavior()
        {
            InitializeComponent();
            foreach (BehaviorNameEntry entry in Globals.behaviorNameEntries)
            {
                listView1.Items.Add("("+entry.Behavior.ToString("X8") + ") " + entry.Name);
            }
            updateTheme();
        }

        private void updateTheme()
        {
            BackColor = Theme.COMBOS_BACKGROUND;
            label_filter.BackColor = Theme.COMBOS_BACKGROUND;
            label_filter.ForeColor = Theme.COMBOS_TEXT;
            selectButton.BackColor = Theme.DEFAULT_BUTTON_BACKGROUND;
            selectButton.ForeColor = Theme.DEFAULT_BUTTON_TEXT;
            cancelButton.BackColor = Theme.DEFAULT_BUTTON_BACKGROUND;
            cancelButton.ForeColor = Theme.DEFAULT_BUTTON_TEXT;
            textBox_filter.BackColor = Theme.DEFAULT_DROPDOWNLIST_BACKGROUND;
            textBox_filter.ForeColor = Theme.DEFAULT_DROPDOWNLIST_TEXT;
            listView1.BackColor = Theme.COMBOS_BACKGROUND;
            listView1.ForeColor = Theme.COMBOS_TEXT;
        }

        private Font textFont = new Font("Courier New", 10, FontStyle.Bold);
        private Brush textBrush = new SolidBrush(Theme.COMBOS_OTHER_TITLE);
        private Pen bgPen = new Pen(Theme.COMBOS_BACKGROUND, 100.0f);
        private Rectangle bgRect = new Rectangle(0, 0, 400, 2);
        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            bgRect = new Rectangle(0, 0, listView1.Columns[0].Width, 2);
            int x = (listView1.Width / 2) - (TextRenderer.MeasureText(e.Header.Text, textFont).Width / 2);
            e.Graphics.DrawRectangle(bgPen, bgRect);
            e.Graphics.DrawString(e.Header.Text, textFont, textBrush, x, 4);
        }

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }
        
        private void selectButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ClickedSelect = true;
                ReturnBehavior = (uint)Convert.ToInt32(listView1.SelectedItems[0].Text.Substring(1, 8), 16);
            }
            else
                ClickedSelect = false;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            ClickedSelect = false;
            Close();
        }

        private Color col1 = Theme.COMBOS_OTHER_MAIN;
        private Color col2 = Theme.COMBOS_OTHER_SECONDARY;
        private Color col3 = Theme.COMBOS_OTHER_HIGHLIGHT_TEXT;
        private Color col4 = Theme.COMBOS_OTHER_HIGHLIGHT;
        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // If this item is the selected item
            if (e.Item.Selected)
            {
                e.Item.ForeColor = col3;
                e.Item.BackColor = col4;
            }
            else
            {
                if (e.ItemIndex % 2 == 0)
                    listView1.Items[e.ItemIndex].BackColor = col1;
                else
                    listView1.Items[e.ItemIndex].BackColor = col2;
            }

            e.DrawBackground();
            e.DrawText();
        }

        /*
            The idea for the filter box comes from aglab2. 
            His implementation was a little overkill though, so I made my own simpler version.
        */
        private void textBox_filter_TextChanged(object sender, EventArgs e)
        {
            BeginUpdate(listView1);

            listView1.Items.Clear();
            foreach (BehaviorNameEntry entry in Globals.behaviorNameEntries)
            {
                string entryName = "(" + entry.Behavior.ToString("X8") + ") " + entry.Name;
                if(entryName.ToUpper().Contains(textBox_filter.Text.ToUpper()))
                    listView1.Items.Add(entryName);
            }

            EndUpdate(listView1);
        }

        private const int WM_USER = 0x0400;
        private const int EM_SETEVENTMASK = (WM_USER + 69);
        private const int WM_SETREDRAW = 0x0b;
        private IntPtr OldEventMask;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private void BeginUpdate(ListView lv)
        {
            SendMessage(lv.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            OldEventMask = SendMessage(lv.Handle, EM_SETEVENTMASK, IntPtr.Zero, IntPtr.Zero);
        }

        private void EndUpdate(ListView lv)
        {
            SendMessage(lv.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            SendMessage(lv.Handle, EM_SETEVENTMASK, IntPtr.Zero, OldEventMask);
        }
    }
}
