using Quad64.src.LevelInfo;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    partial class ScriptDumps : Form
    {
        private Level level;
        private RichTextBox currentTextbox;
        public ScriptDumps(Level level)
        {
            this.level = level;
            InitializeComponent();
            initGeoLayoutForLevelTab();
            initFast3DForLevelTab();
            currentTextbox = lt_ls_textbox;
            organizeCurrentLevelScript(level.LevelScriptCommands_ForDump);
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
            if(currentTextbox.Name.Equals("lt_ls_textbox"))
                organizeCurrentLevelScript(level.LevelScriptCommands_ForDump);
            else if (currentTextbox.Name.Equals("lt_gls_textbox"))
                organizeCurrentGeoLayoutScript(level.Areas[lt_gls_areaIndex].AreaModel.GeoLayoutCommands_ForDump);
            else if (currentTextbox.Name.Equals("lt_f3d_textbox"))
                organizeCurrentFast3DScript(level.Areas[lt_f3d_areaIndex].
                    AreaModel.Fast3DCommands_ForDump[lt_f3d_listbox.SelectedIndex]);
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
    }
}
