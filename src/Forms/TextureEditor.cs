using Quad64.src.Forms.TextureEditorComponents;
using Quad64.src.LevelInfo;
using Quad64.src.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    partial class textureEditor : Form
    {
        private Level level;
        public bool needToUpdateLevel = false;
        public textureEditor(Level level)
        {
            this.level = level;
            DoubleBuffered = true;
            InitializeComponent();
        }
        
        private void TextureEditor_Load(object sender, EventArgs e)
        {
            loadModelsOnlyButtonSet();
            loadObjectsOnlyButtonSet();
            loadLevelOnlyButtonSet();
            setButtons_lt(true, true, false);
            loadOtherTexturesTab();
            loadBackgroundImage();
        }
        
        private void AddNewImage(ref List<RadioButtonWithInfo> buttons, Bitmap bitmap, uint address, EventHandler click_event)
        {
            RadioButtonWithInfo texButton = new RadioButtonWithInfo();
            texButton.Click += click_event;
            texButton.Address = address;
            texButton.BitmapImage = bitmap;
            texButton.Width = 32;
            texButton.Height = 32;
            texButton.BackgroundImage = new Bitmap(bitmap, 28, 28);
            texButton.BackgroundImageLayout = ImageLayout.Center;
            texButton.AutoSize = false;
            texButton.Appearance = Appearance.Button;
            texButton.FlatStyle = FlatStyle.Flat;
            texButton.FlatAppearance.BorderSize = 1;
            texButton.FlatAppearance.BorderColor = Color.Black;

            byte segment = (byte)(address >> 24);
            if (!ROM.Instance.isSegmentMIO0(segment))
            {
                texButton.FlatAppearance.MouseOverBackColor = Color.Goldenrod;
                texButton.FlatAppearance.CheckedBackColor = Color.Gold;
                texButton.FlatAppearance.MouseDownBackColor = Color.DarkGoldenrod;
            }
            else
            {
                texButton.FlatAppearance.MouseOverBackColor = Color.Firebrick;
                texButton.FlatAppearance.CheckedBackColor = Color.DarkRed;
                texButton.FlatAppearance.MouseDownBackColor = Color.Maroon;
            }

            texButton.BackColor = Color.Transparent;
            buttons.Add(texButton);
        }
        
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
        }

        private void textureEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            AreaBackgroundInfo bgInfo = level.getCurrentArea().bgInfo;
            if (checkBox_matchFogColor.Enabled && bgInfo.usesFog)
            {
                foreach (uint location in bgInfo.fogColor_romLocation)
                {
                    ROM.Instance.writeByte(location + 4, (byte)Math.Min((int)((float)numericUpDown_Red.Value * 8.226f), 255));
                    ROM.Instance.writeByte(location + 5, (byte)Math.Min((int)((float)numericUpDown_Green.Value * 8.226f), 255));
                    ROM.Instance.writeByte(location + 6, (byte)Math.Min((int)((float)numericUpDown_Blue.Value * 8.226f), 255));
                }
            }
        }

        private void SaveImage(Image img, string suggestedName)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = suggestedName + ".png";
            saveFileDialog1.Filter = "PNG|*.png|JPEG|*.jpeg|BMP|*.bmp|GIF|*.gif|TIFF|*.tiff";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        img.Save(saveFileDialog1.FileName, ImageFormat.Png);
                        break;
                    case 2:
                        img.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                        break;
                    case 3:
                        img.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                        break;
                    case 4:
                        img.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                        break;
                    case 5:
                        img.Save(saveFileDialog1.FileName, ImageFormat.Tiff);
                        break;
                    default:
                        throw new Exception("Invalid save format!");
                }
            }
            else
            {
                return;
            }
        }

        private string LoadImage()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "PNG|*.png|JPEG|*.jpeg|BMP|*.bmp|GIF|*.gif|TIFF|*.tiff";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                return openFileDialog1.FileName;
            }
            else
            {
                return null;
            }
        }

        private void lt_exportImage_Click(object sender, EventArgs e)
        {
            SaveImage(info_bitmapImage.BackgroundImage, info_SegmentAddress.Text.Substring(info_SegmentAddress.Text.LastIndexOf(" ") + 1));
        }

        private void ot_exportImage_Click(object sender, EventArgs e)
        {
            SaveImage(ot_bitmap_preview.BackgroundImage, ot_info_SegAddress.Text.Substring(ot_info_SegAddress.Text.LastIndexOf(" ") + 1));
        }

        private bool ReplaceButtonImage(ref RadioButtonWithInfo[] buttonList, Bitmap newImage)
        {
            for (int i = 0; i < buttonList.Length; i++)
            {
                if (buttonList[i].Checked)
                {
                    buttonList[i].BitmapImage = newImage;
                    buttonList[i].BackgroundImage = new Bitmap(newImage, 28, 28);
                    return true;
                }
            }
            return false;
        }

        private void ot_importImage_Click(object sender, EventArgs e)
        {
            if (ot_info_RomAddress.Text.Contains("N/A") || ot_info_RomAddress.Text.Contains("null"))
            {
                MessageBox.Show("Importing over compressed MIO0 data is not currently supported in this version. You will need to use an extended ROM file.",
                        "Notice",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                return;
            }
            string filename = LoadImage();
            if (filename != null)
            {
                Bitmap newImg = 
                    new Bitmap(Image.FromFile(filename), new Size(ot_bitmap_preview.BackgroundImage.Width, ot_bitmap_preview.BackgroundImage.Height));
                byte format = 
                    TextureFormats.ConvertStringToFormat(ot_info_Format.Text.Substring(ot_info_Format.Text.LastIndexOf(" ") + 1));
                byte[] data = 
                    TextureFormats.encodeTexture(format, newImg);

                if (data != null)
                {
                    uint rom_offset = 
                        uint.Parse(ot_info_RomAddress.Text.Substring(ot_info_RomAddress.Text.LastIndexOf(" ") + 1), System.Globalization.NumberStyles.HexNumber);
                    uint seg_offset = 
                        uint.Parse(ot_info_SegAddress.Text.Substring(ot_info_SegAddress.Text.LastIndexOf(" ") + 1), System.Globalization.NumberStyles.HexNumber);
                    ROM.Instance.writeByteArray(rom_offset, data);
                    ROM.Instance.writeByteArrayToSegment(seg_offset, data);
                    Bitmap newImage = TextureFormats.decodeTexture(
                        format,
                        data,
                        ot_bitmap_preview.BackgroundImage.Width,
                        ot_bitmap_preview.BackgroundImage.Height,
                        null,
                        false
                    );
                    ot_bitmap_preview.BackgroundImage = newImage;
                    ReplaceButtonImage(ref ot_buttons[ot_list_index], newImage);
                    needToUpdateLevel = true;
                }
            }
        }

        private void lt_importImage_Click(object sender, EventArgs e)
        {
            if (info_Format.Text.EndsWith("CI4") || info_Format.Text.EndsWith("CI8"))
            {
                MessageBox.Show("CI texture importing is not currently supported in this version.",
                        "Notice",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                return;
            }
            if (info_Address.Text.Contains("N/A") || info_Address.Text.Contains("null"))
            {
                MessageBox.Show("Importing over compressed MIO0 data is not currently supported in this version. You will need to use an extended ROM file.",
                        "Notice",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                return;
            }

            string filename = LoadImage();
            if (filename != null)
            {
                Bitmap newImg = new Bitmap(Image.FromFile(filename), new Size(info_bitmapImage.BackgroundImage.Width, info_bitmapImage.BackgroundImage.Height));
                byte format = TextureFormats.ConvertStringToFormat(info_Format.Text.Substring(info_Format.Text.LastIndexOf(" ") + 1));
                byte[] data = TextureFormats.encodeTexture(format, newImg);

                if (data != null)
                {
                    uint rom_offset = uint.Parse(info_Address.Text.Substring(info_Address.Text.LastIndexOf(" ") + 1), System.Globalization.NumberStyles.HexNumber);
                    uint seg_offset = uint.Parse(info_SegmentAddress.Text.Substring(info_SegmentAddress.Text.LastIndexOf(" ") + 1), System.Globalization.NumberStyles.HexNumber);
                    ROM.Instance.writeByteArray(rom_offset, data);
                    ROM.Instance.writeByteArrayToSegment(seg_offset, data);
                    Bitmap newImage = TextureFormats.decodeTexture(
                        format,
                        data,
                        info_bitmapImage.BackgroundImage.Width,
                        info_bitmapImage.BackgroundImage.Height,
                        null,
                        false
                    );
                    info_bitmapImage.BackgroundImage = newImage;

                    switch(lt_list_index)
                    {
                        case 0:
                            if (!ReplaceButtonImage(ref lt_levelButtons, newImage))
                                ReplaceButtonImage(ref lt_modelButtons, newImage);
                            break;
                        case 1:
                            ReplaceButtonImage(ref lt_levelButtons, newImage);
                            break;
                        case 2:
                            ReplaceButtonImage(ref lt_modelButtons, newImage);
                            break;
                        case 3:
                            ReplaceButtonImage(ref lt_objectButtons, newImage);
                            break;
                    }
                    needToUpdateLevel = true;

                }
            }
        }
    }
}
