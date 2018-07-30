using Quad64.src.JSON;
using Quad64.src.LevelInfo;
using Quad64.src.Scripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    partial class ScriptDumps : Form
    {
        private Level level;
        private RichTextBox currentTextbox, lastTextBoxForLevelTab, lastTextBoxForObjectsTab;
        public ScriptDumps(Level level)
        {
            this.level = level;
            InitializeComponent();
            lastTextBoxForLevelTab = lt_ls_textbox;
            lastTextBoxForObjectsTab = ot_gls_textbox;
            initGeoLayoutForLevelTab();
            initFast3DForLevelTab();
            currentTextbox = lt_ls_textbox;
            organizeCurrentLevelScript(level.LevelScriptCommands_ForDump);
            initObjectList();
            setupTheme();
        }

        private static uint bytesToInt(byte[] b, int offset, int length)
        {
            switch (length)
            {
                case 1: return b[0 + offset];
                case 2: return (uint)(b[0 + offset] << 8 | b[1 + offset]);
                case 3: return (uint)(b[0 + offset] << 16 | b[1 + offset] << 8 | b[2 + offset]);
                default: return (uint)(b[0 + offset] << 24 | b[1 + offset] << 16 | b[2 + offset] << 8 | b[3 + offset]);
            }
        }

        private string format_byte(byte b)
        {
            return b.ToString("X2") + " ";
        }

        private string format_short(byte[] cmd, int offset)
        {
            return string.Concat(((ushort)bytesToInt(cmd, offset, 2)).ToString("X4"), " ");
        }

        private string format_int(byte[] cmd, int offset)
        {
            return string.Concat((bytesToInt(cmd, offset, 4)).ToString("X8"), " ");
        }

        private string format_long(byte[] cmd, int offset)
        {
            return string.Concat((bytesToInt(cmd, offset, 4)).ToString("X8"), (bytesToInt(cmd, offset+4, 4)).ToString("X8"), " ");
        }

        private string format_threeBytesAsTwo(byte[] cmd, int offset)
        {
            uint tB = bytesToInt(cmd, offset, 3);

            return string.Concat(((tB >> 12) & 0xFFF).ToString("X3"), " ", (tB & 0xFFF).ToString("X3"), " ");
        }


        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwndLock, Int32 wMsg, Int32 wParam, ref Point pt);

        private static Point GetScrollPos(RichTextBox txtbox)
        {
            const int EM_GETSCROLLPOS = 0x0400 + 221;
            Point pt = new Point();

            SendMessage(txtbox.Handle, EM_GETSCROLLPOS, 0, ref pt);
            return pt;
        }

        private static void SetScrollPos(Point pt, RichTextBox txtbox)
        {
            const int EM_SETSCROLLPOS = 0x0400 + 222;
            SendMessage(txtbox.Handle, EM_SETSCROLLPOS, 0, ref pt);
        }

        private const int WM_USER = 0x0400;
        private const int EM_SETEVENTMASK = (WM_USER + 69);
        private const int WM_SETREDRAW = 0x0b;
        private IntPtr OldEventMask;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private void BeginUpdate(RichTextBox txtbox)
        {
            SendMessage(txtbox.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            OldEventMask = SendMessage(txtbox.Handle, EM_SETEVENTMASK, IntPtr.Zero, IntPtr.Zero);
        }

        private void EndUpdate(RichTextBox txtbox)
        {
            SendMessage(txtbox.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            SendMessage(txtbox.Handle, EM_SETEVENTMASK, IntPtr.Zero, OldEventMask);
        }

        private void switchTextBoxes(RichTextBox newCurrent)
        {
            currentTextbox = newCurrent;
            updateScriptDump(null, null);
            updateScriptDumpWordWrap(null, null);
        }

        private void updateScriptDump(object sender, EventArgs e)
        {
            BeginUpdate(currentTextbox);
            Point scrollPos = GetScrollPos(currentTextbox);
            //Console.WriteLine(currentTextbox.Name);
            if (currentTextbox.Name.EndsWith("_ls_textbox"))
            {
                if (sd_tabs.SelectedIndex == 0)
                    organizeCurrentLevelScript(level.LevelScriptCommands_ForDump);
            }
            else if (currentTextbox.Name.EndsWith("_gls_textbox"))
            {
                if (sd_tabs.SelectedIndex == 0)
                    organizeCurrentGeoLayoutScript(level.Areas[lt_gls_areaIndex].AreaModel.GeoLayoutCommands_ForDump);
                else 
                    if (listBoxObjects.SelectedIndex > -1)
                    {
                        ushort key = objectCombos[listBoxObjects.SelectedIndex].ModelID;;
                        if (level.ModelIDs.ContainsKey(key) && level.ModelIDs[key].GeoLayoutCommands_ForDump.Count > 0)
                        {
                            organizeCurrentGeoLayoutScript(level.ModelIDs[key].GeoLayoutCommands_ForDump);
                        }
                        else
                        {
                            currentTextbox.ResetText();
                            currentTextbox.ForeColor = Theme.SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_TEXT;
                            currentTextbox.Text = "<No script found>";
                        }
                    }
            }
            else if (currentTextbox.Name.EndsWith("_f3d_textbox"))
            {
                if (sd_tabs.SelectedIndex == 0)
                    organizeCurrentFast3DScript(level.Areas[lt_f3d_areaIndex].
                    AreaModel.Fast3DCommands_ForDump[lt_f3d_listbox.SelectedIndex]);
                else if (listBoxObjects.SelectedIndex > -1)
                {
                    ushort key = objectCombos[listBoxObjects.SelectedIndex].ModelID;
                    if (level.ModelIDs.ContainsKey(key) && ot_f3d_listbox.SelectedIndex > -1)
                    {
                        if(ot_f3d_listbox.SelectedItem.ToString().Equals("<Error 80>"))
                            currentTextbox.Text = "Error: Quad64 cannot read display lists from segment 0.";
                        else
                            organizeCurrentFast3DScript(level.ModelIDs[key].Fast3DCommands_ForDump[ot_f3d_listbox.SelectedIndex]);
                    }
                    else
                    {
                        currentTextbox.ResetText();
                        currentTextbox.ForeColor = Theme.SCRIPTDUMPS_FAST3D_TEXTBOX_TEXT;
                        currentTextbox.Text = "<No script found>";
                    }
                }
            }
            else if (currentTextbox.Name.EndsWith("_beh_textbox"))
            {
                if (sd_tabs.SelectedIndex == 1 && listBoxObjects.SelectedIndex > -1)
                {
                    //ushort key = objectCombos[listBoxObjects.SelectedIndex].ModelID;
                   // if (objectCombos.Count)
                   // {
                        List<ScriptDumpCommandInfo> behaviorDump = new List<ScriptDumpCommandInfo>();
                        BehaviorScripts.parse(ref behaviorDump, objectCombos[listBoxObjects.SelectedIndex].Behavior);
                        organizeCurrentBehaviorScript(behaviorDump);
                   // }
                    //else
                    //{
                     //   currentTextbox.ResetText();
                     //   currentTextbox.Text = "<No script found>";
                    //}
                }
            }
            SetScrollPos(scrollPos, currentTextbox);
            EndUpdate(currentTextbox);
        }

        private void updateScriptDumpWordWrap(object sender, EventArgs e)
        {
            BeginUpdate(currentTextbox);
            Point scrollPos = GetScrollPos(currentTextbox);
            currentTextbox.WordWrap = wordWrapCB.Checked;
            SetScrollPos(scrollPos, currentTextbox);
            EndUpdate(currentTextbox);
        }

        private void subTabsLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(subTabsLevel.SelectedIndex)
            {
                case 0:
                    switchTextBoxes(lt_ls_textbox);
                    break;
                case 1:
                    switchTextBoxes(lt_gls_textbox);
                    break;
                case 2:
                    switchTextBoxes(lt_f3d_textbox);
                    break;
            }
        }

        private void sd_tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Switching between level tab and objects tab
            switch (sd_tabs.SelectedIndex)
            {
                case 0:
                    lastTextBoxForObjectsTab = currentTextbox;
                    switchTextBoxes(lastTextBoxForLevelTab);
                    break;
                case 1:
                    lastTextBoxForLevelTab = currentTextbox;
                    if (listBoxObjects.SelectedIndex == -1 && listBoxObjects.Items.Count > 0)
                        listBoxObjects.SelectedIndex = 0;
                    switchTextBoxes(lastTextBoxForObjectsTab);
                    break;
            }
        }

        private void setupTheme()
        {
            BackColor = Theme.SCRIPTDUMPS_BACKGROUND;
            splitContainer1.BackColor = Theme.SCRIPTDUMPS_BACKGROUND;
            tabLevel.BackColor = Theme.SCRIPTDUMPS_BACKGROUND;
            tabObjects.BackColor = Theme.SCRIPTDUMPS_BACKGROUND;
            panel1.BackColor = Theme.SCRIPTDUMPS_OPTIONS_BACKGROUND;
            showRomCB.ForeColor = Theme.SCRIPTDUMPS_TEXT;
            SegAddrCB.ForeColor = Theme.SCRIPTDUMPS_TEXT;
            IndentCB.ForeColor = Theme.SCRIPTDUMPS_TEXT;
            wordWrapCB.ForeColor = Theme.SCRIPTDUMPS_TEXT;
            FormatCB.ForeColor = Theme.SCRIPTDUMPS_TEXT;
            CommCB.ForeColor = Theme.SCRIPTDUMPS_TEXT;
            ListObjectsSortLabel.ForeColor = Theme.SCRIPTDUMPS_TEXT;

            lt_f3d_listbox.BackColor = Theme.SCRIPTDUMPS_FAST3D_LISTBOX_BACKGROUND;
            listBoxObjects.BackColor = Theme.SCRIPTDUMPS_OBJECTSTAB_LISTBOX_BACKGROUND;
            ot_f3d_listbox.BackColor = Theme.SCRIPTDUMPS_FAST3D_LISTBOX_BACKGROUND;

            lt_ls_textbox.BackColor = Theme.SCRIPTDUMPS_LEVEL_TEXTBOX_BACKGROUND;
            lt_gls_textbox.BackColor = Theme.SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_BACKGROUND;
            lt_f3d_textbox.BackColor = Theme.SCRIPTDUMPS_FAST3D_TEXTBOX_BACKGROUND;
            
            ot_gls_textbox.BackColor = Theme.SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_BACKGROUND;
            ot_f3d_textbox.BackColor = Theme.SCRIPTDUMPS_FAST3D_TEXTBOX_BACKGROUND;
            ot_beh_textbox.BackColor = Theme.SCRIPTDUMPS_BEHAVIOR_TEXTBOX_BACKGROUND;

            lt_gls_radioPanel.BackColor = Theme.SCRIPTDUMPS_LEVELTAB_GEOLAYOUT_AREAPANEL_BACKGROUND;
            lt_f3d_radioPanel.BackColor = Theme.SCRIPTDUMPS_LEVELTAB_FAST3D_AREAPANEL_BACKGROUND;

            comboBoxObjectSort.BackColor = Theme.SCRIPTDUMPS_OBJECTSTAB_SORT_DROPDOWNLIST_BACKGROUND;
            comboBoxObjectSort.ForeColor = Theme.SCRIPTDUMPS_OBJECTSTAB_SORT_DROPDOWNLIST_TEXT;
        }

        private void listBoxObjects_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            SolidBrush textBrush = new SolidBrush(Theme.SCRIPTDUMPS_OBJECTSTAB_LISTBOX_TEXT);
            if (isSelected)
            {
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          
                                          Theme.SCRIPTDUMPS_OBJECTSTAB_LISTBOX_HIGHLIGHT);
                textBrush = new SolidBrush(Theme.SCRIPTDUMPS_OBJECTSTAB_LISTBOX_HIGHLIGHTEDTEXT);
            }

            e.DrawBackground();
            e.Graphics.DrawString(listBoxObjects.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        private void lt_f3d_listbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            SolidBrush textBrush = new SolidBrush(Theme.SCRIPTDUMPS_FAST3D_LISTBOX_TEXT);
            if (isSelected)
            {
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Theme.SCRIPTDUMPS_FAST3D_LISTBOX_HIGHLIGHT);
                textBrush = new SolidBrush(Theme.SCRIPTDUMPS_FAST3D_LISTBOX_HIGHLIGHTEDTEXT);
            }
            
            e.DrawBackground();
            e.Graphics.DrawString(lt_f3d_listbox.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        private void ot_f3d_listbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            SolidBrush textBrush = new SolidBrush(Theme.SCRIPTDUMPS_FAST3D_LISTBOX_TEXT);
            if (isSelected)
            {
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Theme.SCRIPTDUMPS_FAST3D_LISTBOX_HIGHLIGHT);
                textBrush = new SolidBrush(Theme.SCRIPTDUMPS_FAST3D_LISTBOX_HIGHLIGHTEDTEXT);
            }

            e.DrawBackground();
            e.Graphics.DrawString(ot_f3d_listbox.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }
    }
}
