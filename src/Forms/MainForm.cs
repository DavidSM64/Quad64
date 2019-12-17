using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;
using Quad64.src.LevelInfo;
using Quad64.Scripts;
using Quad64.src.JSON;
using Quad64.src;
using Quad64.src.TestROM;
using Quad64.src.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Quad64.src.Forms.ToolStripRenderer;
using System.IO;
using Newtonsoft.Json;

namespace Quad64
{
    partial class MainForm : Form
    {
        Color bgColor = Color.Black;
        Camera camera = new Camera();
        Vector3 savedCamPos = new Vector3();
        Matrix4 camMtx = Matrix4.Identity;
        Matrix4 ProjMatrix;
        MultiselectTreeView treeView1;
        readonly System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        bool isMouseDown = false, isShiftDown = false, isControlDown = false, moveState = false;
        bool gridEnabled = false;
        static Level level;
        
        public Level getLevelData { get { return level; } }

        public object SettingsForms { get; private set; }

        private short keepDegreesWithin360(short value)
        {
            if (value < 0)
                return (short)(360 + value);
            else
                return (short)(value % 360);
        }

        private TreeNode makeTreeNode(string text, Color color)
        {
            TreeNode newNode = new TreeNode();
            newNode.Text = text;
            newNode.ForeColor = color;
            return newNode;
        }

        private void initTreeView()
        {
            treeView1 = new MultiselectTreeView();
            treeView1.Name = "treeView1";
            treeView1.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            treeView1.Font = new Font("Segoe UI", 8.0f, FontStyle.Bold);
            treeView1.Nodes.Add(makeTreeNode("3D Objects", Theme.MAIN_TREEVIEW_3DOBJECTS));
            treeView1.Nodes.Add(makeTreeNode("Macro 3D Objects", Theme.MAIN_TREEVIEW_MACRO));
            treeView1.Nodes.Add(makeTreeNode("Special 3D Objects", Theme.MAIN_TREEVIEW_SPECIAL));
            treeView1.Nodes.Add(makeTreeNode("Warps", Theme.MAIN_TREEVIEW_WARPS));
            treeView1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            treeView1.Size = splitContainer3.Panel1.ClientSize;
            treeView1.TabIndex = 0;
            treeView1.TabStop = false;
            treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            //treeView1.BeforeSelect += new TreeViewCancelEventHandler(treeView1_BeforeSelect);
            treeView1.KeyPress += new KeyPressEventHandler(treeView1_KeyPress);
            splitContainer3.Panel1.Controls.Add(treeView1);
            Globals.multi_selected_nodes.Clear();
            for (int i = 0; i < 4; i++)
                Globals.multi_selected_nodes.Add(new List<int>());
        }

        public MainForm()
        {
            InitializeComponent();
            initTreeView();
            OpenTK.Toolkit.Init();
            glControl1.CreateControl();
            SettingsFile.LoadGlobalSettings("default");
            updateTheme();
            glControl1.MouseWheel += new MouseEventHandler(glControl1_Wheel);
            ProjMatrix = Matrix4.CreatePerspectiveFieldOfView(
                Globals.FOV * ((float)Math.PI / 180.0f), 
                (float)glControl1.Width / (float)glControl1.Height, 
                100f, 
                100000f
            );
            glControl1.Enabled = false;
            KeyPreview = true;
            treeView1.HideSelection = false;
            camera.updateMatrix(ref camMtx);
            myTimer.Tick += updateWASDControls;
            myTimer.Interval = 10;
            myTimer.Enabled = false;
            cameraMode.SelectedIndex = 0;
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomToolStripColorTable());

            this.KeyDown += Handle_KeyDown;
        }
        
        // New functions for MainForm.cs
        void Handle_KeyDown( object sender, KeyEventArgs e )
        {
            switch (e.KeyCode)
            {
                case Keys.F1: SaveSelectedObjectsToFile(1); break;
                case Keys.F2: SaveSelectedObjectsToFile(2); break;
                case Keys.F3: SaveSelectedObjectsToFile(3); break;
                case Keys.F4: SaveSelectedObjectsToFile(4); break;
                case Keys.F5: LoadObjectsFromFile(1); break;
                case Keys.F6: LoadObjectsFromFile(2); break;
                case Keys.F7: LoadObjectsFromFile(3); break;
                case Keys.F8: LoadObjectsFromFile(4); break;
            }
        }

        private void SetColorsForMenuStripItem(ref ToolStripItemCollection items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].BackColor = Theme.MAIN_MENUBAR_BACKGROUND;
                items[i].ForeColor = Theme.MAIN_MENUBAR_TEXT;

