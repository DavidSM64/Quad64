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
        int lt_gls_areaIndex = 0;

        private void lt_gls_radioPanel_Resize(object sender, EventArgs e)
        {
            int control_count = lt_gls_radioPanel.Controls.Count;
            int rb_width = lt_gls_radioPanel.Width / control_count;
            for (int i = 0; i < control_count; i++)
            {
                RadioButton but = (RadioButton)lt_gls_radioPanel.Controls[i];
                but.Location = new Point(lt_gls_radioPanel.Location.X + (rb_width * i), lt_gls_radioPanel.Location.Y);
                but.Size = new Size(rb_width, lt_gls_radioPanel.Height);
            }
        }
        
        private void lt_gls_areas_CheckedChanged(object sender, EventArgs e)
        {
            int control_count = lt_gls_radioPanel.Controls.Count;
            for (int i = 0; i < control_count; i++)
            {
                RadioButton but = (RadioButton)lt_gls_radioPanel.Controls[i];
                if (but.Checked)
                {
                    lt_gls_areaIndex = i;
                    switchTextBoxes(lt_gls_textbox);
                    return;
                }
            }
        }

        private void initGeoLayoutForLevelTab()
        {
            //lt_gls_radioPanel
            lt_gls_areaIndex = 0;
            int num_areas = level.Areas.Count;
            int rb_width = lt_gls_radioPanel.Width / num_areas;
            lt_gls_radioPanel.Controls.Clear();
            for (int i = 0; i < num_areas; i++)
            {
                RadioButton newButton = new RadioButton();
                newButton.Appearance = Appearance.Button;
                newButton.TextAlign = ContentAlignment.MiddleCenter;
                newButton.Name = "lt_gls_area" + level.Areas[i].AreaID;
                newButton.Text = "Area " + level.Areas[i].AreaID;
                newButton.Location = new Point(rb_width * i, lt_gls_radioPanel.Location.Y);
                newButton.Size = new Size(rb_width, lt_gls_radioPanel.Height);
                newButton.CheckedChanged += lt_gls_areas_CheckedChanged;
                newButton.BackColor = Theme.SCRIPTDUMPS_LEVELTAB_GEOLAYOUT_AREAPANEL_BACKGROUND;
                newButton.ForeColor = Theme.SCRIPTDUMPS_LEVELTAB_GEOLAYOUT_AREAPANEL_TEXT;
                newButton.FlatStyle = FlatStyle.Flat;
                //newButton.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                if (i == 0)
                    newButton.Checked = true;
                lt_gls_radioPanel.Controls.Add(newButton);
            }
        }

        private string format_GLSCmd(byte[] cmd, bool doFormat)
        {
            if (doFormat)
            {
                string str = format_byte(cmd[0]);
                switch (cmd[0])
                {
                    case 0x00:
                    case 0x02:
                    case 0x0E:
                    case 0x15:
                        str += format_byte(cmd[1]) +
                            format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_int(cmd, 4);
                        break;
                    case 0x08:
                        str += format_byte(cmd[1]) +
                            format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_short(cmd, 4) +
                            format_short(cmd, 6) +
                            format_short(cmd, 8) +
                            format_short(cmd, 10);
                        break;
                    case 0x0A:
                        str += format_byte(cmd[1]) +
                            format_short(cmd, 2) +
                            format_short(cmd, 4) +
                            format_short(cmd, 6);
                        if(cmd.Length == 12)
                            str += format_int(cmd, 8);
                        break;
                    case 0x0D:
                        str += format_byte(cmd[1]) +
                            format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_short(cmd, 4) +
                            format_short(cmd, 6);
                        break;
                    case 0x0F:
                        str += format_byte(cmd[1]) +
                            format_short(cmd, 2) +
                            format_short(cmd, 4) +
                            format_short(cmd, 6) +
                            format_short(cmd, 8) +
                            format_short(cmd, 10) +
                            format_short(cmd, 12) +
                            format_short(cmd, 14) +
                            format_int(cmd, 16);
                        break;
                    case 0x10:
                        str += format_byte(cmd[1]) +
                            format_short(cmd, 2) +
                            format_short(cmd, 4) +
                            format_short(cmd, 6) +
                            format_short(cmd, 8) +
                            format_short(cmd, 10) +
                            format_short(cmd, 12) +
                            format_short(cmd, 14);
                        break;
                    case 0x13:
                        str += format_byte(cmd[1]) +
                            format_short(cmd, 2) +
                            format_short(cmd, 4) +
                            format_short(cmd, 6) +
                            format_int(cmd, 8);
                        break;
                    case 0x16:
                        str += format_byte(cmd[1]) +
                            format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_byte(cmd[4]) +
                            format_byte(cmd[5]) +
                            format_short(cmd, 6);
                        break;
                    case 0x18:
                    case 0x19:
                        str += format_byte(cmd[1]) +
                            format_short(cmd, 2) +
                            format_int(cmd, 4);
                        break;
                    case 0x1D:
                        str += format_byte(cmd[1]) +
                            format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_int(cmd, 4);
                        if(cmd.Length == 12)
                            str += format_int(cmd, 8);
                        break;
                    case 0x20:
                        str += format_byte(cmd[1]) +
                            format_short(cmd, 2);
                        break;
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

        private void organizeCurrentGeoLayoutScript(List<ScriptDumpCommandInfo> glsList)
        {
            // Apparently "RichTextBox.AppendText()" is a REALLY slow function, but luckly you can just give the RichTextBox a string in RTF format. This will drastically speed up performance.
            bool showROMAddr = showRomCB.Checked;
            bool showSegAddr = SegAddrCB.Checked;
            bool showComments = CommCB.Checked;
            bool formatBytes = FormatCB.Checked;
            bool indentBytes = IndentCB.Checked;
            currentTextbox.Clear();
            int currentIndent = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Courier New;}}{\\colortbl;\\red" + Theme.SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_TEXT.R + "\\green" + Theme.SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_TEXT.G + "\\blue" + Theme.SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_TEXT.B + ";\\red" + Theme.SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_COMMENTS.R + "\\green" + Theme.SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_COMMENTS.G + "\\blue" + Theme.SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_COMMENTS.B + "; }\\viewkind4\\uc1\\pard\\f0\\fs17\\cf1 ");
            for (int i = 0; i < glsList.Count; i++)
            {
                ScriptDumpCommandInfo info = glsList[i];
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
                    bool isLastIndex = (i == glsList.Count - 1);

                    switch (info.data[0])
                    {
                        case 0x02:
                        case 0x04:
                            currentIndent++;
                            break;
                        case 0x03:
                        case 0x05:
                            currentIndent--;
                            break;
                    }
                }
                sb.Append(format_GLSCmd(info.data, formatBytes));

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
