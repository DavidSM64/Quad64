using OpenTK.Graphics.OpenGL;
using Quad64.src.JSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Quad64
{
    class ScriptDumpCommandInfo
    {
        public byte[] data;
        public uint romAddress, segAddress;
        public string description;
    }

    class Globals
    {
        // Render Options
        public static bool doWireframe = false;
        public static bool drawObjectModels = true;
        public static bool renderCollisionMap = false;
        public static bool doBackfaceCulling = false;
        public static int FOV = 60; // field of view (in degrees)

        // Editor Options
        public static bool autoLoadROMOnStartup = false;
        public static string pathToAutoLoadROM = "";
        public static bool useHexadecimal = false;
        public static bool useSignedHex = false;

        // Advanced Options
        public static string pathToEmulator = "";
        public static bool autoSaveWhenClickEmulator = false;

        // Speed multipliers
        public static float camSpeedMultiplier = 1.0f;
        public static float objSpeedMultiplier = 1.0f;
        
        // TreeView selection
        public static int list_selected = -1;
        public static int item_selected = -1;
        public static bool isMultiSelected = false;
        public static bool isMultiSelectedFromMultipleLists = false;
        public static bool isMultiSelectedFromSpecialObjects = false;
        public static bool isMultiSelectedFromBothNormalWarpsAndInstantWarps = false;
        public static List<List<int>> multi_selected_nodes = new List<List<int>>();

        // Keeps track if the user needs to save their changes.
        public static bool needToSave = false;
        
        // For the bounding boxes in the area
        public static Color ObjectColor = Color.Red;
        public static Color MacroObjectColor = Color.Blue;
        public static Color SpecialObjectColor = Color.Lime;
        public static Color SelectedObjectColor = Color.Yellow;

        public static bool DEBUG_PLG = false; // parsing level geometry flag
        public static uint DEBUG_PDL = 0x00000000; // parsing display list value
        public static bool DEBUG_PARSING_LEVEL_AREA = false;
        public static bool DEBUG_PARSING_DL = false;

        // Locations in the Vanilla North American ROM (default)
        public static uint[] seg02_location = { 0x108A40, 0x114750 };
        public static uint[] seg15_location = { 0x2ABCA0, 0x2AC6B0 };
        public static uint macro_preset_table = 0xEC7E0;
        public static uint special_preset_table = 0xED350;

        // RAM to ROM conversion for assembly functions in each region
        public static uint RAMtoROM_JP = 0x80245000; // Japan
        public static uint RAMtoROM_NA = 0x80245000; // USA 
        public static uint RAMtoROM_EU = 0x80240800; // Europe
        public static uint RAMtoROM_JS = 0x80248000; // Japan (Shindou)
        public static uint RAMtoROM_IQ = 0x80248000; // Chinese (IQue)

        // Function location that initalizes segment 0x02 for each region
        public static uint seg02_init_JP = 0x80248934; // Japan
        public static uint seg02_init_NA = 0x80248964; // USA 
        public static uint seg02_init_EU = 0x80244100; // Europe
        public static uint seg02_init_JS = 0x8024B958; // Japan (Shindou)
        public static uint seg02_init_IQ = 0x8024B968; // Chinese (IQue)
        

        // Function that draws skybox image:
        public static uint skybox_drawFunc_NA = 0x802763D4;

        public static uint getSkyboxDrawFunction()
        {
            switch (ROM.Instance.Region)
            {
                default:
                    return skybox_drawFunc_NA;
            }
        }
        
        // Function that draws the ending cake image:
        public static uint endCake_drawFunc_NA = 0x802D28CC;

        public static List<ObjectComboEntry> objectComboEntries = new List<ObjectComboEntry>();
        public static List<BehaviorNameEntry> behaviorNameEntries = new List<BehaviorNameEntry>();

        public static BehaviorNameEntry getBehaviorNameEntryFromSegAddress(uint address)
        {
            foreach (BehaviorNameEntry entry in behaviorNameEntries)
            {
                if (entry.Behavior == address)
                    return entry;
            }
            
            BehaviorNameEntry new_bne = new BehaviorNameEntry("Undefined Behavior", address);
            behaviorNameEntries.Add(new_bne);
            
            return behaviorNameEntries[behaviorNameEntries.Count - 1];
        }

        public static string getDefaultObjectComboPath()
        {
           // Console.WriteLine("ROM.Instance.Region = " + ROM.Instance.Region.ToString());
            switch (ROM.Instance.Region)
            {
                default:
                case ROM_Region.NORTH_AMERICA:
                    return "./data/ObjectCombos_NA.json";
                case ROM_Region.EUROPE:
                    return "./data/ObjectCombos_EU.json";
                case ROM_Region.JAPAN:
                    return "./data/ObjectCombos_JP.json";
                case ROM_Region.JAPAN_SHINDOU:
                    return "./data/ObjectCombos_JS.json";
                case ROM_Region.CHINESE_IQUE:
                    return "./data/ObjectCombos_IQ.json";
            }
        }

        public static string getDefaultOtherTexturesPath()
        {
            // Console.WriteLine("ROM.Instance.Region = " + ROM.Instance.Region.ToString());
            switch (ROM.Instance.Region)
            {
                case ROM_Region.NORTH_AMERICA:
                    return "./data/OtherTextures_NA.json";
                default:
                    return "";
            }
        }

        public static string getDefaultBehaviorNamesPath()
        {
            // Console.WriteLine("ROM.Instance.Region = " + ROM.Instance.Region.ToString());
            switch (ROM.Instance.Region)
            {
                default:
                //case ROM_Region.NORTH_AMERICA:
                    return "./data/BehaviorNames.json";
                /*case ROM_Region.EUROPE:
                    return "./data/ObjectCombos_EU.json";
                case ROM_Region.JAPAN:
                    return "./data/ObjectCombos_JP.json";
                case ROM_Region.JAPAN_SHINDOU:
                    return "./data/ObjectCombos_JS.json";*/
            }
        }

        public static void insertNewEntry(ObjectComboEntry newEntry)
        {
            for(int i = 0; i < objectComboEntries.Count; i++)
            {
                if (newEntry.ModelID == objectComboEntries[i].ModelID &&
                    newEntry.ModelSegmentAddress == objectComboEntries[i].ModelSegmentAddress &&
                    newEntry.Behavior == objectComboEntries[i].Behavior)
                {
                    objectComboEntries[i].Name = newEntry.Name; // Update name if it already exists
                    return;
                }
            }
            // Add new entry if it doesn't exist
            objectComboEntries.Add(newEntry);
        }
    }
}
