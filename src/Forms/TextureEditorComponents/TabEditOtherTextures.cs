using LIBMIO0;
using Newtonsoft.Json.Linq;
using Quad64.src.Forms.TextureEditorComponents;
using Quad64.src.JSON;
using Quad64.src.LevelInfo;
using Quad64.src.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    partial class textureEditor : Form
    {
        private RadioButtonWithInfo[][] ot_buttons;
        private string[] ot_list_names;
        private int ot_list_index = 0;

        private bool checkForValidEntry(JObject obj)
        {
            return (
                obj["Name"] != null &&
                obj["Format"] != null && 
                obj["Width"] != null && 
                obj["Height"] != null &&
                (obj["FromSegmentAddress"] != null || obj["FromROMAddress"] != null)
            );
        }


        private uint bytesToInt(byte[] b, int offset, int length)
        {
            switch (length)
            {
                case 1: return b[0 + offset];
                case 2: return (uint)(b[0 + offset] << 8 | b[1 + offset]);
                case 3: return (uint)(b[0 + offset] << 16 | b[1 + offset] << 8 | b[2 + offset]);
                default: return (uint)(b[0 + offset] << 24 | b[1 + offset] << 16 | b[2 + offset] << 8 | b[3 + offset]);
            }
        }

        private int parseLevelScriptTemporarlyForSegments(byte[][] segments, uint[] segmentStarts, bool[] segmentsAreMIO0, byte seg, uint off)
        {
            ROM rom = ROM.Instance;
            byte[] data = segments[seg];
            if (data == null || data.Length < 1)
                return 2;
            bool end = false;
            int endCmd = 0;
            byte l_seg;
            uint l_start, l_end, l_off;
            while (!end)
            {
                byte cmdLen = data[off + 1];
                byte[] cmd = rom.getSubArray_safe(data, off, cmdLen);
                //rom.printArray(cmd, cmdLen);
                switch (cmd[0])
                {
                    case 0x00:
                    case 0x01:
                        {
                            l_seg = cmd[3];
                            l_start = bytesToInt(cmd, 4, 4);
                            l_end = bytesToInt(cmd, 8, 4);
                            l_off = bytesToInt(cmd, 13, 3);
                            segmentStarts[l_seg] = l_start;
                            segments[l_seg] = rom.getROMSection(l_start, l_end);
                            int end_r = parseLevelScriptTemporarlyForSegments(segments, segmentStarts, segmentsAreMIO0, l_seg, l_off);
                            if (end_r == 0x02)
                            {
                                end = true;
                                endCmd = 2;
                            }
                        }
                        break;
                    case 0x02:
                        endCmd = 2;
                        end = true;
                        break;
                    case 0x05:
                        l_seg = cmd[4];
                        l_off = bytesToInt(cmd, 5, 3);
                        if (l_seg == seg)
                        {
                            if ((long)l_off - (long)off == -4)
                            {
                                //Console.WriteLine("Infinite loop detected!");
                                return 0x02;
                            }
                        }
                        endCmd = parseLevelScriptTemporarlyForSegments(segments, segmentStarts, segmentsAreMIO0, l_seg, l_off);
                        end = true;
                        break;
                    case 0x06:
                        l_seg = cmd[4];
                        l_off = bytesToInt(cmd, 5, 3);
                        int end_ret = parseLevelScriptTemporarlyForSegments(segments, segmentStarts, segmentsAreMIO0, l_seg, l_off);
                        if (end_ret == 0x02)
                        {
                            end = true;
                            endCmd = 2;
                        }
                        break;
                    case 0x07:
                        end = true;
                        endCmd = 0x07;
                        break;
                    case 0x17:
                        l_seg = cmd[3];
                        l_start = bytesToInt(cmd, 4, 4);
                        l_end = bytesToInt(cmd, 8, 4);
                        if (l_start < l_end)
                        {
                            segmentStarts[l_seg] = l_start;
                            segments[l_seg] = rom.getROMSection(l_start, l_end);
                        }
                        //rom.setSegment(seg, start, end, false);
                        break;
                    case 0x18:
                    case 0x1A:
                        l_seg = cmd[3];
                        l_start = bytesToInt(cmd, 4, 4);
                        l_end = bytesToInt(cmd, 8, 4);
                        if (l_start < l_end)
                        {
                            byte[] MIO0_header = rom.getSubArray_safe(rom.Bytes, l_start, 0x10);
                            if (bytesToInt(MIO0_header, 0, 4) == 0x4D494F30) // Check MIO0 signature
                            {
                                int compressedOffset = (int)bytesToInt(MIO0_header, 0x8, 4);
                                int uncompressedOffset = (int)bytesToInt(MIO0_header, 0xC, 4);
                                bool isFakeMIO0 = rom.testIfMIO0IsFake(l_start, compressedOffset, uncompressedOffset);
                                segmentsAreMIO0[l_seg] = !isFakeMIO0;
                                if (isFakeMIO0)
                                    segmentStarts[l_seg] = l_start + (uint)uncompressedOffset;
                                segments[l_seg] = MIO0.mio0_decode(rom.getROMSection(l_start, l_end));
                            }
                        }
                        break;
                    case 0x1D:
                        end = true;
                        endCmd = 0x02;
                        break;
                }
                off += cmdLen;
            }
            return endCmd;
        }
        
        private List<RadioButtonWithInfo> parseOtherListJSON(JArray array, out string blockName)
        {
            ROM rom = ROM.Instance;
            blockName = "Unknown";
            List<RadioButtonWithInfo> list = new List<RadioButtonWithInfo>();
            uint segmentAddressForLoadingData = 0;
            byte[][] tempSegments = new byte[0x20][];
            uint[] tempSegmentStarts = new uint[0x20];
            bool[] tempSegmentsAreMIO0 = new bool[0x20];

            foreach (JObject obj in array.Children())
            {
                if (obj["Block Name"] != null)
                {
                    blockName = obj["Block Name"].ToString();
                    segmentAddressForLoadingData = 0; // Reset for every new block
                }
                else if (obj["UseSegmentAddressesFromLevelScriptStartingFrom"] != null)
                {
                    segmentAddressForLoadingData = uint.Parse(obj["UseSegmentAddressesFromLevelScriptStartingFrom"].ToString(), NumberStyles.HexNumber);
                    for (int i = 0; i < 0x20; i++)
                    {
                        tempSegments[i] = new byte[0]; // reset segments
                        tempSegmentsAreMIO0[i] = false;
                        tempSegmentStarts[i] = 0;
                    }
                    tempSegments[0x00] = rom.Bytes;
                    tempSegments[0x15] = rom.cloneSegment(0x15, (byte)level.CurrentAreaID);
                    tempSegmentStarts[0x15] = Globals.seg15_location[0];
                    tempSegments[0x02] = rom.cloneSegment(0x02, (byte)level.CurrentAreaID);
                    tempSegmentStarts[0x02] = Globals.seg02_location[0];
                    tempSegmentsAreMIO0[0x02] = !rom.Seg02_isFakeMIO0;
                    //Console.WriteLine("SegOff:0x{0}", segmentAddressForLoadingData.ToString("X8"));
                    parseLevelScriptTemporarlyForSegments(tempSegments, tempSegmentStarts, tempSegmentsAreMIO0, (byte)(segmentAddressForLoadingData >> 24), segmentAddressForLoadingData & 0xFFFFFF);
                }
                else
                {
                    if (checkForValidEntry(obj))
                    {
                        int width = int.Parse(obj["Width"].ToString());
                        int height = int.Parse(obj["Height"].ToString());
                        byte format = TextureFormats.ConvertStringToFormat(obj["Format"].ToString());
                        uint dataSize = (uint)((TextureFormats.getNumberOfBitsForFormat(format) * width * height) / 8);
                        byte[] data;
                        if (obj["FromSegmentAddress"] != null)
                        {
                            uint segOffset = uint.Parse(obj["FromSegmentAddress"].ToString(), NumberStyles.HexNumber);
                            if (segmentAddressForLoadingData == 0)
                                data = rom.getDataFromSegmentAddress_safe(segOffset, dataSize, (byte)level.CurrentAreaID);
                            else
                            {
                                byte segment = (byte)(segOffset >> 24);
                                uint segment_off = segOffset & 0x00FFFFFF;
                                data = rom.getSubArray_safe(tempSegments[segment], segment_off, dataSize);
                            }
                        }
                        else
                        {
                            if (rom.Type == ROM_Type.VANILLA && obj["ForExtendedROM"] != null)
                            {
                                string forExtendedROM = obj["ForExtendedROM"].ToString().ToLower();
                                if (forExtendedROM.Equals("true"))
                                    continue;
                            }
                            uint romOff = uint.Parse(obj["FromROMAddress"].ToString(), NumberStyles.HexNumber);
                            data = rom.getSubArray_safe(rom.Bytes, romOff, dataSize);
                        }
                        Bitmap image = TextureFormats.decodeTexture(format, data, width, height, null, true);
                        string[] tags = (string[])image.Tag;
                        Array.Resize(ref tags, tags.Length + 3);
                        uint segOff = 0;
                        if (obj["FromSegmentAddress"] != null)
                        {
                            segOff = uint.Parse(obj["FromSegmentAddress"].ToString(), NumberStyles.HexNumber);


                            if (segmentAddressForLoadingData == 0)
                            {
                                if (!rom.isSegmentMIO0((byte)(segOff >> 24), (byte)level.CurrentAreaID))
                                {
                                    tags[tags.Length - 3] = "ROM Address: " + rom.decodeSegmentAddress(segOff, (byte)level.CurrentAreaID).ToString("X");
                                }
                                else
                                    tags[tags.Length - 3] = "ROM Address: N/A";
                            }
                            else {
                                byte seg_temp = (byte)(segOff >> 24);
                                uint seg_temp_offset = segOff & 0x00FFFFFF;
                                if (tempSegmentsAreMIO0[seg_temp])
                                    tags[tags.Length - 3] = "ROM Address: N/A";
                                else
                                    tags[tags.Length - 3] = "ROM Address: " + (tempSegmentStarts[seg_temp] + seg_temp_offset).ToString("X");
                            }

                            tags[tags.Length - 2] = "Seg Addr: " + obj["FromSegmentAddress"];
                        }
                        else
                        {
                            tags[tags.Length - 3] = "ROM Address: " + obj["FromROMAddress"];
                            tags[tags.Length - 2] = "Seg Addr: N/A";
                        }
                        tags[tags.Length - 1] = "Name: " + obj["Name"];
                        image.Tag = tags;

                        AddNewImage(ref list, image, segOff, ot_RadioButtonWithInfo_Click);

                        //Console.WriteLine("Added Other Image: " + obj["Name"]);
                    }
                    //Bitmap image = TextureFormats.decodeTexture();
                    //AddNewImage(ref list, level.ModelIDs[modelID].builder.TextureImages[i], address, ot_RadioButtonWithInfo_Click);
                }
                //Console.WriteLine(obj.ToString());
            }
            return list;
        }

        private void showOtherTexturesButtons()
        {
            if (ot_buttons == null)
                return;
            ot_pan_icons.Controls.Clear();

            //Console.WriteLine("ot_buttons[ot_list_index].Length = " + ot_buttons[ot_list_index].Length);
            ot_pan_icons.Controls.AddRange(ot_buttons[ot_list_index]);

            int offset = 1;
            if (ot_pan_icons.Controls.Count < 121)
                offset = 9;

            int index = 0;
            foreach (RadioButtonWithInfo button in ot_pan_icons.Controls)
            {
                button.Location = new Point((index * 32) % 320 + offset, (((index * 32) / 320) * 32) + 1);
                index++;
            }
        }

        private void loadOtherTexturesTab()
        {
            JArray[] arrays = OtherTexturesFile.LoadOtherTextureFile(Globals.getDefaultOtherTexturesPath());
            if (arrays != null)
            {
                ot_buttons = new RadioButtonWithInfo[arrays.Length][];
                ot_list_names = new string[arrays.Length];
                int count = 0;
                foreach (JArray arr in arrays)
                {
                    string groupName;
                    ot_buttons[count] = parseOtherListJSON(arr, out groupName).ToArray();
                    ot_list_names[count] = groupName;
                    count++;
                }

                int y_offset = 4;
                count = 0;
                foreach (string name in ot_list_names)
                {
                    RadioButton rb = new RadioButton();
                    rb.Appearance = Appearance.Button;
                    rb.FlatStyle = FlatStyle.Flat;
                    rb.BackColor = Theme.TEXTURES_OTHER_BUTTON_BACKGROUND;
                    rb.ForeColor = Theme.TEXTURES_OTHER_BUTTON_TEXT;
                    rb.UseVisualStyleBackColor = true;
                    rb.TextAlign = ContentAlignment.MiddleCenter;
                    rb.Text = name;
                    rb.Font = new Font("Courier New", 8);
                    rb.Tag = count;
                    rb.Click += ot_RadioButtonGroup_Click;
                    rb.AutoSize = false;
                    rb.Size = new Size(160, 24);
                    rb.Location = new Point(3, y_offset);
                    ot_pan_groupButtons.Controls.Add(rb);
                    y_offset += 25;
                    count++;
                }
            }
            else
            {
                ot_buttons = null;
            }

            ot_list_index = 0;
            showOtherTexturesButtons();
        }

        private void ot_RadioButtonGroup_Click(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            ot_list_index = (int)rb.Tag;
            showOtherTexturesButtons();
        }

        private void ot_RadioButtonWithInfo_Click(object sender, EventArgs e)
        {
            ROM rom = ROM.Instance;
            RadioButtonWithInfo button = (RadioButtonWithInfo)sender;
            string[] tags = (string[])button.BitmapImage.Tag;

            ot_bitmap_preview.BackgroundImage = button.BitmapImage;

            //ot_info_SegAddress.Text = "Seg Addr: " + button.Address.ToString("X8");
            //ot_info_RomAddress.Text = "ROM Address: " + (rom.decodeSegmentAddress(button.Address)).ToString("X");

            foreach (string tag in tags)
            {
                if (tag.StartsWith("Width:"))
                    ot_info_width.Text = tag;
                else if (tag.StartsWith("Height:"))
                    ot_info_height.Text = tag;
                else if (tag.StartsWith("Format:"))
                    ot_info_Format.Text = tag;
                else if (tag.StartsWith("Seg Addr:"))
                    ot_info_SegAddress.Text = tag;
                else if (tag.StartsWith("ROM Address:"))
                    ot_info_RomAddress.Text = tag;
                else if (tag.StartsWith("Name:"))
                    ot_name.Text = tag;
            }
        }
    }
}
