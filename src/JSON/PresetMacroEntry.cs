using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quad64.src.JSON
{
    class PresetMacroEntry
    {
        private ushort presetID;
        private byte modelID;
        private uint behavior;
        private byte bp1, bp2;

        public ushort PresetID { get { return presetID; } }
        public byte ModelID { get { return modelID; } }
        public uint Behavior { get { return behavior; } }
        public byte BehaviorParameter1 { get { return bp1; } }
        public byte BehaviorParameter2 { get { return bp2; } }

        public PresetMacroEntry(ushort presetID, byte modelID, uint behavior)
        {
            this.presetID = presetID;
            this.modelID = modelID;
            this.behavior = behavior;
        }

        public PresetMacroEntry(ushort presetID, byte modelID, uint behavior, byte bp1, byte bp2)
        {
            this.presetID = presetID;
            this.modelID = modelID;
            this.behavior = behavior;
            this.bp1 = bp1;
            this.bp2 = bp2;
        }
    }
}
