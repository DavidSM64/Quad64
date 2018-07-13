using Quad64.src.Forms.TextureEditorComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quad64.src.Forms
{
    partial class textureEditor : Form
    {
        private void sb_ExportImage_Click(object sender, EventArgs e)
        {
            if (sb_imageBox.BackgroundImage != null)
            {
                SaveFileDialog saveImgDialog = new SaveFileDialog();
                saveImgDialog.Filter = "PNG|*.png|JPEG|*.jpeg|BMP|*.bmp|GIF|*.gif|TIFF|*.tiff";
                DialogResult result = saveImgDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    if (saveImgDialog.FilterIndex == 1)
                        sb_imageBox.BackgroundImage.Save(saveImgDialog.FileName, ImageFormat.Png);
                    else if (saveImgDialog.FilterIndex == 2)
                        sb_imageBox.BackgroundImage.Save(saveImgDialog.FileName, ImageFormat.Jpeg);
                    else if (saveImgDialog.FilterIndex == 3)
                        sb_imageBox.BackgroundImage.Save(saveImgDialog.FileName, ImageFormat.Bmp);
                    else if (saveImgDialog.FilterIndex == 4)
                        sb_imageBox.BackgroundImage.Save(saveImgDialog.FileName, ImageFormat.Gif);
                    else if (saveImgDialog.FilterIndex == 5)
                        sb_imageBox.BackgroundImage.Save(saveImgDialog.FileName, ImageFormat.Tiff);
                }
            }
        }

        private void sb_importImage_Click(object sender, EventArgs e)
        {
            if (sb_imageBox.BackgroundImage != null)
            {
                OpenFileDialog openImgDialog = new OpenFileDialog();
                openImgDialog.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp";
                DialogResult result = openImgDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    Bitmap img = (Bitmap)sb_imageBox.BackgroundImage;
                    if (tabPage3.Text.Equals("End Cake Image"))
                    {
                       // img = new Bitmap(Image.FromFile(openImgDialog.FileName), new Size(316, 228));
                       // LargeImageLoader.importCakeImage(ref img, 0x07000000);
                    }
                    else
                    {
                        int segA_size = ROM.Instance.getSegment(0xA, (byte)level.CurrentAreaID).Length;
                        int skybox_width = 248;
                        int skybox_height = ((segA_size - 0x140) / 256 / 2 / 32) * 31;
                        img = new Bitmap(Image.FromFile(openImgDialog.FileName), new Size(skybox_width, skybox_height));
                        LargeImageLoader.importSkyboxImage(ref img, 0x0A000000);
                    }
                    sb_imageBox.BackgroundImage = img;
                }
            }
        }


        private void updateBGRGBColor()
        {
            LevelInfo.AreaBackgroundInfo bgInfo = level.getCurrentArea().bgInfo;
            ushort color = (ushort)(
                ((int)numericUpDown_Red.Value << 11) |
                ((int)numericUpDown_Green.Value << 6) |
                ((int)numericUpDown_Blue.Value << 1) | 1);
            bgInfo.id_or_color = color;
            ROM.Instance.writeHalfword(bgInfo.romLocation + 2, color);
            //Console.WriteLine("Updated BG color to 0x{0}", color.ToString("X4"));
            updateBoxColor(color);
            sb_imageBox.Update();
            sb_imageBox.Refresh();
        }

        private ushort getSkyboxConfigurationID()
        {
            int sizeOfSegmentA = ROM.Instance.getSegment(0xA, (byte)level.CurrentAreaID).Length;
            int skybox_height = ((sizeOfSegmentA - 0x140) / 256 / 2 / 32) * 32;

            switch (skybox_height)
            {
                case 160:
                    return 0006;
                case 192:
                    return 0001;
                case 256:
                default:
                    return 0000;
            }
        }

        private void updateBGRGBColor(object sender, EventArgs e)
        {
            updateBGRGBColor();
        }

        private void sb_useSolidColor_CheckedChanged(object sender, EventArgs e)
        {
            LevelInfo.AreaBackgroundInfo bgInfo = level.getCurrentArea().bgInfo;
            //Console.WriteLine("useSolidColor changed! " + sb_useSolidColor.Checked);
            if (sb_useSolidColor.Checked)
            {
                numericUpDown_Red.Enabled = true;
                numericUpDown_Green.Enabled = true;
                numericUpDown_Blue.Enabled = true;
                sb_importImage.Enabled = false;
                sb_exportImage.Enabled = false;
                bgInfo.address = 0x00000000;
                updateBGRGBColor();
            }
            else
            {
                numericUpDown_Red.Enabled = false;
                numericUpDown_Green.Enabled = false;
                numericUpDown_Blue.Enabled = false;
                sb_importImage.Enabled = true;
                sb_exportImage.Enabled = true;
                bgInfo.address = Globals.getSkyboxDrawFunction();
                bgInfo.id_or_color = getSkyboxConfigurationID();
                updateSkyboxTexture();
            }

            ROM.Instance.writeHalfword(bgInfo.romLocation + 2, bgInfo.id_or_color);
            ROM.Instance.writeWord(bgInfo.romLocation + 4, bgInfo.address);
        }

        private void updateBoxColor(ushort color)
        {
            if (sb_imageBox.BackgroundImage == null)
            {
                sb_imageBox.BackgroundImage = new Bitmap(64, 64);
            }
            Graphics g = Graphics.FromImage(sb_imageBox.BackgroundImage);
            int red = (color >> 11) & 31;
            int green = (color >> 6) & 31;
            int blue = (color >> 1) & 31;
            g.Clear(Color.FromArgb((int)(red * 8.226f), (int)(green * 8.226f), (int)(blue * 8.226f)));
        }

        private void updateSkyboxTexture()
        {
            int sizeOfSegmentA = ROM.Instance.getSegment(0xA, (byte)level.CurrentAreaID).Length;
            int skybox_width = 248;
            int skybox_height = ((sizeOfSegmentA - 0x140) / 256 / 2 / 32) * 31;
            Bitmap org = LargeImageLoader.getSkyboxImage(0x0A000000, skybox_width, skybox_height);
            if (org == null)
                return;
            sb_imageBox.BackgroundImage = new Bitmap(org, skybox_width, skybox_height);
            sb_imageBox.BackgroundImage.Tag = org.Tag;
            if (sb_imageBox.BackgroundImage != null)
            {
                sb_imageBox.BackgroundImageLayout = ImageLayout.Center;
                tabPage3.Text = "Sky Background";
            }
        }

        private void updateSkyboxImageInfo(int sizeOfSegmentA)
        {
            int skybox_width = 248;
            int skybox_height = ((sizeOfSegmentA - 0x140) / 256 / 2 / 32) * 31;
            Bitmap org = LargeImageLoader.getSkyboxImage(0x0A000000, skybox_width, skybox_height);
            if (org != null)
            {
                string[] tags = (string[])org.Tag;
                foreach (string tag in tags)
                {
                    if (tag.StartsWith("Width:"))
                        sb_info_Width.Text = tag;
                    else if (tag.StartsWith("Height:"))
                        sb_info_Height.Text = tag;
                    else if (tag.StartsWith("Format:"))
                        sb_info_Format.Text = tag;
                    else if (tag.StartsWith("Seg Addr:"))
                        sb_info_SegAddress.Text = tag;
                    else if (tag.StartsWith("ROM Address:"))
                        sb_info_RomAddress.Text = tag;
                }
            }
        }
        

        private void loadBackgroundImage()
        {
            sb_useTexture.Enabled = true;
            LevelInfo.AreaBackgroundInfo bgInfo = level.getCurrentArea().bgInfo;
            checkBox_matchFogColor.Enabled = bgInfo.usesFog && (bgInfo.fogColor_romLocation.Count > 0);
            //Console.WriteLine("BG info address = 0x{0}", bgInfo.address.ToString("X8"));
            
            int segA_size = 0;
            if (bgInfo.address != 0)
            {
                byte[] segA_data = ROM.Instance.getSegment(0xA, (byte)level.CurrentAreaID);
                if(segA_data != null)
                    segA_size = segA_data.Length;
            }
            // Console.WriteLine("Segment 0xA size: " + ROM.Instance.getSegment(0xA).Length);
            if (segA_size == 0)
                sb_useTexture.Enabled = false;
            else
            {
                sb_useTexture.Enabled = true;
                updateSkyboxImageInfo(segA_size);
            }

            if (bgInfo.address == 0 && !bgInfo.isEndCakeImage)
            {
                sb_imageBox.BackgroundImage = new Bitmap(200, 200);
                sb_imageBox.BackgroundImageLayout = ImageLayout.Center;
                ushort color = bgInfo.id_or_color;
                updateBoxColor(color);
                sb_useSolidColor.Checked = true;
                numericUpDown_Red.Enabled = true;
                numericUpDown_Green.Enabled = true;
                numericUpDown_Blue.Enabled = true;
                sb_importImage.Enabled = false;
                sb_exportImage.Enabled = false;
                int red = (color >> 11) & 31;
                int green = (color >> 6) & 31;
                int blue = (color >> 1) & 31;
                numericUpDown_Red.Value = red;
                numericUpDown_Green.Value = green;
                numericUpDown_Blue.Value = blue;
            }
            else if(segA_size > 0)
            {
                sb_useTexture.Checked = true;
                numericUpDown_Red.Enabled = false;
                numericUpDown_Green.Enabled = false;
                numericUpDown_Blue.Enabled = false;
                sb_importImage.Enabled = true;
                sb_exportImage.Enabled = true;
                tabControl1.TabPages.Remove(tabPage3);
                if (bgInfo.isEndCakeImage)
                {
                    /*
                    Bitmap org = LargeImageLoader.getCakeImage(0x07000000);
                    if (org == null)
                        return;
                    sb_imageBox.BackgroundImage = new Bitmap(org, 316, 228);
                    sb_imageBox.BackgroundImage.Tag = org.Tag;
                    if (sb_imageBox.BackgroundImage != null)
                    {
                        sb_imageBox.BackgroundImageLayout = ImageLayout.Center;
                        tabPage3.Text = "End Cake Image";
                    }*/
                }
                else
                {
                    tabControl1.TabPages.Add(tabPage3);
                    int skybox_width = 248;
                    int skybox_height = ((segA_size - 0x140) / 256 / 2 / 32) * 31;
                    Bitmap org = LargeImageLoader.getSkyboxImage(0x0A000000, skybox_width, skybox_height);
                    if (org == null)
                        return;
                    sb_imageBox.BackgroundImage = new Bitmap(org, skybox_width, skybox_height);
                    sb_imageBox.BackgroundImage.Tag = org.Tag;
                    if (sb_imageBox.BackgroundImage != null)
                    {
                        sb_imageBox.BackgroundImageLayout = ImageLayout.Center;
                        tabPage3.Text = "Sky Background";
                    }
                }
            }
        }

    }
}
