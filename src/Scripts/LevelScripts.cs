using Quad64.src.LevelInfo;
using Quad64.src.Scripts;
using System;

namespace Quad64.Scripts
{
    class LevelScripts
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

        public static int parse(ref Level lvl, byte seg, uint off)
        {
            ROM rom = ROM.Instance;
            byte[] data = rom.getSegment(seg);
            bool end = false;
            int endCmd = 0;
            while (!end)
            {
                byte cmdLen = data[off + 1];
                byte[] cmd = rom.getSubArray(data, off, cmdLen);
               // rom.printArray(cmd, cmdLen);
                switch (cmd[0])
                {
                    case 0x00:
                    case 0x01:
                        CMD_00(ref lvl, cmd, rom);
                        break;
                    case 0x02:
                        endCmd = 2;
                        end = true;
                        break;
                    case 0x05:
                        endCmd = CMD_05(ref lvl, cmd, seg, off);
                        end = true;
                        break;
                    case 0x06:
                        if (CMD_06(ref lvl, cmd) == 0x02)
                        {
                            end = true;
                            endCmd = 2;
                        }
                        break;
                    case 0x07:
                        end = true;
                        endCmd = 0x07;
                        break;
                    case 0x0C:
                        CMD_0C(ref lvl, cmd);
                        break;
                    case 0x17:
                        CMD_17(ref lvl, cmd, rom);
                        break;
                    case 0x18:
                    case 0x1A:
                        CMD_18(ref lvl, cmd, rom);
                        break;
                    case 0x1F:
                        //Globals.DEBUG_PLG = true;
                        CMD_1F(ref lvl, cmd);
                        break;
                    case 0x21:
                        CMD_21(ref lvl, cmd, rom);
                        break;
                    case 0x22:
                        //Globals.DEBUG_PLG = false;
                        CMD_22(ref lvl, cmd, rom);
                        break;
                    case 0x24:
                        CMD_24(ref lvl, cmd, rom, seg, off);
                        break;
                    case 0x26:
                        CMD_26(ref lvl, cmd, rom, seg, off);
                        break;
                    case 0x27:
                        CMD_27(ref lvl, cmd, rom, seg, off);
                        break;
                    case 0x28:
                        CMD_28(ref lvl, cmd, rom, seg, off);
                        break;
                    case 0x2E:
                        CMD_2E(ref lvl, cmd, rom);
                        break;
                    case 0x39:
                        CMD_39(ref lvl, cmd, rom);
                        break;
                }
                off += cmdLen;
            }
            return endCmd;
        }

        private static void CMD_00(ref Level lvl, byte[] cmd, ROM rom)
        {
            byte seg = cmd[3];
            uint start = bytesToInt(cmd, 4, 4);
            uint end = bytesToInt(cmd, 8, 4);
            uint off = bytesToInt(cmd, 13, 3);
            rom.setSegment(seg, start, end, false);
            if (seg == 0x14) return;
            parse(ref lvl, seg, off);
        }

