using Quad64.src.JSON;
using Quad64.src.LevelInfo;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Quad64
{

    class Object3D
    {
        public enum FLAGS {
            POSITION_X = 0x1,
            POSITION_Y = 0x2,
            POSITION_Z = 0x4,
            ROTATION_X = 0x8,
            ROTATION_Y = 0x10,
            ROTATION_Z = 0x20,
            ACT1 = 0x40,
            ACT2 = 0x80,
            ACT3 = 0x100,
            ACT4 = 0x200,
            ACT5 = 0x400,
            ACT6 = 0x800,
            ALLACTS = 0x1000,
            BPARAM_1 = 0x2000,
            BPARAM_2 = 0x4000,
            BPARAM_3 = 0x8000,
            BPARAM_4 = 0x10000,
        }

        private const ushort NUM_OF_CATERGORIES = 7;

        bool isBehaviorReadOnly = false;
        bool isModelIDReadOnly = false;
        
        [CustomSortedCategory("Info", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [Description("Name of the object combo")]
        [DisplayName("Combo Name")]
        [ReadOnly(true)]
        public string Title { get; set; }

        [CustomSortedCategory("Info", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [Description("Location inside the ROM file")]
        [DisplayName("Address")]
        [ReadOnly(true)]
        public string Address { get; set; }
        
        private byte modelID = 0;
        [CustomSortedCategory("Model", 2, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [Description("Model identifer used by the object")]
        [DisplayName("Model ID")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public byte ModelID { get { return modelID; } set { modelID = value; } }

        [CustomSortedCategory("Model", 2, NUM_OF_CATERGORIES)]
        [Browsable(false)]
        [Description("Model identifer used by the object")]
        [DisplayName("Model ID")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [ReadOnly(true)]
        public byte ModelID_ReadOnly { get { return modelID; } }

        private short _xPos, _yPos, _zPos;
        [CustomSortedCategory("Position", 3, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("X")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short xPos { get { return _xPos; } set { _xPos = value; } }
        [CustomSortedCategory("Position", 3, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Y")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short yPos { get { return _yPos; } set { _yPos = value; } }
        [CustomSortedCategory("Position", 3, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Z")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short zPos { get { return _zPos; } set { _zPos = value; } }
        
        private short _xRot, _yRot, _zRot;
        [CustomSortedCategory("Rotation", 4, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("RX")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short xRot { get { return _xRot; } set { _xRot = value; } }
        [CustomSortedCategory("Rotation", 4, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("RY")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short yRot { get { return _yRot; } set { _yRot = value; } }
        [CustomSortedCategory("Rotation", 4, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("RZ")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short zRot { get { return _zRot; } set { _zRot = value; } }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Behavior")]
        //[ReadOnly(true)]
        public string Behavior { get; set; }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(false)]
        [DisplayName("Behavior")]
        [ReadOnly(true)]
        public string Behavior_ReadOnly { get; set; }

        // default names
        private const string BP1DNAME = "B.Param 1";
        private const string BP2DNAME = "B.Param 2";
        private const string BP3DNAME = "B.Param 3";
        private const string BP4DNAME = "B.Param 4";

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName(BP1DNAME)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [Description("")]
        public byte BehaviorParameter1 { get; set; }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName(BP2DNAME)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [Description("")]
        public byte BehaviorParameter2 { get; set; }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName(BP3DNAME)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [Description("")]
        public byte BehaviorParameter3 { get; set; }

        [CustomSortedCategory("Behavior", 5, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName(BP4DNAME)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [Description("")]
        public byte BehaviorParameter4 { get; set; }

        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("All Acts")]
        public bool AllActs { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 1")]
        public bool Act1 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 2")]
        public bool Act2 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 3")]
        public bool Act3 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 4")]
        public bool Act4 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 5")]
        public bool Act5 { get; set; }
        [CustomSortedCategory("Acts", 6, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Act 6")]
        public bool Act6 { get; set; }

        private ulong Flags = 0;

        private bool isReadOnly = false;
        [CustomSortedCategory("Misc", NUM_OF_CATERGORIES, NUM_OF_CATERGORIES)]
        [DisplayName("Read-Only")]
        [Browsable(false)]
        public bool IsReadOnly { get { return isReadOnly; } }

        /**************************************************************************************/

        [Browsable(false)]
        public Level level { get; set; }
        private ObjectComboEntry objectComboEntry;
        private ushort presetID;

        public int getROMAddress()
        {
            return int.Parse(Address.Substring(2), NumberStyles.HexNumber);
        }

        public uint getROMUnsignedAddress()
        {
            return uint.Parse(Address.Substring(2), NumberStyles.HexNumber);
        }

        public void setPresetID(ushort ID)
        {
            presetID = ID;
        }

        public byte getActMask()
        {
            byte actMask = 0;
            if (Act1) actMask |= 0x1;
            if (Act2) actMask |= 0x2;
            if (Act3) actMask |= 0x4;
            if (Act4) actMask |= 0x8;
            if (Act5) actMask |= 0x10;
            if (Act6) actMask |= 0x20;
            return actMask;
        }

        public void setBehaviorFromAddress(uint address)
        {
            Behavior = "0x"+address.ToString("X8");
            Behavior_ReadOnly = Behavior;
        }

        public uint getBehaviorAddress()
        {
            return uint.Parse(Behavior.Substring(2), NumberStyles.HexNumber);
        }

        public void updateROMData()
        {
            if (Address.Equals("N/A")) return;
            ROM rom = ROM.Instance;
            uint romAddr = getROMUnsignedAddress();
            if (Globals.list_selected == 0) // Regular Object
            {
                rom.writeByte(romAddr + 2, getActMask());
                rom.writeByte(romAddr + 3, ModelID);
                rom.writeHalfword(romAddr + 4, xPos);
                rom.writeHalfword(romAddr + 6, yPos);
                rom.writeHalfword(romAddr + 8, zPos);
                rom.writeHalfword(romAddr + 10, xRot);
                rom.writeHalfword(romAddr + 12, yRot);
                rom.writeHalfword(romAddr + 14, zRot);
                rom.writeByte(romAddr + 16, BehaviorParameter1);
                rom.writeByte(romAddr + 17, BehaviorParameter2);
                rom.writeByte(romAddr + 18, BehaviorParameter3);
                rom.writeByte(romAddr + 19, BehaviorParameter4);
                rom.writeWord(romAddr + 20, getBehaviorAddress());
            }
            else if (Globals.list_selected == 1) // Macro Object
            {
                //Console.WriteLine("Preset ID = 0x" + presetID.ToString("X"));
                ushort first = (ushort)((((ushort)(yRot / 2.8125f) & 0x7F) << 9) | (presetID & 0x1FF));
                rom.writeHalfword(romAddr, first);
                rom.writeHalfword(romAddr + 2, xPos);
                rom.writeHalfword(romAddr + 4, yPos);
                rom.writeHalfword(romAddr + 6, zPos);
                rom.writeByte(romAddr + 8, BehaviorParameter1);
                rom.writeByte(romAddr + 9, BehaviorParameter2);
            }
            else if (Globals.list_selected == 2) // Special Object
            {
                Console.WriteLine("Special Preset ID = 0x" + presetID.ToString("X"));
                rom.writeHalfword(romAddr, presetID);
                rom.writeHalfword(romAddr + 2, xPos);
                rom.writeHalfword(romAddr + 4, yPos);
                rom.writeHalfword(romAddr + 6, zPos);
                if (!isHidden(FLAGS.ROTATION_Y))
                {
                    rom.writeHalfword(romAddr + 8, (short)(yRot / 1.40625f));
                    if (!isHidden(FLAGS.BPARAM_1))
                    {
                        rom.writeByte(romAddr + 10, BehaviorParameter1);
                        rom.writeByte(romAddr + 11, BehaviorParameter2);
                    }
                }
            }
        }

        public void MakeBehaviorReadOnly(bool isReadOnly)
        {
            isBehaviorReadOnly = isReadOnly;
        }

        public void MakeModelIDReadOnly(bool isReadOnly)
        {
            isModelIDReadOnly = isReadOnly;
        }

        public void MakeReadOnly()
        {
            TypeDescriptor.AddAttributes(this, new Attribute[] { new ReadOnlyAttribute(true) });
            isReadOnly = true;
        }

        private void HideShowProperty(string property, bool show)
        {
            PropertyDescriptor descriptor =
                TypeDescriptor.GetProperties(this.GetType())[property];
            BrowsableAttribute attrib =
              (BrowsableAttribute)descriptor.Attributes[typeof(BrowsableAttribute)];
            FieldInfo isBrow =
              attrib.GetType().GetField("browsable", BindingFlags.NonPublic | BindingFlags.Instance);

            if (isBrow != null)
                isBrow.SetValue(attrib, show);
        }
        
        private bool isHidden(FLAGS flag)
        {
            return (Flags & (ulong)flag) == (ulong)flag;
        }

        private void updateProperty(string property, FLAGS flag)
        {
            if (isHidden(flag))
                HideShowProperty(property, false);
            else
                HideShowProperty(property, true);
        }

        private void updateReadOnlyProperty(string property, bool isReadOnly)
        {
            if (isReadOnly)
            {
                HideShowProperty(property, false);
                HideShowProperty(property+"_ReadOnly", true);
            }
            else
            {
                HideShowProperty(property, true);
                HideShowProperty(property+"_ReadOnly", false);
            }
        }

        private void ChangePropertyName(string property, string name)
        {
            PropertyDescriptor descriptor =
                TypeDescriptor.GetProperties(this.GetType())[property];
            DisplayNameAttribute attrib =
              (DisplayNameAttribute)descriptor.Attributes[typeof(DisplayNameAttribute)];
            FieldInfo isBrow =
              attrib.GetType().GetField("_displayName", BindingFlags.NonPublic | BindingFlags.Instance);
            
            if (isBrow != null)
                isBrow.SetValue(attrib, name);
        }

        private void ChangePropertyDescription(string property, string description)
        {
            PropertyDescriptor descriptor =
                TypeDescriptor.GetProperties(this.GetType())[property];
            DescriptionAttribute attrib =
              (DescriptionAttribute)descriptor.Attributes[typeof(DescriptionAttribute)];
            FieldInfo isBrow =
              attrib.GetType().GetField("description", BindingFlags.NonPublic | BindingFlags.Instance);
            if (isBrow != null)
                isBrow.SetValue(attrib, description);
        }

        private void UpdatePropertyName(string property, string oce_name, string otherName)
        {
            if (oce_name != null && !oce_name.Equals(""))
                ChangePropertyName(property, oce_name);
            else
                ChangePropertyName(property, otherName);
        }

        private void UpdatePropertyDescription(string property, string oce_desc)
        {
            if (oce_desc != null && !oce_desc.Equals(""))
                ChangePropertyDescription(property, oce_desc);
            else
                ChangePropertyDescription(property, "");
        }

        private void UpdateObjectComboNames()
        {
            if (objectComboEntry != null)
            {
                UpdatePropertyName("BehaviorParameter1", objectComboEntry.BP1_NAME, BP1DNAME);
                UpdatePropertyName("BehaviorParameter2", objectComboEntry.BP2_NAME, BP2DNAME);
                UpdatePropertyName("BehaviorParameter3", objectComboEntry.BP3_NAME, BP3DNAME);
                UpdatePropertyName("BehaviorParameter4", objectComboEntry.BP4_NAME, BP4DNAME);
                UpdatePropertyDescription("BehaviorParameter1", objectComboEntry.BP1_DESCRIPTION);
                UpdatePropertyDescription("BehaviorParameter2", objectComboEntry.BP2_DESCRIPTION);
                UpdatePropertyDescription("BehaviorParameter3", objectComboEntry.BP3_DESCRIPTION);
                UpdatePropertyDescription("BehaviorParameter4", objectComboEntry.BP4_DESCRIPTION);
            }
            else
            {
                ChangePropertyName("BehaviorParameter1", BP1DNAME);
                ChangePropertyName("BehaviorParameter2", BP2DNAME);
                ChangePropertyName("BehaviorParameter3", BP3DNAME);
                ChangePropertyName("BehaviorParameter4", BP4DNAME);
                ChangePropertyDescription("BehaviorParameter1", "");
                ChangePropertyDescription("BehaviorParameter2", "");
                ChangePropertyDescription("BehaviorParameter3", "");
                ChangePropertyDescription("BehaviorParameter4", "");
            }
        }

        public void UpdateProperties()
        {
            updateProperty("xRot", FLAGS.ROTATION_X);
            updateProperty("yRot", FLAGS.ROTATION_Y);
            updateProperty("zRot", FLAGS.ROTATION_Z);
            updateProperty("Act1", FLAGS.ACT1);
            updateProperty("Act2", FLAGS.ACT2);
            updateProperty("Act3", FLAGS.ACT3);
            updateProperty("Act4", FLAGS.ACT4);
            updateProperty("Act5", FLAGS.ACT5);
            updateProperty("Act6", FLAGS.ACT6);
            updateProperty("AllActs", FLAGS.ALLACTS);
            updateProperty("BehaviorParameter1", FLAGS.BPARAM_1);
            updateProperty("BehaviorParameter2", FLAGS.BPARAM_2);
            updateProperty("BehaviorParameter3", FLAGS.BPARAM_3);
            updateProperty("BehaviorParameter4", FLAGS.BPARAM_4);
            updateReadOnlyProperty("Behavior", isBehaviorReadOnly);
            updateReadOnlyProperty("ModelID", isModelIDReadOnly);
            UpdateObjectComboNames();
        }

        public void SetBehaviorParametersToZero()
        {
            BehaviorParameter1 = 0;
            BehaviorParameter2 = 0;
            BehaviorParameter3 = 0;
            BehaviorParameter4 = 0;
        }

        public void DontShowActs()
        {
            Flags |= (ulong)(
                FLAGS.ACT1 | FLAGS.ACT2 | FLAGS.ACT3 | 
                FLAGS.ACT4 | FLAGS.ACT5 | FLAGS.ACT6 |
                FLAGS.ALLACTS);
        }

        public void ShowHideActs(bool hide)
        {
            if (hide)
                Flags |= (ulong)(FLAGS.ACT1 | FLAGS.ACT2 | 
                    FLAGS.ACT3 | FLAGS.ACT4 | FLAGS.ACT5 | FLAGS.ACT6);
            else
                Flags &= ~(ulong)(FLAGS.ACT1 | FLAGS.ACT2 |
                    FLAGS.ACT3 | FLAGS.ACT4 | FLAGS.ACT5 | FLAGS.ACT6);
            UpdateProperties();
        }

        public void HideProperty(FLAGS flag)
        {
            Flags |= (ulong)flag;
        }

        public string getObjectComboName()
        {
            uint behaviorAddr = getBehaviorAddress();
            for (int i = 0; i < Globals.objectComboEntries.Count; i++)
            {
                ObjectComboEntry entry = Globals.objectComboEntries[i];
                uint modelSegmentAddress = 0;
                if (level.ModelIDs.ContainsKey(ModelID))
                    modelSegmentAddress = level.ModelIDs[ModelID].GeoDataSegAddress;
                if (entry.ModelID == ModelID && entry.Behavior == behaviorAddr && entry.ModelSegmentAddress == modelSegmentAddress)
                {
                    objectComboEntry = entry;
                    return entry.Name;
                }
            }
            return "Unknown Combo";
        }

        public bool isPropertyShown(FLAGS flag)
        {
            return !isHidden(flag);
        }
    }
}
