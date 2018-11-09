using OpenTK;
using Quad64.src.LevelInfo;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Quad64.src.Scripts
{
    class Fast3DScripts
    {
         private struct F3D_Vertex
        {
            public short x, y, z, f, u, v; // f = flag (Not sure what it does)
            public byte nx_r, ny_g, nz_b, a;
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

        private static ulong bytesToLong(byte[] b, int offset)
        {
            ulong val = 0;
            for (int i = 0; i < 8; i++)
                val |= ((ulong)b[i + offset] << ((7-i) * 8));
            return val;
        }

        private enum CMD
        {
            F3D_NOOP = 0x00,
            F3D_MTX = 0x01,
            F3D_MOVEMEM = 0x03,
            F3D_VTX = 0x04,
            F3D_DL = 0x06,
            F3D_CLEARGEOMETRYMODE = 0xB6,
            F3D_SETGEOMETRYMODE = 0xB7,
            F3D_ENDDL = 0xB8,
            F3D_SETOTHERMODE_L = 0xB9,
            F3D_SETOTHERMODE_H = 0xBA,
            F3D_TEXTURE = 0xBB,
            F3D_MOVEWORD = 0xBC,
            F3D_POPMTX = 0xBD,
            F3D_CULLDL = 0xBE,
            F3D_TRI1 = 0xBF,
            G_TEXRECT = 0xE4,
            G_TEXRECTFLIP = 0xE5,
            G_RDPLOADSYNC = 0xE6,
            G_RDPPIPESYNC = 0xE7,
            G_RDPTILESYNC = 0xE8,
            G_RDPFULLSYNC = 0xE9,
            G_SETKEYGB = 0xEA,
            G_SETKEYR = 0xEB,
            G_SETCONVERT = 0xEC,
            G_SETSCISSOR = 0xED,
            G_SETPRIMDEPTH = 0xEE,
            G_RDPSETOTHERMODE = 0xEF,
            G_LOADTLUT = 0xF0,
            G_SETTILESIZE = 0xF2,
            G_LOADBLOCK = 0xF3,
            G_SETTILE = 0xF5,
            G_FILLRECT = 0xF6,
            G_SETFILLCOLOR = 0xF7,
            G_SETFOGCOLOR = 0xF8,
            G_SETBLENDCOLOR = 0xF9,
            G_SETPRIMCOLOR = 0xFA,
            G_SETENVCOLOR = 0xFB,
            G_SETCOMBINE = 0xFC,
            G_SETTIMG = 0xFD,
            G_SETZIMG = 0xFE,
            G_SETCIMG = 0xFF
        }
        
        static TempMaterial tempMaterial = new TempMaterial();
        static F3D_Vertex[] vertices = new F3D_Vertex[16];
        
        public static void parse(ref Model3D mdl, ref Level lvl, byte seg, uint off, byte? areaID, int current_depth)
        {
            if (seg == 0 || current_depth >= 300)
                return; // depth was added to prevent infinite loops with 0x06 command.
            ROM rom = ROM.Instance;
            byte[] data = rom.getSegment(seg, areaID);
            if (data == null) return;
            bool end = false;
            while (!end)
            {
                if (off + 8 > data.Length)
                    return;
                byte[] cmd = rom.getSubArray_safe(data, off, 8);
                if (!Enum.IsDefined(typeof(CMD), (int)cmd[0]))
                {
                    return;
                    //throw new Exception("UNDEFINED FAST3D COMMAND: 0x"+cmd[0].ToString("X2"));
                }
                string desc = ((CMD)cmd[0]).ToString();
                bool alreadyAdded = false;

                switch ((CMD)cmd[0])
                {
                    case CMD.F3D_NOOP:
                        desc = "Do nothing";
                        if (bytesToInt(cmd, 0, 4) != 0)
                            return;
                        break;
                    case CMD.F3D_MTX:
                        desc = "G_MTX";
                        // Detect if empty data has been found.
                        if (bytesToLong(cmd, 0) == 0x0101010101010101)
                            return;
                        break;
                    case CMD.F3D_MOVEMEM:
                        switchTextureStatus(ref mdl, ref tempMaterial, true, areaID);
                        F3D_MOVEMEM(ref tempMaterial, ref lvl, cmd, ref desc, areaID);
                        break;
                    case CMD.F3D_VTX:
                        switchTextureStatus(ref mdl, ref tempMaterial, false, areaID);
                        //if (tempMaterial.id != 0) return;
                        if(!F3D_VTX(vertices, ref lvl, cmd, ref desc, areaID))
                            return;
                        break;
                    case CMD.F3D_DL:
                        alreadyAdded = true;
                        if (cmd[1] == 0x00)
                            desc = "Jump and link to display list at 0x" + bytesToInt(cmd, 4, 4).ToString("X8");
                        else
                            desc = "Jump to display list at 0x" + bytesToInt(cmd, 4, 4).ToString("X8");
                            
                        addF3DCommandToDump(ref mdl, cmd, seg, off, desc, areaID);
                        F3D_DL(ref mdl, ref lvl, cmd, areaID, current_depth);
                        if (cmd[1] == 1)
                            end = true;
                        break;
                    case CMD.F3D_CLEARGEOMETRYMODE:
                        tempMaterial.geometryMode &= ~bytesToInt(cmd, 4, 4);
                        break;
                    case CMD.F3D_SETGEOMETRYMODE:
                        tempMaterial.geometryMode |= bytesToInt(cmd, 4, 4);
                        break;
                    case CMD.F3D_ENDDL:
                        desc = "End of display list";
                        end = true;
                        break;
                    case CMD.F3D_TEXTURE:
                        F3D_TEXTURE(ref tempMaterial, cmd);
                        break;
                    case CMD.F3D_TRI1:
                        switchTextureStatus(ref mdl, ref tempMaterial, false, areaID);
                        //if (tempMaterial.id != 0) return;
                        F3D_TRI1(vertices, ref mdl, ref lvl, ref tempMaterial, cmd, ref desc);
                        break;
                    case CMD.G_RDPLOADSYNC:
                        desc = "RDP Load sync";
                        break;
                    case CMD.G_RDPPIPESYNC:
                        desc = "RDP Pipe sync";
                        break;
                    case CMD.G_RDPTILESYNC:
                        desc = "RDP Tile sync";
                        break;
                    case CMD.G_RDPFULLSYNC:
                        desc = "RDP Full sync";
                        break;
                    case CMD.G_LOADTLUT:
                        G_LOADTLUT(cmd, ref tempMaterial, ref desc, areaID);
                        break;
                    case CMD.G_SETTILESIZE:
                        switchTextureStatus(ref mdl, ref tempMaterial, true, areaID);
                        G_SETTILESIZE(cmd, ref tempMaterial, ref desc);
                        break;
                    case CMD.G_LOADBLOCK:
                        desc = "Load " + (((bytesToInt(cmd, 4, 4) >> 12) & 0xFFF) + 1) + " texels into texture memory cache";
                        break;
                    case CMD.G_SETTILE:
                        G_SETTILE(ref tempMaterial, cmd);
                        break;
                    case CMD.G_SETFOGCOLOR:
                        mdl.builder.UsesFog = true;
                        mdl.builder.FogColor = Color.FromArgb(cmd[4], cmd[5], cmd[6]);
                        mdl.builder.FogColor_romLocation.Add(rom.getSegmentStart(seg, areaID) + off);
                        //Console.WriteLine("Fog color = 0x{0}", bytesToInt(cmd, 4, 4).ToString("X8"));
                        break;
                    case CMD.G_SETCOMBINE:
                        if(G_SETCOMBINE(ref tempMaterial, cmd))
                            switchTextureStatus(ref mdl, ref tempMaterial, true, areaID);
                        break;
                    case CMD.G_SETTIMG:
                        switchTextureStatus(ref mdl, ref tempMaterial, true, areaID);
                        G_SETTIMG(ref tempMaterial, cmd, ref desc);
                        break;
                }
                if(!alreadyAdded)
                    addF3DCommandToDump(ref mdl, cmd, seg, off, desc, areaID);
                off += 8;
            }
        }


        private static void addF3DCommandToDump(ref Model3D mdl, byte[] cmd, byte seg, uint offset, string description, byte? areaID)
        {
            ScriptDumpCommandInfo info = new ScriptDumpCommandInfo();
            info.data = cmd;
            info.description = description;
            info.segAddress = (uint)(seg << 24) | offset;

            info.romAddress = ROM.Instance.decodeSegmentAddress_safe(seg, offset, areaID);
            //Console.WriteLine("Adding to dump " + (mdl.Fast3DCommands_ForDump.Count - 1));
            if(mdl.Fast3DCommands_ForDump.Count > 0)
                mdl.Fast3DCommands_ForDump[mdl.Fast3DCommands_ForDump.Count - 1].Add(info);
        }

        private static void switchTextureStatus(ref Model3D mdl, ref TempMaterial temp, bool status, byte? areaID)
        {
            ROM rom = ROM.Instance;
            
            if (mdl.builder.processingTexture != status)
            {
                if (status == false)
                {
                    if (!mdl.builder.hasTexture(temp.segOff))
                    {
                        if (temp.segOff != 0 && temp.w != 0 && temp.h != 0)
                        {
                            mdl.builder.AddTexture(
                                TextureFormats.decodeTexture(
                                    temp.format,
                                    rom.getDataFromSegmentAddress_safe(
                                        temp.segOff,
                                        (uint)(temp.w * temp.h * 4),
                                        areaID
                                    ),
                                    temp.w,
                                    temp.h,
                                    temp.palette,
                                    temp.isPaletteRGBA16
                                ),
                                mdl.builder.newTexInfo(temp.wrapS, temp.wrapT),
                                temp.segOff
                            );
                        }
                        else
                        {
                            mdl.builder.AddTexture(
                                TextureFormats.createColorTexture(System.Drawing.Color.FromArgb((int)temp.color)),
                                mdl.builder.newTexInfo(temp.wrapS, temp.wrapT),
                                temp.segOff
                            );
                        }
                    }
                }
                mdl.builder.processingTexture = status;
            }
        }
        
        private static void F3D_MOVEMEM(ref TempMaterial temp, ref Level lvl, byte[] cmd, ref string desc, byte? areaID)
        {
            if (cmd[1] == 0x86)
            {
                ROM rom = ROM.Instance;
                byte[] colData = rom.getDataFromSegmentAddress(bytesToInt(cmd, 4, 4), 4, areaID);
                temp.color = (bytesToInt(colData, 0, 3) | 0xFF000000);
                desc = "Load light values for normal shading from address 0x" + bytesToInt(cmd, 4, 4).ToString("X8");
                //rom.printArray(colData, 4);
            }
            else if (cmd[1] == 0x88)
            {
                desc = "Load dark values for normal shading from address 0x" + bytesToInt(cmd, 4, 4).ToString("X8");
            }
        }

        private static bool F3D_VTX(F3D_Vertex[] vertices, ref Level lvl, byte[] cmd, ref string desc, byte? areaID)
        {
            ROM rom = ROM.Instance;
            int amount = ((cmd[2] << 8) | cmd[3]) / 0x10;
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);

            desc = "Load " + amount + " vertices from address 0x" + bytesToInt(cmd, 4, 4).ToString("X8");
            // Console.WriteLine("04: Amt = " + amount + ", Seg = " + seg.ToString("X2")+", Off = "+off.ToString("X6"));
            if (rom.getSegment(seg, areaID) == null)
                return false;
            byte[] vData = rom.getSubArray_safe(rom.getSegment(seg, areaID), off, (uint)amount * 0x10);
            for (int i = 0; i < amount; i++)
            {
                vertices[i].x = (short)((vData[i * 0x10 + 0] << 8) | vData[i * 0x10 + 1]);
                vertices[i].y = (short)((vData[i * 0x10 + 2] << 8) | vData[i * 0x10 + 3]);
                vertices[i].z = (short)((vData[i * 0x10 + 4] << 8) | vData[i * 0x10 + 5]);
                vertices[i].f = (short)((vData[i * 0x10 + 9] << 8) | vData[i * 0x10 + 7]);
                vertices[i].u = (short)((vData[i * 0x10 + 8] << 8) | vData[i * 0x10 + 9]);
                vertices[i].v = (short)((vData[i * 0x10 + 10] << 8) | vData[i * 0x10 + 11]);
                vertices[i].nx_r = vData[i * 0x10 + 12];
                vertices[i].ny_g = vData[i * 0x10 + 13];
                vertices[i].nz_b = vData[i * 0x10 + 14];
                vertices[i].a = vData[i * 0x10 + 15];
            }
            return true;
        }

        private static void F3D_DL(ref Model3D mdl, ref Level lvl, byte[] cmd, byte? areaID, int current_depth)
        {
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);
            parse(ref mdl, ref lvl, seg, off, areaID, current_depth + 1);
        }

        private static Vector4 getColor(uint color)
        {
            return new Vector4(
                ((color >> 16) & 0xFF) / 256.0f, 
                ((color >> 8) & 0xFF) / 256.0f, 
                (color & 0xFF) / 256.0f, 
                1.0f
           );
        }

        private static void F3D_TEXTURE(ref TempMaterial temp, byte[] cmd)
        {
            ushort tsX = (ushort)bytesToInt(cmd, 4, 2);
            ushort tsY = (ushort)bytesToInt(cmd, 6, 2);
            
            if ((temp.geometryMode & 0x40000) == 0x40000)
            {
                temp.w = (ushort)((tsX >> 6));
                temp.h = (ushort)((tsY >> 6));
                if (temp.w == 31) temp.w = 32;
                else if (temp.w == 62) temp.w = 64;
                if (temp.h == 31) temp.h = 32;
                else if (temp.h == 62) temp.h = 64;
            }
            else
            {
                if (tsX != 0xFFFF)
                    temp.texScaleX = (float)tsX / 65536.0f;
                else
                    temp.texScaleX = 1.0f;
                if (tsY != 0xFFFF)
                    temp.texScaleY = (float)tsY / 65536.0f;
                else
                    temp.texScaleY = 1.0f;
            }
        }

        private static void F3D_TRI1(F3D_Vertex[] vertices, ref Model3D mdl, ref Level lvl, ref TempMaterial temp, byte[] cmd, ref string desc)
        {
            desc = "Draw triangle using vertices (" + (cmd[5] / 0x0A) + "," + cmd[6] / 0x0A + "," + cmd[7] / 0x0A + ")";
            mdl.builder.numTriangles++;
            F3D_Vertex a = vertices[cmd[5] / 0x0A];
            Vector3 a_pos = new Vector3(a.x, a.y, a.z);
            Vector2 a_uv = new Vector2(a.u * temp.texScaleX, a.v * temp.texScaleY);
            Vector4 a_color = new Vector4(a.nx_r / 255.0f, a.ny_g / 255.0f, a.nz_b / 255.0f, 1.0f);
            F3D_Vertex b = vertices[cmd[6] / 0x0A];
            Vector3 b_pos = new Vector3(b.x, b.y, b.z);
            Vector2 b_uv = new Vector2(b.u * temp.texScaleX, b.v * temp.texScaleY);
            Vector4 b_color = new Vector4(b.nx_r / 255.0f, b.ny_g / 255.0f, b.nz_b / 255.0f, 1.0f);
            F3D_Vertex c = vertices[cmd[7] / 0x0A];
            Vector3 c_pos = new Vector3(c.x, c.y, c.z);
            Vector2 c_uv = new Vector2(c.u * temp.texScaleX, c.v * temp.texScaleY);
            Vector4 c_color = new Vector4(c.nx_r / 255.0f, c.ny_g / 255.0f, c.nz_b / 255.0f, 1.0f);

            //System.Console.WriteLine("Adding new Triangle: " + a_pos + "," + b_pos + "," + c_pos);

            if ((temp.geometryMode & 0x20000) != 0)
            {
                mdl.builder.AddTempVertex(a_pos, a_uv, getColor(temp.color));
                mdl.builder.AddTempVertex(b_pos, b_uv, getColor(temp.color));
                mdl.builder.AddTempVertex(c_pos, c_uv, getColor(temp.color));
            }
            else
            {
                mdl.builder.AddTempVertex(a_pos, a_uv, a_color);
                mdl.builder.AddTempVertex(b_pos, b_uv, b_color);
                mdl.builder.AddTempVertex(c_pos, c_uv, c_color);
            }
        }

        private static void G_LOADTLUT(byte[] cmd, ref TempMaterial temp, ref string desc, byte? areaID)
        {
            byte paletteTileDescriptor = cmd[4];
            ushort numColorsToLoadInPalette = (ushort)((bytesToInt(cmd, 5, 2) >> 6) + 1);
            desc = "Load " + numColorsToLoadInPalette + " colors into texture memory cache";
            byte[] segmentData = ROM.Instance.getSegment((byte)(temp.segOff >> 24), areaID);
            uint offset = temp.segOff & 0x00FFFFFF;
            for (int i = 0; i < numColorsToLoadInPalette; i++)
            {
                temp.palette[i] = (ushort)((segmentData[offset + (i * 2)] << 8) | segmentData[offset + (i * 2) + 1]);
            }
        }

        private static void G_SETTILESIZE(byte[] cmd, ref TempMaterial temp, ref string desc)
        {
            temp.w = (ushort)((((cmd[5] << 8) | (cmd[6] & 0xF0)) >> 6) + 1);
            temp.h = (ushort)((((cmd[6] & 0x0F) << 8 | cmd[7]) >> 2) + 1);
            desc = "Set texture size (width = "+temp.w + ", height = "+temp.h+")";
        }

        private static int getWrap(int flag)
        {
            switch (flag)
            {
                case 0:
                default:
                    return (int)OpenTK.Graphics.OpenGL.All.Repeat;
                case 1:
                    return (int)OpenTK.Graphics.OpenGL.All.MirroredRepeat;
                case 2:
                    return (int)OpenTK.Graphics.OpenGL.All.ClampToEdge;
            }
        }

        private static void G_SETTILE(ref TempMaterial temp, byte[] cmd)
        {
            if (bytesToInt(cmd, 4, 4) == 0x07000000)
                return;

            if (cmd[4] == 0x00) // Make sure the tile is TX_RENDERTILE (0x0) and not TX_LOADTILE (0x7)
            {
                /* 
                    The format for a texture should actually be used from SetTile (0xF5) command,
                    and not the SetTextureImage (0xFD) command. If you used the format from 0xFD,
                    then you will have issues with 4-bit textures. This is because the N64 4-bit 
                    textures use 16-bit formats to load data.
                */
                temp.format = cmd[1];
            }

            temp.wrapT = getWrap((cmd[5] >> 2) & 0x2);
            temp.wrapS = getWrap(cmd[6] & 0x2);
        }

        private static bool G_SETCOMBINE(ref TempMaterial temp, byte[] cmd)
        {
            if(bytesToLong(cmd, 0) == 0xFCFFFFFFFFFE793C)
            {
                temp.segOff = 0;
                return true;
            }
            return false;
        }

        private static void G_SETTIMG(ref TempMaterial temp, byte[] cmd, ref string desc)
        {
            temp.segOff = bytesToInt(cmd, 4, 4);
            byte format = (byte)(cmd[1] & 0xF8);
            desc = "Set load format to " + TextureFormats.ConvertFormatToString((byte)(format >> 5), (byte)(format >> 3)) +
                " & address to 0x" + temp.segOff.ToString("X8");
            temp.isPaletteRGBA16 = (format != 0x70);
        }
    }

    class TempMaterial
    {
        public uint id = 0;
        public ushort w = 0, h = 0;
        public uint segOff = 0, color = 0xFFFFFFFF;
        public byte format = 0x10;
        public uint geometryMode = 0x22205;
        public float texScaleX = 1.0f, texScaleY = 1.0f;
        public int wrapS = (int)OpenTK.Graphics.OpenGL.All.Repeat,
            wrapT = (int)OpenTK.Graphics.OpenGL.All.Repeat;

        public ushort[] palette = new ushort[256];
        public bool isPaletteRGBA16 = true;
        //public byte paletteTileDescriptor = 1;
        //public ushort numColorsToLoadInPalette = 0;
    }
}
