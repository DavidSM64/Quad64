using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quad64.src.Scripts
{
    class TextureFormats
    {
        public static Bitmap createColorTexture(Color color)
        {
            Bitmap tex = new Bitmap(1, 1);
            Graphics.FromImage(tex).Clear(color);
            return tex;
        }

        public static string ConvertFormatToString(byte formatColorType, byte formatByteSize)
        {
            string formatStr = "";
            switch (formatColorType & 7) {
                case 0: formatStr = "RGBA"; break;
                case 1: formatStr = "YUV"; break;
                case 2: formatStr = "CI"; break;
                case 3: formatStr = "IA"; break;
                case 4: formatStr = "I"; break;
                default: formatStr = "UNKNOWN"; break;
            }
            switch (formatByteSize & 3)
            {
                case 0: formatStr += "4"; break;
                case 1: formatStr += "8"; break;
                case 2: formatStr += "16"; break;
                case 3: formatStr += "32"; break;
            }
            return formatStr;
        }

        public static byte ConvertStringToFormat(string str)
        {
            str = str.ToLower();
            if (str.Equals("rgba16"))
                return 0x10;
            else if (str.Equals("rgba32"))
                return 0x18;
            else if (str.Equals("ci4"))
                return 0x40;
            else if (str.Equals("ci8"))
                return 0x48;
            else if (str.Equals("ia4"))
                return 0x60;
            else if (str.Equals("ia8"))
                return 0x68;
            else if (str.Equals("ia16"))
                return 0x70;
            else if (str.Equals("i4"))
                return 0x80;
            else if (str.Equals("i8"))
                return 0x88;
            else if (str.Equals("1bpp")) // Not a real F3D format.
                return 0x00;

            return 0x10;
        }

        public static int getNumberOfBitsForFormat(byte format)
        {
            switch (format)
            {
                case 0x00: // Note: "1 bit per pixel" is not a Fast3D format.
                    return 1;
                case 0x40:
                case 0x60:
                case 0x80:
                    return 4;
                case 0x48:
                case 0x68:
                case 0x88:
                    return 8;
                case 0x10:
                case 0x70:
                case 0x90:
                default:
                    return 16;
                case 0x18:
                    return 32;
            }
        }

        public static byte[] encodeTexture(byte format, Bitmap texture)
        {
            switch (format)
            {
                default:
                case 0x00: // Note: "1 bit per pixel" is not a Fast3D format.
                    return encode1BPP(texture);
                case 0x10:
                    return encodeRGBA16(texture);
                case 0x18:
                    return encodeRGBA32(texture);
                case 0x60:
                    return encodeIA4(texture);
                case 0x68:
                    return encodeIA8(texture);
                case 0x70:
                    return encodeIA16(texture);
                case 0x80:
                    return encodeI4(texture);
                case 0x88:
                    return encodeI4(texture);
                case 0x40:
                case 0x48:
                    throw new ArgumentException("CI texture encoding is not currently supported in this version.");
            }
        }
        
        public static byte getBit(int color, int bit)
        {
            return (byte)(((color >> 24) & 0xFF) > 0 ? (1 << bit) : 0);
        }

        public static byte[] encode1BPP(Bitmap texture)
        {
            int data_size = (texture.Width * texture.Height) / 8;
            byte[] data = new byte[data_size];
            for (int i = 0; i < data_size; i++)
            {
                int x = (i * 8) % texture.Width;
                int y = (i * 8) / texture.Width;

                data[i] = (byte)(
                    getBit(texture.GetPixel(x + 0, y).ToArgb(), 7) |
                    getBit(texture.GetPixel(x + 1, y).ToArgb(), 6) |
                    getBit(texture.GetPixel(x + 2, y).ToArgb(), 5) |
                    getBit(texture.GetPixel(x + 3, y).ToArgb(), 4) |
                    getBit(texture.GetPixel(x + 4, y).ToArgb(), 3) |
                    getBit(texture.GetPixel(x + 5, y).ToArgb(), 2) |
                    getBit(texture.GetPixel(x + 6, y).ToArgb(), 1) |
                    getBit(texture.GetPixel(x + 7, y).ToArgb(), 0)
                );
            }
            return data;
        }

        public static byte[] encodeRGBA16(Bitmap texture)
        {
            int data_size = (texture.Width * texture.Height) * 2;
            byte[] data = new byte[data_size];
            for (int i = 0; i < data_size / 2; i++)
            {
                int x = i % texture.Width;
                int y = i / texture.Width;
                Color pix = texture.GetPixel(x, y);
                byte red = (byte)((pix.R / 256.0f) * 32.0f);
                byte green = (byte)((pix.G / 256.0f) * 32.0f);
                byte blue = (byte)((pix.B / 256.0f) * 32.0f);
                byte alpha = (byte)(pix.A >= 128.0f ? 1 : 0);
                
                data[i * 2] = (byte)((red << 3) | (green >> 2));
                data[(i * 2) + 1] = (byte)(((green & 3) << 6) | (blue << 1) | alpha);
            }
            return data;
        }
        
        public static byte[] encodeRGBA32(Bitmap texture)
        {
            int data_size = (texture.Width * texture.Height) * 4;
            byte[] data = new byte[data_size];
            for (int i = 0; i < data_size / 4; i++)
            {
                int x = i % texture.Width;
                int y = i / texture.Width;
                Color pix = texture.GetPixel(x, y);

                data[(i * 4) + 0] = pix.R;
                data[(i * 4) + 1] = pix.G;
                data[(i * 4) + 2] = pix.B;
                data[(i * 4) + 3] = pix.A;
            }
            return data;
        }

        public static byte[] encodeIA4(Bitmap texture)
        {
            int data_size = (texture.Width * texture.Height) / 2;
            byte[] data = new byte[data_size];
            for (int i = 0; i < data_size; i++)
            {
                int x = (i * 2) % texture.Width;
                int y = (i * 2) / texture.Width;

                Color pix1 = texture.GetPixel(x, y);
                byte pix1_avg = (byte)((((pix1.R + pix1.G + pix1.B) / 3) / 255.0f) * 8.0f);
                byte upper = (byte)((pix1_avg << 1) | (pix1.A < 255 ? 0 : 1));

                Color pix2 = texture.GetPixel(x + 1, y);
                byte pix2_avg = (byte)((((pix2.R + pix2.G + pix2.B) / 3) / 255.0f) * 8.0f);
                byte lower = (byte)((pix2_avg << 1) | (pix2.A < 255 ? 0 : 1));

                data[i] = (byte)(((upper & 0xF) << 4) | (lower & 0xF));
            }
            return data;
        }

        public static byte[] encodeIA8(Bitmap texture)
        {
            int data_size = texture.Width * texture.Height;
            byte[] data = new byte[data_size];
            for (int i = 0; i < data_size; i++)
            {
                int x = i % texture.Width;
                int y = i / texture.Width;

                Color pix = texture.GetPixel(x, y);
                byte pix_avg = (byte)((((pix.R + pix.G + pix.B) / 3) / 255.0f) * 16.0f);
                byte pix_alpha = (byte)((pix.A / 255.0f) * 16.0f);

                data[i] = (byte)(((pix_avg & 0xF) << 4) | (pix_alpha & 0xF));
            }
            return data;
        }

        public static byte[] encodeIA16(Bitmap texture)
        {
            int data_size = texture.Width * texture.Height * 2;
            byte[] data = new byte[data_size];
            for (int i = 0; i < data_size / 2; i++)
            {
                int x = i % texture.Width;
                int y = i / texture.Width;

                Color pix = texture.GetPixel(x, y);
                byte pix_avg = (byte)((pix.R + pix.G + pix.B) / 3);

                data[i * 2] = pix_avg;
                data[(i * 2) + 1] = pix.A; 
            }
            return data;
        }

        public static byte[] encodeI4(Bitmap texture)
        {
            int data_size = (texture.Width * texture.Height) / 2;
            byte[] data = new byte[data_size];
            for (int i = 0; i < data_size; i++)
            {
                int x = (i * 2) % texture.Width;
                int y = (i * 2) / texture.Width;

                Color pix1 = texture.GetPixel(x, y);
                byte upper = (byte)((((pix1.R + pix1.G + pix1.B) / 3) / 255.0f) * 16.0f);

                Color pix2 = texture.GetPixel(x + 1, y);
                byte lower = (byte)((((pix2.R + pix2.G + pix2.B) / 3) / 255.0f) * 16.0f);

                data[i] = (byte)(((upper & 0xF) << 4) | (lower & 0xF));
            }
            return data;
        }


        public static byte[] encodeI8(Bitmap texture)
        {
            int data_size = texture.Width * texture.Height;
            byte[] data = new byte[data_size];
            for (int i = 0; i < data_size; i++)
            {
                int x = i % texture.Width;
                int y = i / texture.Width;

                Color pix = texture.GetPixel(x, y); 

                data[i] = (byte)((pix.R + pix.G + pix.B) / 3);
            }
            return data;
        }

        public static Bitmap decodeTexture(byte format, byte[] data, int width, int height, ushort[] palette, bool isPaletteRGBA16)
        {
            switch (format)
            {
                default:
                case 0x00: // Note: "1 bit per pixel" is not a Fast3D format.
                    return decode1BPP(data, width, height);
                case 0x10:
                    return decodeRGBA16(data, width, height);
                case 0x18:
                    return decodeRGBA32(data, width, height);
                case 0x40:
                    return decodeCI4(data, width, height, palette, isPaletteRGBA16);
                case 0x48:
                    return decodeCI8(data, width, height, palette, isPaletteRGBA16);
                case 0x60:
                    return decodeIA4(data, width, height);
                case 0x68:
                    return decodeIA8(data, width, height);
                case 0x70:
                    return decodeIA16(data, width, height);
                case 0x80:
                case 0x90:
                    return decodeI4(data, width, height);
                case 0x88:
                    return decodeI8(data, width, height);
            }
        }


        public static Bitmap decode1BPP(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);
            if (data.Length >= (width * height) / 8) // Sanity Check
            {
                int len = (width * height) / 8;
                for (int i = 0; i < len; ++i)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        byte intensity = (byte)((data[i] >> (7 - x)) & 1);
                        if (intensity > 0)
                            intensity = 0xFF;
                        int alpha = intensity;
                        int pos = (i * 8) + x;
                        tex.SetPixel(pos % width, pos / width, Color.FromArgb(alpha, intensity, intensity, intensity));
                    }
                }
            }
            tex.Tag = new string[] { "Format: 1BPP", "Width: " + width,
             "Height: " + height };
            return tex;
        }

        public static Bitmap decodeRGBA32(byte[] data, int width, int height)
        {
            Console.WriteLine("Texture size = (" + width + "x" + height + ")");
            Console.WriteLine("data.Length = (" + data.Length + ")");
            Bitmap tex = new Bitmap(width, height);

            if (data.Length >= width * height * 4) // Sanity Check
            {
                BitmapData bitmapData = tex.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, tex.PixelFormat);

                int len = width * height;
                for (int i = 0; i < len; i++)
                {
                    // Swap red and blue values
                    byte temp_red = data[(i * 4) + 0];
                    data[(i * 4) + 0] = data[(i * 4) + 2];
                    data[(i * 4) + 2] = temp_red;
                }
                Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);
                tex.UnlockBits(bitmapData);

            }
            tex.Tag = new string[] { "Format: RGBA32", "Width: " + width,
             "Height: " + height };
            return tex;
        }

        public static Bitmap decodeRGBA16(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);
            if (data.Length >= width * height * 2) // Sanity Check
            {
                BitmapData bitmapData = tex.LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.ReadWrite, tex.PixelFormat);
                byte[] pixels = new byte[width * height * 4];

                int len = width * height;
                for (int i = 0; i < len; i++)
                {
                    ushort pixel = (ushort)((data[i * 2] << 8) | data[i * 2 + 1]);
                    pixels[(i * 4) + 2] = (byte)(((pixel >> 11) & 0x1F) * 8); // Red
                    pixels[(i * 4) + 1] = (byte)(((pixel >> 6) & 0x1F) * 8); // Green
                    pixels[(i * 4) + 0] = (byte)(((pixel >> 1) & 0x1F) * 8); // Blue
                    pixels[(i * 4) + 3] = (pixel & 1) > 0 ? (byte)0xFF : (byte)0x00; // (Transparency)
                }
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                tex.UnlockBits(bitmapData);
            }

            tex.Tag = new string[] { "Format: RGBA16", "Width: " + width,
             "Height: " + height };
            return tex;
        }

        public static Bitmap decodeIA16(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);
            if (data.Length >= width * height * 2) // Sanity Check
            {
                BitmapData bitmapData = tex.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, tex.PixelFormat);
                byte[] pixels = new byte[width * height * 4];

                int len = width * height;
                for (int i = 0; i < len; i++)
                {
                    pixels[(i * 4) + 2] = data[i * 2]; // Red
                    pixels[(i * 4) + 1] = data[i * 2]; // Green
                    pixels[(i * 4) + 0] = data[i * 2]; // Blue
                    pixels[(i * 4) + 3] = data[(i * 2) + 1]; // Alpha
                }
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                tex.UnlockBits(bitmapData);
            }
            tex.Tag = new string[] { "Format: IA16", "Width: " + width,
             "Height: " + height};
            return tex;
        }

        public static Bitmap decodeIA8(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);
            if (data.Length >= width * height) // Sanity Check
            {
                BitmapData bitmapData = tex.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, tex.PixelFormat);
                byte[] pixels = new byte[width * height * 4];

                int len = width * height;
                for (int i = 0; i < len; i++)
                {
                    byte intensity = (byte)(((data[i] >> 4) & 0xF) * 16);
                    pixels[(i * 4) + 2] = intensity; // Red
                    pixels[(i * 4) + 1] = intensity; // Green
                    pixels[(i * 4) + 0] = intensity; // Blue
                    pixels[(i * 4) + 3] = (byte)((data[i] & 0xF) * 16); // Alpha
                }
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                tex.UnlockBits(bitmapData);
            }
            tex.Tag = new string[] { "Format: IA8", "Width: " + width,
             "Height: " + height };
            return tex;
        }
        public static Bitmap decodeIA4(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);

            if (data.Length >= (width * height) / 2) // Sanity Check
            {
                BitmapData bitmapData = tex.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, tex.PixelFormat);
                byte[] pixels = new byte[width * height * 4];

                int len = (width * height) / 2;
                for (int i = 0; i < len; i++)
                {
                    byte twoPixels = data[i];

                    byte intensity = (byte)((twoPixels >> 5) * 32);
                    pixels[(i * 8) + 2] = intensity; // Red
                    pixels[(i * 8) + 1] = intensity; // Green
                    pixels[(i * 8) + 0] = intensity; // Blue
                    pixels[(i * 8) + 3] = (byte)(((twoPixels >> 4) & 0x1) * 255); // Alpha

                    intensity = (byte)(((twoPixels >> 1) & 0x7) * 32);
                    pixels[(i * 8) + 6] = intensity; // Red
                    pixels[(i * 8) + 5] = intensity; // Green
                    pixels[(i * 8) + 4] = intensity; // Blue
                    pixels[(i * 8) + 7] = (byte)((twoPixels & 0x1) * 255); // Alpha
                }
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                tex.UnlockBits(bitmapData);
                tex.Tag = new string[] { "Format: IA4", "Width: " + width,
             "Height: " + height };
            }
            return tex;
        }
        public static Bitmap decodeI8(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);

            if (data.Length >= width * height) // Sanity Check
            {
                BitmapData bitmapData = tex.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, tex.PixelFormat);
                byte[] pixels = new byte[width * height * 4];

                int len = width * height;
                for (int i = 0; i < len; i++)
                {
                    byte intensity = data[i];
                    pixels[(i * 4) + 2] = intensity; // Red
                    pixels[(i * 4) + 1] = intensity; // Green
                    pixels[(i * 4) + 0] = intensity; // Blue
                    pixels[(i * 4) + 3] = 0xFF; // Alpha
                }
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                tex.UnlockBits(bitmapData);

                tex.Tag = new string[] { "Format: I8", "Width: " + width,
             "Height: " + height };
            }
            return tex;
        }
        public static Bitmap decodeI4(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);

            if (data.Length >= (width * height) / 2) // Sanity Check
            {
                BitmapData bitmapData = tex.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, tex.PixelFormat);
                byte[] pixels = new byte[width * height * 4];

                int len = (width * height) / 2;
                for (int i = 0; i < len; i++)
                {
                    byte twoPixels = data[i];

                    byte intensity = (byte)((twoPixels >> 4) * 16);
                    pixels[(i * 8) + 2] = intensity; // Red
                    pixels[(i * 8) + 1] = intensity; // Green
                    pixels[(i * 8) + 0] = intensity; // Blue
                    pixels[(i * 8) + 3] = 0xFF; // Alpha

                    intensity = (byte)((twoPixels & 0xF) * 16);
                    pixels[(i * 8) + 6] = intensity; // Red
                    pixels[(i * 8) + 5] = intensity; // Green
                    pixels[(i * 8) + 4] = intensity; // Blue
                    pixels[(i * 8) + 7] = 0xFF; // Alpha
                }
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                tex.UnlockBits(bitmapData);
            }
            tex.Tag = new string[] { "Format: I4", "Width: " + width,
             "Height: " + height };
            return tex;
        }

        public static Bitmap decodeCI4(byte[] data, int width, int height, ushort[] palette, bool isPaletteRGBA16)
        {
            Bitmap tex = new Bitmap(width, height);

            if (data.Length >= (width * height) / 2) // Sanity Check
            {
                BitmapData bitmapData = tex.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, tex.PixelFormat);
                byte[] pixels = new byte[width * height * 4];

                int len = (width * height) / 2;
                for (int i = 0; i < len; i++)
                {
                    ushort pixel = palette[(data[i] >> 4) & 0xF];
                    pixels[(i * 8) + 2] = (byte)(((pixel >> 11) & 0x1F) * 8); // Red
                    pixels[(i * 8) + 1] = (byte)(((pixel >> 6) & 0x1F) * 8); // Green
                    pixels[(i * 8) + 0] = (byte)(((pixel >> 1) & 0x1F) * 8); // Blue
                    pixels[(i * 8) + 3] = (pixel & 1) > 0 ? (byte)0xFF : (byte)0x00; // Alpha

                    pixel = palette[(data[i]) & 0xF];
                    pixels[(i * 8) + 6] = (byte)(((pixel >> 11) & 0x1F) * 8); // Red
                    pixels[(i * 8) + 5] = (byte)(((pixel >> 6) & 0x1F) * 8); // Green
                    pixels[(i * 8) + 4] = (byte)(((pixel >> 1) & 0x1F) * 8); // Blue
                    pixels[(i * 8) + 7] = (pixel & 1) > 0 ? (byte)0xFF : (byte)0x00; // Alpha

                }
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                tex.UnlockBits(bitmapData);
            }
            tex.Tag = new string[] { "Format: CI4", "Width: " + width,
             "Height: " + height };
            return tex;
        }

        public static Bitmap decodeCI8(byte[] data, int width, int height, ushort[] palette, bool isPaletteRGBA16)
        {
            Bitmap tex = new Bitmap(width, height);

            if (data.Length >= width * height) // Sanity Check
            {
                BitmapData bitmapData = tex.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, tex.PixelFormat);
                byte[] pixels = new byte[width * height * 4];

                int len = width * height;
                for (int i = 0; i < len; i++)
                {
                    ushort pixel = palette[data[i]];
                    pixels[(i * 4) + 2] = (byte)(((pixel >> 11) & 0x1F) * 8); // Red
                    pixels[(i * 4) + 1] = (byte)(((pixel >> 6) & 0x1F) * 8); // Green
                    pixels[(i * 4) + 0] = (byte)(((pixel >> 1) & 0x1F) * 8); // Blue
                    pixels[(i * 4) + 3] = (pixel & 1) > 0 ? (byte)0xFF : (byte)0x00; // (Transparency)
                                                                                     //tex.SetPixel(i % width, i / width, Color.FromArgb(alpha, red, green, blue));
                }
                Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                tex.UnlockBits(bitmapData);
            }
            tex.Tag = new string[] { "Format: CI8", "Width: " + width,
             "Height: " + height };
            return tex;
        }
    }
}
