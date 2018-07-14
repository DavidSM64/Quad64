using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quad64.src.JSON
{
    class SettingsFile
    {
        public static void SaveGlobalSettings(string profileName)
        {
            JObject s = new JObject();
            s["EnableWireframe"] = Globals.doWireframe.ToString();
            s["EnableBackfaceCulling"] = Globals.doBackfaceCulling.ToString();
            s["DrawObjectModels"] = Globals.drawObjectModels.ToString();
            s["RenderCollisionMap"] = Globals.renderCollisionMap.ToString();
            s["AutoLoadROMFile"] = Globals.autoLoadROMOnStartup.ToString();
            s["LastROMFile"] = Globals.pathToAutoLoadROM;
            s["EnableHex"] = Globals.useHexadecimal.ToString();
            s["SignedHex"] = Globals.useSignedHex.ToString();
            s["EmulatorPath"] = Globals.pathToEmulator;
            s["AutoSaveOnLaunchROM"] = Globals.autoSaveWhenClickEmulator.ToString();
            s["FieldOfView"] = Globals.FOV.ToString();
            s["Theme"] = Theme.lastThemePath;

            string savePath = "./data/profiles/" + profileName + "/";
            Directory.CreateDirectory(savePath); // Create directory if it doesn't exist!
            File.WriteAllText(savePath + "Settings.json", s.ToString());
        }

        public static void LoadGlobalSettings(string profileName)
        {
            string fp = "./data/profiles/" + profileName + "/Settings.json";
            if (File.Exists(fp))
            {
                string json = File.ReadAllText(fp);
                JObject o = JObject.Parse(json);
                if (o["EnableWireframe"] != null)
                    Globals.doWireframe = bool.Parse(o["EnableWireframe"].ToString());
                if (o["EnableBackfaceCulling"] != null)
                    Globals.doBackfaceCulling = bool.Parse(o["EnableBackfaceCulling"].ToString());
                if (o["DrawObjectModels"] != null)
                    Globals.drawObjectModels = bool.Parse(o["DrawObjectModels"].ToString());
                if (o["RenderCollisionMap"] != null)
                    Globals.renderCollisionMap = bool.Parse(o["RenderCollisionMap"].ToString());
                if (o["AutoLoadROMFile"] != null)
                    Globals.autoLoadROMOnStartup = bool.Parse(o["AutoLoadROMFile"].ToString());
                if (o["LastROMFile"] != null)
                    Globals.pathToAutoLoadROM = o["LastROMFile"].ToString();
                if (o["EnableHex"] != null)
                    Globals.useHexadecimal = bool.Parse(o["EnableHex"].ToString());
                if (o["SignedHex"] != null)
                    Globals.useSignedHex = bool.Parse(o["SignedHex"].ToString());
                if(o["EmulatorPath"] != null)
                    Globals.pathToEmulator = o["EmulatorPath"].ToString();
                if (o["AutoSaveOnLaunchROM"] != null)
                    Globals.autoSaveWhenClickEmulator = bool.Parse(o["AutoSaveOnLaunchROM"].ToString());
                if (o["FieldOfView"] != null)
                    Globals.FOV = int.Parse(o["FieldOfView"].ToString());
                if (o["Theme"] != null)
                    Theme.LoadColorsFromJSONFile(o["Theme"].ToString());
            }
        }
    }
}
