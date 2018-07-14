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

        private string format_LSCmd(byte[] cmd, bool doFormat)
        {
            if (doFormat)
            {
                string str = format_byte(cmd[0]) + format_byte(cmd[1]);
                switch (cmd[0])
                {
                    case 0x00:
                    case 0x01:
                        str += format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_int(cmd, 4) +
                            format_int(cmd, 8);
                        break;
                    case 0x03:
                    case 0x04:
                    case 0x08:
                    case 0x13:
                    case 0x38:
                        str += format_short(cmd, 2);
                        break;
                    case 0x05:
                    case 0x06:
                    case 0x0B:
                    case 0x0D:
                    case 0x0E:
                    case 0x1F:
                    case 0x21:
                    case 0x22:
                    case 0x2E:
                    case 0x2F:
                    case 0x39:
                        str += format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_int(cmd, 4);
                        break;
                    case 0x0C:
                    case 0x17:
                    case 0x18:
                    case 0x1A:
                    case 0x23:
                    case 0x25:
                        str += format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_int(cmd, 4) +
                            format_int(cmd, 8);
                        break;
                    case 0x11:
                    case 0x12:
                        str += format_short(cmd, 2) + format_int(cmd, 4);
                        break;
                    case 0x16:
                        str += format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_int(cmd, 4) +
                            format_int(cmd, 8) +
                            format_int(cmd, 12);
                        break;
                    case 0x2B:
                    case 0x3B:
                        str += format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_short(cmd, 4) +
                            format_short(cmd, 6) +
                            format_short(cmd, 8) +
                            format_short(cmd, 10);
                        break;
                    case 0x24:
                        str += format_byte(cmd[2]) +
                            format_byte(cmd[3]) +
                            format_short(cmd, 4) +
                            format_short(cmd, 6) +
                            format_short(cmd, 8) +
                            format_short(cmd, 10) +
                            format_short(cmd, 12) +
                            format_short(cmd, 14) +
                            format_byte(cmd[16]) +
                            format_byte(cmd[17]) +
                            format_byte(cmd[18]) +
                            format_byte(cmd[19]) +
                            format_int(cmd, 20);
                        break;
                    default:
                        for (int i = 2; i < cmd[1]; i++)
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

        private void organizeCurrentLevelScript(List<ScriptDumpCommandInfo> lsList)
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
            sb.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Courier New;}}{\\colortbl;\\red" + Theme.SCRIPTDUMPS_LEVEL_TEXTBOX_TEXT.R + "\\green" + Theme.SCRIPTDUMPS_LEVEL_TEXTBOX_TEXT.G + "\\blue" + Theme.SCRIPTDUMPS_LEVEL_TEXTBOX_TEXT.B + ";\\red" + Theme.SCRIPTDUMPS_LEVEL_TEXTBOX_COMMENTS.R + "\\green" + Theme.SCRIPTDUMPS_LEVEL_TEXTBOX_COMMENTS.G + "\\blue" + Theme.SCRIPTDUMPS_LEVEL_TEXTBOX_COMMENTS.B + "; }\\viewkind4\\uc1\\pard\\f0\\fs17\\cf1 ");
            for (int i = 0; i < lsList.Count; i++)
            {
                ScriptDumpCommandInfo info = lsList[i];
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
                    bool isLastIndex = (i == lsList.Count - 1);

                    switch (info.data[0])
                    {
                        case 0x1B:
                        case 0x1F:
                        case 0x06:
                        case 0x00:
                        case 0x01:
                            currentIndent++;
                            break;
                        case 0x1D:
                        case 0x20:
                        case 0x02:
                        case 0x07:
                            currentIndent--;
                            break;
                    }
                }
                sb.Append(format_LSCmd(info.data, formatBytes));

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
