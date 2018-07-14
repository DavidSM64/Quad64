using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    partial class ScriptDumps : Form
    {
        int lt_f3d_areaIndex = 0;
        
        private void lt_f3d_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switchTextBoxes(lt_f3d_textbox);
        }

        private void lt_f3d_radioPanel_Resize(object sender, EventArgs e)
        {
            int control_count = lt_f3d_radioPanel.Controls.Count;
            int rb_width = lt_f3d_radioPanel.Width / control_count;
            for (int i = 0; i < control_count; i++)
            {
                RadioButton but = (RadioButton)lt_f3d_radioPanel.Controls[i];
                but.Location = new Point(rb_width * i, lt_f3d_radioPanel.Location.Y);
                but.Size = new Size(rb_width, lt_f3d_radioPanel.Height);
            }
        }
        
        private void lt_f3d_areas_CheckedChanged(object sender, EventArgs e)
        {
            int control_count = lt_f3d_radioPanel.Controls.Count;
            for (int i = 0; i < control_count; i++)
            {
                RadioButton but = (RadioButton)lt_f3d_radioPanel.Controls[i];
                if (but.Checked)
                {
                    lt_f3d_areaIndex = i;
                    refreshLevelTabFast3DList();
                    switchTextBoxes(lt_f3d_textbox);
                    return;
                }
            }
        }

        private void refreshLevelTabFast3DList()
        {
            lt_f3d_listbox.Items.Clear();
            List<List<ScriptDumpCommandInfo>> list = level.Areas[lt_f3d_areaIndex].AreaModel.Fast3DCommands_ForDump;
            for (int i = 0; i < list.Count; i++)
            {
                lt_f3d_listbox.Items.Add(list[i][0].segAddress.ToString("X8"));
            }
            lt_f3d_listbox.SelectedIndex = 0;
        }

        private void initFast3DForLevelTab()
        {
            refreshLevelTabFast3DList();
            lt_f3d_areaIndex = 0;
            int num_areas = level.Areas.Count;
            int rb_width = lt_f3d_radioPanel.Width / num_areas;
            lt_f3d_radioPanel.Controls.Clear();
            for (int i = 0; i < num_areas; i++)
            {
                RadioButton newButton = new RadioButton();
                newButton.Appearance = Appearance.Button;
                newButton.TextAlign = ContentAlignment.MiddleCenter;
                newButton.Name = "lt_f3d_area" + level.Areas[i].AreaID;
                newButton.Text = "Area " + level.Areas[i].AreaID;
                newButton.Location = new Point(rb_width * i, lt_f3d_radioPanel.Location.Y);
                newButton.Size = new Size(rb_width, lt_f3d_radioPanel.Height);
                newButton.CheckedChanged += lt_f3d_areas_CheckedChanged;
                newButton.BackColor = Theme.SCRIPTDUMPS_LEVELTAB_FAST3D_AREAPANEL_BACKGROUND;
                newButton.ForeColor = Theme.SCRIPTDUMPS_LEVELTAB_FAST3D_AREAPANEL_TEXT;
                newButton.FlatStyle = FlatStyle.Flat;
                //newButton.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                if (i == 0)
                    newButton.Checked = true;
                lt_f3d_radioPanel.Controls.Add(newButton);
            }
        }

        private string format_F3DCmd(byte[] cmd, bool doFormat)
        {
            if (doFormat)
            {
                string str = format_byte(cmd[0]);
                switch (cmd[0])
                {
                    case 0x01:
                    case 0x03:
                    case 0x06:
                    case 0xB6:
                    case 0xB7:
                    case 0xFD:
                    case 0xFE:
                        str += format_byte(cmd[1]) +
                            format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_int(cmd, 4);
                        break;
                    case 0xF0:
                    case 0xF2:
                    case 0xF3:
                        str += format_byte(cmd[1]) +
                            format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_byte(cmd[4]) +
                            format_threeBytesAsTwo(cmd, 5);
                        break;
                    /*
                    case 0x00:
                    case 0xE6:
                    case 0xE7:
                    case 0xE8:
                    case 0xE9:
                    case 0xB8:
                    case 0xFC:
                        str = format_long(cmd, 0);
                        break;
                    */
                    default:
                        for (int i = 1; i < cmd.Length; i++)
                            str += format_byte(cmd[i]);
                        break;
                }

                return str;
            }
            else
            {
                return BitConverter.ToString(cmd).Replace("-", " ") + " ";
            }
        }

        private void organizeCurrentFast3DScript(List<ScriptDumpCommandInfo> f3dList)
        {
            bool showROMAddr = showRomCB.Checked;
            bool showSegAddr = SegAddrCB.Checked;
            bool showComments = CommCB.Checked;
            bool formatBytes = FormatCB.Checked;
            bool indentBytes = IndentCB.Checked;
            currentTextbox.Clear();
            int currentIndent = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Courier New;}}{\\colortbl;\\red"+Theme.SCRIPTDUMPS_FAST3D_TEXTBOX_TEXT.R+ "\\green" + Theme.SCRIPTDUMPS_FAST3D_TEXTBOX_TEXT.G + "\\blue" + Theme.SCRIPTDUMPS_FAST3D_TEXTBOX_TEXT.B + ";\\red" + Theme.SCRIPTDUMPS_FAST3D_TEXTBOX_COMMENTS.R + "\\green" + Theme.SCRIPTDUMPS_FAST3D_TEXTBOX_COMMENTS.G + "\\blue" + Theme.SCRIPTDUMPS_FAST3D_TEXTBOX_COMMENTS.B + "; }\\viewkind4\\uc1\\pard\\f0\\fs17\\cf1 ");
            for (int i = 0; i < f3dList.Count; i++)
            {
                ScriptDumpCommandInfo info = f3dList[i];
                if (showROMAddr || showSegAddr)
                {
                    sb.Append("[");
                    if (showROMAddr)
                    {
                        sb.Append(info.romAddress.ToString("X8"));
                        if (showSegAddr)
                        {
                            sb.Append(" / ");
                            sb.Append(info.segAddress.ToString("X8"));
                        }
                    }
                    else if (showSegAddr)
                        sb.Append(info.segAddress.ToString("X8"));
                    sb.Append("] ");
                }
                if (indentBytes)
                {
                    for (int j = 0; j < currentIndent; j++)
                        sb.Append("   ");
                    bool isLastIndex = (i == f3dList.Count - 1);
                    
                    switch (info.data[0])
                    {
                        case 0x06:
                            currentIndent++;
                            break;
                        case 0xB8:
                            if(currentIndent > 0)
                                currentIndent--;
                            break;
                    }
                }
                sb.Append(format_F3DCmd(info.data, formatBytes));

                if (showComments)
                {
                    sb.Append("\\cf2 // "); // Set to color #2 (Green)
                    sb.Append(info.description);
                    sb.Append("\\cf1 "); // Set to color #1 (Black)
                }

                sb.Append("\\line "); // Add a new line
            }
            sb.Append("}");
            currentTextbox.Rtf = sb.ToString();
        }
    }
}
