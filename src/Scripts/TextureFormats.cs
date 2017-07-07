using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Quad64.src.Scripts
{
    class TextureFormats
    {
        public static Bitmap createColorTexture(Color color)
        {
            Bitmap tex = new Bitmap(1, 1);
            tex.SetPixel(0, 0, color);
            return tex;
        }

        public static Bitmap decodeTexture(byte format, byte[] data, int width, int height)
        {
            switch (format)
            {
                default:
                case 0x10:
                    return decodeRGBA16(data, width, height);
                case 0x18:
                    return decodeRGBA32(data, width, height);
                case 0x60:
                    return decodeIA4(data, width, height);
                case 0x68:
                    return decodeIA8(data, width, height);
                case 0x70:
                    return decodeIA16(data, width, height);
                case 0x40: // Interpret CI textures as grayscale (for now)
                case 0x80:
                case 0x90:
                    return decodeI4(data, width, height);
                case 0x48: // Interpret CI textures as grayscale (for now)
                case 0x88:
                    return decodeI8(data, width, height);
            }
        }

        public static Bitmap decodeRGBA32(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);

            for (int i = 0; i < width * height; ++i)
            {
                byte red = data[i * 4 + 0]; // Red
                byte green = data[i * 4 + 1]; // Green
                byte blue = data[i * 4 + 2]; // Blue
                byte alpha = data[i * 4 + 3]; // Alpha (Transparency)
                tex.SetPixel(i % width, i / width, Color.FromArgb(alpha, red, green, blue));
            }

            return tex;
        }

        public static Bitmap decodeRGBA16(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);

            for (int i = 0; i < width * height; ++i)
            {
                ushort pixel = (ushort)((data[i * 2] << 8) | data[i * 2 + 1]);
                byte red = (byte)(((pixel >> 11) & 0x1F) * 8); // Red
                byte green = (byte)(((pixel >> 6) & 0x1F) * 8); // Green
                byte blue = (byte)(((pixel >> 1) & 0x1F) * 8); // Blue
                byte alpha = (pixel & 1) > 0 ? (byte)0xFF : (byte)0x00; // Alpha (Transparency)
                tex.SetPixel(i % width, i / width, Color.FromArgb(alpha, red, green, blue));
            }

            return tex;
        }

        public static Bitmap decodeIA16(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);

            for (int i = 0; i < width * height; ++i)
            {
                ushort pixel = (ushort)((data[i * 2] << 8) | data[i * 2 + 1]);
                byte intensity = data[i * 2];
                byte alpha = data[i * 2 + 1];
                tex.SetPixel(i % width, i / width, Color.FromArgb(alpha, intensity, intensity, intensity));
            }

            return tex;
        }

        public static Bitmap decodeIA8(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);

            for (int i = 0; i < width * height; ++i)
            {
                byte intensity = (byte)(((data[i] >> 4) & 0xF) * 16);
                byte alpha = (byte)((data[i] & 0xF) * 16);
                tex.SetPixel(i % width, i / width, Color.FromArgb(alpha, intensity, intensity, intensity));
            }

            return tex;
        }
        public static Bitmap decodeIA4(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);
            int len = (width * height) / 2;
            for (int i = 0; i < len; i++)
            {
                byte twoPixels = data[i];
                byte intensity = (byte)((twoPixels >> 5) * 32);
                byte alpha = (byte)(((twoPixels >> 4) & 0x1) * 256);
                tex.SetPixel((i * 2) % width, (i * 2) / width, Color.FromArgb(alpha, intensity, intensity, intensity));

                intensity = (byte)(((twoPixels >> 1) & 0x7) * 32);
                alpha = (byte)((twoPixels & 0x1) * 256);
                tex.SetPixel(((i * 2) + 1) % width, ((i * 2) + 1) / width, Color.FromArgb(alpha, intensity, intensity, intensity));
            }
            return tex;
        }
        public static Bitmap decodeI8(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);

            for (int i = 0; i < width * height; ++i)
            {
                byte intensity = data[i];
                tex.SetPixel(i % width, i / width, Color.FromArgb(intensity, intensity, intensity));
            }

            return tex;
        }
        public static Bitmap decodeI4(byte[] data, int width, int height)
        {
            Bitmap tex = new Bitmap(width, height);
            int len = (width * height)/2;
            for (int i = 0; i < len; i++)
            {
                byte twoPixels = data[i];
                byte intensity = (byte)((twoPixels >> 4) * 16);
                tex.SetPixel((i * 2) % width, (i * 2) / width, Color.FromArgb(intensity, intensity, intensity));

                intensity = (byte)((twoPixels & 0xF) * 16);
                tex.SetPixel(((i*2) + 1) % width, ((i * 2) + 1) / width, Color.FromArgb(intensity, intensity, intensity));
            }
            return tex;
        }
    }
}
