using Quad64.src.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    partial class ScriptDumps : Form
    {
        List<ObjectComboEntry> objectCombos = null;

        private void initGeoLayoutForObjectTab()
        {
            refreshObjectsTabFast3DList();
        }
        
        private void subTabsObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshObjectsTabFast3DList();
            if (subTabsObjects.SelectedTab != null && listBoxObjects.SelectedIndex > -1)
            {
                switch (subTabsObjects.SelectedIndex)
                {
                    case 0:
                        switchTextBoxes(ot_gls_textbox);
                        break;
                    case 1:
                        switchTextBoxes(ot_f3d_textbox);
                        break;
                    case 2:
                        switchTextBoxes(ot_beh_textbox);
                        break;
                }
            }
        }

        private void listBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshObjectsTabFast3DList();
            if (subTabsObjects.SelectedTab != null)
            {
                switch (subTabsObjects.SelectedIndex)
                {
                    case 0:
                        switchTextBoxes(ot_gls_textbox);
                        break;
                    case 1:
                        switchTextBoxes(ot_f3d_textbox);
                        break;
                    case 2:
                        switchTextBoxes(ot_beh_textbox);
                        break;
                }
            }
        }

        private void initObjectList()
        {
            // get shallow copy of 'level.LevelObjectCombos'
            objectCombos = level.LevelObjectCombos.GetRange(0, level.LevelObjectCombos.Count);

            List<string> undefined_names = new List<string>();
            for(int i = 0; i < level.Areas.Count; i++)
            {
                for (int j = 0; j < level.Areas[i].Objects.Count; j++)
                {
                    string combo_name = level.Areas[i].Objects[j].getObjectComboName();
                    if (combo_name.StartsWith("Undefined Combo (") && !undefined_names.Contains(combo_name))
                    {
                        undefined_names.Add(combo_name);
                        string[] combo = combo_name.Substring(17, 16).Split(',');
                        byte modelID = Convert.ToByte(combo[0].Substring(2), 16);
                        uint modelAddress = 0;
                        if (level.ModelIDs.ContainsKey(modelID))
                        {
                            modelAddress = level.ModelIDs[modelID].GeoDataSegAddress;
                        }
                        uint behavior = Convert.ToUInt32(combo[1].Substring(3), 16);
                        ObjectComboEntry newOCE = new ObjectComboEntry(combo_name, modelID, modelAddress, behavior);
                        objectCombos.Add(newOCE);
                        Console.WriteLine("Found undefined combo! " + combo_name);
                    }
                }
            }
            comboBoxObjectSort.SelectedIndex = 0;
            updateObjectList();
        }

        private int findObjectComboInList(string name, uint behavior, uint geoLayout, byte modelID)
        {
            for (int i = 0; i < objectCombos.Count; i++)
            {
                if (objectCombos[i].Name == name && objectCombos[i].Behavior == behavior && 
                    objectCombos[i].ModelSegmentAddress == geoLayout && objectCombos[i].ModelID == modelID) {
                    return i;
                }
            }
            return -1;
        }

        private void comboBoxObjectSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comboName = "";
            uint behavior = 0, geoLayoutAddress = 0;
            byte modelID = 0;
            bool changed = false;
            if (listBoxObjects.SelectedIndex > -1)
            {
                comboName = objectCombos[listBoxObjects.SelectedIndex].Name;
                behavior = objectCombos[listBoxObjects.SelectedIndex].Behavior;
                geoLayoutAddress = objectCombos[listBoxObjects.SelectedIndex].ModelSegmentAddress;
                modelID = objectCombos[listBoxObjects.SelectedIndex].ModelID;
                changed = true;
            }

            updateObjectList();

            if(changed)
                listBoxObjects.SelectedIndex = findObjectComboInList(comboName, behavior, geoLayoutAddress, modelID);
        }

        private void updateObjectList()
        {
            switch (comboBoxObjectSort.SelectedIndex)
            {
                case 0:
                    objectCombos = objectCombos.OrderBy(o => o.Name).ToList();
                    break;
                case 1:
                    objectCombos = objectCombos.OrderBy(
                        o => 
                        (level.ModelIDs.ContainsKey(o.ModelID) ? 
                            (level.ModelIDs[o.ModelID].GeoLayoutCommands_ForDump.Count > 0 ?
                            level.ModelIDs[o.ModelID].GeoLayoutCommands_ForDump[0].segAddress 
                            : 0)
                            : 0)
                        ).ToList();
                    break;
                case 2:
                    objectCombos = objectCombos.OrderBy(
                        o =>
                        (level.ModelIDs.ContainsKey(o.ModelID) ?
                            (level.ModelIDs[o.ModelID].GeoLayoutCommands_ForDump.Count > 0 ?
                            level.ModelIDs[o.ModelID].GeoLayoutCommands_ForDump[0].romAddress
                            : 0)
                            : 0)
                        ).ToList();
                    break;
                case 3:
                    objectCombos = objectCombos.OrderBy(
                        o =>
                        (level.ModelIDs.ContainsKey(o.ModelID) ?
                            (level.ModelIDs[o.ModelID].Fast3DCommands_ForDump.Count > 0 ?
                            (level.ModelIDs[o.ModelID].Fast3DCommands_ForDump[0].Count > 0 ?
                            level.ModelIDs[o.ModelID].Fast3DCommands_ForDump[0][0].segAddress
                            : 0)
                            : 0)
                            : 0)
                        ).ToList();
                    break;
                case 4:
                    objectCombos = objectCombos.OrderBy(
                        o =>
                        (level.ModelIDs.ContainsKey(o.ModelID) ?
                            (level.ModelIDs[o.ModelID].Fast3DCommands_ForDump.Count > 0 ?
                            (level.ModelIDs[o.ModelID].Fast3DCommands_ForDump[0].Count > 0 ?
                            level.ModelIDs[o.ModelID].Fast3DCommands_ForDump[0][0].romAddress
                            : 0)
                            : 0)
                            : 0)
                        ).ToList();
                    break;
                case 5:
                    objectCombos = objectCombos.OrderBy(o => o.Behavior).ToList();
                    break;
                case 6:
                    objectCombos = objectCombos.OrderBy(o => ROM.Instance.decodeSegmentAddress_safe(o.Behavior, null)).ToList();
                    break;
            }
            
            listBoxObjects.Items.Clear();
            foreach (ObjectComboEntry entry in objectCombos)
            {
                listBoxObjects.Items.Add(entry.Name);
            }

            //listBoxObjects_SelectedIndexChanged(null, null);
        }
    }
}
