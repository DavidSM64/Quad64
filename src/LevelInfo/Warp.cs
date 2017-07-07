using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Quad64.src.LevelInfo
{
    class Warp
    {
        public Warp(bool isPaintingWarp)
        {
            this.isPaintingWarp = isPaintingWarp;
        }
        
        private const ushort NUM_OF_CATERGORIES = 2;
        private bool isPaintingWarp = false;

        private byte warpFrom_ID;
        [CustomSortedCategory("Connect Warps", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("From ID")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public byte WarpFrom_ID { get { return warpFrom_ID; } set { warpFrom_ID = value; } }

        private byte warpTo_LevelID;
        [CustomSortedCategory("Connect Warps", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("To Level")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public byte WarpTo_LevelID { get { return warpTo_LevelID; } set { warpTo_LevelID = value; } }

        private byte warpTo_AreaID;
        [CustomSortedCategory("Connect Warps", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("To Area")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public byte WarpTo_AreaID { get { return warpTo_AreaID; } set { warpTo_AreaID = value; } }

        private byte warpTo_WarpID;
        [CustomSortedCategory("Connect Warps", 1, NUM_OF_CATERGORIES)]
        [Browsable(true)]
        [DisplayName("To ID")]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        public byte WarpTo_WarpID { get { return warpTo_WarpID; } set { warpTo_WarpID = value; } }
        
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
            rom.writeByte(romAddr + 2, WarpFrom_ID);
            rom.writeByte(romAddr + 3, WarpTo_LevelID);
            rom.writeByte(romAddr + 4, WarpTo_AreaID);
            rom.writeByte(romAddr + 5, WarpTo_WarpID);
        }

        private string getLevelName()
        {
            ROM rom = ROM.Instance;
            foreach (KeyValuePair<string, ushort> entry in rom.levelIDs)
            {
                if (entry.Value == WarpTo_LevelID)
                    return entry.Key + " ("+warpTo_AreaID+")";
            }
            return "Unknown" + " (" + warpTo_AreaID + ")";
        }

        private string getWarpName()
        {
            if (isPaintingWarp)
            {
                return " [to "+ getLevelName()+"]";
            }
            else
            {
                switch (WarpFrom_ID)
                {
                    case 0xF0:
                        return " (Success)"+ " [to " + getLevelName() + "]";
                    case 0xF1:
                        return " (Failure)" + " [to " + getLevelName() + "]";
                    case 0xF2:
                    case 0xF3:
                        return " (Special)" + " [to " + getLevelName() + "]";
                    default:
                        return " [to " + getLevelName() + "]";
                }
            }
        }

        public override string ToString()
        {
            //isPaintingWarp
            string warpName = "Warp 0x";
            if (isPaintingWarp)
                warpName = "Painting 0x";

            warpName += WarpFrom_ID.ToString("X2") + getWarpName();
            
            return warpName;
        }
    }
}
