using OpenTK;
using Quad64.Scripts;
using Quad64.src.JSON;
using Quad64.src.Scripts;
using Quad64.src.Viewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Quad64.src.LevelInfo
{

    class AreaBackgroundInfo
    {
        public uint address = 0;
        public ushort id_or_color = 0;
        public bool isEndCakeImage = false;
        public uint romLocation = 0;
        public bool usesFog = false;
        public Color fogColor = Color.White;
        public List<uint> fogColor_romLocation = new List<uint>();
    }

    class Area
    {
        private Level parent;
        private ushort areaID;
        public ushort AreaID { get { return areaID; } }
        private uint geoLayoutPointer;
        public uint GeometryLayoutPointer { get { return geoLayoutPointer; } }
        public AreaBackgroundInfo bgInfo = new AreaBackgroundInfo();

        public Model3D AreaModel = new Model3D();
        public CollisionMap collision = new CollisionMap();

        public List<Object3D> Objects = new List<Object3D>();
        public List<Object3D> MacroObjects = new List<Object3D>();
        public List<Object3D> SpecialObjects = new List<Object3D>();
        public List<Warp> Warps = new List<Warp>();
        public List<Warp> PaintingWarps = new List<Warp>();
        public List<WarpInstant> InstantWarps = new List<WarpInstant>();

        public Area(ushort areaID, uint geoLayoutPointer, Level parent)
        {
            this.areaID = areaID;
            this.geoLayoutPointer = geoLayoutPointer;
            this.parent = parent;
        }

        private readonly Vector3 boundOff = new Vector3(25f, 25f, 25f);

        private bool isObjectSelected(int list, int obj)
        {
            if (!Globals.isMultiSelected)
            {
                if (list == Globals.list_selected && obj == Globals.item_selected)
                    return true;
            }
            else
            {
                foreach (int selectedIndex in Globals.multi_selected_nodes[list])
                {
                    if (selectedIndex == obj)
                        return true;
                }
            }
            return false;
        }

        public void drawPicking()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Object3D obj = Objects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRot, obj.yRot, obj.zRot, 1.0f);
                Vector3 position = new Vector3(obj.xPos, obj.yPos, obj.zPos);
                if (obj.ModelID != 0)
                {
                    if (parent.ModelIDs.ContainsKey(obj.ModelID))
                    {
                        Model3D model = parent.ModelIDs[obj.ModelID];
                        BoundingBox.draw_solid(scale, rotation, position,
                            System.Drawing.Color.FromArgb(i%256, i/256, 1), 
                            model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                    }
                }
                else
                {
                    BoundingBox.draw_solid(scale, rotation, position,
                        System.Drawing.Color.FromArgb(i % 256, i / 256, 1),
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
            for (int i = 0; i < MacroObjects.Count; i++)
            {
                Object3D obj = MacroObjects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRot, obj.yRot, obj.zRot, 1.0f);
                Vector3 position = new Vector3(obj.xPos, obj.yPos, obj.zPos);
                if (obj.ModelID != 0)
                {
                    if (parent.ModelIDs.ContainsKey(obj.ModelID))
                    {
                        Model3D model = parent.ModelIDs[obj.ModelID];
                        BoundingBox.draw_solid(scale, rotation, position,
                            System.Drawing.Color.FromArgb(i % 256, i / 256, 2),
                            model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                    }
                }
                else
                {
                    BoundingBox.draw_solid(scale, rotation, position,
                        System.Drawing.Color.FromArgb(i % 256, i / 256, 2),
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
            for (int i = 0; i < SpecialObjects.Count; i++)
            {
                Object3D obj = SpecialObjects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRot, obj.yRot, obj.zRot, 1.0f);
                Vector3 position = new Vector3(obj.xPos, obj.yPos, obj.zPos);
                if (obj.ModelID != 0)
                {
                    if (parent.ModelIDs.ContainsKey(obj.ModelID))
                    {
                        Model3D model = parent.ModelIDs[obj.ModelID];
                        BoundingBox.draw_solid(scale, rotation, position,
                            System.Drawing.Color.FromArgb(i % 256, i / 256, 3),
                            model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                    }
                }
                else
                {
                    BoundingBox.draw_solid(scale, rotation, position,
                        System.Drawing.Color.FromArgb(i % 256, i / 256, 3),
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
        }

        public void drawEverything()
        {
            if(Globals.renderCollisionMap)
                collision.drawCollisionMap(false);
            else
                AreaModel.drawModel(Vector3.One, Quaternion.Identity, Vector3.Zero);

            for (int i = 0; i < Objects.Count; i++)
            {
                Object3D obj = Objects[i];
                Vector3 scale = Vector3.One;
                // Need to slighting increase the model's size, just in-case of overlapping bounding boxes.
                if (isObjectSelected(0, i))
                    scale = new Vector3(1.001f, 1.001f, 1.001f);
                Quaternion rotation = new Quaternion(obj.xRot, obj.yRot, obj.zRot, 1.0f);
                Vector3 position = new Vector3(obj.xPos, obj.yPos, obj.zPos);
                if (obj.ModelID != 0 && parent.ModelIDs.ContainsKey(obj.ModelID))
                {
                    Model3D model = parent.ModelIDs[obj.ModelID];
                    if (Globals.drawObjectModels)
                        model.drawModel(scale, rotation, position);
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(0, i) ? Globals.SelectedObjectColor : Globals.ObjectColor,
                        model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                }
                else
                {
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(0, i) ? Globals.SelectedObjectColor : Globals.ObjectColor,
                        new Vector3(150.0f, 150.0f, 150.0f), 
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
            for (int i = 0; i < MacroObjects.Count; i++)
            {
                Object3D obj = MacroObjects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRot, obj.yRot, obj.zRot, 1.0f);
                Vector3 position = new Vector3(obj.xPos, obj.yPos, obj.zPos);
                if (obj.ModelID != 0 && parent.ModelIDs.ContainsKey(obj.ModelID))
                {
                    Model3D model = parent.ModelIDs[obj.ModelID];
                    if (Globals.drawObjectModels)
                        model.drawModel(scale, rotation, position);
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(1, i) ? Globals.SelectedObjectColor : Globals.MacroObjectColor,
                        model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                }
                else
                {
                    BoundingBox.draw(scale, rotation, position, 
                        isObjectSelected(1, i) ? Globals.SelectedObjectColor : Globals.MacroObjectColor,
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
            for (int i = 0; i < SpecialObjects.Count; i++)
            {
                Object3D obj = SpecialObjects[i];
                Vector3 scale = Vector3.One;
                Quaternion rotation = new Quaternion(obj.xRot, obj.yRot, obj.zRot, 1.0f);
                Vector3 position = new Vector3(obj.xPos, obj.yPos, obj.zPos);
                if (obj.ModelID != 0 && parent.ModelIDs.ContainsKey(obj.ModelID))
                {
                    Model3D model = parent.ModelIDs[obj.ModelID];
                    if (Globals.drawObjectModels)
                        model.drawModel(scale, rotation, position);
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(2, i) ? Globals.SelectedObjectColor : Globals.SpecialObjectColor,
                        model.UpperBoundary + boundOff, model.LowerBoundary - boundOff);
                }
                else
                {
                    BoundingBox.draw(scale, rotation, position,
                        isObjectSelected(2, i) ? Globals.SelectedObjectColor : Globals.SpecialObjectColor,
                        new Vector3(150.0f, 150.0f, 150.0f),
                        new Vector3(-150.0f, -150.0f, -150.0f));
                }
            }
        }
    }

    class Level
    {
        private ushort levelID;
        public ushort LevelID { get { return levelID; } }
        private ushort currentAreaID;
        public ushort CurrentAreaID { get { return currentAreaID; } set { currentAreaID = value; } }
        public List<Area> Areas = new List<Area>();
        public AreaBackgroundInfo temp_bgInfo = new AreaBackgroundInfo();
        public Dictionary<ushort, Model3D> ModelIDs = new Dictionary<ushort, Model3D>();

        public List<ObjectComboEntry> LevelObjectCombos = new List<ObjectComboEntry>();

        public List<PresetMacroEntry> MacroObjectPresets = new List<PresetMacroEntry>();
        public List<PresetMacroEntry> SpecialObjectPresets_8 = new List<PresetMacroEntry>();
        public List<PresetMacroEntry> SpecialObjectPresets_10 = new List<PresetMacroEntry>();
        public List<PresetMacroEntry> SpecialObjectPresets_12 = new List<PresetMacroEntry>();

        public List<ScriptDumpCommandInfo> LevelScriptCommands_ForDump = new List<ScriptDumpCommandInfo>();


        public ObjectComboEntry getObjectComboFromData(byte modelID, uint modelAddress, uint behavior, out int index)
        {
            for (int i = 0; i < LevelObjectCombos.Count; i++)
            {
                ObjectComboEntry oce = LevelObjectCombos[i];
                if (oce.ModelID == modelID && oce.ModelSegmentAddress == modelAddress
                    && oce.Behavior == behavior)
                {
                    index = i;
                    return oce;
                }
            }
            index = -1;
            return null;
        }

        private void AddMacroObjectEntries()
        {
            MacroObjectPresets.Clear();
            ROM rom = ROM.Instance;
            ushort pID = 0x1F;
            for (int i = 0; i < 366; i++)
            {
                uint offset = (uint)(Globals.macro_preset_table + (i * 8));
                byte modelID = rom.readByte(offset + 5);
                uint behavior = rom.readWordUnsigned(offset);
                byte bp1 = rom.readByte(offset + 6);
                byte bp2 = rom.readByte(offset + 7);
                MacroObjectPresets.Add(new PresetMacroEntry(pID, modelID, behavior, bp1, bp2));
                pID++;
            }
        }

        public void AddSpecialObjectPreset_8(ushort presetID, byte modelId, uint behavior)
        {
            SpecialObjectPresets_8.Add(new PresetMacroEntry(presetID, modelId, behavior));
        }

        public void AddSpecialObjectPreset_10(ushort presetID, byte modelId, uint behavior)
        {
            SpecialObjectPresets_10.Add(new PresetMacroEntry(presetID, modelId, behavior));
        }

        public void AddSpecialObjectPreset_12(ushort presetID, byte modelId, uint behavior, byte bp1, byte bp2)
        {
            SpecialObjectPresets_12.Add(new PresetMacroEntry(presetID, modelId, behavior, bp1, bp2));
        }

        public void AddObjectCombos(byte modelId, uint modelSegAddress)
        {
            for (int i = 0; i < Globals.objectComboEntries.Count; i++)
            {
                ObjectComboEntry oce = Globals.objectComboEntries[i];
                if (oce.ModelID == modelId && oce.ModelSegmentAddress == modelSegAddress)
                    LevelObjectCombos.Add(oce);
            }
        }

        public void sortAndAddNoModelEntries()
        {
            for (int i = 0; i < Globals.objectComboEntries.Count; i++)
            {
                ObjectComboEntry oce = Globals.objectComboEntries[i];
                if (oce.ModelID == 0x00)
                    LevelObjectCombos.Add(oce);
            }
            LevelObjectCombos.Sort((x, y) => string.Compare(x.Name, y.Name));
        }

        public void printLevelObjectCombos()
        {
            for (int i = 0; i < LevelObjectCombos.Count; i++)
                Console.WriteLine(LevelObjectCombos[i].ToString());
        }

        public bool hasArea(ushort areaID)
        {
            foreach (Area a in Areas)
                if (a.AreaID == areaID)
                    return true;
            return false;
        }

        public HashSet<ushort> getModelIDsUsedByObjects()
        {
            HashSet<ushort> modelIDs = new HashSet<ushort>();

            foreach (Area area in Areas)
            {
                foreach (Object3D obj in area.Objects)
                    if (obj.ModelID > 0)
                        modelIDs.Add(obj.ModelID);
                foreach (Object3D obj in area.MacroObjects)
                    if (obj.ModelID > 0)
                        modelIDs.Add(obj.ModelID);
                foreach (Object3D obj in area.SpecialObjects)
                    if (obj.ModelID > 0)
                        modelIDs.Add(obj.ModelID);
            }

            return modelIDs;
        }

        public Area getCurrentArea()
        {
            foreach (Area a in Areas)
                if (a.AreaID == currentAreaID)
                    return a;
            return Areas[0]; // return default area
        }
        
        public void setAreaBackgroundInfo(ref Area area)
        {
            area.bgInfo.address = temp_bgInfo.address;
            area.bgInfo.id_or_color = temp_bgInfo.id_or_color;
            area.bgInfo.isEndCakeImage = temp_bgInfo.isEndCakeImage;
            area.bgInfo.romLocation = temp_bgInfo.romLocation;
            area.bgInfo.usesFog = temp_bgInfo.usesFog;
            area.bgInfo.fogColor = temp_bgInfo.fogColor;
            area.bgInfo.fogColor_romLocation = temp_bgInfo.fogColor_romLocation;
        }

        public Level(ushort levelID, ushort startArea) {

            ROM.Instance.clearSegments();
            this.levelID = levelID;
            currentAreaID = startArea;
            LevelObjectCombos.Clear();
            LevelScriptCommands_ForDump.Clear();
            AddMacroObjectEntries();
        }
    }
}
