using OpenTK;
using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;

namespace Quad64.src.Scripts
{
    class GeoScriptNode
    {
        public int ID = 0;
        public GeoScriptNode parent = null;
        public Vector3 offset = Vector3.Zero;
        public bool callSwitch = false, isSwitch = false;
        public uint switchFunc = 0, switchCount = 0, switchPos = 0;
    }

    class GeoScripts
    {
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
        
        private static GeoScriptNode rootNode;
        private static GeoScriptNode nodeCurrent;

        private static Vector3 getTotalOffset()
        {
            Vector3 newOffset = Vector3.Zero;
            GeoScriptNode n = nodeCurrent;
            while (n.parent != null)
            {
                newOffset += n.offset;
                n = n.parent;
            }
            return newOffset;
        }

        public static void resetNodes()
        {
            rootNode = new GeoScriptNode();
            nodeCurrent = rootNode;
        }

        public static void parse(ref Model3D mdl, ref Level lvl, byte seg, uint off)
        {
            if (seg == 0)  return;
            ROM rom = ROM.Instance;
            byte[] data = rom.getSegment(seg);
            bool end = false;
            while (!end)
            {
                byte cmdLen = getCmdLength(data[off]);
                byte[] cmd = rom.getSubArray(data, off, cmdLen);
                //rom.printArray(cmd, cmdLen);
                if (cmd[0] != 0x05 && nodeCurrent.isSwitch && nodeCurrent.switchPos != 1)
                {
                    if (nodeCurrent.switchFunc == 0x8029DB48)
                    {
                        //rom.printArray(cmd, cmdLen);
                        //Console.WriteLine(nodeCurrent.switchPos);
                    }
                    nodeCurrent.switchPos++;
                    off += cmdLen;
                    continue;
                }

                switch (cmd[0])
                {
                    case 0x00:
                    case 0x01:
                        end = true;
                        break;
                    case 0x02:
                        CMD_02(ref mdl, ref lvl, cmd);
                        break;
                    case 0x03:
                        end = true;
                        break;
                    case 0x04:
                        CMD_04();
                        break;
                    case 0x05:
                        if(nodeCurrent != rootNode)
                            nodeCurrent = nodeCurrent.parent;
                        break;
                    case 0x0E:
                        //rom.printArray(cmd, cmdLen);
                        CMD_0E(ref mdl, ref lvl, cmd);
                        break;
                    case 0x10:
                        //CMD_10(ref mdl, ref lvl, cmd);
                        break;
                    case 0x11:
                        //rom.printArray(cmd, cmdLen);
                        CMD_11(ref mdl, ref lvl, cmd);
                        break;
                    case 0x13:
                        //rom.printArray(cmd, cmdLen);
                        CMD_13(ref mdl, ref lvl, cmd);
                        break;
                    case 0x15:
                        CMD_15(ref mdl, ref lvl, cmd);
                       // rom.printArray(cmd, cmdLen);
                        break;
                    case 0x1D:
                        CMD_1D(ref mdl, cmd);
                        break;
                }
                off += cmdLen;
                if (nodeCurrent.isSwitch)
                    nodeCurrent.switchPos++;
            }
        }

        private static void CMD_02(ref Model3D mdl, ref Level lvl, byte[] cmd)
        {
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);
            parse(ref mdl, ref lvl, seg, off);
        }


        private static void CMD_04()
        {
            GeoScriptNode newNode = new GeoScriptNode();
            newNode.ID = nodeCurrent.ID + 1;
            newNode.parent = nodeCurrent;
            if (nodeCurrent.callSwitch)
            {
                newNode.switchPos = 0;
                newNode.switchCount = nodeCurrent.switchCount;
                newNode.switchFunc = nodeCurrent.switchFunc;
                newNode.isSwitch = true;
            }
            nodeCurrent = newNode;
        }

        private static void CMD_0E(ref Model3D mdl, ref Level lvl, byte[] cmd)
        {
            nodeCurrent.switchFunc = bytesToInt(cmd, 4, 4);
            // Special Ignore cases
            //if (nodeCurrent.switchFunc == 0x8029DBD4) return;
            nodeCurrent.switchCount = cmd[3];
            //nodeCurrent.callSwitch = true;
        }

        private static void CMD_10(ref Model3D mdl, ref Level lvl, byte[] cmd)
        {
            short x = (short)bytesToInt(cmd, 4, 2);
            short y = (short)bytesToInt(cmd, 6, 2);
            short z = (short)bytesToInt(cmd, 8, 2);
            nodeCurrent.offset += new Vector3(x, y, z);
        }
        private static void CMD_11(ref Model3D mdl, ref Level lvl, byte[] cmd)
        {
            short x = (short)bytesToInt(cmd, 2, 2);
            short y = (short)bytesToInt(cmd, 4, 2);
            short z = (short)bytesToInt(cmd, 6, 2);
            //mdl.builder.GeoLayoutOffset += new OpenTK.Vector3(x, y, z);
        }
        private static void CMD_13(ref Model3D mdl, ref Level lvl, byte[] cmd)
        {
            byte drawLayer = cmd[1];
            short x = (short)bytesToInt(cmd, 2, 2);
            short y = (short)bytesToInt(cmd, 4, 2);
            short z = (short)bytesToInt(cmd, 6, 2);
            uint seg_offset = bytesToInt(cmd, 8, 4);
            byte seg = (byte)(seg_offset >> 24);
            uint off = seg_offset & 0xFFFFFF;
            mdl.builder.Offset = new OpenTK.Vector3(x, y, z) + getTotalOffset();
            // Don't bother processing duplicate display lists.
            if (seg_offset != 0)
            {
                if (!mdl.hasGeoDisplayList(off))
                    Fast3DScripts.parse(ref mdl, ref lvl, seg, off);
            }
            else
            {
                nodeCurrent.offset += new OpenTK.Vector3(x, y, z);
            }
        }

        private static void CMD_15(ref Model3D mdl, ref Level lvl, byte[] cmd)
        {
           // if (bytesToInt(cmd, 4, 4) != 0x07006D70) return;
            byte drawLayer = cmd[1];
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);
            mdl.builder.Offset = getTotalOffset();
            // Don't bother processing duplicate display lists.
            if (!mdl.hasGeoDisplayList(off))
            {
                Globals.DEBUG_PDL = bytesToInt(cmd, 4, 4);
                Fast3DScripts.parse(ref mdl, ref lvl, seg, off);
            }
        }
        
        private static void CMD_1D(ref Model3D mdl, byte[] cmd)
        {
            uint scale = bytesToInt(cmd, 4, 4);
            mdl.builder.currentScale = (float)scale / 65536.0f;
        }

        private static byte getCmdLength(byte cmd)
        {
            switch (cmd)
            {
                case 0x02:
                case 0x0D:
                case 0x0E:
                case 0x11:
                case 0x12:
                case 0x14:
                case 0x15:
                case 0x16:
                case 0x18:
                case 0x19:
                case 0x1A:
                case 0x1D:
                    return 0x08;
                case 0x08:
                case 0x0A:
                case 0x13:
                case 0x1C:
                    return 0x0C;
                case 0x1F:
                    return 0x10;
                case 0x0F:
                case 0x10:
                    return 0x14;
                default:
                    return 0x04;
            }
        }
    }
}