        private static int CMD_05(ref Level lvl, byte[] cmd, byte currentSeg, uint currentOff)
        {
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);
            if (seg == currentSeg)
            {
                if ((long)off - (long)currentOff == -4) {
                    Console.WriteLine("Infinite loop detected!");
                    return 0x02;
                }
            }
            return parse(ref lvl, seg, off);
        }

        private static int CMD_06(ref Level lvl, byte[] cmd)
        {
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);
            return parse(ref lvl, seg, off);
        }

        private static void CMD_0C(ref Level lvl, byte[] cmd)
        {
            var lvlcheck = bytesToInt(cmd, 4, 4);
            if (lvlcheck == lvl.LevelID)
            {
                byte seg = cmd[8];
                uint off = bytesToInt(cmd, 9, 3);
                parse(ref lvl, seg, off);
            }
        }

        private static void CMD_17(ref Level lvl, byte[] cmd, ROM rom)
        {
            byte seg = cmd[3];
            uint start = bytesToInt(cmd, 4, 4);
            uint end = bytesToInt(cmd, 8, 4);
            rom.setSegment(seg, start, end, false);
        }
        
        private static void CMD_18(ref Level lvl, byte[] cmd, ROM rom)
        {
            byte seg = cmd[3];
            uint start = bytesToInt(cmd, 4, 4);
            uint end = bytesToInt(cmd, 8, 4);
            byte[] MIO0_header = rom.getSubArray(rom.Bytes, start, 0x10);

            if (bytesToInt(MIO0_header, 0, 4) == 0x4D494F30) // Check MIO0 signature
            {
               rom.setSegment(seg, start, end, true);
            }
        }
        
        private static void CMD_1F(ref Level lvl, byte[] cmd)
        {
            byte areaID = cmd[2];
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);

            Area newArea = new Area(areaID, bytesToInt(cmd, 4, 4), lvl);
            GeoScripts.resetNodes();
            newArea.AreaModel.GeoDataSegAddress = bytesToInt(cmd, 4, 4);
            GeoScripts.parse(ref newArea.AreaModel, ref lvl, seg, off);
            lvl.Areas.Add(newArea);
            lvl.CurrentAreaID = areaID;
            newArea.AreaModel.buildBuffers();
           // newArea.AreaModel.outputTextureAtlasToPng("Area_"+areaID+"_TexAtlus.png");
        }

        private static void CMD_21(ref Level lvl, byte[] cmd, ROM rom)
        {
            byte modelID = cmd[3];
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);

            Model3D newModel = new Model3D();
            newModel.GeoDataSegAddress = bytesToInt(cmd, 4, 4);
            lvl.AddObjectCombos(modelID, newModel.GeoDataSegAddress);

            if (rom.getSegment(seg) != null)
                Fast3DScripts.parse(ref newModel, ref lvl, seg, off);

            if (lvl.ModelIDs.ContainsKey(modelID))
                lvl.ModelIDs.Remove(modelID);
            newModel.buildBuffers();
            lvl.ModelIDs.Add(modelID, newModel);
        }

        private static void CMD_22(ref Level lvl, byte[] cmd, ROM rom)
        {
            byte modelID = cmd[3];
            byte seg = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);
            //Console.WriteLine("Size of seg 0x"+seg.ToString("X2")+" = " + rom.getSegment(seg).Length);
            Model3D newModel = new Model3D();
            newModel.GeoDataSegAddress = bytesToInt(cmd, 4, 4);
            lvl.AddObjectCombos(modelID, newModel.GeoDataSegAddress);
            if (rom.getSegment(seg) != null)
            {
                GeoScripts.resetNodes();
                GeoScripts.parse(ref newModel, ref lvl, seg, off);
            }
            if (lvl.ModelIDs.ContainsKey(modelID))
                lvl.ModelIDs.Remove(modelID);
            newModel.buildBuffers();
            lvl.ModelIDs.Add(modelID, newModel);
        }

        private static void CMD_24(ref Level lvl, byte[] cmd, ROM rom, byte seg, uint off)
        {
            Object3D newObj = new Object3D();
            if (rom.isSegmentMIO0(seg))
            {
                newObj.MakeReadOnly();
                newObj.Address = "N/A";
            }
            else
            {
                newObj.Address = "0x" + rom.decodeSegmentAddress(seg, off).ToString("X");
            }
            byte actMask = cmd[2];
            newObj.AllActs = (actMask == 0x1F);
            newObj.Act1 = ((actMask & 0x1) == 0x1);
            newObj.Act2 = ((actMask & 0x2) == 0x2);
            newObj.Act3 = ((actMask & 0x4) == 0x4);
            newObj.Act4 = ((actMask & 0x8) == 0x8);
            newObj.Act5 = ((actMask & 0x10) == 0x10);
            newObj.Act6 = ((actMask & 0x20) == 0x20);
            newObj.ShowHideActs(newObj.AllActs);
            newObj.ModelID = cmd[3];
            newObj.xPos = (short)bytesToInt(cmd, 0x4, 2);
            newObj.yPos = (short)bytesToInt(cmd, 0x6, 2);
            newObj.zPos = (short)bytesToInt(cmd, 0x8, 2);
            newObj.xRot = (short)bytesToInt(cmd, 0xA, 2);
            newObj.yRot = (short)bytesToInt(cmd, 0xC, 2);
            newObj.zRot = (short)bytesToInt(cmd, 0xE, 2);
            newObj.MakeBehaviorReadOnly(false);
            newObj.MakeModelIDReadOnly(false);
            newObj.setBehaviorFromAddress(bytesToInt(cmd, 0x14, 4));
            newObj.BehaviorParameter1 = cmd[0x10];
            newObj.BehaviorParameter2 = cmd[0x11];
            newObj.BehaviorParameter3 = cmd[0x12];
            newObj.BehaviorParameter4 = cmd[0x13];
            newObj.level = lvl;
            lvl.getCurrentArea().Objects.Add(newObj);
        }


        private static void CMD_26(ref Level lvl, byte[] cmd, ROM rom, byte seg, uint off)
        {
            Warp warp = new Warp(false);
            if (rom.isSegmentMIO0(seg))
            {
                warp.MakeReadOnly();
                warp.Address = "N/A";
            }
            else
            {
                warp.Address = "0x" + rom.decodeSegmentAddress(seg, off).ToString("X");
            }
            warp.WarpFrom_ID = cmd[2];
            warp.WarpTo_LevelID = cmd[3];
            warp.WarpTo_AreaID = cmd[4];
            warp.WarpTo_WarpID = cmd[5];
            lvl.getCurrentArea().Warps.Add(warp);
        }

        private static void CMD_27(ref Level lvl, byte[] cmd, ROM rom, byte seg, uint off)
        {
            Warp warp = new Warp(true);
            if (rom.isSegmentMIO0(seg))
            {
                warp.MakeReadOnly();
                warp.Address = "N/A";
            }
            else
            {
                warp.Address = "0x" + rom.decodeSegmentAddress(seg, off).ToString("X");
            }
            warp.WarpFrom_ID = cmd[2];
            warp.WarpTo_LevelID = cmd[3];
            warp.WarpTo_AreaID = cmd[4];
            warp.WarpTo_WarpID = cmd[5];
            lvl.getCurrentArea().PaintingWarps.Add(warp);
        }
        private static void CMD_28(ref Level lvl, byte[] cmd, ROM rom, byte seg, uint off)
        {
            WarpInstant warp = new WarpInstant();
            if (rom.isSegmentMIO0(seg))
            {
                warp.MakeReadOnly();
                warp.Address = "N/A";
            }
            else
            {
                warp.Address = "0x" + rom.decodeSegmentAddress(seg, off).ToString("X");
            }
            warp.TriggerID = cmd[2];
            warp.AreaID = cmd[3];
            warp.TeleX = (short)bytesToInt(cmd, 4, 2);
            warp.TeleY = (short)bytesToInt(cmd, 6, 2);
            warp.TeleZ = (short)bytesToInt(cmd, 8, 2);
            lvl.getCurrentArea().InstantWarps.Add(warp);
        }

        /* Process collision map, Special Objects, and waterboxes. */
        private static void CMD_2E(ref Level lvl, byte[] cmd, ROM rom)
        {
            if (cmd.Length < 8)
                return;

            ushort sub_cmd = 0x40;
            byte segment = cmd[4];
            uint off = bytesToInt(cmd, 5, 3);
            byte[] data = rom.getSegment(segment);
            sub_cmd = (ushort)bytesToInt(data, (int)off, 2);
            
            uint num_verts = (ushort)bytesToInt(data, (int)off + 2, 2);
            off += 4;
            for (int i = 0; i < num_verts; i++)
            {
                short x = (short)bytesToInt(data, (int)off + 0, 2);
                short y = (short)bytesToInt(data, (int)off + 2, 2);
                short z = (short)bytesToInt(data, (int)off + 4, 2);
                lvl.getCurrentArea().collision.AddVertex(new OpenTK.Vector3(x,y,z));
                off += 6;
            }
            //off += (num_verts * 6);

            while (sub_cmd != 0x0041)
            {
                sub_cmd = (ushort)bytesToInt(data, (int)off, 2);
                //Console.WriteLine(sub_cmd.ToString("X8"));
                if (sub_cmd == 0x0041) break;
                //rom.printArraySection(data, (int)off, 4 + (int)collisionLength(sub_cmd));
                lvl.getCurrentArea().collision.NewTriangleList((int)bytesToInt(data, (int)off, 2));
                uint num_tri = (ushort)bytesToInt(data, (int)off + 2, 2);
                uint col_len = collisionLength(sub_cmd);
                off += 4;
                for (int i = 0; i < num_tri; i++)
                {
                    uint a = bytesToInt(data, (int)off + 0, 2);
                    uint b = bytesToInt(data, (int)off + 2, 2);
                    uint c = bytesToInt(data, (int)off + 4, 2);
                    lvl.getCurrentArea().collision.AddTriangle(a,b,c);
                    off += col_len;
                }
            }
            lvl.getCurrentArea().collision.buildCollisionMap();
            off += 2;
            bool end = false;
            while (!end)
            {
                sub_cmd = (ushort)bytesToInt(data, (int)off, 2);
                switch (sub_cmd)
                {
                    case 0x0042:
                        end = true;
                        break;
                    case 0x0043:
                        uint num_obj = (ushort)bytesToInt(data, (int)off + 2, 2);
                        off += 4;
                        for(int i = 0; i < num_obj; i++)
                        {
                            ushort obj_id = (ushort)bytesToInt(data, (int)off, 2);
                            byte[] entry = getSpecialObjectEntry((byte)obj_id);
                            uint obj_len = getSpecialObjectLength(obj_id);
                            Object3D newObj = new Object3D();
                            if (rom.isSegmentMIO0(segment))
                            {
                                newObj.MakeReadOnly();
                                newObj.Address = "N/A";
                            }
                            else
                            {
                                newObj.Address = "0x" + rom.decodeSegmentAddress(segment, off).ToString("X");
                            }
                            newObj.setPresetID(obj_id);
                            newObj.level = lvl;
                            newObj.HideProperty(Object3D.FLAGS.ROTATION_X);
                            newObj.HideProperty(Object3D.FLAGS.ROTATION_Z);
                            newObj.HideProperty(Object3D.FLAGS.BPARAM_3);
                            newObj.HideProperty(Object3D.FLAGS.BPARAM_4);
                            newObj.xPos = (short)bytesToInt(data, (int)off + 2, 2);
                            newObj.yPos = (short)bytesToInt(data, (int)off + 4, 2);
                            newObj.zPos = (short)bytesToInt(data, (int)off + 6, 2);
                            newObj.BehaviorParameter1 = entry[1];
                            newObj.BehaviorParameter2 = entry[2];
                            newObj.MakeBehaviorReadOnly(true);
                            newObj.MakeModelIDReadOnly(true);
                            if (obj_len > 8)
                            {
                                newObj.yRot = (short)(bytesToInt(data, (int)off + 8, 2) * 1.40625);
                                if (obj_len > 10)
                                {
                                    newObj.BehaviorParameter1 = data[off + 10];
                                    newObj.BehaviorParameter2 = data[off + 11];
                                    lvl.AddSpecialObjectPreset_12(obj_id, entry[3], 
                                        bytesToInt(entry, 4, 4), data[off + 10], data[off + 11]);
                                }
                                else
                                {
                                    lvl.AddSpecialObjectPreset_10(obj_id, entry[3], bytesToInt(entry, 4, 4));
                                    newObj.HideProperty(Object3D.FLAGS.BPARAM_1);
                                    newObj.HideProperty(Object3D.FLAGS.BPARAM_2);
                                }
                            }
                            else
                            {
                                lvl.AddSpecialObjectPreset_8(obj_id, entry[3], bytesToInt(entry, 4, 4));
                                newObj.HideProperty(Object3D.FLAGS.BPARAM_1);
                                newObj.HideProperty(Object3D.FLAGS.BPARAM_2);
                                newObj.HideProperty(Object3D.FLAGS.ROTATION_Y);
                            }
                            newObj.ModelID = entry[3];
                            uint behavior = bytesToInt(entry, 4, 4);
                            newObj.setBehaviorFromAddress(behavior);
                            newObj.DontShowActs();
                            if (behavior != 0)
                                lvl.getCurrentArea().SpecialObjects.Add(newObj);
                            off += obj_len;
                        }
                        break;
                    case 0x0044:
                        // Also skipping water boxes. Will come back to it later.
                        uint num_boxes = (ushort)bytesToInt(data, (int)off + 2, 2);
                        off += 4 + (num_boxes * 0xC);
                        break;
                }
            }

        }

        private static byte[] getSpecialObjectEntry(byte presetID)
        {
            ROM rom = ROM.Instance;
            byte[] data = new byte[8];
            uint offset = Globals.special_preset_table;
            byte got = rom.Bytes[offset];
            while(got != 0xFF)
            {
                if (got == presetID)
                {
                    Array.Copy(rom.Bytes, offset, data, 0, 8);
                    break;
                }
                offset += 8;
                got = rom.Bytes[offset];
            }
            
            return data;
        }

        private static uint getSpecialObjectLength(int obj)
        {
            if (obj > 0x64 && obj < 0x79) return 10;
            else if (obj > 0x78 && obj < 0x7E) return 8;
            else if (obj > 0x7D && obj < 0x83) return 10;
            else if (obj > 0x88 && obj < 0x8E) return 10;
            else if (obj > 0x82 && obj < 0x8A) return 12;
            else if (obj == 0x40) return 10;
            else if (obj == 0x64) return 12;
            else if (obj == 0xC0) return 8;
            else if (obj == 0xE0) return 12;
            else if (obj == 0xCD) return 12;
            else if (obj == 0x00) return 10;
            return 8;
        }

        private static uint collisionLength(int type)
        {
            switch (type)
            {
                case 0x0E:
                case 0x24:
                case 0x25:
                case 0x27:
                case 0x2C:
                case 0x2D:
                case 0x40:
                    return 8;
                default:
                    return 6;
            }
        }

        private static void CMD_39(ref Level lvl, byte[] cmd, ROM rom)
        {
            if (cmd.Length < 8)
                return;
            uint pos = bytesToInt(cmd, 4, 4);

            byte[] data = rom.getDataFromSegmentAddress_safe(pos, 10);

            lvl.getCurrentArea().MacroObjects.Clear();
            bool endList = false;
            while (!endList)
            {
                //rom.printArray(data, 10);
                uint id = bytesToInt(data, 0, 2) & 0x1FF;
                if (id == 0 || id == 0x1E) break;
                Object3D newObj = new Object3D();
                if (rom.isSegmentMIO0(cmd[4]))
                {
                    newObj.MakeReadOnly();
                    newObj.Address = "N/A";
                }
                else
                {
                    newObj.Address = "0x" + rom.decodeSegmentAddress(pos).ToString("X");
                }

                uint table_off = (id - 0x1F) * 8;
                byte[] entryData = rom.getSubArray(rom.Bytes, Globals.macro_preset_table + table_off, 8);
                newObj.level = lvl;
                newObj.setBehaviorFromAddress(bytesToInt(entryData, 0, 4));
                newObj.HideProperty(Object3D.FLAGS.ROTATION_X);
                newObj.HideProperty(Object3D.FLAGS.ROTATION_Z);
                newObj.HideProperty(Object3D.FLAGS.BPARAM_3);
                newObj.HideProperty(Object3D.FLAGS.BPARAM_4);
                newObj.ModelID = entryData[5];
                ushort firstAndSecond = (ushort)bytesToInt(data, 0, 2);
                newObj.setPresetID((ushort)(firstAndSecond & 0x1FF));
                newObj.yRot = (short)((firstAndSecond >> 9) * 2.8125);
                newObj.xPos = (short)bytesToInt(data, 2, 2);
                newObj.yPos = (short)bytesToInt(data, 4, 2);
                newObj.zPos = (short)bytesToInt(data, 6, 2);
                newObj.DontShowActs();
                newObj.MakeBehaviorReadOnly(true);
                newObj.MakeModelIDReadOnly(true);
                ushort bp = (ushort)bytesToInt(data, 8, 2);
                if(data[8] != 0)
                    newObj.BehaviorParameter1 = data[8];
                else
                    newObj.BehaviorParameter1 = entryData[6];

                if (data[9] != 0)
                    newObj.BehaviorParameter2 = data[9];
                else
                    newObj.BehaviorParameter2 = entryData[7];
                
                lvl.getCurrentArea().MacroObjects.Add(newObj);
                pos += 10;
                data = rom.getDataFromSegmentAddress_safe(pos, 10);
            }
            //uint end = bytesToInt(cmd, 8, 4);
            //rom.setSegment(seg, start, end, false);
        }
    }
}
