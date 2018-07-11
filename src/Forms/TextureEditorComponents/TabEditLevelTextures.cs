using Quad64.src.Forms.TextureEditorComponents;
using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    partial class textureEditor : Form
    {
        private RadioButtonWithInfo[] lt_levelButtons;
        private RadioButtonWithInfo[] lt_modelButtons;
        private RadioButtonWithInfo[] lt_objectButtons;
        private byte lt_list_index = 0;

        private void RadioButtonWithInfo_Click(object sender, EventArgs e)
        {
            ROM rom = ROM.Instance;
            RadioButtonWithInfo button = (RadioButtonWithInfo)sender;
            string[] tags = (string[])button.BitmapImage.Tag;

            info_bitmapImage.BackgroundImage = button.BitmapImage;
            info_SegmentAddress.Text = "Seg Addr: " + button.Address.ToString("X8");
            byte segment = (byte)(button.Address >> 24);
            if (!ROM.Instance.isSegmentMIO0(segment, (byte)level.CurrentAreaID))
                info_Address.Text = "ROM Address: " + (rom.decodeSegmentAddress(button.Address, (byte)level.CurrentAreaID)).ToString("X");
            else
                info_Address.Text = "ROM Address: N/A";

            foreach (string tag in tags)
            {
                if (tag.StartsWith("Width:"))
                    info_Width.Text = tag;
                else if (tag.StartsWith("Height:"))
                    info_Height.Text = tag;
                else if (tag.StartsWith("Format:"))
                    info_Format.Text = tag;
            }
        }

        private void loadModelsOnlyButtonSet()
        {
            HashSet<uint> addresses = new HashSet<uint>();
            List<RadioButtonWithInfo> buttons = new List<RadioButtonWithInfo>();
            foreach (KeyValuePair<ushort, Model3D> entry in level.ModelIDs)
            {
                for (int i = 0; i < entry.Value.builder.TextureImages.Count; i++)
                {
                    uint address = entry.Value.builder.TextureAddresses[i];
                    if (address == 0)
                        continue;
                    if (!addresses.Contains(address))
                    {
                        AddNewImage(ref buttons, entry.Value.builder.TextureImages[i], address, RadioButtonWithInfo_Click);
                        addresses.Add(address);
                    }
                }
            }
            lt_modelButtons = buttons.ToArray();
        }

        private void loadObjectsOnlyButtonSet()
        {
            List<RadioButtonWithInfo> buttons = new List<RadioButtonWithInfo>();
            HashSet<uint> addresses = new HashSet<uint>();
            HashSet<ushort> modelIDs = level.getModelIDsUsedByObjects();

            foreach (ushort modelID in modelIDs)
            {
                if (level.ModelIDs.ContainsKey(modelID))
                {
                    for (int i = 0; i < level.ModelIDs[modelID].builder.TextureImages.Count; i++)
                    {
                        uint address = level.ModelIDs[modelID].builder.TextureAddresses[i];
                        if (address == 0)
                            continue;
                        if (!addresses.Contains(address))
                        {
                            AddNewImage(ref buttons, level.ModelIDs[modelID].builder.TextureImages[i], address, RadioButtonWithInfo_Click);
                            addresses.Add(address);
                        }
                    }
                }
            }
            lt_objectButtons = buttons.ToArray();
        }

        private void loadLevelOnlyButtonSet()
        {
            List<RadioButtonWithInfo> buttons = new List<RadioButtonWithInfo>();
            HashSet<uint> addresses = new HashSet<uint>();
            foreach (Area area in level.Areas)
            {
                for (int i = 0; i < area.AreaModel.builder.TextureImages.Count; i++)
                {
                    uint address = area.AreaModel.builder.TextureAddresses[i];
                    if (address == 0)
                        continue;
                    if (!addresses.Contains(address))
                    {
                        AddNewImage(ref buttons, area.AreaModel.builder.TextureImages[i], address,  RadioButtonWithInfo_Click);
                        addresses.Add(address);
                    }
                }
            }
            lt_levelButtons = buttons.ToArray();
        }

        private void setButtons_lt(bool useLevel, bool useModels, bool useObjects)
        {
            iconsPanel.Controls.Clear();

            if (useLevel)
                iconsPanel.Controls.AddRange(lt_levelButtons.ToArray());
            if (useModels)
                iconsPanel.Controls.AddRange(lt_modelButtons.ToArray());
            if (useObjects)
                iconsPanel.Controls.AddRange(lt_objectButtons.ToArray());

            int offset = 1;
            if (iconsPanel.Controls.Count < 145)
                offset = 9;

            int index = 0;
            foreach (RadioButtonWithInfo button in iconsPanel.Controls)
            {
                button.Location = new Point((index * 32) % 384 + offset, (((index * 32) / 384) * 32) + 1);
                index++;
            }
        }
        
        private void radioButton1_lt_Click(object sender, EventArgs e)
        {
            setButtons_lt(true, true, false);
            lt_list_index = 0;
        }

        private void radioButton2_lt_Click(object sender, EventArgs e)
        {
            setButtons_lt(true, false, false);
            lt_list_index = 1;
        }

        private void radioButton3_lt_Click(object sender, EventArgs e)
        {
            setButtons_lt(false, true, false);
            lt_list_index = 2;
        }

        private void radioButton4_lt_Click(object sender, EventArgs e)
        {
            setButtons_lt(false, false, true);
            lt_list_index = 3;
        }

    }
}
