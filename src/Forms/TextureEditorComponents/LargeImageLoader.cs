using System.Drawing;

namespace Quad64.src.Forms.TextureEditorComponents
{
    class LargeImageLoader
    {
        private static void parseRGBA16Section(ref Bitmap map, uint address, int x, int y, int w, int h)
        {
            byte[] data = ROM.Instance.getSubArray_safe(ROM.Instance.Bytes, address, (uint)((w + 1) * (h + 1) * 2));
            for (int yy = 0; yy < h; ++yy)
            {
                for (int xx = 0; xx < w; ++xx)
                {
                    int i = ((yy * (w + 1)) + xx) * 2;
                    ushort pixel = (ushort)((data[i] << 8) | data[i + 1]);
                    byte red = (byte)(((pixel >> 11) & 0x1F) * 8); // Red
                    byte green = (byte)(((pixel >> 6) & 0x1F) * 8); // Green
                    byte blue = (byte)(((pixel >> 1) & 0x1F) * 8); // Blue
                    byte alpha = (pixel & 1) > 0 ? (byte)0xFF : (byte)0x00; // Alpha (Transparency)
                    map.SetPixel(x + xx, y + yy, Color.FromArgb(alpha, red, green, blue));
                }
            }
        }

        private static void importPixel(uint baseAddress, int x, int y, int lineWidth, dynamic texelValue, byte textureSize)
        {
            if (textureSize == 2)
            {
                ROM.Instance.writeHalfword((uint)(baseAddress + ((y * lineWidth + x) * 2)), texelValue);
            }
        }

        private static void importRGBA16Section(ref Bitmap map, uint address, int src_x, int src_y, int start_x, int start_y, int offset_x, int offset_y, int w, int h, int lineWidth)
        {
            ROM rom = ROM.Instance;

            for (int yy = start_y; yy < h; ++yy)
            {
                for (int xx = start_x; xx < w; ++xx)
                {
                    int pixel = map.GetPixel(src_x + xx, src_y + yy).ToArgb();
                    byte blue = (byte)((pixel & 0xFF) / 8);
                    byte green = (byte)(((pixel >> 8) & 0xFF) / 8);
                    byte red = (byte)(((pixel >> 16) & 0xFF) / 8);
                    byte alpha = (byte)(((pixel >> 24) & 0xFF) > 0 ? 1 : 0);
                    ushort halfword = (ushort)((red << 11) | (green << 6) | (blue << 1) | alpha);
                    importPixel(address, xx + offset_x, yy + offset_y, lineWidth, halfword, 2);
                }
            }

        }

        public static void importSkyboxImage(ref Bitmap img, uint seg_address)
        {
            uint rom_address = ROM.Instance.decodeSegmentAddress(seg_address, null);

            int wTiles = img.Width / 31;
            int hTiles = img.Height / 31;
            uint current_address = rom_address;
            for (int y = 0; y < hTiles; y++)
            {
                for (int x = 0; x < wTiles; x++)
                {
                    importRGBA16Section(ref img, current_address, x * 31, y * 31, 0, 0, 0, 0, 31, 31, 32);
                    if (x < wTiles - 1)
                    {
                        importRGBA16Section(ref img, current_address, ((x + 1) * 31) - 30, y * 31, 30, 0, 1, 0, 31, 31, 32);
                        importRGBA16Section(ref img, current_address, ((x + 1) * 31) - 30, y * 31, 30, 30, 1, 1, 31, 31, 32);
                    }
                    else
                    {
                        // I know that the -30 might not make any sense, but trust me that the math works out.
                        importRGBA16Section(ref img, current_address, -30, y * 31, 30, 0, 1, 0, 31, 31, 32);
                        importRGBA16Section(ref img, current_address, -30, y * 31, 30, 30, 1, 1, 31, 31, 32);
                    }

                    if (y < hTiles - 1)
                    {
                        importRGBA16Section(ref img, current_address, (x * 31), ((y + 1) * 31) - 30, 0, 30, 0, 1, 31, 31, 32);
                    }
                    else
                    {
                        importRGBA16Section(ref img, current_address, (x * 31), -30, 0, 30, 0, 1, 31, 31, 32);
                    }

                    current_address += 2048;
                }
            }
        }

        public static Bitmap getSkyboxImage(uint seg_address, int w, int h)
        {
            if (!ROM.Instance.isSegmentMIO0((byte)(seg_address >> 24), null))
            {
                uint rom_address = ROM.Instance.decodeSegmentAddress(seg_address, null);
                uint current_address = rom_address;
                Bitmap img = new Bitmap(w, h);
                for (int y = 0; y < (h/31); y++)
                {
                    for (int x = 0; x < (w/31); x++)
                    {
                        parseRGBA16Section(ref img, current_address, x * 31, y * 31, 31, 31);
                        current_address += 2048;
                    }
                }

                img.Tag = new string[] {
                    "Format: RGBA16",
                    "Width: " + w,
                    "Height: " + h,
                    "ROM Address: " + rom_address.ToString("X"),
                    "Seg Addr: " + seg_address.ToString("X8")
                };
                return img;
            }
            return null;
        }
        /*
        public static Bitmap getCakeImage(uint seg_address)
        {
            if (!ROM.Instance.isSegmentMIO0((byte)(seg_address >> 24)))
            {
                uint rom_address = ROM.Instance.decodeSegmentAddress(seg_address);
                Bitmap img = new Bitmap(316, 228);
                int wTiles = img.Width / 79;
                int hTiles = img.Height / 19;
                uint current_address = rom_address;
                for (int y = 0; y < hTiles; y++)
                {
                    for (int x = 0; x < wTiles; x++)
                    {
                        parseRGBA16Section(ref img, current_address, x * 79, y * 19, 79, 19);
                        current_address += 3200;
                    }
                }

                img.Tag = new string[] {
                    "Format: RGBA16",
                    "Width: 316",
                    "Height: 228",
                    "ROM Address: " + rom_address.ToString("X"),
                    "Seg Addr: " + seg_address.ToString("X8")
                };
                return img;
            }
            return null;
        }*/

    }
}
