using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    partial class ScriptDumps : Form
    {
        private void refreshObjectsTabFast3DList()
        {
            ot_f3d_listbox.Items.Clear();
            ot_f3d_textbox.ResetText();
            
            ushort key = objectCombos[listBoxObjects.SelectedIndex].ModelID;
            if (level.ModelIDs.ContainsKey(key) && level.ModelIDs[key].Fast3DCommands_ForDump.Count > 0)
            {
                List<List<ScriptDumpCommandInfo>> list = level.ModelIDs[key].Fast3DCommands_ForDump;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Count > 0)
                    {
                        ot_f3d_listbox.Items.Add(list[i][0].segAddress.ToString("X8"));
                    }
                    else
                    {
                        ot_f3d_listbox.Items.Add("<Error 80>");
                    }
                }
                
                ot_f3d_listbox.SelectedIndex = 0;
            }
            else
            {
                ot_f3d_textbox.Text = "<No script found>";
            }
        }
        
        private void ot_f3d_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switchTextBoxes(ot_f3d_textbox);
        }
    }
}
