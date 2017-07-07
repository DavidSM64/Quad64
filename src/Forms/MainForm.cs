using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;
using PropertyGridExtensionHacks;
using Quad64.src.LevelInfo;
using Quad64.Scripts;
using Quad64.src.JSON;
using Quad64.src;
using Quad64.src.Viewer;
using System.IO;
using Quad64.src.TestROM;
using Quad64.src.Forms;

namespace Quad64
{
    partial class MainForm : Form
    {
        Model3D model1 = new Model3D(), model2 = new Model3D();
        Color bgColor = Color.Black;
        Camera camera = new Camera();
        Vector3 savedCamPos = new Vector3();
        Matrix4 camMtx = Matrix4.Identity;
        Matrix4 ProjMatrix;
        bool isMouseDown = false, isShiftDown = false, moveState = false;
        static Level level;
        float FOV = 1.048f;

        public Level getLevelData { get { return level; } }

        public object SettingsForms { get; private set; }

        private short keepDegreesWithin360(short value)
        {
            if (value < 0)
                return (short)(360 + value);
            else
                return (short)(value % 360);
        }
        
        public MainForm()
        {
            InitializeComponent();
            SettingsFile.LoadGlobalSettings("default");
            glControl1.MouseWheel += new MouseEventHandler(glControl1_Wheel);
            ProjMatrix = Matrix4.CreatePerspectiveFieldOfView(FOV, (float)glControl1.Width/(float)glControl1.Height, 100f, 100000f);
            glControl1.Enabled = false;
            KeyPreview = true;
            treeView1.HideSelection = false;
            camera.updateMatrix(ref camMtx);
            //foreach(ObjectComboEntry entry in Globals.objectComboEntries) Console.WriteLine(entry.ToString());
        }

        private void loadROM(bool startingUp)
        {
            ROM rom = ROM.Instance;
            if (startingUp && !Globals.pathToAutoLoadROM.Equals(""))
            {
                rom.readFile(Globals.pathToAutoLoadROM);
            }
            else
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.Filter = "Z64 ROM|*.z64|V64 ROM|*.v64|N64 ROM|*.n64|All Files|*";
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    ROM.Instance.readFile(openFileDialog1.FileName);
                }
                else
                {
                    return;
                }
            }
            Globals.objectComboEntries.Clear();
            ModelComboFile.parseObjectCombos(Globals.getDefaultObjectComboPath());

            rom.setSegment(0x15, Globals.seg15_location[0], Globals.seg15_location[1], false);
            rom.setSegment(0x02, Globals.seg02_location[0], Globals.seg02_location[1], rom.isSegmentMIO0(0x02));