                if (items[i] is ToolStripMenuItem)
                {
                    if (((ToolStripMenuItem)items[i]).DropDownItems.Count > 0)
                    {
                        ToolStripItemCollection children = ((ToolStripMenuItem)items[i]).DropDownItems;
                        SetColorsForMenuStripItem(ref children);
                    }
                }
            }
        }

        private void ToolStripSeparator_Paint(object sender, PaintEventArgs e)
        {
            ToolStripSeparator sep = (ToolStripSeparator)sender;
            e.Graphics.FillRectangle(new SolidBrush(sep.BackColor), 0, 0, sep.Width, sep.Height);
            e.Graphics.DrawLine(new Pen(sep.ForeColor), 30, sep.Height / 2, sep.Width - 4, sep.Height / 2);
        }

        private void updateTheme()
        {
            BackColor = Theme.MAIN_BACKGROUND;
            treeView1.BackColor = Theme.MAIN_TREEVIEW_BACKGROUND;
            treeView1.ForeColor = Theme.MAIN_TREEVIEW_TEXT;
            treeView1.Nodes[0].ForeColor = Theme.MAIN_TREEVIEW_3DOBJECTS; // "3D Objects" text
            treeView1.Nodes[1].ForeColor = Theme.MAIN_TREEVIEW_MACRO; // "Macro 3D Objects" text
            treeView1.Nodes[2].ForeColor = Theme.MAIN_TREEVIEW_SPECIAL; // "Special 3D Objects" text
            treeView1.Nodes[3].ForeColor = Theme.MAIN_TREEVIEW_WARPS; // "Warps" text

            propertyGrid1.ViewForeColor = Theme.MAIN_PROPERTIES_TEXT;
            propertyGrid1.ViewBackColor = Theme.MAIN_PROPERTIES_BACKGROUND;
            propertyGrid1.LineColor = Theme.MAIN_PROPERTIES_LINES;

            controlsPanel.BackColor = Theme.MAIN_CONTROLS_BACKGROUND;
            moveObjectLabel.ForeColor = Theme.MAIN_CONTROLS_TEXT;
            moveSpeedLabel.ForeColor = Theme.MAIN_CONTROLS_TEXT;
            rotateObjectLabel.ForeColor = Theme.MAIN_CONTROLS_TEXT;
            objSpeedLabel.ForeColor = Theme.MAIN_CONTROLS_TEXT;
            cameraModeLabel.ForeColor = Theme.MAIN_CONTROLS_TEXT;
            camSpeedPercentageLabel.ForeColor = Theme.MAIN_CONTROLS_TEXT;
            cameraSpeedLabel.ForeColor = Theme.MAIN_CONTROLS_TEXT;
            moveCameraLabel.ForeColor = Theme.MAIN_CONTROLS_TEXT;

            cameraMode.BackColor = Theme.MAIN_CONTROLS_DROPDOWNLIST_BACKGROUND;
            cameraMode.ForeColor = Theme.MAIN_CONTROLS_DROPDOWNLIST_TEXT;

            dropToGround.BackColor = Theme.MAIN_CONTROLS_BUTTON_BACKGROUND;
            dropToGround.ForeColor = Theme.MAIN_CONTROLS_BUTTON_TEXT;
            keepOnGround.BackColor = Theme.MAIN_CONTROLS_BUTTON_BACKGROUND;
            keepOnGround.ForeColor = Theme.MAIN_CONTROLS_BUTTON_TEXT;
            gridButton.BackColor = Theme.MAIN_CONTROLS_BUTTON_BACKGROUND;
            gridButton.ForeColor = Theme.MAIN_CONTROLS_BUTTON_TEXT;

            gridSize.BackColor = Theme.MAIN_CONTROLS_UPDOWN_BACKGROUND;
            gridSize.ForeColor = Theme.MAIN_CONTROLS_UPDOWN_TEXT;

            menuStrip1.BackColor = Theme.MAIN_MENUBAR_BACKGROUND;
            menuStrip1.ForeColor = Theme.MAIN_MENUBAR_TEXT;
            ToolStripItemCollection menuItems = menuStrip1.Items;
            SetColorsForMenuStripItem(ref menuItems);

            triangleCount.ForeColor = Theme.DEFAULT_TEXT;
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
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();

            Globals.objectComboEntries.Clear();
            Globals.behaviorNameEntries.Clear();
            BehaviorNameFile.parseBehaviorNames(Globals.getDefaultBehaviorNamesPath());
            ModelComboFile.parseObjectCombos(Globals.getDefaultObjectComboPath());
            rom.setSegment(0x15, Globals.seg15_location[0], Globals.seg15_location[1], false, null);
            rom.setSegment(0x02, Globals.seg02_location[0], Globals.seg02_location[1],
                rom.isSegmentMIO0(0x02, null), rom.Seg02_isFakeMIO0, rom.Seg02_uncompressedOffset, null);
            level = new Level(0x10, 1);
            LevelScripts.parse(ref level, 0x15, 0);
            level.sortAndAddNoModelEntries();
            level.CurrentAreaID = level.Areas[0].AreaID;
            refreshObjectsInList();
            glControl1.Enabled = true;
            bgColor = Color.CornflowerBlue;
            camera.setLevel(level);
            updateAreaButtons();

            rom.hasLookedAtLevelIDs = true;

            //stopWatch.Stop();
            //Console.WriteLine("Startup time: " + stopWatch.Elapsed.Milliseconds + "ms");

            updateTriangleCount();
            glControl1.Invalidate();

            forceGC(); // Force garbage collection.
        }

        private void refreshObjectsInList()
        {
            BeginUpdate(treeView1);
            Globals.list_selected = -1;
            Globals.item_selected = -1;
            Globals.multi_selected_nodes[0].Clear();
            Globals.multi_selected_nodes[1].Clear();
            Globals.multi_selected_nodes[2].Clear();
            Globals.multi_selected_nodes[3].Clear();
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
            EndUpdate(treeView1);
        }

        private void drawGrid()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;

            GL.Disable(EnableCap.DepthTest);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, w, h, 0, -1, 1000);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.PushMatrix();

            GL.Color3(Color.Black);
            float numberLines = (int)gridSize.Value;
            float lineWidthOffset = (float)w / numberLines;
            float lineHeightOffset = (float)h / numberLines;
            GL.Begin(PrimitiveType.Lines);
            for (int i = 0; i < numberLines; i++)
            {
                // draw vertical line
                GL.Vertex2(lineWidthOffset * i, 0);
                GL.Vertex2(lineWidthOffset * i, h);

                // draw horizontal line
                GL.Vertex2(0, lineHeightOffset * i);
                GL.Vertex2(w, lineHeightOffset * i);
            }
            GL.End();
            
            GL.PopMatrix();
            GL.Enable(EnableCap.DepthTest);
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

                if (gridEnabled)
                    drawGrid();

                glControl1.SwapBuffers();
            }
        }

        private bool treeViewAlreadyHasNodeSelected(TreeNode node)
        {
            foreach (TreeNode tNode in treeView1.SelectedNodes)
            {
                if (tNode.Equals(node))
                    return true;
            }
            return false;
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
                if (pixel[2] == 255 || pixel[2] == 0)
                    return; // If a pixel is fully white or fully black, then ignore picking.
            }
            if (pixel[2] > 0 && pixel[2] < 4)
            {
                Globals.list_selected = pixel[2] - 1;
                Globals.item_selected = (pixel[1] * 256) + pixel[0];
                if (isControlDown)
                {
                    bool setSelected = !treeViewAlreadyHasNodeSelected(treeView1.Nodes[Globals.list_selected].Nodes[Globals.item_selected]);
                    treeView1.ToggleNode(treeView1.Nodes[Globals.list_selected].Nodes[Globals.item_selected],
                    setSelected);
                    if (setSelected)
                        updateAfterSelect(treeView1.Nodes[Globals.list_selected].Nodes[Globals.item_selected]);
                    else
                        treeView1_updateMultiselection();
                }
                else
                {
                    treeView1.SelectSingleNode(treeView1.Nodes[Globals.list_selected].Nodes[Globals.item_selected]);
                    updateAfterSelect(treeView1.Nodes[Globals.list_selected].Nodes[Globals.item_selected]);
                }

                if (camera.isOrbitCamera())
                {
                    camera.updateOrbitCamera(ref camMtx);
                }
                glControl1.Invalidate();
            }
            //Color pickedColor = Color.FromArgb(pixel[0], pixel[1], pixel[2]);
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
            isControlDown = e.Control;
            if (!isMouseDown)
                moveState = false;
            switch (e.KeyCode)
            {
                case Keys.W: isWDown = false; break;
                case Keys.S: isSDown = false; break;
                case Keys.A: isADown = false; break;
                case Keys.D: isDDown = false; break;
            }
            if (!isWDown && !isSDown && !isADown && !isDDown)
                myTimer.Enabled = false;
        }

        private void glControl1_Leave(object sender, EventArgs e)
        {
            isWDown = false;
            isSDown = false;
            isADown = false;
            isDDown = false;
            myTimer.Enabled = false;
        }

        bool isWDown = false, isSDown = false, isADown = false, isDDown = false;
        private void updateWASDControls(object sender, EventArgs e)
        {
            if (!isShiftDown && camera.CamMode == CameraMode.FLY)
            {
                if (isWDown)
                {
                    camera.resetMouseStuff();
                    camera.updateCameraMatrixWithScrollWheel(50, ref camMtx);
                    savedCamPos = camera.Position;
                    glControl1.Invalidate();
                }
                else if (isSDown)
                {
                    camera.resetMouseStuff();
                    camera.updateCameraMatrixWithScrollWheel(-50, ref camMtx);
                    savedCamPos = camera.Position;
                    glControl1.Invalidate();
                }

                if (isDDown)
                {
                    camera.updateCameraOffsetDirectly(50, 0, ref camMtx);
                    glControl1.Invalidate();
                }
                else if (isADown)
                {
                    camera.updateCameraOffsetDirectly(-50, 0, ref camMtx);
                    glControl1.Invalidate();
                }
            }
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            isShiftDown = e.Shift;
            isControlDown = e.Control;
            switch (e.KeyCode)
            {
                case Keys.W:
                    isWDown = true;
                    myTimer.Enabled = true;
                    break;
                case Keys.S:
                    isSDown = true;
                    myTimer.Enabled = true;
                    break;
                case Keys.A:
                    isADown = true;
                    myTimer.Enabled = true;
                    break;
                case Keys.D:
                    isDDown = true;
                    myTimer.Enabled = true;
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

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);

            if (Globals.doBackfaceCulling)
                GL.Enable(EnableCap.CullFace);
            else
                GL.Disable(EnableCap.CullFace);
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            glControl1.Context.Update(glControl1.WindowInfo);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            ProjMatrix = Matrix4.CreatePerspectiveFieldOfView(Globals.FOV * ((float)Math.PI / 180.0f), (float)glControl1.Width / (float)glControl1.Height, 100f, 100000f);
            //ProjMatrix = Matrix4.CreateOrthographic(1000f, 1000f, 100f, 100000f);

            glControl1.Invalidate();
        }

        private void loadROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult saveResult = Prompts.ShowShouldSaveDialog();
            if (saveResult != DialogResult.Cancel)
                loadROM(false);
        }

        private void saveROMAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Z64 ROM|*.z64|V64 ROM|*.v64|N64 ROM (little endian)|*.n64|N64 ROM (big endian)|*.n64";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                if (saveFileDialog1.FilterIndex == 1 || saveFileDialog1.FilterIndex == 4)
                    ROM.Instance.saveFileAs(saveFileDialog1.FileName, ROM_Endian.BIG);
                else if (saveFileDialog1.FilterIndex == 2)
                    ROM.Instance.saveFileAs(saveFileDialog1.FileName, ROM_Endian.MIXED);
                else if (saveFileDialog1.FilterIndex == 3)
                    ROM.Instance.saveFileAs(saveFileDialog1.FileName, ROM_Endian.LITTLE);
            }
        }

        private void saveROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ROM.Instance.saveFile();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
            updateFieldOfView();
            glControl1.Invalidate();
            propertyGrid1.Refresh();
            glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
        }

        private void replaceBehavior(int index, ref SelectBehavior behWindow)
        {
            Area area = level.getCurrentArea();
            area.Objects[index].setBehaviorFromAddress(behWindow.ReturnBehavior);
            treeView1.Nodes[0].Nodes[index].Text = area.Objects[index].getObjectComboName();
        }

        private void replaceObject(int index, ref SelectComboPreset comboWindow)
        {
            Area area = level.getCurrentArea();
            area.Objects[index].ModelID = comboWindow.ReturnObjectCombo.ModelID;
            area.Objects[index].setBehaviorFromAddress(comboWindow.ReturnObjectCombo.Behavior);
            treeView1.Nodes[0].Nodes[index].Text = area.Objects[index].getObjectComboName();
            area.Objects[index].SetBehaviorParametersToZero();
            area.Objects[index].Act1 = true;
            area.Objects[index].Act2 = true;
            area.Objects[index].Act3 = true;
            area.Objects[index].Act4 = true;
            area.Objects[index].Act5 = true;
            area.Objects[index].Act6 = false;
            area.Objects[index].AllActs = true;
            area.Objects[index].ShowHideActs(true);
            area.Objects[index].UpdateProperties();
        }

        private void replaceMacroObject(int index, ref SelectComboPreset comboWindow)
        {
            //Console.WriteLine(comboWindow.ReturnPresetMacro.PresetID);
            Area area = level.getCurrentArea();
            area.MacroObjects[index].ModelID = comboWindow.ReturnPresetMacro.ModelID;
            area.MacroObjects[index].setPresetID(comboWindow.ReturnPresetMacro.PresetID);
            area.MacroObjects[index].setBehaviorFromAddress(comboWindow.ReturnPresetMacro.Behavior);
            //area.MacroObjects[Globals.item_selected].SetBehaviorParametersToZero();
            area.MacroObjects[index].BehaviorParameter1
                = comboWindow.ReturnPresetMacro.BehaviorParameter1;
            area.MacroObjects[index].BehaviorParameter2
                = comboWindow.ReturnPresetMacro.BehaviorParameter2;
            treeView1.Nodes[1].Nodes[index].Text
                = area.MacroObjects[index].getObjectComboName();
            area.MacroObjects[index].UpdateProperties();
        }

        private void replaceSpecialObject(int index, ref SelectComboPreset comboWindow)
        {
            //Console.WriteLine(comboWindow.ReturnPresetMacro.PresetID);
            Area area = level.getCurrentArea();
            area.SpecialObjects[index].ModelID = comboWindow.ReturnPresetMacro.ModelID;
            area.SpecialObjects[index].setPresetID(comboWindow.ReturnPresetMacro.PresetID);
            area.SpecialObjects[index].setBehaviorFromAddress(comboWindow.ReturnPresetMacro.Behavior);
            //area.SpecialObjects[Globals.item_selected].SetBehaviorParametersToZero();
            area.SpecialObjects[index].BehaviorParameter1
                = comboWindow.ReturnPresetMacro.BehaviorParameter1;
            area.SpecialObjects[index].BehaviorParameter2
                = comboWindow.ReturnPresetMacro.BehaviorParameter2;
            treeView1.Nodes[2].Nodes[index].Text
                = area.SpecialObjects[index].getObjectComboName();
            area.SpecialObjects[index].UpdateProperties();
        }

        private void replaceWarp(int index, ref EditWarp editWarpWindow)
        {
            //Console.WriteLine(comboWindow.ReturnPresetMacro.PresetID);
            Area area = level.getCurrentArea();
            area.Warps[index].WarpFrom_ID = editWarpWindow.fromID;
            area.Warps[index].WarpTo_AreaID = editWarpWindow.toArea;
            area.Warps[index].WarpTo_LevelID = editWarpWindow.toLevel;
            area.Warps[index].WarpTo_WarpID = editWarpWindow.toID;
            
            treeView1.Nodes[3].Nodes[index].Text = area.Warps[index].ToString();
            //  area.SpecialObjects[index].UpdateProperties();
        }

        private void replacePaintingWarp(int index, ref EditWarp editWarpWindow)
        {
            //Console.WriteLine(comboWindow.ReturnPresetMacro.PresetID);
            Area area = level.getCurrentArea();
            area.PaintingWarps[index].WarpFrom_ID = editWarpWindow.fromID;
            area.PaintingWarps[index].WarpTo_AreaID = editWarpWindow.toArea;
            area.PaintingWarps[index].WarpTo_LevelID = editWarpWindow.toLevel;
            area.PaintingWarps[index].WarpTo_WarpID = editWarpWindow.toID;

            treeView1.Nodes[3].Nodes[index].Text = area.Warps[index].ToString();
            //  area.SpecialObjects[index].UpdateProperties();
        }

        private void replaceInstantWarp(int index, ref EditWarp editWarpWindow)
        {
            //Console.WriteLine(comboWindow.ReturnPresetMacro.PresetID);
            Area area = level.getCurrentArea();
            area.InstantWarps[index].AreaID = editWarpWindow.toArea;
            area.InstantWarps[index].TriggerID = editWarpWindow.triggerID;
            area.InstantWarps[index].TeleX = editWarpWindow.tX;
            area.InstantWarps[index].TeleY = editWarpWindow.tY;
            area.InstantWarps[index].TeleZ = editWarpWindow.tZ;
            treeView1.Nodes[3].Nodes[index].Text = area.InstantWarps[index].ToString();
        }

        private void behaviorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectBehavior behWindow = new SelectBehavior();
            behWindow.ShowDialog();
            if (behWindow.ClickedSelect)
            {
                if (!Globals.isMultiSelected)
                    replaceBehavior(Globals.item_selected, ref behWindow);
                else
                    for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
                        replaceBehavior(treeView1.SelectedNodes[i].Index, ref behWindow);

                updateSelectedObjectsInROM();
                glControl1.Invalidate();
                propertyGrid1.Refresh();
                glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
            }
        }

        private void objectComboNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameObjectCombo roc = new RenameObjectCombo(getSelectedObject().Title);
            roc.ShowDialog();
            if (roc.ClickedSelect)
            {

                if (!Globals.isMultiSelected)
                    getSelectedObject().renameObjectCombo(roc.ReturnName);
                else
                {
                    Area area = level.getCurrentArea();
                    for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
                    {
                        TreeNode node = treeView1.SelectedNodes[i];
                        if (node.Parent.Text.Equals("3D Objects"))
                            area.Objects[node.Index].renameObjectCombo(roc.ReturnName);
                        else if (node.Parent.Text.Equals("Macro 3D Objects"))
                            area.MacroObjects[node.Index].renameObjectCombo(roc.ReturnName);
                        else if (node.Parent.Text.Equals("Special 3D Objects"))
                            area.SpecialObjects[node.Index].renameObjectCombo(roc.ReturnName);
                    }
                }
                // Console.WriteLine("Changed object combo name to " + roc.ReturnName);

                updateSelectedObjectsInROM();
                glControl1.Invalidate();
                propertyGrid1.Refresh();
                refreshObjectsInList();
                glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
            }

        }

        private void objectComboPresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectComboPreset comboWindow;
            switch (Globals.list_selected)
            {
                case 0:
                    comboWindow =
                        new SelectComboPreset(level, 0, "Select Object Combos", Theme.COMBOS_3DOBJECTS_TITLE);
                    comboWindow.ShowDialog();
                    if (comboWindow.ClickedSelect)
                    {
                        if (!Globals.isMultiSelected)
                            replaceObject(Globals.item_selected, ref comboWindow);
                        else
                            for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
                                replaceObject(treeView1.SelectedNodes[i].Index, ref comboWindow);
                    }
                    break;
                case 1:
                    comboWindow =
                        new SelectComboPreset(level, 1, "Select Macro Preset", Theme.COMBOS_MACRO_TITLE);
                    comboWindow.ShowDialog();
                    if (comboWindow.ClickedSelect)
                    {
                        if (!Globals.isMultiSelected)
                            replaceMacroObject(Globals.item_selected, ref comboWindow);
                        else
                            for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
                                replaceMacroObject(treeView1.SelectedNodes[i].Index, ref comboWindow);
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
                            new SelectComboPreset(level, specialListType, "Select Special Preset", Theme.COMBOS_SPECIAL_TITLE);
                        comboWindow.ShowDialog();
                        if (comboWindow.ClickedSelect)
                        {
                            if (!Globals.isMultiSelected)
                                replaceSpecialObject(Globals.item_selected, ref comboWindow);
                            else
                                for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
                                    replaceSpecialObject(treeView1.SelectedNodes[i].Index, ref comboWindow);
                        }
                        break;
                    }
            }
            glControl1.Invalidate();
            propertyGrid1.Refresh();
            glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
            updateSelectedObjectsInROM();
            Globals.needToSave = true;
        }


        private void warpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int warpIndex = 0;
            bool isPaintingWarp = false;
            object warp = getSelectedWarp(out warpIndex, out isPaintingWarp);
            Area area = level.getCurrentArea();
            if (warp is Warp)
            {
                Warp w = (Warp)warp;
                string title = "Update Painting Warp";
                if (w.ToString().StartsWith("Warp "))
                    title = "Update Warp";

                EditWarp editWarpWindow = new EditWarp(title, w.WarpFrom_ID, w.WarpTo_LevelID, w.WarpTo_AreaID, w.WarpTo_WarpID);
                editWarpWindow.ShowDialog();
                if (editWarpWindow.pressedSaved)
                {
                    if(isPaintingWarp)
                        replacePaintingWarp(warpIndex - area.Warps.Count, ref editWarpWindow);
                    else
                        replaceWarp(warpIndex, ref editWarpWindow);
                    updateSelectedWarpsInROM();
                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                    Globals.needToSave = true;
                }
            }
            else if (warp is WarpInstant)
            {
                WarpInstant w = (WarpInstant)warp;
                EditWarp editWarpWindow = new EditWarp(w.TriggerID, w.AreaID, w.TeleX, w.TeleY, w.TeleZ);
                editWarpWindow.ShowDialog();
                if (editWarpWindow.pressedSaved)
                {
                    replaceInstantWarp(warpIndex - area.Warps.Count - area.PaintingWarps.Count, ref editWarpWindow);
                    updateSelectedWarpsInROM();
                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                    Globals.needToSave = true;
                }
            }

            //EditWarp editWarpWindow = new EditWarp();
            //editWarpWindow.ShowDialog();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            string glString = GL.GetString(StringName.Version).Split(' ')[0];
            if (glString.StartsWith("1."))
            {
                MessageBox.Show(
                    "Error: You have an outdated version of OpenGL, which is not supported by this program." +
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

        private void updateTriangleCount()
        {
            triangleCount.Text = "Area Triangle Count: " + level.getCurrentArea().AreaModel.getNumberOfTrianglesInModel().ToString();
        }

        private void trySwitchArea(ushort toArea)
        {
            if (level.getCurrentArea().AreaID == toArea)
                return;

            if (level.hasArea(toArea))
            {
                level.CurrentAreaID = toArea;
                updateTriangleCount();
                refreshObjectsInList();
                glControl1.Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void resetObjectVariables()
        {
            cameraMode.SelectedIndex = 0;

            treeView1.SelectedNode = null;
            Globals.list_selected = -1;
            Globals.item_selected = -1;
            Globals.isMultiSelected = false;
            Globals.isMultiSelectedFromMultipleLists = false;
        }

        private void switchLevel(ushort levelID)
        {
            Level testLevel = new Level(levelID, 1);
            LevelScripts.parse(ref testLevel, 0x15, 0);
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            //stopWatch.Stop();
            //Console.WriteLine("RunTime (LevelScripts.parse): " + stopWatch.Elapsed.Milliseconds + "ms");

            if (testLevel.Areas.Count > 0)
            {
                level = testLevel;
                camera.setCameraMode(CameraMode.FLY, ref camMtx);
                camera.setLevel(level);
                level.sortAndAddNoModelEntries();
                level.CurrentAreaID = level.Areas[0].AreaID;
                resetObjectVariables();
                refreshObjectsInList();
                updateTriangleCount();
                glControl1.Invalidate();
                updateAreaButtons();

                forceGC(); // Force garbage collection.
            }
            else
            {
                ushort id = levelID;
                MessageBox.Show("Error: No areas found in level ID: 0x" + id.ToString("X"));
            }
        }

        private void selectLeveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Opening SelectLevelForm!");
            SelectLevelForm newLevel = new SelectLevelForm(level.LevelID);
            newLevel.ShowDialog();
            if (newLevel.changeLevel)
            {
                switchLevel(newLevel.levelID);
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

        private void treeView1_updateMultiselection()
        {
            Globals.multi_selected_nodes[0].Clear();
            Globals.multi_selected_nodes[1].Clear();
            Globals.multi_selected_nodes[2].Clear();
            Globals.multi_selected_nodes[3].Clear();

            foreach (TreeNode node in treeView1.SelectedNodes)
            {
                if (node.Parent.Text.Equals("3D Objects"))
                    Globals.multi_selected_nodes[0].Add(node.Index);
                else if (node.Parent.Text.Equals("Macro 3D Objects"))
                    Globals.multi_selected_nodes[1].Add(node.Index);
                else if (node.Parent.Text.Equals("Special 3D Objects"))
                    Globals.multi_selected_nodes[2].Add(node.Index);
                else if (node.Parent.Text.Equals("Warps"))
                    Globals.multi_selected_nodes[3].Add(node.Index);
            }
        }

        bool atLeastTwoBools(bool a, bool b, bool c)
        {
            return a ? (b || c) : (b && c);
        }

        private void updateAfterSelect(TreeNode node)
        {
            if (node.Parent == null)
            {

                Globals.isMultiSelected = false;
                Globals.isMultiSelectedFromMultipleLists = false;
                Globals.isMultiSelectedFromSpecialObjects = false;
                Globals.isMultiSelectedFromBothNormalWarpsAndInstantWarps = false;
                propertyGrid1.SelectedObject = null;
                Globals.list_selected = -1;
                Globals.item_selected = -1;
                objectComboPresetToolStripMenuItem.Enabled = false;
                objectComboNameToolStripMenuItem.Enabled = false;
                behaviorToolStripMenuItem.Enabled = false;
                warpToolStripMenuItem.Enabled = false;

                glControl1.Invalidate();
                glControl1.Update();
            }
            else
            {
                objectComboPresetToolStripMenuItem.Enabled = true;
                objectComboNameToolStripMenuItem.Enabled = true;
                behaviorToolStripMenuItem.Enabled = true;
                warpToolStripMenuItem.Enabled = false;
                if (treeView1.SelectedNodes.Count > 1)
                {
                    Area area = level.getCurrentArea();
                    string parent_text = treeView1.SelectedNodes[0].Parent.Text;

                    if (parent_text.Equals("3D Objects"))
                        Globals.list_selected = 0;
                    else if (parent_text.Equals("Macro 3D Objects"))
                        Globals.list_selected = 1;
                    else if (parent_text.Equals("Special 3D Objects"))
                        Globals.list_selected = 2;
                    else if (parent_text.Equals("Warps"))
                        Globals.list_selected = 3;

                    if (Globals.list_selected != 0)
                        behaviorToolStripMenuItem.Enabled = false;

                    Globals.isMultiSelectedFromMultipleLists = false;
                    Globals.isMultiSelectedFromSpecialObjects = false;
                    bool hasSO_8 = false, hasSO_10 = false, hasSO_12 = false;
                    Globals.isMultiSelectedFromBothNormalWarpsAndInstantWarps = false;
                    if (Globals.list_selected == 2)
                    {
                        Object3D obj3d_0 = area.SpecialObjects[treeView1.SelectedNodes[0].Index];
                        switch (obj3d_0.createdFromLevelScriptCommand)
                        {
                            case Object3D.FROM_LS_CMD.CMD_2E_8:
                                hasSO_8 = true;
                                break;
                            case Object3D.FROM_LS_CMD.CMD_2E_10:
                                hasSO_10 = true;
                                break;
                            case Object3D.FROM_LS_CMD.CMD_2E_12:
                                hasSO_12 = true;
                                break;
                        }
                    }

                    for (int i = 1; i < treeView1.SelectedNodes.Count; i++)
                    {

                        if (!treeView1.SelectedNodes[i].Parent.Text.Equals(parent_text))
                        {
                            Globals.isMultiSelectedFromMultipleLists = true;
                            objectComboPresetToolStripMenuItem.Enabled = false;
                            objectComboNameToolStripMenuItem.Enabled = false;
                            behaviorToolStripMenuItem.Enabled = false;
                            warpToolStripMenuItem.Enabled = false;
                            if (Globals.list_selected != 2 || (Globals.list_selected == 2 && Globals.isMultiSelectedFromSpecialObjects))
                                break;
                        }

                        if (treeView1.SelectedNodes[i].Parent.Text.Equals("Special 3D Objects") && !Globals.isMultiSelectedFromSpecialObjects)
                        {
                            Object3D obj3d_i = area.SpecialObjects[treeView1.SelectedNodes[i].Index];
                            switch (obj3d_i.createdFromLevelScriptCommand)
                            {
                                case Object3D.FROM_LS_CMD.CMD_2E_8:
                                    hasSO_8 = true;
                                    break;
                                case Object3D.FROM_LS_CMD.CMD_2E_10:
                                    hasSO_10 = true;
                                    break;
                                case Object3D.FROM_LS_CMD.CMD_2E_12:
                                    hasSO_12 = true;
                                    break;
                            }

                            //Console.WriteLine(hasSO_8 + "/" + hasSO_10 + "/" + hasSO_12);
                            if (atLeastTwoBools(hasSO_8, hasSO_10, hasSO_12))
                            {
                                Globals.isMultiSelectedFromSpecialObjects = true;
                                objectComboPresetToolStripMenuItem.Enabled = false;
                                objectComboNameToolStripMenuItem.Enabled = false;
                                behaviorToolStripMenuItem.Enabled = false;
                                warpToolStripMenuItem.Enabled = false;
                                if (Globals.isMultiSelectedFromMultipleLists)
                                    break;
                            }
                        }
                    }

                    Object3D[] selectedObjs = new Object3D[treeView1.SelectedNodes.Count];
                    object[] selectedWarps = new object[treeView1.SelectedNodes.Count];
                    Object3D.FLAGS showFlags =
                        Object3D.FLAGS.POSITION_X | Object3D.FLAGS.POSITION_Y | Object3D.FLAGS.POSITION_Z |
                        Object3D.FLAGS.ROTATION_Y | Object3D.FLAGS.BPARAM_1 | Object3D.FLAGS.BPARAM_2;;

                    for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
                    {
                        string local_parent_text = treeView1.SelectedNodes[i].Parent.Text;

                        if (local_parent_text.Equals("3D Objects"))
                            selectedObjs[i] = area.Objects[treeView1.SelectedNodes[i].Index];
                        else if (local_parent_text.Equals("Macro 3D Objects"))
                        {
                            selectedObjs[i] = area.MacroObjects[treeView1.SelectedNodes[i].Index];
                        }
                        else if (local_parent_text.Equals("Special 3D Objects"))
                        {
                            selectedObjs[i] = area.SpecialObjects[treeView1.SelectedNodes[i].Index];
                            if (selectedObjs[i].createdFromLevelScriptCommand == Object3D.FROM_LS_CMD.CMD_2E_8)
                                showFlags &= ~(Object3D.FLAGS.BPARAM_1 | Object3D.FLAGS.BPARAM_2 | Object3D.FLAGS.ROTATION_Y);
                            else if (selectedObjs[i].createdFromLevelScriptCommand == Object3D.FROM_LS_CMD.CMD_2E_10)
                                showFlags &= ~(Object3D.FLAGS.BPARAM_1 | Object3D.FLAGS.BPARAM_2);
                        }
                        else if (local_parent_text.Equals("Warps"))
                        {
                            if (treeView1.SelectedNodes[i].Index < area.Warps.Count)
                                selectedWarps[i] = area.Warps[treeView1.SelectedNodes[i].Index];
                            else if (treeView1.SelectedNodes[i].Index < area.Warps.Count + area.PaintingWarps.Count)
                                selectedWarps[i] = area.PaintingWarps[treeView1.SelectedNodes[i].Index - area.Warps.Count];
                            else
                                selectedWarps[i] = area.InstantWarps[treeView1.SelectedNodes[i].Index - area.Warps.Count - area.PaintingWarps.Count];
                        }
                    }
                    for (int i = 0; i < selectedObjs.Length; i++)
                    {
                        {
                            selectedObjs[i].RevealTemporaryHiddenFields();
                            // Console.WriteLine(Globals.isMultiSelectedFromSpecialObjects);
                            if (Globals.isMultiSelectedFromMultipleLists || Globals.isMultiSelectedFromSpecialObjects)
                            {
                                selectedObjs[i].HideFieldsTemporarly(showFlags);
                            }
                        }
                    }

                    if(Globals.list_selected == 3)
                        propertyGrid1.SelectedObjects = selectedWarps;
                    else
                        propertyGrid1.SelectedObjects = selectedObjs;

                    treeView1_updateMultiselection();
                    Globals.isMultiSelected = true;
                    glControl1.Invalidate();
                    glControl1.Update();
                    return;
                }
                // Console.WriteLine("Single Node!");
                Globals.isMultiSelected = false;
                Globals.isMultiSelectedFromMultipleLists = false;
                Globals.isMultiSelectedFromSpecialObjects = false;

                if (node.Parent.Text.Equals("3D Objects"))
                {
                    behaviorToolStripMenuItem.Enabled = true;
                    warpToolStripMenuItem.Enabled = false;
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
                    behaviorToolStripMenuItem.Enabled = false;
                    warpToolStripMenuItem.Enabled = false;
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
                    behaviorToolStripMenuItem.Enabled = false;
                    warpToolStripMenuItem.Enabled = false;
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
                    behaviorToolStripMenuItem.Enabled = false;
                    objectComboNameToolStripMenuItem.Enabled = false;
                    warpToolStripMenuItem.Enabled = true;
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
                obj.RevealTemporaryHiddenFields();
                if (obj.IsReadOnly)
                {
                    objectComboPresetToolStripMenuItem.Enabled = false;
                    objectComboNameToolStripMenuItem.Enabled = false;
                    behaviorToolStripMenuItem.Enabled = false;
                }
                obj.UpdateProperties();
                propertyGrid1.Refresh();
            }

            glControl1.Invalidate();
            glControl1.Update();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            updateAfterSelect(e.Node);
        }

        void updateFieldOfView()
        {
            ProjMatrix = Matrix4.CreatePerspectiveFieldOfView(Globals.FOV * ((float)Math.PI / 180.0f), (float)glControl1.Width / (float)glControl1.Height, 100f, 100000f);
            glControl1.Invalidate();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            /*
            fovText.Text = "FOV: " + trackBar_FOV.Value + "°";
            FOV = trackBar_FOV.Value * ((float)Math.PI/180.0f);
            if (FOV < 0.1f)
                FOV = 0.1f;
            ProjMatrix = Matrix4.CreatePerspectiveFieldOfView(FOV, (float)glControl1.Width / (float)glControl1.Height, 100f, 100000f);
            glControl1.Invalidate();
            */
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string label = e.ChangedItem.Label;
            if (Globals.list_selected > -1 && Globals.list_selected < 3)
            {
                if (!Globals.isMultiSelected)
                {
                    Object3D obj = getSelectedObject();
                    if (obj == null) return;
                    if (label.Equals("All Acts"))
                    {
                        bool isAllActs = (bool)e.ChangedItem.Value;
                        obj.ShowHideActs(isAllActs);

                        if (isAllActs)
                        {
                            obj.Act1 = true;
                            obj.Act2 = true;
                            obj.Act3 = true;
                            obj.Act4 = true;
                            obj.Act5 = true;
                            obj.Act6 = false;
                        }

                        propertyGrid1.Refresh();
                    }
                    else if (label.Equals("Behavior") || label.Equals("Model ID"))
                    {
                        if (Globals.item_selected > -1)
                            treeView1.Nodes[Globals.list_selected].Nodes[Globals.item_selected].Text
                                = obj.getObjectComboName();
                    }
                    obj.updateROMData();
                }
                else
                {
                    for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
                    {
                        TreeNode currentNode = treeView1.SelectedNodes[i];
                        Area area = level.getCurrentArea();
                        string local_parent_text = currentNode.Parent.Text;
                        Object3D obj = null;

                        if (local_parent_text.Equals("3D Objects"))
                            obj = area.Objects[currentNode.Index];
                        else if (local_parent_text.Equals("Macro 3D Objects"))
                            obj = area.MacroObjects[currentNode.Index];
                        else if (local_parent_text.Equals("Special 3D Objects"))
                            obj = area.SpecialObjects[currentNode.Index];

                        if (obj == null)
                            continue;

                        Object3D.FLAGS flag = obj.getFlagFromDisplayName(e.ChangedItem.Label);
                        if (flag != 0)
                            if (!obj.isPropertyShown(flag))
                                continue;

                        if (e.ChangedItem.Label.Equals("Model ID"))
                            if (!obj.canEditModelID)
                                continue;

                        if (e.ChangedItem.Label.Equals("Behavior"))
                            if (!obj.canEditBehavior)
                                continue;

                        if (e.ChangedItem.Label.Equals("All Acts"))
                        {
                            bool isAllActs = (bool)e.ChangedItem.Value;
                            obj.ShowHideActs(isAllActs);

                            if (isAllActs)
                            {
                                obj.Act1 = true;
                                obj.Act2 = true;
                                obj.Act3 = true;
                                obj.Act4 = true;
                                obj.Act5 = true;
                                obj.Act6 = false;
                            }

                            propertyGrid1.Refresh();
                        }
                        obj.updateROMData();
                    }
                }

                updateSelectedObjectsInROM();
                if (camera.isOrbitCamera())
                    camera.updateOrbitCamera(ref camMtx);
                glControl1.Invalidate();
            }
            else if (Globals.list_selected == 3)
            {
                int index = 0;
                bool isPaintingWarp = false;
                object warp = getSelectedWarp(out index, out isPaintingWarp);
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
            Globals.needToSave = true;
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
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, e.Node.ForeColor, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                e.DrawDefault = true;
            }
        }
        
        private void cameraMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cameraMode.SelectedIndex) {
                case 0: // Fly
                    camera.setCameraMode(CameraMode.FLY, ref camMtx);
                    break;
                case 1: // Orbit
                    camera.setCameraMode(CameraMode.ORBIT, ref camMtx);
                    break;
                case 2: // Top
                    camera.setCameraMode_LookDirection(LookDirection.TOP, ref camMtx);
                    break;
                case 3: // Bottom
                    camera.setCameraMode_LookDirection(LookDirection.BOTTOM, ref camMtx);
                    break;
                case 4: // Left
                    camera.setCameraMode_LookDirection(LookDirection.LEFT, ref camMtx);
                    break;
                case 5: // Right
                    camera.setCameraMode_LookDirection(LookDirection.RIGHT, ref camMtx);
                    break;
                case 6: // Front
                    camera.setCameraMode_LookDirection(LookDirection.FRONT, ref camMtx);
                    break;
                case 7: // Back
                    camera.setCameraMode_LookDirection(LookDirection.BACK, ref camMtx);
                    break;
            }
            camera.updateMatrix(ref camMtx);
            glControl1.Invalidate();
        }
        
        private void gridButton_CheckedChanged(object sender, EventArgs e)
        {
            if (gridButton.Checked)
            {
                gridSize.Enabled = true;
                gridEnabled = true;
            }
            else
            {
                gridSize.Enabled = false;
                gridEnabled = false;
            }
            glControl1.Invalidate();
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
                //Console.WriteLine(e.X+"/"+ e.Y);
                camera.updateCameraOffsetWithMouse(savedCamPos, e.X, e.Y, glControl1.Width, glControl1.Height, ref camMtx);
                glControl1.Invalidate();
            }
        }

        private Object3D getSelectedObject()
        {
            if (Globals.list_selected == -1 || Globals.item_selected == -1)
            {
                return null;
            }
            switch (Globals.list_selected)
            {
                case 0:
                    return level.getCurrentArea().Objects[Globals.item_selected];
                case 1:
                    return level.getCurrentArea().MacroObjects[Globals.item_selected];
                case 2:
                    return level.getCurrentArea().SpecialObjects[Globals.item_selected];
                default:
                    {
                        if (!Globals.isMultiSelected)
                            return null;
                        else
                        {
                            string parentText = treeView1.SelectedNodes[0].Parent.Text;
                            if (parentText.Equals("3D Objects"))
                                return level.getCurrentArea().Objects[treeView1.SelectedNodes[0].Index];
                            else if (parentText.Equals("Macro 3D Objects"))
                                return level.getCurrentArea().MacroObjects[treeView1.SelectedNodes[0].Index];
                            else if (parentText.Equals("Special 3D Objects"))
                                return level.getCurrentArea().SpecialObjects[treeView1.SelectedNodes[0].Index];
                            else
                                return null;
                        }
                    }
            }
        }

        private object getSelectedWarp(out int warpIndex, out bool isPaintingWarp)
        {
            isPaintingWarp = false;
            if (Globals.list_selected == -1 || Globals.item_selected == -1)
            {
                warpIndex = -1;
                return null;
            }
            switch (Globals.list_selected)
            {
                case 3:
                    {
                        Area area = level.getCurrentArea();
                        int index = Globals.item_selected;
                        warpIndex = index;
                        if (index < area.Warps.Count)
                        {
                            propertyGrid1.SelectedObject = area.Warps[index];
                        }
                        else if (index < area.Warps.Count + area.PaintingWarps.Count)
                        {
                            isPaintingWarp = true;
                            propertyGrid1.SelectedObject = area.PaintingWarps[index - area.Warps.Count];
                        }
                        else
                        {
                            propertyGrid1.SelectedObject = area.InstantWarps[index - area.Warps.Count - area.PaintingWarps.Count];
                        }
                        return propertyGrid1.SelectedObject;
                    }
                default:
                    {
                        warpIndex = -1;
                        return null;
                    }
            }
        }

        void updateSelectedWarpsInROM()
        {
            if (!Globals.isMultiSelected)
            {
                int index = 0;
                bool isPaintingWarp = false;
                object warp = getSelectedWarp(out index, out isPaintingWarp);
                if (warp is Warp)
                    ((Warp)warp).updateROMData();
                else if (warp is WarpInstant)
                    ((WarpInstant)warp).updateROMData();
            }
            else
            {
                Area area = level.getCurrentArea();
                foreach (int index in Globals.multi_selected_nodes[3])
                {
                    if (index < area.Warps.Count)
                        area.Warps[index].updateROMData();
                    else if (index < area.Warps.Count + area.PaintingWarps.Count)
                        area.PaintingWarps[index - area.Warps.Count].updateROMData();
                    else
                        area.InstantWarps[index - area.Warps.Count - area.PaintingWarps.Count].updateROMData();
                }
            }
        }

        void updateSelectedObjectsInROM()
        {
            if (!Globals.isMultiSelected)
            {
                Object3D obj = getSelectedObject();
                if (obj != null)
                    obj.updateROMData();
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    foreach (int selectedIndex in Globals.multi_selected_nodes[i])
                    {
                        switch (i)
                        {
                            case 0:
                                level.getCurrentArea().Objects[selectedIndex].updateROMData();
                                break;
                            case 1:
                                level.getCurrentArea().MacroObjects[selectedIndex].updateROMData();
                                break;
                            case 2:
                                level.getCurrentArea().SpecialObjects[selectedIndex].updateROMData();
                                break;
                        }
                    }
                }
            }
        }

        bool moveObj_mouseDown = false;
        int moveObj_lastMouseX = 0;
        int moveObj_lastMouseY = 0;
        //short moveObj_savedX=0, moveObj_savedY=0, moveObj_savedZ=0;

        struct Vec3S {
            public short X, Y, Z;
        };

        private void saveObjectPositionToList(ref List<Vec3S> posList)
        {
            Object3D obj;
            posList.Clear();
            if (!Globals.isMultiSelected)
            {
                Vec3S pos;
                obj = getSelectedObject();
                pos.X = obj.xPos;
                pos.Y = obj.yPos;
                pos.Z = obj.zPos;
                posList.Add(pos);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < Globals.multi_selected_nodes[i].Count; j++)
                    {
                        int selectedIndex = Globals.multi_selected_nodes[i][j];
                        Vec3S pos;
                        switch (i)
                        {
                            case 0:
                                obj = level.getCurrentArea().Objects[selectedIndex];
                                break;
                            case 1:
                                obj = level.getCurrentArea().MacroObjects[selectedIndex];
                                break;
                            case 2:
                                obj = level.getCurrentArea().SpecialObjects[selectedIndex];
                                break;
                            default:
                                return;
                        }
                        pos.X = obj.xPos;
                        pos.Y = obj.yPos;
                        pos.Z = obj.zPos;
                        posList.Add(pos);
                    }
                }
            }
        }
        
        private void saveObjectRotationToList(ref List<Vec3S> posList)
        {
            Object3D obj;
            posList.Clear();
            if (!Globals.isMultiSelected)
            {
                Vec3S rot;
                obj = getSelectedObject();
                rot.X = obj.xRot;
                rot.Y = obj.yRot;
                rot.Z = obj.zRot;
                posList.Add(rot);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < Globals.multi_selected_nodes[i].Count; j++)
                    {
                        int selectedIndex = Globals.multi_selected_nodes[i][j];
                        Vec3S rot;
                        switch (i)
                        {
                            case 0:
                                obj = level.getCurrentArea().Objects[selectedIndex];
                                break;
                            case 1:
                                obj = level.getCurrentArea().MacroObjects[selectedIndex];
                                break;
                            case 2:
                                obj = level.getCurrentArea().SpecialObjects[selectedIndex];
                                break;
                            default:
                                return;
                        }
                        rot.X = obj.xRot;
                        rot.Y = obj.yRot;
                        rot.Z = obj.zRot;
                        posList.Add(rot);
                    }
                }
            }
        }
        
        List<Vec3S> moveObj_saved = new List<Vec3S>();
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
                saveObjectPositionToList(ref moveObj_saved);
            }
        }

        private void moveObj_MouseUp(object sender, MouseEventArgs e)
        {
            moveObj_mouseDown = false;
            updateSelectedObjectsInROM();
            //Object3D obj = getSelectedObject();
            //if (obj != null)
            //obj.updateROMData();
        }

        private void moveObjectXZ(Object3D obj, MouseEventArgs e, Vec3S savedPos, bool forRotation)
        {
            if (obj == null) return;
            if (obj.IsReadOnly) return;
            float speedMult = 30.0f;

            int mx;
            int my;
            if (!forRotation)
            {
                mx = e.X - moveObj_lastMouseX;
                my = -(e.Y - moveObj_lastMouseY);
            }
            else
            {
                mx = e.X - rotObj_lastMouseX;
                my = -(e.Y - rotObj_lastMouseY);
            }
            
            float CX = (float)Math.Sin(camera.Yaw);
            float CZ = (float)-Math.Cos(camera.Yaw);
            float CX_2 = (float)Math.Sin(camera.Yaw + (Math.PI / 2));
            float CZ_2 = (float)-Math.Cos(camera.Yaw + (Math.PI / 2));

            if (!forRotation)
            {
                if (obj.isPropertyShown(Object3D.FLAGS.POSITION_X))
                    obj.xPos = (short)(savedPos.X - (short)(CX * my * speedMult * Globals.objSpeedMultiplier) - (short)(CX_2 * mx * speedMult * Globals.objSpeedMultiplier));
                if (obj.isPropertyShown(Object3D.FLAGS.POSITION_Z))
                    obj.zPos = (short)(savedPos.Z - (short)(CZ * my * speedMult * Globals.objSpeedMultiplier) - (short)(CZ_2 * mx * speedMult * Globals.objSpeedMultiplier));
                if (keepOnGround.Checked)
                    dropObjectToGround();
            }
            else
            {
                speedMult = 0.5f;
                if (obj.isPropertyShown(Object3D.FLAGS.ROTATION_X))
                {
                    obj.xRot = (short)(savedPos.X - (short)(CX * my * speedMult * Globals.objSpeedMultiplier) - (short)(CX_2 * mx * speedMult * Globals.objSpeedMultiplier));
                    obj.xRot = keepDegreesWithin360(obj.xRot);
                }
                if (obj.isPropertyShown(Object3D.FLAGS.ROTATION_Z))
                {
                    obj.zRot = (short)(savedPos.Z - (short)(CZ * my * speedMult * Globals.objSpeedMultiplier) - (short)(CZ_2 * mx * speedMult * Globals.objSpeedMultiplier));
                    obj.zRot = keepDegreesWithin360(obj.zRot);
                }
            }
            
            if (camera.isOrbitCamera())
                camera.updateOrbitCamera(ref camMtx);
        }

        private void moveObj_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveObj_mouseDown)
            {
                if (Globals.item_selected > -1 && Globals.list_selected > -1)
                {
                    if (!Globals.isMultiSelected)
                    {
                        moveObjectXZ(getSelectedObject(), e, moveObj_saved[0], false);
                    }
                    else
                    {
                        int mo_s_incr = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            //foreach (int selectedIndex in Globals.multi_selected_nodes[i])
                            for(int j = 0; j < Globals.multi_selected_nodes[i].Count; j++)
                            {
                                int selectedIndex = Globals.multi_selected_nodes[i][j];
                                switch (i)
                                {
                                    case 0:
                                        moveObjectXZ(level.getCurrentArea().Objects[selectedIndex], e, moveObj_saved[mo_s_incr++], false);
                                        break;
                                    case 1:
                                        moveObjectXZ(level.getCurrentArea().MacroObjects[selectedIndex], e, moveObj_saved[mo_s_incr++], false);
                                        break;
                                    case 2:
                                        moveObjectXZ(level.getCurrentArea().SpecialObjects[selectedIndex], e, moveObj_saved[mo_s_incr++], false);
                                        break;
                                }
                            }
                        }
                    }

                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                    Globals.needToSave = true;
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
                //saveObjectPositionToList(ref moveObj_saved);
                moveObj_UpDown_mouseDown = true;
            }
        }
        private void movObj_UpDown_MouseUp(object sender, MouseEventArgs e)
        {
            moveObj_UpDown_mouseDown = false;
            updateSelectedObjectsInROM();
            //Object3D obj = getSelectedObject();
            //if (obj != null)
            //    obj.updateROMData();
        }

        private void moveObjectY(Object3D obj, MouseEventArgs e, bool forRotation)
        {
            if (obj == null) return;
            if (obj.IsReadOnly) return;
            if (!forRotation)
            {
                if (obj.isPropertyShown(Object3D.FLAGS.POSITION_Y))
                {
                    obj.yPos -= (short)(30 * (e.Y - moveObj_UpDown_lastMouseY) * Globals.objSpeedMultiplier);
                }
            }
            else
            {
                if (obj.isPropertyShown(Object3D.FLAGS.ROTATION_Y))
                {
                    obj.yRot -= (short)((e.Y - rotObj_Yaw_lastMouseY) * Globals.objSpeedMultiplier);
                    obj.yRot = keepDegreesWithin360(obj.yRot);
                }
            }

            if (camera.isOrbitCamera())
                camera.updateOrbitCamera(ref camMtx);
        }

        private void movObj_UpDown_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveObj_UpDown_mouseDown)
            {
                if (Globals.item_selected > -1 && Globals.list_selected > -1)
                {
                    //Object3D obj = getSelectedObject();
                    if (!Globals.isMultiSelected)
                    {
                        moveObjectY(getSelectedObject(), e, false);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            //foreach (int selectedIndex in Globals.multi_selected_nodes[i])
                            for (int j = 0; j < Globals.multi_selected_nodes[i].Count; j++)
                            {
                                int selectedIndex = Globals.multi_selected_nodes[i][j];
                                switch (i)
                                {
                                    case 0:
                                        moveObjectY(level.getCurrentArea().Objects[selectedIndex], e, false);
                                        break;
                                    case 1:
                                        moveObjectY(level.getCurrentArea().MacroObjects[selectedIndex], e, false);
                                        break;
                                    case 2:
                                        moveObjectY(level.getCurrentArea().SpecialObjects[selectedIndex], e, false);
                                        break;
                                }
                            }
                        }
                    }
                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                    moveObj_UpDown_lastMouseY = e.Y;
                    Globals.needToSave = true;
                }
            }
        }
        
        bool rotObj_mouseDown = false;
        int rotObj_lastMouseX = 0;
        int rotObj_lastMouseY = 0;

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
                saveObjectRotationToList(ref moveObj_saved);
            }
        }

        private void rotObj_MouseUp(object sender, MouseEventArgs e)
        {
            rotObj_mouseDown = false;
            updateSelectedObjectsInROM();
            //Object3D obj = getSelectedObject();
            //if (obj != null)
            //    obj.updateROMData();
        }

        private void rotObj_MouseMove(object sender, MouseEventArgs e)
        {
            if (rotObj_mouseDown)
            {
                if (Globals.item_selected > -1 && Globals.list_selected > -1)
                {
                    if (!Globals.isMultiSelected)
                    {
                        moveObjectXZ(getSelectedObject(), e, moveObj_saved[0], true);
                    }
                    else
                    {
                        int mo_s_incr = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            //foreach (int selectedIndex in Globals.multi_selected_nodes[i])
                            for (int j = 0; j < Globals.multi_selected_nodes[i].Count; j++)
                            {
                                int selectedIndex = Globals.multi_selected_nodes[i][j];
                                switch (i)
                                {
                                    case 0:
                                        moveObjectXZ(level.getCurrentArea().Objects[selectedIndex], e, moveObj_saved[mo_s_incr++], true);
                                        break;
                                    case 1:
                                        moveObjectXZ(level.getCurrentArea().MacroObjects[selectedIndex], e, moveObj_saved[mo_s_incr++], true);
                                        break;
                                    case 2:
                                        moveObjectXZ(level.getCurrentArea().SpecialObjects[selectedIndex], e, moveObj_saved[mo_s_incr++], true);
                                        break;
                                }
                            }
                        }
                    }

                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                    Globals.needToSave = true;
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
                //Object3D obj = getSelectedObject();
                //if (obj == null) return;
                //obj.yPos = level.getCurrentArea().collision.dropToGround(new Vector3(obj.xPos, obj.yPos, obj.zPos));
                if (!Globals.isMultiSelected)
                {
                    Object3D obj = getSelectedObject();
                    if (obj == null) return;
                    obj.yPos = level.getCurrentArea().collision.dropToGround(new Vector3(obj.xPos, obj.yPos, obj.zPos));
                }
                else
                {
                    Object3D obj = null;
                    for (int i = 0; i < 3; i++)
                    {
                        //foreach (int selectedIndex in Globals.multi_selected_nodes[i])
                        for (int j = 0; j < Globals.multi_selected_nodes[i].Count; j++)
                        {
                            int selectedIndex = Globals.multi_selected_nodes[i][j];
                            switch (i)
                            {
                                case 0:
                                    obj = level.getCurrentArea().Objects[selectedIndex];
                                    break;
                                case 1:
                                    obj = level.getCurrentArea().MacroObjects[selectedIndex];
                                    break;
                                case 2:
                                    obj = level.getCurrentArea().SpecialObjects[selectedIndex];
                                    break;
                            }
                            if (obj == null) continue;
                            obj.yPos = level.getCurrentArea().collision.dropToGround(new Vector3(obj.xPos, obj.yPos, obj.zPos));
                        }
                    }
                }

                if (camera.isOrbitCamera())
                    camera.updateOrbitCamera(ref camMtx);
                updateSelectedObjectsInROM();
                glControl1.Invalidate();
                propertyGrid1.Refresh();
                glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                Globals.needToSave = true;
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Globals.needToSave)
            {
                DialogResult saveResult = Prompts.ShowShouldSaveDialog();
                e.Cancel = (saveResult == DialogResult.Cancel);
            }
        }

        private void texturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           textureEditor texEdit = new textureEditor(level);
           texEdit.ShowDialog();
           if (texEdit.needToUpdateLevel)
           {
                switchLevel(level.LevelID);
           }
           //new Thread(() => new textureEditor(level).ShowDialog()).Start();
        }

        private void scriptsDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptDumps dumpWindow = new ScriptDumps(level);
            dumpWindow.ShowDialog();
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
            updateSelectedObjectsInROM();
            //Object3D obj = getSelectedObject();
            //if (obj != null)
            //    obj.updateROMData();
        }

        private void rotObj_Yaw_MouseMove(object sender, MouseEventArgs e)
        {
            if (rotObj_Yaw_mouseDown)
            {
                if (Globals.item_selected > -1 && Globals.list_selected > -1)
                {
                    if (!Globals.isMultiSelected)
                    {
                        moveObjectY(getSelectedObject(), e, true);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            //foreach (int selectedIndex in Globals.multi_selected_nodes[i])
                            for (int j = 0; j < Globals.multi_selected_nodes[i].Count; j++)
                            {
                                int selectedIndex = Globals.multi_selected_nodes[i][j];
                                switch (i)
                                {
                                    case 0:
                                        moveObjectY(level.getCurrentArea().Objects[selectedIndex], e, true);
                                        break;
                                    case 1:
                                        moveObjectY(level.getCurrentArea().MacroObjects[selectedIndex], e, true);
                                        break;
                                    case 2:
                                        moveObjectY(level.getCurrentArea().SpecialObjects[selectedIndex], e, true);
                                        break;
                                }
                            }
                        }
                    }
                    glControl1.Invalidate();
                    propertyGrid1.Refresh();
                    glControl1.Update(); // Needed after calling propertyGrid1.Refresh();
                    rotObj_Yaw_lastMouseY = e.Y;
                    Globals.needToSave = true;
                }
            }
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            float newValue;
            if (trackBar_camSpeed.Value > 50)
                newValue = 100.0f+((trackBar_camSpeed.Value-50)*8f);
            else
                newValue = (trackBar_camSpeed.Value/50.0f) * 100f;
            
            if (newValue < 1f)
                newValue = 1f;
            else if (newValue > 96f && newValue < 114f)
                newValue = 100f;

            camSpeedPercentageLabel.Text = (int)(newValue) + "%";
            Globals.camSpeedMultiplier = newValue/100.0f;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {

            float newValue;
            if (trackBar_moveSpeed.Value > 50)
                newValue = 100.0f + ((trackBar_moveSpeed.Value - 50) * 8f);
            
            else
               newValue = (trackBar_moveSpeed.Value / 50.0f) * 100f;
            if (newValue < 1f)
                newValue = 1f;
            else if (newValue > 96f && newValue < 114f)
                newValue = 100f;

            objSpeedLabel.Text = (int)(newValue) + "%";
            Globals.objSpeedMultiplier = newValue / 100.0f;
        }

        private void gridSize_ValueChanged(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }
        

        private const int WM_USER = 0x0400;

        private void themesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemeSelector selector = new ThemeSelector();
            selector.ShowDialog();
            if (selector.doUpdate)
            {
                Theme.LoadColorsFromJSONFile(selector.themePath);
                updateTheme();
                SettingsFile.SaveGlobalSettings("default");
            }
        }

        private const int EM_SETEVENTMASK = (WM_USER + 69);
        private const int WM_SETREDRAW = 0x0b;
        private IntPtr OldEventMask;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private void BeginUpdate(Control c)
        {
            SendMessage(c.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
            OldEventMask = SendMessage(c.Handle, EM_SETEVENTMASK, IntPtr.Zero, IntPtr.Zero);
        }

        private void EndUpdate(Control c)
        {
            SendMessage(c.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            SendMessage(c.Handle, EM_SETEVENTMASK, IntPtr.Zero, OldEventMask);
        }
        
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        public static void forceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }

        private void SaveSelectedObjectsToFile( int slot )
        {
            List<ObjectData> selectedObjects = new List<ObjectData>();

            if( Globals.isMultiSelected )
            {
                if(
                    Globals.isMultiSelectedFromMultipleLists ||
                    Globals.multi_selected_nodes[0].Count < 1
                ) return;

                foreach( int index in Globals.multi_selected_nodes[0] )
                {
                    selectedObjects.Add(level.getCurrentArea().Objects[index].Data);
                }
            } else
            {
                if ( Globals.list_selected != 0 || Globals.item_selected < 0 ) return;

                selectedObjects.Add(level.getCurrentArea().Objects[Globals.item_selected].Data);
            }

            using( StreamWriter tempFile = new StreamWriter( "./data/profiles/default/saved-objects-" + slot.ToString() + ".json" ) )
            {
                tempFile.WriteLine(JsonConvert.SerializeObject(selectedObjects));
            }
        }

        private void LoadObjectsFromFile( int slot )
        {
            ObjectData[] objectsToLoad;
            try {
                using( StreamReader tempFile = new StreamReader( "./data/profiles/default/saved-objects-" + slot.ToString() + ".json" ) )
                {
                    string json = tempFile.ReadLine();
                    objectsToLoad = JsonConvert.DeserializeObject<ObjectData[]>( json );
                }
            } catch( Exception ex )
            {
                Console.Error.WriteLine( ex );
                return;
            }

            List<Object3D> objects = level.getCurrentArea().Objects;
            if( Globals.isMultiSelected )
            {
                if ( Globals.isMultiSelectedFromMultipleLists ||
                    Globals.multi_selected_nodes[0].Count != objectsToLoad.Length
                ) return;

                for( int i = 0; i < objectsToLoad.Length; i++)
                {
                    Object3D objectToUpdate = objects[Globals.multi_selected_nodes[0][i]];
                    objectToUpdate.ReplaceData(objectsToLoad[i]);
                    objectToUpdate.updateROMData();
                }
            } else
            {
                if (
                    Globals.list_selected != 0 ||
                    Globals.item_selected < 0 ||
                    objectsToLoad.Length < 1
                ) return;

                int j = 0;
                for( int i = Globals.item_selected; i < level.getCurrentArea().Objects.Count && j < objectsToLoad.Length; i++ )
                {
                    objects[i].ReplaceData(objectsToLoad[j]);
                    objects[i].updateROMData();
                    j++;
                }

            }
            refreshObjectsInList();

            glControl1.Invalidate();
            propertyGrid1.Refresh();
            glControl1.Update();
            Globals.needToSave = true;
        }

    }
}
