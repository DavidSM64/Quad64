using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Quad64.src
{
    class Theme
    {
        /*** Default colors to fallback on if a specific color is not defined ***/
        public static Color DEFAULT_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color DEFAULT_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color DEFAULT_PANEL_BACKGROUND = Color.FromArgb(0xE0, 0xE0, 0xE0);
        public static Color DEFAULT_LISTBOX_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color DEFAULT_LISTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color DEFAULT_LISTBOX_HIGHLIGHT = Color.FromArgb(0x00, 0x78, 0xD7);
        public static Color DEFAULT_LISTBOX_HIGHLIGHTEDTEXT = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color DEFAULT_TEXTBOX_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color DEFAULT_TEXTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color DEFAULT_TEXTBOX_COMMENTS = Color.FromArgb(0x00, 0x80, 0x00);
        public static Color DEFAULT_DROPDOWNLIST_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color DEFAULT_DROPDOWNLIST_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color DEFAULT_BUTTON_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color DEFAULT_BUTTON_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color DEFAULT_UPDOWN_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color DEFAULT_UPDOWN_TEXT = Color.FromArgb(0x00, 0x00, 0x00);

        /******************** Main Form Specifics ********************/
        public static Color MAIN_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);

        public static Color MAIN_TREEVIEW_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color MAIN_TREEVIEW_TEXT = Color.FromArgb(0, 0, 0);
        public static Color MAIN_TREEVIEW_HIGHTLIGHT = Color.FromArgb(0x70, 0xBB, 0xDB);
        public static Color MAIN_TREEVIEW_3DOBJECTS = Color.FromArgb(192, 0, 0);
        public static Color MAIN_TREEVIEW_MACRO = Color.FromArgb(0, 0, 192);
        public static Color MAIN_TREEVIEW_SPECIAL = Color.FromArgb(0, 192, 0);
        public static Color MAIN_TREEVIEW_WARPS = Color.FromArgb(0, 0, 0);

        public static Color MAIN_PROPERTIES_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color MAIN_PROPERTIES_LINES = Color.FromArgb(0xA9, 0xA9, 0xA9);
        public static Color MAIN_PROPERTIES_TEXT = Color.FromArgb(0, 0, 0);
        
        public static Color MAIN_CONTROLS_BACKGROUND = Color.FromArgb(0xDC, 0xDC, 0xDC);
        public static Color MAIN_CONTROLS_TEXT = Color.FromArgb(0, 0, 0);
        public static Color MAIN_CONTROLS_DROPDOWNLIST_BACKGROUND = Color.FromArgb(0xDC, 0xDC, 0xDC);
        public static Color MAIN_CONTROLS_DROPDOWNLIST_TEXT = Color.FromArgb(0, 0, 0);
        public static Color MAIN_CONTROLS_BUTTON_BACKGROUND = Color.FromArgb(0xDC, 0xDC, 0xDC);
        public static Color MAIN_CONTROLS_BUTTON_TEXT = Color.FromArgb(0, 0, 0);
        public static Color MAIN_CONTROLS_UPDOWN_BACKGROUND = Color.FromArgb(0xDC, 0xDC, 0xDC);
        public static Color MAIN_CONTROLS_UPDOWN_TEXT = Color.FromArgb(0, 0, 0);

        public static Color MAIN_MENUBAR_BORDER = Color.FromArgb(0, 0, 0);
        public static Color MAIN_MENUBAR_TEXT = Color.FromArgb(0, 0, 0);
        public static Color MAIN_MENUBAR_BACKGROUND = Color.FromArgb(0xD3, 0xD3, 0xD3);
        public static Color MAIN_MENUBAR_ITEM_SELECTED = Color.FromArgb(0xF9, 0xF9, 0xF9);
        public static Color MAIN_MENUBAR_ITEM_HIGHLIGHT = Color.FromArgb(0xB5, 0xD7, 0xF3);

        /******************** Object Combo Specifics ********************/

        public static Color COMBOS_BACKGROUND = Color.FromArgb(255, 255, 255);
        public static Color COMBOS_TEXT = Color.FromArgb(0, 0, 0);

        public static Color COMBOS_3DOBJECTS_TITLE = Color.DarkRed;
        public static Color COMBOS_3DOBJECTS_MAIN = Color.FromArgb(250, 250, 250);
        public static Color COMBOS_3DOBJECTS_SECONDARY = Color.FromArgb(250, 240, 240);
        public static Color COMBOS_3DOBJECTS_HIGHLIGHT = Color.FromArgb(200, 200, 255);
        public static Color COMBOS_3DOBJECTS_HIGHLIGHT_TEXT = Color.FromArgb(0, 0, 0);

        public static Color COMBOS_MACRO_TITLE = Color.DarkBlue;
        public static Color COMBOS_MACRO_MAIN = Color.FromArgb(250, 250, 250);
        public static Color COMBOS_MACRO_SECONDARY = Color.FromArgb(240, 240, 250);
        public static Color COMBOS_MACRO_HIGHLIGHT = Color.FromArgb(200, 200, 255);
        public static Color COMBOS_MACRO_HIGHLIGHT_TEXT = Color.FromArgb(0, 0, 0);

        public static Color COMBOS_SPECIAL_TITLE = Color.DarkGreen;
        public static Color COMBOS_SPECIAL_MAIN = Color.FromArgb(250, 250, 250);
        public static Color COMBOS_SPECIAL_SECONDARY = Color.FromArgb(240, 250, 240);
        public static Color COMBOS_SPECIAL_HIGHLIGHT = Color.FromArgb(200, 200, 255);
        public static Color COMBOS_SPECIAL_HIGHLIGHT_TEXT = Color.FromArgb(0, 0, 0);

        public static Color COMBOS_OTHER_TITLE = Color.DimGray;
        public static Color COMBOS_OTHER_MAIN = Color.FromArgb(250, 250, 250);
        public static Color COMBOS_OTHER_SECONDARY = Color.FromArgb(240, 240, 240);
        public static Color COMBOS_OTHER_HIGHLIGHT = Color.FromArgb(200, 200, 255);
        public static Color COMBOS_OTHER_HIGHLIGHT_TEXT = Color.FromArgb(0, 0, 0);

        /******************** Texture Editor Specifics ********************/
        public static Color TEXTURES_BACKGROUND = Color.FromArgb(240, 240, 240);

        public static Color TEXTURES_LEVEL_TEXT = Color.FromArgb(0, 0, 0);
        public static Color TEXTURES_LEVEL_BUTTON_BACKGROUND = Color.FromArgb(225, 225, 225);
        public static Color TEXTURES_LEVEL_BUTTON_TEXT = Color.FromArgb(0, 0, 0);
        public static Color TEXTURES_LEVEL_BACKGROUND_LEFT = Color.FromArgb(0xB2, 0x22, 0x22);
        public static Color TEXTURES_LEVEL_BACKGROUND_MIDDLE = Color.FromArgb(0x00, 0x80, 0x00);
        public static Color TEXTURES_LEVEL_BACKGROUND_RIGHT = Color.FromArgb(0x64, 0x95, 0xED);

        public static Color TEXTURES_OTHER_TEXT = Color.FromArgb(0, 0, 0);
        public static Color TEXTURES_OTHER_BUTTON_BACKGROUND = Color.FromArgb(225, 225, 225);
        public static Color TEXTURES_OTHER_BUTTON_TEXT = Color.FromArgb(0, 0, 0);
        public static Color TEXTURES_OTHER_BACKGROUND_LEFT = Color.FromArgb(0xB2, 0x22, 0x22);
        public static Color TEXTURES_OTHER_BACKGROUND_MIDDLE = Color.FromArgb(0x00, 0x80, 0x00);
        public static Color TEXTURES_OTHER_BACKGROUND_RIGHT = Color.FromArgb(0x64, 0x95, 0xED);
        
        public static Color TEXTURES_SKY_TEXT = Color.FromArgb(0, 0, 0);
        public static Color TEXTURES_SKY_BUTTON_BACKGROUND = Color.FromArgb(225, 225, 225);
        public static Color TEXTURES_SKY_BUTTON_TEXT = Color.FromArgb(0, 0, 0);
        public static Color TEXTURES_SKY_BACKGROUND_LEFT = Color.FromArgb(0x66, 0xCD, 0xAA);
        public static Color TEXTURES_SKY_BACKGROUND_RIGHT = Color.FromArgb(0x64, 0x95, 0xED);

        /******************** Script Dumps Specifics ********************/
        public static Color SCRIPTDUMPS_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color SCRIPTDUMPS_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color SCRIPTDUMPS_OPTIONS_BACKGROUND = Color.FromArgb(0xDC, 0xDC, 0xDC);

        public static Color SCRIPTDUMPS_LEVEL_TEXTBOX_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color SCRIPTDUMPS_LEVEL_TEXTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color SCRIPTDUMPS_LEVEL_TEXTBOX_COMMENTS = Color.FromArgb(0x00, 0x80, 0x00);

        public static Color SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_COMMENTS = Color.FromArgb(0x00, 0x80, 0x00);

        public static Color SCRIPTDUMPS_FAST3D_TEXTBOX_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color SCRIPTDUMPS_FAST3D_TEXTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color SCRIPTDUMPS_FAST3D_TEXTBOX_COMMENTS = Color.FromArgb(0x00, 0x80, 0x00);
        public static Color SCRIPTDUMPS_FAST3D_LISTBOX_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color SCRIPTDUMPS_FAST3D_LISTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color SCRIPTDUMPS_FAST3D_LISTBOX_HIGHLIGHT = Color.FromArgb(0x00, 0x78, 0xD7);
        public static Color SCRIPTDUMPS_FAST3D_LISTBOX_HIGHLIGHTEDTEXT = Color.FromArgb(0xF0, 0xF0, 0xF0);

        public static Color SCRIPTDUMPS_BEHAVIOR_TEXTBOX_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color SCRIPTDUMPS_BEHAVIOR_TEXTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color SCRIPTDUMPS_BEHAVIOR_TEXTBOX_COMMENTS = Color.FromArgb(0x00, 0x80, 0x00);
        
        public static Color SCRIPTDUMPS_OBJECTSTAB_LISTBOX_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color SCRIPTDUMPS_OBJECTSTAB_LISTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color SCRIPTDUMPS_OBJECTSTAB_LISTBOX_HIGHLIGHT = Color.FromArgb(0x00, 0x78, 0xD7);
        public static Color SCRIPTDUMPS_OBJECTSTAB_LISTBOX_HIGHLIGHTEDTEXT = Color.FromArgb(0xF0, 0xF0, 0xF0);

        public static Color SCRIPTDUMPS_LEVELTAB_GEOLAYOUT_AREAPANEL_BACKGROUND = Color.FromArgb(0xF5, 0xF5, 0xF5);
        public static Color SCRIPTDUMPS_LEVELTAB_GEOLAYOUT_AREAPANEL_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
        public static Color SCRIPTDUMPS_LEVELTAB_FAST3D_AREAPANEL_BACKGROUND = Color.FromArgb(0xF5, 0xF5, 0xF5);
        public static Color SCRIPTDUMPS_LEVELTAB_FAST3D_AREAPANEL_TEXT = Color.FromArgb(0x00, 0x00, 0x00);

        public static Color SCRIPTDUMPS_OBJECTSTAB_SORT_DROPDOWNLIST_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
        public static Color SCRIPTDUMPS_OBJECTSTAB_SORT_DROPDOWNLIST_TEXT = Color.FromArgb(0x00, 0x00, 0x00);

        public static void LoadColor(ref Color color, Color default_color, JToken token)
        {
            if (token != null)
            {
                if (token["R"] != null && token["G"] != null && token["B"] != null)
                {
                    byte R, G, B;
                    bool red = byte.TryParse(token["R"].ToString(), out R);
                    bool green = byte.TryParse(token["G"].ToString(), out G);
                    bool blue = byte.TryParse(token["B"].ToString(), out B);

                    if (red && green && blue)
                    {
                        color = Color.FromArgb(R, G, B);
                        return;
                    }
                }
            }

            // Load failed, so use default color instead.
            color = default_color;
        }

        public static string lastThemePath = "";

        public static void LoadColorsFromJSONFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                lastThemePath = filepath;

                // Reset defaults
                DEFAULT_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
                DEFAULT_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
                DEFAULT_PANEL_BACKGROUND = Color.FromArgb(0xDC, 0xDC, 0xDC);
                DEFAULT_LISTBOX_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
                DEFAULT_LISTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
                DEFAULT_LISTBOX_HIGHLIGHT = Color.FromArgb(0x00, 0x78, 0xD7);
                DEFAULT_LISTBOX_HIGHLIGHTEDTEXT = Color.FromArgb(0xFF, 0xFF, 0xFF);
                DEFAULT_TEXTBOX_BACKGROUND = Color.FromArgb(0xFF, 0xFF, 0xFF);
                DEFAULT_TEXTBOX_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
                DEFAULT_TEXTBOX_COMMENTS = Color.FromArgb(0x00, 0x80, 0x00);
                DEFAULT_DROPDOWNLIST_BACKGROUND = Color.FromArgb(0xD0, 0xD0, 0xD0);
                DEFAULT_DROPDOWNLIST_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
                DEFAULT_BUTTON_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
                DEFAULT_BUTTON_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
                DEFAULT_UPDOWN_BACKGROUND = Color.FromArgb(0xF0, 0xF0, 0xF0);
                DEFAULT_UPDOWN_TEXT = Color.FromArgb(0x00, 0x00, 0x00);
                

                string json = File.ReadAllText(filepath);
                JObject o = JObject.Parse(json);
                if (o["Defaults"] != null)
                {
                    LoadColor(ref DEFAULT_BACKGROUND, DEFAULT_BACKGROUND, o["Defaults"]["Background"]);
                    LoadColor(ref DEFAULT_TEXT, DEFAULT_TEXT, o["Defaults"]["Text"]);
                    LoadColor(ref DEFAULT_PANEL_BACKGROUND, DEFAULT_PANEL_BACKGROUND, o["Defaults"]["Panel-Background"]);
                    LoadColor(ref DEFAULT_LISTBOX_BACKGROUND, DEFAULT_LISTBOX_BACKGROUND, o["Defaults"]["ListBox-Background"]);
                    LoadColor(ref DEFAULT_LISTBOX_TEXT, DEFAULT_LISTBOX_TEXT, o["Defaults"]["ListBox-Text"]);
                    LoadColor(ref DEFAULT_LISTBOX_HIGHLIGHT, DEFAULT_LISTBOX_HIGHLIGHT, o["Defaults"]["ListBox-Highlight"]);
                    LoadColor(ref DEFAULT_LISTBOX_HIGHLIGHTEDTEXT, DEFAULT_LISTBOX_HIGHLIGHTEDTEXT, o["Defaults"]["ListBox-HighlightedText"]);
                    LoadColor(ref DEFAULT_TEXTBOX_BACKGROUND, DEFAULT_TEXTBOX_BACKGROUND, o["Defaults"]["TextBox-Background"]);
                    LoadColor(ref DEFAULT_TEXTBOX_TEXT, DEFAULT_TEXTBOX_TEXT, o["Defaults"]["TextBox-Text"]);
                    LoadColor(ref DEFAULT_TEXTBOX_COMMENTS, DEFAULT_TEXTBOX_COMMENTS, o["Defaults"]["TextBox-Comments"]);
                    LoadColor(ref DEFAULT_DROPDOWNLIST_BACKGROUND, DEFAULT_DROPDOWNLIST_BACKGROUND, o["Defaults"]["DropDown-Background"]);
                    LoadColor(ref DEFAULT_DROPDOWNLIST_TEXT, DEFAULT_DROPDOWNLIST_TEXT, o["Defaults"]["DropDown-Text"]);
                    LoadColor(ref DEFAULT_BUTTON_BACKGROUND, DEFAULT_BUTTON_BACKGROUND, o["Defaults"]["Button-Background"]);
                    LoadColor(ref DEFAULT_BUTTON_TEXT, DEFAULT_BUTTON_TEXT, o["Defaults"]["Button-Text"]);
                    LoadColor(ref DEFAULT_UPDOWN_BACKGROUND, DEFAULT_UPDOWN_BACKGROUND, o["Defaults"]["NumericUpDown-Background"]);
                    LoadColor(ref DEFAULT_UPDOWN_TEXT, DEFAULT_UPDOWN_TEXT, o["Defaults"]["NumericUpDown-Text"]);
                }

                if (o["ScriptDumps"] != null)
                {
                    LoadColor(ref SCRIPTDUMPS_BACKGROUND, DEFAULT_BACKGROUND, o["ScriptDumps"]["Background"]);
                    LoadColor(ref SCRIPTDUMPS_TEXT, DEFAULT_TEXT, o["ScriptDumps"]["Text"]);
                    LoadColor(ref SCRIPTDUMPS_OPTIONS_BACKGROUND, DEFAULT_PANEL_BACKGROUND, o["ScriptDumps"]["Panel-Background"]);

                    LoadColor(ref SCRIPTDUMPS_OBJECTSTAB_SORT_DROPDOWNLIST_BACKGROUND, DEFAULT_DROPDOWNLIST_BACKGROUND, o["ScriptDumps"]["DropDown-Background"]);
                    LoadColor(ref SCRIPTDUMPS_OBJECTSTAB_SORT_DROPDOWNLIST_TEXT, DEFAULT_DROPDOWNLIST_TEXT, o["ScriptDumps"]["DropDown-Text"]);
                    
                    LoadColor(ref SCRIPTDUMPS_LEVELTAB_GEOLAYOUT_AREAPANEL_BACKGROUND, DEFAULT_PANEL_BACKGROUND, o["ScriptDumps"]["GeoLayoutScripts-AreaPanel-Background"]);
                    LoadColor(ref SCRIPTDUMPS_LEVELTAB_GEOLAYOUT_AREAPANEL_TEXT, DEFAULT_TEXT, o["ScriptDumps"]["GeoLayoutScripts-AreaPanel-Text"]);
                    LoadColor(ref SCRIPTDUMPS_LEVELTAB_FAST3D_AREAPANEL_BACKGROUND, DEFAULT_PANEL_BACKGROUND, o["ScriptDumps"]["Fast3DScripts-AreaPanel-Background"]);
                    LoadColor(ref SCRIPTDUMPS_LEVELTAB_FAST3D_AREAPANEL_TEXT, DEFAULT_TEXT, o["ScriptDumps"]["Fast3DScripts-AreaPanel-Text"]);

                    LoadColor(ref SCRIPTDUMPS_OBJECTSTAB_LISTBOX_BACKGROUND, DEFAULT_LISTBOX_BACKGROUND, o["ScriptDumps"]["Objects-ListBox-Background"]);
                    LoadColor(ref SCRIPTDUMPS_OBJECTSTAB_LISTBOX_TEXT, DEFAULT_LISTBOX_TEXT, o["ScriptDumps"]["Objects-ListBox-Background"]);
                    LoadColor(ref SCRIPTDUMPS_OBJECTSTAB_LISTBOX_HIGHLIGHT, DEFAULT_LISTBOX_HIGHLIGHT, o["ScriptDumps"]["Objects-ListBox-Highlight"]);
                    LoadColor(ref SCRIPTDUMPS_OBJECTSTAB_LISTBOX_HIGHLIGHTEDTEXT, DEFAULT_LISTBOX_HIGHLIGHTEDTEXT, o["ScriptDumps"]["Objects-ListBox-HighlightedText"]);
                    
                    LoadColor(ref SCRIPTDUMPS_FAST3D_LISTBOX_BACKGROUND, DEFAULT_LISTBOX_BACKGROUND, o["ScriptDumps"]["Fast3DScripts-ListBox-Background"]);
                    LoadColor(ref SCRIPTDUMPS_FAST3D_LISTBOX_TEXT, DEFAULT_LISTBOX_TEXT, o["ScriptDumps"]["Fast3DScripts-ListBox-Text"]);
                    LoadColor(ref SCRIPTDUMPS_FAST3D_LISTBOX_HIGHLIGHT, DEFAULT_LISTBOX_HIGHLIGHT, o["ScriptDumps"]["Fast3DScripts-ListBox-Highlight"]);
                    LoadColor(ref SCRIPTDUMPS_FAST3D_LISTBOX_HIGHLIGHTEDTEXT, DEFAULT_LISTBOX_HIGHLIGHTEDTEXT, o["ScriptDumps"]["Fast3DScripts-ListBox-HighlightedText"]);

                    LoadColor(ref SCRIPTDUMPS_LEVEL_TEXTBOX_BACKGROUND, DEFAULT_TEXTBOX_BACKGROUND, o["ScriptDumps"]["LevelScripts-Background"]);
                    LoadColor(ref SCRIPTDUMPS_LEVEL_TEXTBOX_TEXT, DEFAULT_TEXTBOX_TEXT, o["ScriptDumps"]["LevelScripts-Text"]);
                    LoadColor(ref SCRIPTDUMPS_LEVEL_TEXTBOX_COMMENTS, DEFAULT_TEXTBOX_COMMENTS, o["ScriptDumps"]["LevelScripts-Comments"]);

                    LoadColor(ref SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_BACKGROUND, DEFAULT_TEXTBOX_BACKGROUND, o["ScriptDumps"]["GeoLayoutScripts-Background"]);
                    LoadColor(ref SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_TEXT, DEFAULT_TEXTBOX_TEXT, o["ScriptDumps"]["GeoLayoutScripts-Text"]);
                    LoadColor(ref SCRIPTDUMPS_GEOLAYOUT_TEXTBOX_COMMENTS, DEFAULT_TEXTBOX_COMMENTS, o["ScriptDumps"]["GeoLayoutScripts-Comments"]);

                    LoadColor(ref SCRIPTDUMPS_FAST3D_TEXTBOX_BACKGROUND, DEFAULT_TEXTBOX_BACKGROUND, o["ScriptDumps"]["Fast3DScripts-Background"]);
                    LoadColor(ref SCRIPTDUMPS_FAST3D_TEXTBOX_TEXT, DEFAULT_TEXTBOX_TEXT, o["ScriptDumps"]["Fast3DScripts-Text"]);
                    LoadColor(ref SCRIPTDUMPS_FAST3D_TEXTBOX_COMMENTS, DEFAULT_TEXTBOX_COMMENTS, o["ScriptDumps"]["Fast3DScripts-Comments"]);

                    LoadColor(ref SCRIPTDUMPS_BEHAVIOR_TEXTBOX_BACKGROUND, DEFAULT_TEXTBOX_BACKGROUND, o["ScriptDumps"]["BehaviorScripts-Background"]);
                    LoadColor(ref SCRIPTDUMPS_BEHAVIOR_TEXTBOX_TEXT, DEFAULT_TEXTBOX_TEXT, o["ScriptDumps"]["BehaviorScripts-Text"]);
                    LoadColor(ref SCRIPTDUMPS_BEHAVIOR_TEXTBOX_COMMENTS, DEFAULT_TEXTBOX_COMMENTS, o["ScriptDumps"]["BehaviorScripts-Comments"]);
                }

                if (o["Main"] != null)
                {
                    LoadColor(ref MAIN_BACKGROUND, DEFAULT_BACKGROUND, o["Main"]["Background"]);

                    LoadColor(ref MAIN_TREEVIEW_BACKGROUND, DEFAULT_BACKGROUND, o["Main"]["TreeView-Background"]);
                    LoadColor(ref MAIN_TREEVIEW_TEXT, DEFAULT_TEXT, o["Main"]["TreeView-Text"]);
                    LoadColor(ref MAIN_TREEVIEW_HIGHTLIGHT, Color.FromArgb(0x70, 0xBB, 0xDB), o["Main"]["TreeView-Highlight"]);
                    LoadColor(ref MAIN_TREEVIEW_3DOBJECTS, Color.FromArgb(192, 0, 0), o["Main"]["TreeView-3DObjectsLabel"]);
                    LoadColor(ref MAIN_TREEVIEW_MACRO, Color.FromArgb(0, 0, 192), o["Main"]["TreeView-MacroObjectsLabel"]);
                    LoadColor(ref MAIN_TREEVIEW_SPECIAL, Color.FromArgb(0, 192, 0), o["Main"]["TreeView-SpecialObjectsLabel"]);
                    LoadColor(ref MAIN_TREEVIEW_WARPS, Color.FromArgb(0, 0, 0), o["Main"]["TreeView-WarpsLabel"]);

                    LoadColor(ref MAIN_PROPERTIES_BACKGROUND, DEFAULT_BACKGROUND, o["Main"]["Properties-Background"]);
                    LoadColor(ref MAIN_PROPERTIES_TEXT, DEFAULT_TEXT, o["Main"]["Properties-Text"]);
                    LoadColor(ref MAIN_PROPERTIES_LINES, Color.FromArgb(0xA9, 0xA9, 0xA9), o["Main"]["Properties-Lines"]);
                    
                    LoadColor(ref MAIN_CONTROLS_BACKGROUND, Color.FromArgb(0x90, 0x90, 0x90), o["Main"]["Controls-Background"]);
                    LoadColor(ref MAIN_CONTROLS_TEXT, DEFAULT_TEXT, o["Main"]["Controls-Text"]);
                    LoadColor(ref MAIN_CONTROLS_BUTTON_BACKGROUND, DEFAULT_BUTTON_BACKGROUND, o["Main"]["Controls-Button-Background"]);
                    LoadColor(ref MAIN_CONTROLS_BUTTON_TEXT, DEFAULT_BUTTON_TEXT, o["Main"]["Controls-Button-Text"]);
                    LoadColor(ref MAIN_CONTROLS_DROPDOWNLIST_BACKGROUND, DEFAULT_DROPDOWNLIST_BACKGROUND, o["Main"]["Controls-DropDown-Background"]);
                    LoadColor(ref MAIN_CONTROLS_DROPDOWNLIST_TEXT, DEFAULT_DROPDOWNLIST_TEXT, o["Main"]["Controls-DropDown-Text"]);
                    LoadColor(ref MAIN_CONTROLS_UPDOWN_BACKGROUND, DEFAULT_UPDOWN_BACKGROUND, o["Main"]["Controls-NumericUpDown-Background"]);
                    LoadColor(ref MAIN_CONTROLS_UPDOWN_TEXT, DEFAULT_UPDOWN_TEXT, o["Main"]["Controls-NumericUpDown-Text"]);

                    LoadColor(ref MAIN_MENUBAR_TEXT, DEFAULT_TEXT, o["Main"]["MenuBar-Text"]);
                    LoadColor(ref MAIN_MENUBAR_BACKGROUND, DEFAULT_PANEL_BACKGROUND, o["Main"]["MenuBar-Background"]);
                    LoadColor(ref MAIN_MENUBAR_BORDER, Color.Black, o["Main"]["MenuBar-Item-Border"]);
                    LoadColor(ref MAIN_MENUBAR_ITEM_SELECTED, Color.FromArgb(0xF9, 0xF9, 0xF9), o["Main"]["MenuBar-Item-Selected"]);
                    LoadColor(ref MAIN_MENUBAR_ITEM_HIGHLIGHT, Color.FromArgb(0xB5, 0xD7, 0xF3), o["Main"]["MenuBar-Item-Highlight"]);
                 }

                if (o["TextureEditor"] != null)
                {
                    LoadColor(ref TEXTURES_BACKGROUND, DEFAULT_BACKGROUND, o["TextureEditor"]["Background"]);

                    LoadColor(ref TEXTURES_LEVEL_BACKGROUND_LEFT, Color.FromArgb(0xB2, 0x22, 0x22), o["TextureEditor"]["LevelTab-Background-Left"]);
                    LoadColor(ref TEXTURES_LEVEL_BACKGROUND_MIDDLE, Color.FromArgb(0x00, 0x80, 0x00), o["TextureEditor"]["LevelTab-Background-Middle"]);
                    LoadColor(ref TEXTURES_LEVEL_BACKGROUND_RIGHT, Color.FromArgb(0x64, 0x95, 0xED), o["TextureEditor"]["LevelTab-Background-Right"]);
                    LoadColor(ref TEXTURES_LEVEL_TEXT, DEFAULT_TEXT, o["TextureEditor"]["LevelTab-Text"]);
                    LoadColor(ref TEXTURES_LEVEL_BUTTON_BACKGROUND, DEFAULT_BUTTON_BACKGROUND, o["TextureEditor"]["LevelTab-Button-Background"]);
                    LoadColor(ref TEXTURES_LEVEL_BUTTON_TEXT, DEFAULT_BUTTON_TEXT, o["TextureEditor"]["LevelTab-Button-Text"]);

                    LoadColor(ref TEXTURES_OTHER_BACKGROUND_LEFT, Color.FromArgb(0xB2, 0x22, 0x22), o["TextureEditor"]["OtherTab-Background-Left"]);
                    LoadColor(ref TEXTURES_OTHER_BACKGROUND_MIDDLE, Color.FromArgb(0x00, 0x80, 0x00), o["TextureEditor"]["OtherTab-Background-Middle"]);
                    LoadColor(ref TEXTURES_OTHER_BACKGROUND_RIGHT, Color.FromArgb(0x64, 0x95, 0xED), o["TextureEditor"]["OtherTab-Background-Right"]);
                    LoadColor(ref TEXTURES_OTHER_TEXT, DEFAULT_TEXT, o["TextureEditor"]["OtherTab-Text"]);
                    LoadColor(ref TEXTURES_OTHER_BUTTON_BACKGROUND, DEFAULT_BUTTON_BACKGROUND, o["TextureEditor"]["OtherTab-Button-Background"]);
                    LoadColor(ref TEXTURES_OTHER_BUTTON_TEXT, DEFAULT_BUTTON_TEXT, o["TextureEditor"]["OtherTab-Button-Text"]);

                    LoadColor(ref TEXTURES_SKY_BACKGROUND_LEFT, Color.FromArgb(0x66, 0xCD, 0xAA), o["TextureEditor"]["SkyTab-Background-Left"]);
                    LoadColor(ref TEXTURES_SKY_BACKGROUND_RIGHT, Color.FromArgb(0x64, 0x95, 0xED), o["TextureEditor"]["SkyTab-Background-Right"]);
                    LoadColor(ref TEXTURES_SKY_TEXT, DEFAULT_TEXT, o["TextureEditor"]["SkyTab-Text"]);
                    LoadColor(ref TEXTURES_SKY_BUTTON_BACKGROUND, DEFAULT_BUTTON_BACKGROUND, o["TextureEditor"]["SkyTab-Button-Background"]);
                    LoadColor(ref TEXTURES_SKY_BUTTON_TEXT, DEFAULT_BUTTON_TEXT, o["TextureEditor"]["SkyTab-Button-Text"]);
                }

                if (o["ObjectCombos"] != null)
                {
                    LoadColor(ref COMBOS_BACKGROUND, DEFAULT_BACKGROUND, o["ObjectCombos"]["Background"]);
                    LoadColor(ref COMBOS_TEXT, DEFAULT_TEXT, o["ObjectCombos"]["Text"]);

                    LoadColor(ref COMBOS_3DOBJECTS_TITLE, Color.DarkRed, o["ObjectCombos"]["3DObjects-Title"]);
                    LoadColor(ref COMBOS_3DOBJECTS_MAIN, Color.FromArgb(250, 250, 250), o["ObjectCombos"]["3DObjects-Item"]);
                    LoadColor(ref COMBOS_3DOBJECTS_SECONDARY, Color.FromArgb(250, 240, 240), o["ObjectCombos"]["3DObjects-Item-Alt"]);
                    LoadColor(ref COMBOS_3DOBJECTS_HIGHLIGHT, Color.FromArgb(200, 200, 255), o["ObjectCombos"]["3DObjects-Item-Highlight"]);
                    LoadColor(ref COMBOS_3DOBJECTS_HIGHLIGHT_TEXT, DEFAULT_TEXT, o["ObjectCombos"]["3DObjects-Item-Highlight-Text"]);

                    LoadColor(ref COMBOS_MACRO_TITLE, Color.DarkBlue, o["ObjectCombos"]["MacroObjects-Title"]);
                    LoadColor(ref COMBOS_MACRO_MAIN, Color.FromArgb(250, 250, 250), o["ObjectCombos"]["MacroObjects-Item"]);
                    LoadColor(ref COMBOS_MACRO_SECONDARY, Color.FromArgb(240, 240, 250), o["ObjectCombos"]["MacroObjects-Item-Alt"]);
                    LoadColor(ref COMBOS_MACRO_HIGHLIGHT, Color.FromArgb(200, 200, 255), o["ObjectCombos"]["MacroObjects-Item-Highlight"]);
                    LoadColor(ref COMBOS_MACRO_HIGHLIGHT_TEXT, DEFAULT_TEXT, o["ObjectCombos"]["MacroObjects-Item-Highlight-Text"]);

                    LoadColor(ref COMBOS_SPECIAL_TITLE, Color.DarkGreen, o["ObjectCombos"]["SpecialObjects-Title"]);
                    LoadColor(ref COMBOS_SPECIAL_MAIN, Color.FromArgb(250, 250, 250), o["ObjectCombos"]["SpecialObjects-Item"]);
                    LoadColor(ref COMBOS_SPECIAL_SECONDARY, Color.FromArgb(240, 250, 240), o["ObjectCombos"]["SpecialObjects-Item-Alt"]);
                    LoadColor(ref COMBOS_SPECIAL_HIGHLIGHT, Color.FromArgb(200, 200, 255), o["ObjectCombos"]["SpecialObjects-Item-Highlight"]);
                    LoadColor(ref COMBOS_SPECIAL_HIGHLIGHT_TEXT, DEFAULT_TEXT, o["ObjectCombos"]["SpecialObjects-Item-Highlight-Text"]);

                    LoadColor(ref COMBOS_OTHER_TITLE, Color.DimGray, o["ObjectCombos"]["Other-Title"]);
                    LoadColor(ref COMBOS_OTHER_MAIN, Color.FromArgb(250, 250, 250), o["ObjectCombos"]["Other-Item"]);
                    LoadColor(ref COMBOS_OTHER_SECONDARY, Color.FromArgb(240, 240, 240), o["ObjectCombos"]["Other-Item-Alt"]);
                    LoadColor(ref COMBOS_OTHER_HIGHLIGHT, Color.FromArgb(200, 200, 255), o["ObjectCombos"]["Other-Item-Highlight"]);
                    LoadColor(ref COMBOS_OTHER_HIGHLIGHT_TEXT, DEFAULT_TEXT, o["ObjectCombos"]["Other-Item-Highlight-Text"]);
                }

            }
            else
            {
                Console.WriteLine("THEME ERROR: File: \"" + filepath + "\" could not be found!");
            }
        }
    }
}
