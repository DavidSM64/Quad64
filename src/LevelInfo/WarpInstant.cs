using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Quad64.src.LevelInfo
{
    class WarpInstant
    {
        
        private const ushort NUM_OF_CATERGORIES = 2;

        private byte triggerID;
        [CustomSortedCategory("Instant Warp", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Trigger ID")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public byte TriggerID { get { return triggerID; } set { triggerID = value; } }

        private byte areaID;
        [CustomSortedCategory("Instant Warp", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("To Area")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public byte AreaID { get { return areaID; } set { areaID = value; } }

        private short teleX;
        [CustomSortedCategory("Instant Warp", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Teleport X")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short TeleX { get { return teleX; } set { teleX = value; } }

        private short teleY;
        [CustomSortedCategory("Instant Warp", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Teleport Y")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short TeleY { get { return teleY; } set { teleY = value; } }

        private short teleZ;
        [CustomSortedCategory("Instant Warp", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("Teleport Z")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public short TeleZ { get { return teleZ; } set { teleZ = value; } }

        [CustomSortedCategory("Info", 2, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [Description("Location inside the ROM file")]
        [DisplayName("Address")]
        [ReadOnly(true)]
        public string Address { get; set; }
        
        public void MakeReadOnly()
        {
            TypeDescriptor.AddAttributes(this, new Attribute[] { new ReadOnlyAttribute(true) });
        }

        public int getROMAddress()
        {
            return int.Parse(Address.Substring(2), NumberStyles.HexNumber);
        }

        public uint getROMUnsignedAddress()
        {
            return uint.Parse(Address.Substring(2), NumberStyles.HexNumber);
        }

        private void HideShowProperty(string property, bool show)
        {
            PropertyDescriptor descriptor =
                TypeDescriptor.GetProperties(this.GetType())[property];
            BrowsableAttribute attrib =
              (BrowsableAttribute)descriptor.Attributes[typeof(BrowsableAttribute)];
            FieldInfo isBrow =
              attrib.GetType().GetField("browsable", BindingFlags.NonPublic | BindingFlags.Instance);
            isBrow.SetValue(attrib, show);
        }

        public void updateROMData()
        {
            if (Address.Equals("N/A")) return;
            ROM rom = ROM.Instance;
            uint romAddr = getROMUnsignedAddress();
            rom.writeByte(romAddr + 2, TriggerID);
            rom.writeByte(romAddr + 3, AreaID);
            rom.writeHalfword(romAddr + 4, TeleX);
            rom.writeHalfword(romAddr + 6, TeleY);
            rom.writeHalfword(romAddr + 8, TeleZ);
        }
        
        private string getWarpName()
        {
            return " [to Area "+AreaID+"]";
        }

        public override string ToString()
        {
            //isPaintingWarp
            string warpName = "Instant Warp 0x";

            warpName += TriggerID.ToString("X2") + getWarpName();
            
            return warpName;
        }
    }
}
