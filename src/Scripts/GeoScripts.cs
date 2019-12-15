using OpenTK;
using Quad64.src.LevelInfo;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        public static void parse(ref Model3D mdl, ref Level lvl, byte seg, uint off, byte? areaID)
        {
            if (seg == 0)  return;
            ROM rom = ROM.Instance;
            byte[] data = rom.getSegment(seg, areaID);
            bool end = false;
            while (!end)
            {
                byte cmdLen = getCmdLength(data[off]);
                byte[] cmd = rom.getSubArray_safe(data, off, cmdLen);
                string desc = "Unknown command";
                bool alreadyAdded = false;
                /*
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
                }*/

                switch (cmd[0])
                {
                    case 0x00:
                        desc = "Branch geometry layout to address 0x" + bytesToInt(cmd, 4, 4).ToString("X8");
                        addGLSCommandToDump(ref mdl, cmd, seg, off, desc, areaID);
                        alreadyAdded = true;
                        CMD_00(ref mdl, ref lvl, cmd, areaID);
                        break;
                    case 0x01:
                        desc = "End geometry layout";
                        end = true;
                        break;
                    case 0x02:
                        desc = "Branch geometry layout to address 0x" + bytesToInt(cmd, 4, 4).ToString("X8");
                        addGLSCommandToDump(ref mdl, cmd, seg, off, desc, areaID);
                        alreadyAdded = true;
                        CMD_02(ref mdl, ref lvl, cmd, areaID);

                        if (cmd[1] == 0x01)
                        {
                            if (nodeCurrent.parent == null || (nodeCurrent.parent != null && nodeCurrent.parent.callSwitch == false))
                            {
                                // If the next command is not another 0x02 command, or a 0x05 command...
                                if (data[off + cmdLen] != 0x02 && data[off + cmdLen] != 0x05)
                                {
                                    end = true;
                                }
                            }
                        }
                        break;
                    case 0x03:
                        desc = "Return from branch";
                        end = true;
                        break;
                    case 0x04:
                        desc = "Open New Node";
                        CMD_04();
                        break;
                    case 0x05:
                        desc = "Close Node";
                        if (nodeCurrent != rootNode)
                            nodeCurrent = nodeCurrent.parent;
                        break;
                    case 0x08:
                        desc = "Set screen rendering area (" +
                        "center X = " + (short)bytesToInt(cmd, 4, 2) +
                        ", center Y = " + (short)bytesToInt(cmd, 6, 2) +
                        ", Width = " + (short)(bytesToInt(cmd, 8, 2)*2) +
                        ", Height = " + (short)(bytesToInt(cmd, 10, 2)*2) + ")";
                        break;
                    case 0x0A:
                        desc = "Set camera frustum (" +
                        "FOV = " + (short)bytesToInt(cmd, 2, 2) +
                        ", Near = " + (short)bytesToInt(cmd, 4, 2) +
                        ", Far = " + (short)bytesToInt(cmd, 6, 2) + ")";
                        break;
                    case 0x0B:
                        desc = "Start geometry layout";
                        break;
                    case 0x0C:
                        if(cmd[1] == 0x00)
                            desc = "Disable Z-Buffer";
                        else
                            desc = "Enable Z-Buffer";
                        break;
                    case 0x0D:
                        desc = "Set render range from camera (min = " + 
                        (short)bytesToInt(cmd, 4, 2) + ", max = " +
                        (short)bytesToInt(cmd, 6, 2) + ")";
                        break;
                    case 0x0E:
                        desc = "Switch case with following display lists using ASM function 0x" + bytesToInt(cmd, 4, 4).ToString("X8");
                        //rom.printArray(cmd, cmdLen);
                        CMD_0E(ref mdl, ref lvl, cmd);
                        break;
                    case 0x10:
                        desc = "Translate and rotate";
                        //CMD_10(ref mdl, ref lvl, cmd);
                        break;
                    case 0x11:
                        //rom.printArray(cmd, cmdLen);
                        desc = "Translate Node";
                        CMD_11(ref mdl, ref lvl, cmd);
                        break;
                    case 0x13:
                        desc = "Load display list 0x" + bytesToInt(cmd, 8, 4).ToString("X8") +
                            " into layer "+cmd[1]+ " and offset position by ("+
                            (short)bytesToInt(cmd, 2, 2) +
                            "," + (short)bytesToInt(cmd, 2, 2) +
                            "," + (short)bytesToInt(cmd, 2, 2) + 
                            ")";
                        //rom.printArray(cmd, cmdLen);
                        CMD_13(ref mdl, ref lvl, cmd, areaID);
                        break;
                    case 0x14:
                        desc = "Billboard Model";
                        //CMD_10(ref mdl, ref lvl, cmd);
                        break;
                    case 0x15:
                        desc = "Load display list 0x" + bytesToInt(cmd, 4, 4).ToString("X8") +
                            " into layer " + cmd[1];
                        CMD_15(ref mdl, ref lvl, cmd, areaID);
                       // rom.printArray(cmd, cmdLen);
                        break;
                    case 0x16:
                        desc = "Start geometry layout with a shadow. (type = " + cmd[3] + 
                            ", solidity = " + cmd[5] + ", scale = " + 
                            bytesToInt(cmd, 6, 2) + ")";
                        //CMD_10(ref mdl, ref lvl, cmd);
                        break;
                    case 0x17:
                        desc = "Setup display lists for level objects";
                        break;
                    case 0x18:
                        desc = "Create display list(s) from the ASM function 0x" + bytesToInt(cmd, 4, 4).ToString("X8")
                            + " (a0 = " + bytesToInt(cmd, 2, 2) + ")";
                        CMD_18(ref mdl, ref lvl, cmd);
                        // rom.printArray(cmd, cmdLen);
                        break;
                    case 0x19:
                        if (bytesToInt(cmd, 4, 4) == 0x00000000)
                        {
                            desc = "Draw solid color background. Color = (";
                            ushort color = (ushort)bytesToInt(cmd, 2, 2);
                            desc += (((color >> 11) & 0x1F) * 8) + ","
                                + (((color >> 6) & 0x1F) * 8) + ","
                                + (((color >> 1) & 0x1F) * 8) + ")";
                        }
                        else
                        {
                            desc = "Draw background image. bgID = " + bytesToInt(cmd, 2, 2) + 
                                ", calls ASM function 0x" + bytesToInt(cmd, 4, 4).ToString("X8");
                        }
                        CMD_19(ref mdl, ref lvl, cmd, rom.decodeSegmentAddress(seg, off, areaID));
                        // rom.printArray(cmd, cmdLen);
                        break;
                    case 0x1D:
                        desc = "Scale following node by " + ((bytesToInt(cmd, 4, 4) / 65536.0f) * 100.0f) + "%";
                        CMD_1D(ref mdl, cmd);
                        break;
                    case 0x1A:
                    case 0x1E:
                    case 0x1F:
                        desc = "Do nothing";
                        break;
                    case 0x20:
                        desc = "Start geometry layout with render area of " + bytesToInt(cmd, 2, 2);
                        break;
                }
                if (!alreadyAdded)
                    addGLSCommandToDump(ref mdl, cmd, seg, off, desc, areaID);
                off += cmdLen;
                /*
                if (nodeCurrent.isSwitch)
                    nodeCurrent.switchPos++;
                    */
            }
        }

        private static void addGLSCommandToDump(ref Model3D mdl, byte[] cmd, byte seg, uint offset, string description, byte? areaID)
        {
            ScriptDumpCommandInfo info = new ScriptDumpCommandInfo();
            info.data = cmd;
            info.description = description;
            info.segAddress = (uint)(seg << 24) | offset;
            info.romAddress = ROM.Instance.decodeSegmentAddress_safe(seg, offset, areaID);
            mdl.GeoLayoutCommands_ForDump.Add(info);
        }
        
        private static void CMD_00(ref Model3D mdl, ref Level lvl, byte[] cmd, byte? areaID)
        {
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);
            parse(ref mdl, ref lvl, seg, off, areaID);
        }

        private static void CMD_02(ref Model3D mdl, ref Level lvl, byte[] cmd, byte? areaID)
        {
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);
            parse(ref mdl, ref lvl, seg, off, areaID);
        }


        private static void CMD_04()
        {
            GeoScriptNode newNode = new GeoScriptNode();
            newNode.ID = nodeCurrent.ID + 1;
            newNode.parent = nodeCurrent;
            /*
            if (nodeCurrent.callSwitch)
            {
                newNode.switchPos = 0;
                newNode.switchCount = nodeCurrent.switchCount;
                newNode.switchFunc = nodeCurrent.switchFunc;
                //newNode.isSwitch = true;
            }
            */
            nodeCurrent = newNode;
        }

        private static void CMD_0E(ref Model3D mdl, ref Level lvl, byte[] cmd)
        {
            //nodeCurrent.switchFunc = bytesToInt(cmd, 4, 4);
            // Special Ignore cases
            //if (nodeCurrent.switchFunc == 0x8029DBD4) return;
            //nodeCurrent.switchCount = cmd[3];
            nodeCurrent.callSwitch = true;
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
        private static void CMD_13(ref Model3D mdl, ref Level lvl, byte[] cmd, byte? areaID)
        {
            byte drawLayer = cmd[1];
            short x = (short)bytesToInt(cmd, 2, 2);
            short y = (short)bytesToInt(cmd, 4, 2);
            short z = (short)bytesToInt(cmd, 6, 2);
            uint seg_offset = bytesToInt(cmd, 8, 4);
            byte seg = (byte)(seg_offset >> 24);
            if (seg > 0x20)
                return;
            uint off = seg_offset & 0xFFFFFF;
            mdl.builder.Offset = new OpenTK.Vector3(x, y, z) + getTotalOffset();
            // Don't bother processing duplicate display lists.
            if (seg_offset != 0)
            {
                if (!mdl.hasGeoDisplayList(off))
                {
                    Fast3DScripts.parse(ref mdl, ref lvl, seg, off, areaID, 0);
                }
                lvl.temp_bgInfo.usesFog = mdl.builder.UsesFog;
                lvl.temp_bgInfo.fogColor = mdl.builder.FogColor;
                lvl.temp_bgInfo.fogColor_romLocation = mdl.builder.FogColor_romLocation;
            }
            else
            {
                nodeCurrent.offset += new OpenTK.Vector3(x, y, z);
            }
        }

        private static void CMD_15(ref Model3D mdl, ref Level lvl, byte[] cmd, byte? areaID)
        {
           // if (bytesToInt(cmd, 4, 4) != 0x07006D70) return;
            byte drawLayer = cmd[1];
            byte seg = cmd[4];
            if (seg > 0x20)
                return;
            uint off = bytesToInt(cmd, 5, 3);
            mdl.builder.Offset = getTotalOffset();
            // Don't bother processing duplicate display lists.
            if (!mdl.hasGeoDisplayList(off))
            {
                Fast3DScripts.parse(ref mdl, ref lvl, seg, off, areaID, 0);
            }

            lvl.temp_bgInfo.usesFog = mdl.builder.UsesFog;
            lvl.temp_bgInfo.fogColor = mdl.builder.FogColor;
            lvl.temp_bgInfo.fogColor_romLocation = mdl.builder.FogColor_romLocation;
        }
        
        private static void CMD_18(ref Model3D mdl, ref Level lvl, byte[] cmd)
        {
            ROM rom = ROM.Instance;
            uint asmAddress = bytesToInt(cmd, 4, 4);
            switch (rom.Region)
            {
                case ROM_Region.NORTH_AMERICA:
                    if (asmAddress == Globals.endCake_drawFunc_NA)
                    {
                        lvl.temp_bgInfo.isEndCakeImage = true;
                    }
                    break;
            }
        }

        private static void CMD_19(ref Model3D mdl, ref Level lvl, byte[] cmd, uint romOffset)
        {
            lvl.temp_bgInfo.id_or_color = (ushort)bytesToInt(cmd, 2, 2);
            lvl.temp_bgInfo.address = bytesToInt(cmd, 4, 4);
            lvl.temp_bgInfo.isEndCakeImage = false;
            lvl.temp_bgInfo.romLocation = romOffset;
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
                case 0x00:
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
                case 0x1E:
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