            level = new Level(0x10, 1);
            LevelScripts.parse(ref level, 0x15, 0);
            level.sortAndAddNoModelEntries();
            level.CurrentAreaID = level.Areas[0].AreaID;
            refreshObjectsInList();
            glControl1.Enabled = true;
            bgColor = Color.CornflowerBlue;
            camera.setLevel(level);
            updateAreaButtons();
            glControl1.Invalidate();
        }

        private void refreshObjectsInList()
        {
            Globals.list_selected = -1;
            Globals.item_selected = -1;
            propertyGrid1.SelectedObject = null;
            TreeNode objects = treeView1.Nodes[0];
            objects.Nodes.Clear();
            foreach (Object3D obj in level.getCurrentArea().Objects)
            {
                obj.Title = obj.getObjectComboName();
                objects.Nodes.Add(obj.Title);
               // objects.Nodes.Add("0x" + obj.Behavior.ToString("X8"));
            }

            TreeNode macro_objects = treeView1.Nodes[1];
            macro_objects.Nodes.Clear();
            foreach (Object3D obj in level.getCurrentArea().MacroObjects)
            {
                obj.Title = obj.getObjectComboName();
                macro_objects.Nodes.Add(obj.Title);
                //macro_objects.Nodes.Add("0x" + obj.Behavior.ToString("X8"));
            }

            TreeNode special_objects = treeView1.Nodes[2];
            special_objects.Nodes.Clear();
            foreach (Object3D obj in level.getCurrentArea().SpecialObjects)
            {
                obj.Title = obj.getObjectComboName();
                special_objects.Nodes.Add(obj.Title);
                //special_objects.Nodes.Add("0x" + obj.Behavior.ToString("X8"));
            }

            TreeNode warps = treeView1.Nodes[3];
            warps.Nodes.Clear();
            foreach (Warp warp in level.getCurrentArea().Warps)
            {
                warps.Nodes.Add(warp.ToString());
            }
            foreach (Warp warp in level.getCurrentArea().PaintingWarps)
            {
                warps.Nodes.Add(warp.ToString());
            }
            foreach (WarpInstant warp in level.getCurrentArea().InstantWarps)
            {
                warps.Nodes.Add(warp.ToString());
            }
        }
        
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(bgColor);
            if (level != null)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadMatrix(ref ProjMatrix);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref camMtx);

                //level.getCurrentArea().drawPicking();
                level.getCurrentArea().drawEverything();

                glControl1.SwapBuffers();
            }
        }
        
        private void selectObject(int mx, int my)
        {
            int h = glControl1.Height;
            //Console.WriteLine("Picking... mx = "+mx+", my = "+my);
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f); // Set background to solid white
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref ProjMatrix);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camMtx);
            level.getCurrentArea().collision.drawCollisionMap(true); // Draw collision map as solid black
            level.getCurrentArea().drawPicking(); // Draw solid color object bounding boxes
            byte[] pixel = new byte[4];
            GL.ReadPixels(mx, h - my, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);
            if (pixel[0] == pixel[1] && pixel[1] == pixel[2])
            {
                if(pixel[2] == 255 || pixel[2] == 0)
                    return; // If a pixel is fully white or fully black, then ignore picking.
            }
            if (pixel[2] > 0 && pixel[2] < 4)
            {
                Globals.list_selected = pixel[2] - 1;
                Globals.item_selected = (pixel[1] * 256) + pixel[0];
                treeView1.SelectedNode = 
                    treeView1.Nodes[Globals.list_selected].Nodes[Globals.item_selected];
                switch (Globals.list_selected)
                {
                    case 0:
                        propertyGrid1.SelectedObject = 
                            level.getCurrentArea().Objects[Globals.item_selected];
                        break;
                    case 1:
                        propertyGrid1.SelectedObject = 
                            level.getCurrentArea().MacroObjects[Globals.item_selected];
                        break;
                    case 2:
                        propertyGrid1.SelectedObject = 
                            level.getCurrentArea().SpecialObjects[Globals.item_selected];
                        break;
                }
                if (camera.isOrbitCamera())
                {
                    camera.updateOrbitCamera(ref camMtx);
                    glControl1.Invalidate();
                }
            }
            Color pickedColor = Color.FromArgb(pixel[0], pixel[1], pixel[2]);
            //Console.WriteLine(pickedColor.ToString());
            //Console.WriteLine("Picking Done");
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            savedCamPos = camera.Position;
            if (e.Button == MouseButtons.Right)
            {
                selectObject(e.X, e.Y);
                glControl1.Invalidate();
            }
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            camera.resetMouseStuff();
            isMouseDown = false;
            if (!isShiftDown)
                moveState = false;
        }

        private void glControl1_MouseLeave(object sender, EventArgs e)
        {
            camera.resetMouseStuff();
            isMouseDown = false;
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && e.Button == MouseButtons.Left)
            {
                if (!isShiftDown && !moveState)
                {
                    camera.updateCameraMatrixWithMouse(e.X, e.Y, ref camMtx);
                }
                else
                {
                    moveState = true;
                    camera.updateCameraOffsetWithMouse(savedCamPos, e.X, e.Y, glControl1.Width, glControl1.Height, ref camMtx);
                }
                glControl1.Invalidate();
            }
        }

        private void glControl1_Wheel(object sender, MouseEventArgs e)
        {
            camera.resetMouseStuff();
            camera.updateCameraMatrixWithScrollWheel((int)(e.Delta * 1.5f), ref camMtx);
            savedCamPos = camera.Position;
            glControl1.Invalidate();
        }

        private void glControl1_KeyUp(object sender, KeyEventArgs e)
        {
            isShiftDown = e.Shift;
            if (!isMouseDown)
                moveState = false;
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            isShiftDown = e.Shift;
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            glControl1.Context.Update(glControl1.WindowInfo);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            ProjMatrix = Matrix4.CreatePerspectiveFieldOfView(FOV, (float)glControl1.Width/(float)glControl1.Height, 100f, 100000f);
            glControl1.Invalidate();
        }
        
        private void loadROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadROM(false);
        }

        private void saveROMAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Z64 ROM|*.z64|V64 ROM|*.v64|N64 ROM|*.n64|All Files|*";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                if(saveFileDialog1.FilterIndex == 1)
                    ROM.Instance.saveFileAs(saveFileDialog1.FileName, ROM_Endian.BIG);
                else if (saveFileDialog1.FilterIndex == 2)
                    ROM.Instance.saveFileAs(saveFileDialog1.FileName, ROM_Endian.MIXED);
                else if (saveFileDialog1.FilterIndex == 3)
                    ROM.Instance.saveFileAs(saveFileDialog1.FileName, ROM_Endian.LITTLE);
            }
        }

        private void saveROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ROM.Instance.saveFileAs(ROM.Instance.Filepath, ROM.Instance.Endian);
        }
        
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
            glControl1.Invalidate();
            propertyGrid1.Refresh();
            glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
        }

        private void objectComboPresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectComboPreset comboWindow;
            switch (Globals.list_selected)
            {
                case 0:
                    comboWindow =
                        new SelectComboPreset(level, 0, "Select Object Combos", Color.DarkRed);
                    comboWindow.ShowDialog();
                    if(comboWindow.ClickedSelect)
                    {
                        Area area = level.getCurrentArea();
                        area.Objects[Globals.item_selected].ModelID = comboWindow.ReturnObjectCombo.ModelID;
                        area.Objects[Globals.item_selected].setBehaviorFromAddress(comboWindow.ReturnObjectCombo.Behavior);
                        treeView1.Nodes[0].Nodes[Globals.item_selected].Text
                            = area.Objects[Globals.item_selected].getObjectComboName();
                        area.Objects[Globals.item_selected].SetBehaviorParametersToZero();
                        area.Objects[Globals.item_selected].UpdateProperties();
                    }
                    break;
                case 1:
                    comboWindow =
                        new SelectComboPreset(level, 1, "Select Macro Preset", Color.DarkBlue);
                    comboWindow.ShowDialog();
                    if (comboWindow.ClickedSelect)
                    {
                        Console.WriteLine(comboWindow.ReturnPresetMacro.PresetID);
                        Area area = level.getCurrentArea();
                        area.MacroObjects[Globals.item_selected].ModelID = comboWindow.ReturnPresetMacro.ModelID;
                        area.MacroObjects[Globals.item_selected].setPresetID(comboWindow.ReturnPresetMacro.PresetID);
                        area.MacroObjects[Globals.item_selected].setBehaviorFromAddress(comboWindow.ReturnPresetMacro.Behavior);
                        //area.MacroObjects[Globals.item_selected].SetBehaviorParametersToZero();
                        area.MacroObjects[Globals.item_selected].BehaviorParameter1
                            = comboWindow.ReturnPresetMacro.BehaviorParameter1;
                        area.MacroObjects[Globals.item_selected].BehaviorParameter2
                            = comboWindow.ReturnPresetMacro.BehaviorParameter2;
                        treeView1.Nodes[1].Nodes[Globals.item_selected].Text
                            = area.MacroObjects[Globals.item_selected].getObjectComboName();
                        area.MacroObjects[Globals.item_selected].UpdateProperties();
                    }
                    break;
                case 2:
                    {
                        Object3D obj = getSelectedObject();
                        int specialListType = 2;
                        if (obj.isPropertyShown(Object3D.FLAGS.BPARAM_1))
                            specialListType = 4;
                        else if (obj.isPropertyShown(Object3D.FLAGS.ROTATION_Y))
                            specialListType = 3;
                        comboWindow =
                            new SelectComboPreset(level, specialListType, "Select Special Preset", Color.DarkGreen);
                        comboWindow.ShowDialog();
                        if (comboWindow.ClickedSelect)
                        {
                            Console.WriteLine(comboWindow.ReturnPresetMacro.PresetID);
                            Area area = level.getCurrentArea();
                            area.SpecialObjects[Globals.item_selected].ModelID = comboWindow.ReturnPresetMacro.ModelID;
                            area.SpecialObjects[Globals.item_selected].setPresetID(comboWindow.ReturnPresetMacro.PresetID);
                            area.SpecialObjects[Globals.item_selected].setBehaviorFromAddress(comboWindow.ReturnPresetMacro.Behavior);
                            //area.SpecialObjects[Globals.item_selected].SetBehaviorParametersToZero();
                            area.SpecialObjects[Globals.item_selected].BehaviorParameter1
                                = comboWindow.ReturnPresetMacro.BehaviorParameter1;
                            area.SpecialObjects[Globals.item_selected].BehaviorParameter2
                                = comboWindow.ReturnPresetMacro.BehaviorParameter2;
                            treeView1.Nodes[2].Nodes[Globals.item_selected].Text
                                = area.SpecialObjects[Globals.item_selected].getObjectComboName();
                            area.SpecialObjects[Globals.item_selected].UpdateProperties();
                        }
                        break;
                    }
            }
            glControl1.Invalidate();
            propertyGrid1.Refresh();
            glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            string glString = GL.GetString(StringName.Version).Split(' ')[0];
            if (glString.StartsWith("1."))
            {
                MessageBox.Show(
                    "Error: You have an outdated version of OpenGL, which is not supported by this program."+
                    " The program will now exit.\n\n" +
                    "OpenGL version: [" + GL.GetString(StringName.Version) + "]\n",
                    "OpenGL version error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
                Close();
            }
            Text += " (OpenGL v" + glString + ")";
            loadROM(Globals.autoLoadROMOnStartup);
        }

        private void updateAreaButtons()
        {
            int areas = 0x00;
            foreach (Area area in level.Areas)
                areas |= (1 << area.AreaID);
            Area0Button.Enabled = ((areas & 0x1) == 0x1);
            Area1Button.Enabled = ((areas & 0x2) == 0x2);
            Area2Button.Enabled = ((areas & 0x4) == 0x4);
            Area3Button.Enabled = ((areas & 0x8) == 0x8);
            Area4Button.Enabled = ((areas & 0x10) == 0x10);
            Area5Button.Enabled = ((areas & 0x20) == 0x20);
            Area6Button.Enabled = ((areas & 0x40) == 0x40);
            Area7Button.Enabled = ((areas & 0x80) == 0x80);
        }

        private void trySwitchArea(ushort toArea)
        {
            if (level.getCurrentArea().AreaID == toArea)
                return;

            if (level.hasArea(toArea))
            {
                level.CurrentAreaID = toArea;
                refreshObjectsInList();
                glControl1.Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.P:
                    if (Globals.list_selected != -1 && Globals.item_selected != -1)
                    {
                        int listSel = Globals.list_selected;
                        int objSel = Globals.item_selected;
                        Object3D obj = getSelectedObject();
                        if (obj == null) return;
                        string newName = Prompts.ShowInputDialog("Type the new combo name", "New combo name");
                        if (newName.Length > 0)
                        {
                            obj.Title = newName;
                            uint segmentAddress = 0;
                            if(level.ModelIDs.ContainsKey(obj.ModelID))
                                segmentAddress = level.ModelIDs[obj.ModelID].GeoDataSegAddress;
                            ObjectComboEntry oce = new ObjectComboEntry(newName, obj.ModelID,
                                segmentAddress, obj.getBehaviorAddress());
                            Globals.insertNewEntry(oce);
                            refreshObjectsInList();
                            treeView1.SelectedNode = treeView1.Nodes[listSel].Nodes[objSel];
                            Globals.list_selected = listSel;
                            Globals.item_selected = objSel;
                            propertyGrid1.Refresh();
                        }
                        ModelComboFile.writeObjectCombosFile(Globals.getDefaultObjectComboPath());
                        Console.WriteLine("Saved Object Combos!");
                    }
                    break;
                case Keys.D1:
                    trySwitchArea(1);
                    break;
                case Keys.D2:
                    trySwitchArea(2);
                    break;
                case Keys.D3:
                    trySwitchArea(3);
                    break;
                case Keys.D4:
                    trySwitchArea(4);
                    break;
                case Keys.D5:
                    trySwitchArea(5);
                    break;
                case Keys.D6:
                    trySwitchArea(6);
                    break;
                case Keys.D7:
                    trySwitchArea(7);
                    break;
                case Keys.D0:
                    trySwitchArea(0);
                    break;
            }
        }

        private void resetObjectVariables()
        {
            radioButton1.Checked = true;
            treeView1.SelectedNode = null;
            Globals.list_selected = -1;
            Globals.item_selected = -1;
        }

        private void selectLeveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Opening SelectLevelForm!");
            SelectLevelForm newLevel = new SelectLevelForm(level.LevelID);
            newLevel.ShowDialog();
            if (newLevel.changeLevel)
            {
                //Console.WriteLine("Changing Level to " + newLevel.levelID);
                level = new Level(newLevel.levelID, 1);
                camera.setCameraMode(CameraMode.FLY, ref camMtx);
                camera.setLevel(level);
                LevelScripts.parse(ref level, 0x15, 0);
                level.sortAndAddNoModelEntries();
                level.CurrentAreaID = level.Areas[0].AreaID;
                resetObjectVariables();
                refreshObjectsInList();
                glControl1.Invalidate();
                updateAreaButtons();
            }
        }


        private void testROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchROM.OpenEmulator();
            SettingsFile.SaveGlobalSettings("default");
        }

        private void rOMInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ROMInfoForm romInfo = new ROMInfoForm();
            romInfo.ShowDialog();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode node = e.Node;
            //Console.WriteLine("Selected: " + node.Text);
            if (node.Parent == null)
            {
                propertyGrid1.SelectedObject = null;
                Globals.list_selected = -1;
                Globals.item_selected = -1;
                objectComboPresetToolStripMenuItem.Enabled = false;
            }
            else
            {
                objectComboPresetToolStripMenuItem.Enabled = true;
                if (node.Parent.Text.Equals("3D Objects"))
                {
                    Globals.list_selected = 0;
                    Globals.item_selected = node.Index;
                    propertyGrid1.SelectedObject = level.getCurrentArea().Objects[node.Index];
                    if (camera.isOrbitCamera())
                    {
                        camera.updateOrbitCamera(ref camMtx);
                        glControl1.Invalidate();
                    }
                    //int addr = level.getCurrentArea().Objects[node.Index].getROMAddress();
                    //ROM.Instance.printROMSection(addr, addr + 0x18);
                }
                else if (node.Parent.Text.Equals("Macro 3D Objects"))
                {
                    Globals.list_selected = 1;
                    Globals.item_selected = node.Index;
                    propertyGrid1.SelectedObject = level.getCurrentArea().MacroObjects[node.Index];
                    if (camera.isOrbitCamera())
                    {
                        camera.updateOrbitCamera(ref camMtx);
                        glControl1.Invalidate();
                    }
                    //int addr = level.getCurrentArea().MacroObjects[node.Index].getROMAddress();
                    //ROM.Instance.printROMSection(addr, addr + 10);
                }
                else if (node.Parent.Text.Equals("Special 3D Objects"))
                {
                    Globals.list_selected = 2;
                    Globals.item_selected = node.Index;
                    propertyGrid1.SelectedObject = level.getCurrentArea().SpecialObjects[node.Index];
                    if (camera.isOrbitCamera())
                    {
                        camera.updateOrbitCamera(ref camMtx);
                        glControl1.Invalidate();
                    }
                    //int addr = level.getCurrentArea().SpecialObjects[node.Index].getROMAddress();
                    //ROM.Instance.printROMSection(addr, addr + 12);
                }
                else if (node.Parent.Text.Equals("Warps"))
                {
                    Globals.list_selected = 3;
                    Globals.item_selected = node.Index;
                    Area area = level.getCurrentArea();
                    if (node.Index < area.Warps.Count)
                        propertyGrid1.SelectedObject = area.Warps[node.Index];
                    else if (node.Index < area.Warps.Count + area.PaintingWarps.Count)
                        propertyGrid1.SelectedObject = area.PaintingWarps[node.Index - area.Warps.Count];
                    else
                        propertyGrid1.SelectedObject = area.InstantWarps[node.Index - area.Warps.Count - area.PaintingWarps.Count];
                }
            }
            
            Object3D obj = getSelectedObject();
            if (obj != null)
            {
                if(obj.IsReadOnly)
                    objectComboPresetToolStripMenuItem.Enabled = false;
                obj.UpdateProperties();
                propertyGrid1.Refresh();
            }
            
            glControl1.Invalidate();
            glControl1.Update();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            fovText.Text = "FOV: " + trackBar1.Value + "°";
            FOV = trackBar1.Value * ((float)Math.PI/180.0f);
            if (FOV < 0.1f)
                FOV = 0.1f;
            ProjMatrix = Matrix4.CreatePerspectiveFieldOfView(FOV, (float)glControl1.Width / (float)glControl1.Height, 100f, 100000f);
            glControl1.Invalidate();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string label = e.ChangedItem.Label;
            if (Globals.list_selected > -1 && Globals.list_selected < 3)
            {
                Object3D obj = getSelectedObject();
                if (obj == null) return;
                if (label.Equals("All Acts"))
                {
                    obj.ShowHideActs((bool)e.ChangedItem.Value);
                    propertyGrid1.Refresh();
                }
                else if(label.Equals("Behavior") || label.Equals("Model ID"))
                {
                    if(Globals.item_selected > -1)
                        treeView1.Nodes[Globals.list_selected].Nodes[Globals.item_selected].Text
                            = obj.getObjectComboName();
                }
                obj.updateROMData();
                if (camera.isOrbitCamera())
                    camera.updateOrbitCamera(ref camMtx);
                glControl1.Invalidate();
            }
            else if (Globals.list_selected == 3)
            {
                object warp = getSelectedWarp();
                string warpTypeName = warp.GetType().Name;
                if (warpTypeName.Equals("Warp"))
                {
                    ((Warp)warp).updateROMData();
                }
                else if (warpTypeName.Equals("WarpInstant"))
                {
                    ((WarpInstant)warp).updateROMData();
                }
            }
        }

        // I never want CategorizedAlphabetical, so I change it back to Categorized if detected.
        private void propertyGrid1_PropertySortChanged(object sender, EventArgs e)
        {
            if (propertyGrid1.PropertySort == PropertySort.CategorizedAlphabetical)
                propertyGrid1.PropertySort = PropertySort.Categorized;
        }

        /* 
        Taken from: https://stackoverflow.com/a/21199864. This basically makes it 
        so that the object name in the list will always stay highlighted.
        */
        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node == null) return;

            // if treeview's HideSelection property is "True", 
            // this will always returns "False" on unfocused treeview
            var selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
            var unfocused = !e.Node.TreeView.Focused;

            // we need to do owner drawing only on a selected node
            // and when the treeview is unfocused, else let the OS do it for us
            if (selected && unfocused)
            {
                var font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, SystemColors.HighlightText, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                e.DrawDefault = true;
            }
        }
        
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                camera.setCameraMode(CameraMode.ORBIT, ref camMtx);
                camera.updateMatrix(ref camMtx);
                glControl1.Invalidate();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                camera.setCameraMode(CameraMode.FLY, ref camMtx);
                camera.updateMatrix(ref camMtx);
                glControl1.Invalidate();
            }
        }


        private void starAct_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton box = (RadioButton)sender;
            if (box.Checked)
                box.BackgroundImage = Properties.Resources.icon_Star1;
            else
                box.BackgroundImage = Properties.Resources.icon_Star1_gray;
        }


        int moveCam_InOut_lastPosY = 0;
        bool moveCam_InOut_mouseDown = false;
        private void moveCam_InOut_MouseDown(object sender, MouseEventArgs e)
        {
            moveCam_InOut_mouseDown = true;
            moveCam_InOut_lastPosY = e.Y;
        }
        private void moveCam_InOut_MouseUp(object sender, MouseEventArgs e)
        {
            moveCam_InOut_mouseDown = false;
        }
        private void moveCam_InOut_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveCam_InOut_mouseDown)
            {
                camera.resetMouseStuff();
                camera.updateCameraMatrixWithScrollWheel((e.Y - moveCam_InOut_lastPosY) * -10, ref camMtx);
                savedCamPos = camera.Position;
                moveCam_InOut_lastPosY = e.Y;
                glControl1.Invalidate();
            }
        }
        
        bool moveCam_strafe_mouseDown = false;
        private void moveCam_strafe_MouseDown(object sender, MouseEventArgs e)
        {
            savedCamPos = camera.Position;
            moveCam_strafe_mouseDown = true;
        }
        private void moveCam_strafe_MouseUp(object sender, MouseEventArgs e)
        {
            camera.resetMouseStuff();
            moveCam_strafe_mouseDown = false;
        }

        private void moveCam_strafe_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveCam_strafe_mouseDown)
            {
                camera.updateCameraOffsetWithMouse(savedCamPos, e.X, e.Y, glControl1.Width, glControl1.Height, ref camMtx);
                glControl1.Invalidate();
            }
        }
        
        private Object3D getSelectedObject()
        {
            if (Globals.list_selected == -1 || Globals.item_selected == -1)
                return null;
            switch (Globals.list_selected)
            {
                case 0:
                    return level.getCurrentArea().Objects[Globals.item_selected];
                case 1:
                    return level.getCurrentArea().MacroObjects[Globals.item_selected];
                case 2:
                    return level.getCurrentArea().SpecialObjects[Globals.item_selected];
                default:
                    return null;
            }
        }

        private object getSelectedWarp()
        {
            if (Globals.list_selected == -1 || Globals.item_selected == -1)
                return null;
            switch (Globals.list_selected)
            {
                case 3:
                    {
                        Area area = level.getCurrentArea();
                        int index = Globals.item_selected;
                        if (index < area.Warps.Count)
                        {
                            propertyGrid1.SelectedObject = area.Warps[index];
                        }
                        else if (index < area.Warps.Count + area.PaintingWarps.Count)
                        {
                            propertyGrid1.SelectedObject = area.PaintingWarps[index - area.Warps.Count];
                        }
                        else
                        {
                            propertyGrid1.SelectedObject = area.InstantWarps[index - area.Warps.Count - area.PaintingWarps.Count];
                        }
                        return propertyGrid1.SelectedObject;
                    }
                default:
                    return null;
            }
        }

        bool moveObj_mouseDown = false;
        int moveObj_lastMouseX = 0;
        int moveObj_lastMouseY = 0;
        short moveObj_savedX=0, moveObj_savedY=0, moveObj_savedZ=0;
        private void moveObj_MouseDown(object sender, MouseEventArgs e)
        {
            if (Globals.item_selected > -1 && Globals.list_selected > -1)
            {
                Object3D obj = getSelectedObject();
                if (obj == null) return;
                if (obj.IsReadOnly) return;
                moveObj_mouseDown = true;
                moveObj_lastMouseX = e.X;
                moveObj_lastMouseY = e.Y;
                moveObj_savedX = obj.xPos;
                moveObj_savedY = obj.yPos;
                moveObj_savedZ = obj.zPos;
            }
        }
        private void moveObj_MouseUp(object sender, MouseEventArgs e)
        {
            moveObj_mouseDown = false;
            Object3D obj = getSelectedObject();
            if (obj != null)
                obj.updateROMData();
        }
        private void moveObj_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveObj_mouseDown)
            {
                if (Globals.item_selected > -1 && Globals.list_selected > -1)
                {
                    Object3D obj = getSelectedObject();
                    if (obj == null) return;
                    if (obj.IsReadOnly) return;
                    short speedMult = 30;

                    int mx = e.X - moveObj_lastMouseX;
                    int my = -(e.Y - moveObj_lastMouseY);

                    float CX = (float)Math.Sin(camera.Yaw);
                    float CZ = (float)-Math.Cos(camera.Yaw);
                    float CX_2 = (float)Math.Sin(camera.Yaw + (Math.PI / 2));
                    float CZ_2 = (float)-Math.Cos(camera.Yaw + (Math.PI / 2));

                    if (obj.isPropertyShown(Object3D.FLAGS.POSITION_X))
                        obj.xPos = (short)(moveObj_savedX - (short)(CX * my * speedMult * Globals.objSpeedMultiplier) - (short)(CX_2 * mx * speedMult * Globals.objSpeedMultiplier));
                    if (obj.isPropertyShown(Object3D.FLAGS.POSITION_Z))
                        obj.zPos = (short)(moveObj_savedZ - (short)(CZ * my * speedMult * Globals.objSpeedMultiplier) - (short)(CZ_2 * mx * speedMult * Globals.objSpeedMultiplier));
                    if (keepOnGround.Checked)
                        dropObjectToGround();
                    if (camera.isOrbitCamera())
                        camera.updateOrbitCamera(ref camMtx);
                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                }
            }
        }
        
        bool moveObj_UpDown_mouseDown = false;
        int moveObj_UpDown_lastMouseY = 0;
        private void movObj_UpDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (Globals.item_selected > -1 && Globals.list_selected > -1)
            {
                moveObj_UpDown_lastMouseY = e.Y;
                moveObj_UpDown_mouseDown = true;
            }
        }
        private void movObj_UpDown_MouseUp(object sender, MouseEventArgs e)
        {
            moveObj_UpDown_mouseDown = false;
            Object3D obj = getSelectedObject();
            if (obj != null)
                obj.updateROMData();
        }
        private void movObj_UpDown_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveObj_UpDown_mouseDown)
            {
                if (Globals.item_selected > -1 && Globals.list_selected > -1)
                {
                    Object3D obj = getSelectedObject();
                    if (obj == null) return;
                    if (obj.IsReadOnly) return;
                    obj.yPos -= (short)(30 * (e.Y - moveObj_UpDown_lastMouseY) * Globals.objSpeedMultiplier);
                    if (camera.isOrbitCamera())
                        camera.updateOrbitCamera(ref camMtx);
                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                    moveObj_UpDown_lastMouseY = e.Y;
                }
            }
        }
        
        bool rotObj_mouseDown = false;
        int rotObj_lastMouseX = 0;
        int rotObj_lastMouseY = 0;
        short rotObj_savedX = 0, rotObj_savedY = 0, rotObj_savedZ = 0;

        private void rotObj_MouseDown(object sender, MouseEventArgs e)
        {
            if (Globals.item_selected > -1 && Globals.list_selected > -1)
            {
                Object3D obj = getSelectedObject();
                if (obj == null) return;
                if (obj.IsReadOnly) return;
                rotObj_mouseDown = true;
                rotObj_lastMouseX = e.X;
                rotObj_lastMouseY = e.Y;
                rotObj_savedX = obj.xRot;
                rotObj_savedY = obj.yRot;
                rotObj_savedZ = obj.zRot;
            }
        }

        private void rotObj_MouseUp(object sender, MouseEventArgs e)
        {
            rotObj_mouseDown = false;
            Object3D obj = getSelectedObject();
            if (obj != null)
                obj.updateROMData();
        }

        private void rotObj_MouseMove(object sender, MouseEventArgs e)
        {
            if (rotObj_mouseDown)
            {
                if (Globals.item_selected > -1 && Globals.list_selected > -1)
                {
                    Object3D obj = getSelectedObject();
                    if (obj == null) return;
                    if (obj.IsReadOnly) return;
                    float speedMult = 0.5f;

                    int mx = e.X - rotObj_lastMouseX;
                    int my = -(e.Y - rotObj_lastMouseY);

                    float CZ = (float)Math.Sin(camera.Yaw);
                    float CX = (float)-Math.Cos(camera.Yaw);
                    float CZ_2 = (float)Math.Sin(camera.Yaw + (Math.PI / 2));
                    float CX_2 = (float)-Math.Cos(camera.Yaw + (Math.PI / 2));
                    if (obj.isPropertyShown(Object3D.FLAGS.ROTATION_X))
                    {
                        obj.xRot = (short)(rotObj_savedX - (short)(CX * my * speedMult * Globals.objSpeedMultiplier) - (short)(CX_2 * mx * speedMult * Globals.objSpeedMultiplier));
                        obj.xRot = keepDegreesWithin360(obj.xRot);
                    }
                    if (obj.isPropertyShown(Object3D.FLAGS.ROTATION_Z))
                    {
                        obj.zRot = (short)(rotObj_savedZ - (short)(CZ * my * speedMult * Globals.objSpeedMultiplier) - (short)(CZ_2 * mx * speedMult * Globals.objSpeedMultiplier));
                        obj.zRot = keepDegreesWithin360(obj.zRot);
                    }

                    if (camera.isOrbitCamera())
                        camera.updateOrbitCamera(ref camMtx);
                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                }
            }
        }

        private void treeView1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dropObjectToGround()
        {
            if (Globals.item_selected > -1 && Globals.list_selected > -1)
            {
                Object3D obj = getSelectedObject();
                if (obj == null) return;
                obj.yPos = level.getCurrentArea().collision.dropToGround(new Vector3(obj.xPos, obj.yPos, obj.zPos));
                if (camera.isOrbitCamera())
                    camera.updateOrbitCamera(ref camMtx);
                glControl1.Invalidate();
                propertyGrid1.Refresh();
                glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
            }
        }

        private void dropToGround_Click(object sender, EventArgs e)
        {
            dropObjectToGround();
        }
        
        private void AreaButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ushort area = ushort.Parse(item.Text.Substring(item.Text.LastIndexOf(" ")+1));
            trySwitchArea(area);
        }

        bool rotObj_Yaw_mouseDown = false;
        int rotObj_Yaw_lastMouseY = 0;
        private void rotObj_Yaw_MouseDown(object sender, MouseEventArgs e)
        {
            if (Globals.item_selected > -1 && Globals.list_selected > -1)
            {
                rotObj_Yaw_lastMouseY = e.Y;
                rotObj_Yaw_mouseDown = true;
            }
        }

        private void rotObj_Yaw_MouseUp(object sender, MouseEventArgs e)
        {
            rotObj_Yaw_mouseDown = false;
            Object3D obj = getSelectedObject();
            if (obj != null)
                obj.updateROMData();
        }

        private void rotObj_Yaw_MouseMove(object sender, MouseEventArgs e)
        {
            if (rotObj_Yaw_mouseDown)
            {
                if (Globals.item_selected > -1 && Globals.list_selected > -1)
                {
                    Object3D obj = getSelectedObject();
                    if (obj == null) return;
                    if (obj.IsReadOnly) return;
                    if (obj.isPropertyShown(Object3D.FLAGS.ROTATION_Y))
                    {
                        obj.yRot -= (short)((e.Y - rotObj_Yaw_lastMouseY) * Globals.objSpeedMultiplier);
                        obj.yRot = keepDegreesWithin360(obj.yRot);
                    }
                    if (camera.isOrbitCamera())
                        camera.updateOrbitCamera(ref camMtx);
                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                    rotObj_Yaw_lastMouseY = e.Y;
                }
            }
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            float newValue;
            if (trackBar3.Value > 50)
                newValue = 100.0f+((trackBar3.Value-50)*8f);
            else
                newValue = (trackBar3.Value/50.0f) * 100f;
            
            if (newValue < 1f)
                newValue = 1f;
            else if (newValue > 96f && newValue < 114f)
                newValue = 100f;

            camSpeedLabel.Text = (int)(newValue) + "%";
            Globals.camSpeedMultiplier = newValue/100.0f;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {

            float newValue;
            if (trackBar2.Value > 50)
                newValue = 100.0f + ((trackBar2.Value - 50) * 8f);
            
            else
               newValue = (trackBar2.Value / 50.0f) * 100f;
            if (newValue < 1f)
                newValue = 1f;
            else if (newValue > 96f && newValue < 114f)
                newValue = 100f;

            objSpeedLabel.Text = (int)(newValue) + "%";
            Globals.objSpeedMultiplier = newValue / 100.0f;
        }
    }
}
