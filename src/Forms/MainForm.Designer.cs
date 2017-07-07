namespace Quad64
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("3D Objects");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Macro 3D Objects");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Special 3D Objects");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Warps");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveROMAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.testROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectComboPresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectLeveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Area0Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Area1Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Area2Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Area3Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Area4Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Area5Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Area6Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Area7Button = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rOMInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.act6 = new System.Windows.Forms.RadioButton();
            this.act5 = new System.Windows.Forms.RadioButton();
            this.act4 = new System.Windows.Forms.RadioButton();
            this.act3 = new System.Windows.Forms.RadioButton();
            this.act2 = new System.Windows.Forms.RadioButton();
            this.act1 = new System.Windows.Forms.RadioButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.keepOnGround = new System.Windows.Forms.CheckBox();
            this.dropToGround = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.fovText = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.camSpeedLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.objSpeedLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.moveCamPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.moveCam_strafe = new System.Windows.Forms.PictureBox();
            this.moveCam_InOut = new System.Windows.Forms.PictureBox();
            this.rotateObjectPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rotObj = new System.Windows.Forms.PictureBox();
            this.rotObj_Yaw = new System.Windows.Forms.PictureBox();
            this.moveObjectPanel = new System.Windows.Forms.Panel();
            this.moveObjectLabel = new System.Windows.Forms.Label();
            this.moveObj = new System.Windows.Forms.PictureBox();
            this.movObj_UpDown = new System.Windows.Forms.PictureBox();
            this.glControl1 = new OpenTK.GLControl();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.moveCamPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moveCam_strafe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveCam_InOut)).BeginInit();
            this.rotateObjectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rotObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotObj_Yaw)).BeginInit();
            this.moveObjectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moveObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movObj_UpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightGray;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.levelToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(174, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadROMToolStripMenuItem,
            this.saveROMToolStripMenuItem,
            this.saveROMAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.testROMToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadROMToolStripMenuItem
            // 
            this.loadROMToolStripMenuItem.Name = "loadROMToolStripMenuItem";
            this.loadROMToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.loadROMToolStripMenuItem.Text = "Load ROM";
            this.loadROMToolStripMenuItem.Click += new System.EventHandler(this.loadROMToolStripMenuItem_Click);
            // 
            // saveROMToolStripMenuItem
            // 
            this.saveROMToolStripMenuItem.Name = "saveROMToolStripMenuItem";
            this.saveROMToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.saveROMToolStripMenuItem.Text = "Save ROM";
            this.saveROMToolStripMenuItem.Click += new System.EventHandler(this.saveROMToolStripMenuItem_Click);
            // 
            // saveROMAsToolStripMenuItem
            // 
            this.saveROMAsToolStripMenuItem.Name = "saveROMAsToolStripMenuItem";
            this.saveROMAsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.saveROMAsToolStripMenuItem.Text = "Save ROM as...";
            this.saveROMAsToolStripMenuItem.Click += new System.EventHandler(this.saveROMAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(148, 6);
            // 
            // testROMToolStripMenuItem
            // 
            this.testROMToolStripMenuItem.Name = "testROMToolStripMenuItem";
            this.testROMToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.testROMToolStripMenuItem.Text = "Launch ROM";
            this.testROMToolStripMenuItem.Click += new System.EventHandler(this.testROMToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectComboPresetToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // objectComboPresetToolStripMenuItem
            // 
            this.objectComboPresetToolStripMenuItem.Enabled = false;
            this.objectComboPresetToolStripMenuItem.Name = "objectComboPresetToolStripMenuItem";
            this.objectComboPresetToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.objectComboPresetToolStripMenuItem.Text = "Object Combo/Preset";
            this.objectComboPresetToolStripMenuItem.Click += new System.EventHandler(this.objectComboPresetToolStripMenuItem_Click);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectLeveToolStripMenuItem,
            this.selectAreaToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // selectLeveToolStripMenuItem
            // 
            this.selectLeveToolStripMenuItem.Name = "selectLeveToolStripMenuItem";
            this.selectLeveToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.selectLeveToolStripMenuItem.Text = "Select Level";
            this.selectLeveToolStripMenuItem.Click += new System.EventHandler(this.selectLeveToolStripMenuItem_Click);
            // 
            // selectAreaToolStripMenuItem
            // 
            this.selectAreaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Area0Button,
            this.Area1Button,
            this.Area2Button,
            this.Area3Button,
            this.Area4Button,
            this.Area5Button,
            this.Area6Button,
            this.Area7Button});
            this.selectAreaToolStripMenuItem.Name = "selectAreaToolStripMenuItem";
            this.selectAreaToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.selectAreaToolStripMenuItem.Text = "Select Area";
            // 
            // Area0Button
            // 
            this.Area0Button.Name = "Area0Button";
            this.Area0Button.Size = new System.Drawing.Size(107, 22);
            this.Area0Button.Text = "Area 0";
            this.Area0Button.Click += new System.EventHandler(this.AreaButton_Click);
            // 
            // Area1Button
            // 
            this.Area1Button.Name = "Area1Button";
            this.Area1Button.Size = new System.Drawing.Size(107, 22);
            this.Area1Button.Text = "Area 1";
            this.Area1Button.Click += new System.EventHandler(this.AreaButton_Click);
            // 
            // Area2Button
            // 
            this.Area2Button.Name = "Area2Button";
            this.Area2Button.Size = new System.Drawing.Size(107, 22);
            this.Area2Button.Text = "Area 2";
            this.Area2Button.Click += new System.EventHandler(this.AreaButton_Click);
            // 
            // Area3Button
            // 
            this.Area3Button.Name = "Area3Button";
            this.Area3Button.Size = new System.Drawing.Size(107, 22);
            this.Area3Button.Text = "Area 3";
            this.Area3Button.Click += new System.EventHandler(this.AreaButton_Click);
            // 
            // Area4Button
            // 
            this.Area4Button.Name = "Area4Button";
            this.Area4Button.Size = new System.Drawing.Size(107, 22);
            this.Area4Button.Text = "Area 4";
            this.Area4Button.Click += new System.EventHandler(this.AreaButton_Click);
            // 
            // Area5Button
            // 
            this.Area5Button.Name = "Area5Button";
            this.Area5Button.Size = new System.Drawing.Size(107, 22);
            this.Area5Button.Text = "Area 5";
            this.Area5Button.Click += new System.EventHandler(this.AreaButton_Click);
            // 
            // Area6Button
            // 
            this.Area6Button.Name = "Area6Button";
            this.Area6Button.Size = new System.Drawing.Size(107, 22);
            this.Area6Button.Text = "Area 6";
            this.Area6Button.Click += new System.EventHandler(this.AreaButton_Click);
            // 
            // Area7Button
            // 
            this.Area7Button.Name = "Area7Button";
            this.Area7Button.Size = new System.Drawing.Size(107, 22);
            this.Area7Button.Text = "Area 7";
            this.Area7Button.Click += new System.EventHandler(this.AreaButton_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.rOMInfoToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Misc";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // rOMInfoToolStripMenuItem
            // 
            this.rOMInfoToolStripMenuItem.Name = "rOMInfoToolStripMenuItem";
            this.rOMInfoToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.rOMInfoToolStripMenuItem.Text = "ROM Info";
            this.rOMInfoToolStripMenuItem.Click += new System.EventHandler(this.rOMInfoToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(880, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 24);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.act6);
            this.panel2.Controls.Add(this.act5);
            this.panel2.Controls.Add(this.act4);
            this.panel2.Controls.Add(this.act3);
            this.panel2.Controls.Add(this.act2);
            this.panel2.Controls.Add(this.act1);
            this.panel2.Location = new System.Drawing.Point(763, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(117, 24);
            this.panel2.TabIndex = 2;
            // 
            // act6
            // 
            this.act6.Appearance = System.Windows.Forms.Appearance.Button;
            this.act6.BackgroundImage = global::Quad64.Properties.Resources.icon_Star1_gray;
            this.act6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.act6.FlatAppearance.BorderSize = 0;
            this.act6.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.act6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.act6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.act6.Location = new System.Drawing.Point(94, 4);
            this.act6.Margin = new System.Windows.Forms.Padding(0);
            this.act6.Name = "act6";
            this.act6.Size = new System.Drawing.Size(16, 16);
            this.act6.TabIndex = 5;
            this.act6.TabStop = true;
            this.act6.UseVisualStyleBackColor = true;
            this.act6.Visible = false;
            this.act6.CheckedChanged += new System.EventHandler(this.starAct_CheckedChanged);
            // 
            // act5
            // 
            this.act5.Appearance = System.Windows.Forms.Appearance.Button;
            this.act5.BackgroundImage = global::Quad64.Properties.Resources.icon_Star1_gray;
            this.act5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.act5.FlatAppearance.BorderSize = 0;
            this.act5.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.act5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.act5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.act5.Location = new System.Drawing.Point(76, 4);
            this.act5.Margin = new System.Windows.Forms.Padding(0);
            this.act5.Name = "act5";
            this.act5.Size = new System.Drawing.Size(16, 16);
            this.act5.TabIndex = 4;
            this.act5.TabStop = true;
            this.act5.UseVisualStyleBackColor = true;
            this.act5.Visible = false;
            this.act5.CheckedChanged += new System.EventHandler(this.starAct_CheckedChanged);
            // 
            // act4
            // 
            this.act4.Appearance = System.Windows.Forms.Appearance.Button;
            this.act4.BackgroundImage = global::Quad64.Properties.Resources.icon_Star1_gray;
            this.act4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.act4.FlatAppearance.BorderSize = 0;
            this.act4.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.act4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.act4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.act4.Location = new System.Drawing.Point(58, 4);
            this.act4.Margin = new System.Windows.Forms.Padding(0);
            this.act4.Name = "act4";
            this.act4.Size = new System.Drawing.Size(16, 16);
            this.act4.TabIndex = 3;
            this.act4.TabStop = true;
            this.act4.UseVisualStyleBackColor = true;
            this.act4.Visible = false;
            this.act4.CheckedChanged += new System.EventHandler(this.starAct_CheckedChanged);
            // 
            // act3
            // 
            this.act3.Appearance = System.Windows.Forms.Appearance.Button;
            this.act3.BackgroundImage = global::Quad64.Properties.Resources.icon_Star1_gray;
            this.act3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.act3.FlatAppearance.BorderSize = 0;
            this.act3.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.act3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.act3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.act3.Location = new System.Drawing.Point(40, 4);
            this.act3.Margin = new System.Windows.Forms.Padding(0);
            this.act3.Name = "act3";
            this.act3.Size = new System.Drawing.Size(16, 16);
            this.act3.TabIndex = 2;
            this.act3.TabStop = true;
            this.act3.UseVisualStyleBackColor = true;
            this.act3.Visible = false;
            this.act3.CheckedChanged += new System.EventHandler(this.starAct_CheckedChanged);
            // 
            // act2
            // 
            this.act2.Appearance = System.Windows.Forms.Appearance.Button;
            this.act2.BackgroundImage = global::Quad64.Properties.Resources.icon_Star1_gray;
            this.act2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.act2.FlatAppearance.BorderSize = 0;
            this.act2.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.act2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.act2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.act2.Location = new System.Drawing.Point(22, 4);
            this.act2.Margin = new System.Windows.Forms.Padding(0);
            this.act2.Name = "act2";
            this.act2.Size = new System.Drawing.Size(16, 16);
            this.act2.TabIndex = 1;
            this.act2.TabStop = true;
            this.act2.UseVisualStyleBackColor = true;
            this.act2.Visible = false;
            this.act2.CheckedChanged += new System.EventHandler(this.starAct_CheckedChanged);
            // 
            // act1
            // 
            this.act1.Appearance = System.Windows.Forms.Appearance.Button;
            this.act1.BackgroundImage = global::Quad64.Properties.Resources.icon_Star1_gray;
            this.act1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.act1.FlatAppearance.BorderSize = 0;
            this.act1.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.act1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.act1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.act1.Location = new System.Drawing.Point(4, 4);
            this.act1.Margin = new System.Windows.Forms.Padding(0);
            this.act1.Name = "act1";
            this.act1.Size = new System.Drawing.Size(16, 16);
            this.act1.TabIndex = 0;
            this.act1.TabStop = true;
            this.act1.UseVisualStyleBackColor = true;
            this.act1.Visible = false;
            this.act1.CheckedChanged += new System.EventHandler(this.starAct_CheckedChanged);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.Color.White;
            this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeView1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.HideSelection = false;
            this.treeView1.Indent = 12;
            this.treeView1.ItemHeight = 16;
            this.treeView1.Location = new System.Drawing.Point(1, 1);
            this.treeView1.Name = "treeView1";
            treeNode1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            treeNode1.Name = "objects";
            treeNode1.Text = "3D Objects";
            treeNode2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            treeNode2.Name = "Node0";
            treeNode2.Text = "Macro 3D Objects";
            treeNode3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            treeNode3.Name = "special3DNode";
            treeNode3.Text = "Special 3D Objects";
            treeNode4.Name = "warps";
            treeNode4.Text = "Warps";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(217, 206);
            this.treeView1.TabIndex = 0;
            this.treeView1.TabStop = false;
            this.treeView1.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView1_DrawNode);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeView1_KeyPress);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.CommandsBackColor = System.Drawing.SystemColors.Control;
            this.propertyGrid1.CommandsDisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.propertyGrid1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertyGrid1.LineColor = System.Drawing.Color.LightGray;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid1.Size = new System.Drawing.Size(216, 276);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.UseCompatibleTextRendering = true;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            this.propertyGrid1.PropertySortChanged += new System.EventHandler(this.propertyGrid1_PropertySortChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(1, 25);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel3);
            this.splitContainer2.Panel2.Controls.Add(this.glControl1);
            this.splitContainer2.Panel2MinSize = 600;
            this.splitContainer2.Size = new System.Drawing.Size(879, 491);
            this.splitContainer2.SplitterDistance = 219;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer3.Size = new System.Drawing.Size(219, 491);
            this.splitContainer3.SplitterDistance = 208;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.DimGray;
            this.panel3.Controls.Add(this.keepOnGround);
            this.panel3.Controls.Add(this.dropToGround);
            this.panel3.Controls.Add(this.radioButton2);
            this.panel3.Controls.Add(this.radioButton1);
            this.panel3.Controls.Add(this.fovText);
            this.panel3.Controls.Add(this.trackBar1);
            this.panel3.Controls.Add(this.camSpeedLabel);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.trackBar3);
            this.panel3.Controls.Add(this.objSpeedLabel);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.trackBar2);
            this.panel3.Controls.Add(this.moveCamPanel);
            this.panel3.Controls.Add(this.rotateObjectPanel);
            this.panel3.Controls.Add(this.moveObjectPanel);
            this.panel3.Location = new System.Drawing.Point(3, 370);
            this.panel3.MaximumSize = new System.Drawing.Size(10000, 120);
            this.panel3.MinimumSize = new System.Drawing.Size(600, 120);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(649, 120);
            this.panel3.TabIndex = 5;
            // 
            // keepOnGround
            // 
            this.keepOnGround.Appearance = System.Windows.Forms.Appearance.Button;
            this.keepOnGround.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keepOnGround.Location = new System.Drawing.Point(254, 79);
            this.keepOnGround.Margin = new System.Windows.Forms.Padding(0);
            this.keepOnGround.Name = "keepOnGround";
            this.keepOnGround.Size = new System.Drawing.Size(95, 21);
            this.keepOnGround.TabIndex = 0;
            this.keepOnGround.TabStop = false;
            this.keepOnGround.Text = "Keep on ground";
            this.keepOnGround.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.keepOnGround.UseVisualStyleBackColor = true;
            // 
            // dropToGround
            // 
            this.dropToGround.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropToGround.Location = new System.Drawing.Point(254, 57);
            this.dropToGround.Margin = new System.Windows.Forms.Padding(0);
            this.dropToGround.Name = "dropToGround";
            this.dropToGround.Size = new System.Drawing.Size(95, 21);
            this.dropToGround.TabIndex = 0;
            this.dropToGround.TabStop = false;
            this.dropToGround.Text = "Drop to ground";
            this.dropToGround.UseVisualStyleBackColor = true;
            this.dropToGround.Click += new System.EventHandler(this.dropToGround_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton2.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(605, 98);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(40, 20);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.Text = "Orbit";
            this.radioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(569, 98);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(38, 20);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Fly";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // fovText
            // 
            this.fovText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fovText.AutoSize = true;
            this.fovText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fovText.Location = new System.Drawing.Point(579, 54);
            this.fovText.Name = "fovText";
            this.fovText.Size = new System.Drawing.Size(63, 14);
            this.fovText.TabIndex = 14;
            this.fovText.Text = "FOV: 60°";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(565, 67);
            this.trackBar1.Maximum = 120;
            this.trackBar1.Minimum = 15;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(86, 45);
            this.trackBar1.SmallChange = 2;
            this.trackBar1.TabIndex = 13;
            this.trackBar1.TabStop = false;
            this.trackBar1.TickFrequency = 15;
            this.trackBar1.Value = 60;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // camSpeedLabel
            // 
            this.camSpeedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camSpeedLabel.AutoSize = true;
            this.camSpeedLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.camSpeedLabel.Location = new System.Drawing.Point(592, 13);
            this.camSpeedLabel.Name = "camSpeedLabel";
            this.camSpeedLabel.Size = new System.Drawing.Size(35, 14);
            this.camSpeedLabel.TabIndex = 12;
            this.camSpeedLabel.Text = "100%";
            this.camSpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(569, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "Cam speed:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar3
            // 
            this.trackBar3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar3.LargeChange = 10;
            this.trackBar3.Location = new System.Drawing.Point(565, 25);
            this.trackBar3.Maximum = 100;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(82, 45);
            this.trackBar3.SmallChange = 5;
            this.trackBar3.TabIndex = 10;
            this.trackBar3.TabStop = false;
            this.trackBar3.TickFrequency = 10;
            this.trackBar3.Value = 50;
            this.trackBar3.ValueChanged += new System.EventHandler(this.trackBar3_ValueChanged);
            // 
            // objSpeedLabel
            // 
            this.objSpeedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.objSpeedLabel.AutoSize = true;
            this.objSpeedLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objSpeedLabel.Location = new System.Drawing.Point(286, 15);
            this.objSpeedLabel.Name = "objSpeedLabel";
            this.objSpeedLabel.Size = new System.Drawing.Size(35, 14);
            this.objSpeedLabel.TabIndex = 9;
            this.objSpeedLabel.Text = "100%";
            this.objSpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(261, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "Move speed:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar2
            // 
            this.trackBar2.LargeChange = 10;
            this.trackBar2.Location = new System.Drawing.Point(253, 27);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(94, 45);
            this.trackBar2.SmallChange = 5;
            this.trackBar2.TabIndex = 7;
            this.trackBar2.TabStop = false;
            this.trackBar2.TickFrequency = 10;
            this.trackBar2.Value = 50;
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // moveCamPanel
            // 
            this.moveCamPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moveCamPanel.Controls.Add(this.label2);
            this.moveCamPanel.Controls.Add(this.moveCam_strafe);
            this.moveCamPanel.Controls.Add(this.moveCam_InOut);
            this.moveCamPanel.Location = new System.Drawing.Point(436, 2);
            this.moveCamPanel.Margin = new System.Windows.Forms.Padding(1);
            this.moveCamPanel.Name = "moveCamPanel";
            this.moveCamPanel.Size = new System.Drawing.Size(125, 120);
            this.moveCamPanel.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Move Camera";
            // 
            // moveCam_strafe
            // 
            this.moveCam_strafe.BackgroundImage = global::Quad64.Properties.Resources.MoveCamera;
            this.moveCam_strafe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveCam_strafe.Location = new System.Drawing.Point(2, 0);
            this.moveCam_strafe.MinimumSize = new System.Drawing.Size(0, 10);
            this.moveCam_strafe.Name = "moveCam_strafe";
            this.moveCam_strafe.Size = new System.Drawing.Size(96, 97);
            this.moveCam_strafe.TabIndex = 2;
            this.moveCam_strafe.TabStop = false;
            this.moveCam_strafe.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveCam_strafe_MouseDown);
            this.moveCam_strafe.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveCam_strafe_MouseMove);
            this.moveCam_strafe.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveCam_strafe_MouseUp);
            // 
            // moveCam_InOut
            // 
            this.moveCam_InOut.BackgroundImage = global::Quad64.Properties.Resources.MoveCamera_Y;
            this.moveCam_InOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveCam_InOut.Location = new System.Drawing.Point(100, 0);
            this.moveCam_InOut.MinimumSize = new System.Drawing.Size(0, 10);
            this.moveCam_InOut.Name = "moveCam_InOut";
            this.moveCam_InOut.Size = new System.Drawing.Size(20, 97);
            this.moveCam_InOut.TabIndex = 3;
            this.moveCam_InOut.TabStop = false;
            this.moveCam_InOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveCam_InOut_MouseDown);
            this.moveCam_InOut.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveCam_InOut_MouseMove);
            this.moveCam_InOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveCam_InOut_MouseUp);
            // 
            // rotateObjectPanel
            // 
            this.rotateObjectPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rotateObjectPanel.Controls.Add(this.label1);
            this.rotateObjectPanel.Controls.Add(this.rotObj);
            this.rotateObjectPanel.Controls.Add(this.rotObj_Yaw);
            this.rotateObjectPanel.Location = new System.Drawing.Point(127, 2);
            this.rotateObjectPanel.Margin = new System.Windows.Forms.Padding(1);
            this.rotateObjectPanel.Name = "rotateObjectPanel";
            this.rotateObjectPanel.Size = new System.Drawing.Size(124, 120);
            this.rotateObjectPanel.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Rotate Object";
            // 
            // rotObj
            // 
            this.rotObj.BackgroundImage = global::Quad64.Properties.Resources.RotateObject;
            this.rotObj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rotObj.Location = new System.Drawing.Point(3, 0);
            this.rotObj.MinimumSize = new System.Drawing.Size(0, 10);
            this.rotObj.Name = "rotObj";
            this.rotObj.Size = new System.Drawing.Size(96, 97);
            this.rotObj.TabIndex = 2;
            this.rotObj.TabStop = false;
            this.rotObj.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rotObj_MouseDown);
            this.rotObj.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rotObj_MouseMove);
            this.rotObj.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rotObj_MouseUp);
            // 
            // rotObj_Yaw
            // 
            this.rotObj_Yaw.BackgroundImage = global::Quad64.Properties.Resources.RotateObject_Y;
            this.rotObj_Yaw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rotObj_Yaw.Location = new System.Drawing.Point(101, 0);
            this.rotObj_Yaw.MinimumSize = new System.Drawing.Size(0, 10);
            this.rotObj_Yaw.Name = "rotObj_Yaw";
            this.rotObj_Yaw.Size = new System.Drawing.Size(20, 97);
            this.rotObj_Yaw.TabIndex = 3;
            this.rotObj_Yaw.TabStop = false;
            this.rotObj_Yaw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rotObj_Yaw_MouseDown);
            this.rotObj_Yaw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rotObj_Yaw_MouseMove);
            this.rotObj_Yaw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rotObj_Yaw_MouseUp);
            // 
            // moveObjectPanel
            // 
            this.moveObjectPanel.Controls.Add(this.moveObjectLabel);
            this.moveObjectPanel.Controls.Add(this.moveObj);
            this.moveObjectPanel.Controls.Add(this.movObj_UpDown);
            this.moveObjectPanel.Location = new System.Drawing.Point(1, 2);
            this.moveObjectPanel.Margin = new System.Windows.Forms.Padding(1);
            this.moveObjectPanel.Name = "moveObjectPanel";
            this.moveObjectPanel.Size = new System.Drawing.Size(124, 120);
            this.moveObjectPanel.TabIndex = 4;
            // 
            // moveObjectLabel
            // 
            this.moveObjectLabel.AutoSize = true;
            this.moveObjectLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveObjectLabel.Location = new System.Drawing.Point(8, 99);
            this.moveObjectLabel.Name = "moveObjectLabel";
            this.moveObjectLabel.Size = new System.Drawing.Size(96, 16);
            this.moveObjectLabel.TabIndex = 4;
            this.moveObjectLabel.Text = "Move Object";
            // 
            // moveObj
            // 
            this.moveObj.BackgroundImage = global::Quad64.Properties.Resources.MoveObject;
            this.moveObj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveObj.Location = new System.Drawing.Point(3, 0);
            this.moveObj.MinimumSize = new System.Drawing.Size(0, 10);
            this.moveObj.Name = "moveObj";
            this.moveObj.Size = new System.Drawing.Size(96, 97);
            this.moveObj.TabIndex = 2;
            this.moveObj.TabStop = false;
            this.moveObj.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveObj_MouseDown);
            this.moveObj.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveObj_MouseMove);
            this.moveObj.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveObj_MouseUp);
            // 
            // movObj_UpDown
            // 
            this.movObj_UpDown.BackgroundImage = global::Quad64.Properties.Resources.MoveObject_Y;
            this.movObj_UpDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.movObj_UpDown.Location = new System.Drawing.Point(101, 0);
            this.movObj_UpDown.MinimumSize = new System.Drawing.Size(0, 10);
            this.movObj_UpDown.Name = "movObj_UpDown";
            this.movObj_UpDown.Size = new System.Drawing.Size(20, 97);
            this.movObj_UpDown.TabIndex = 3;
            this.movObj_UpDown.TabStop = false;
            this.movObj_UpDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.movObj_UpDown_MouseDown);
            this.movObj_UpDown.MouseMove += new System.Windows.Forms.MouseEventHandler(this.movObj_UpDown_MouseMove);
            this.movObj_UpDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.movObj_UpDown_MouseUp);
            // 
            // glControl1
            // 
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("glControl1.BackgroundImage")));
            this.glControl1.Location = new System.Drawing.Point(3, 2);
            this.glControl1.MinimumSize = new System.Drawing.Size(600, 120);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(649, 365);
            this.glControl1.TabIndex = 0;
            this.glControl1.TabStop = false;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyUp);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseLeave += new System.EventHandler(this.glControl1_MouseLeave);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 538);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(820, 522);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quad64 v0.1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.moveCamPanel.ResumeLayout(false);
            this.moveCamPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moveCam_strafe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveCam_InOut)).EndInit();
            this.rotateObjectPanel.ResumeLayout(false);
            this.rotateObjectPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rotObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotObj_Yaw)).EndInit();
            this.moveObjectPanel.ResumeLayout(false);
            this.moveObjectPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moveObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.movObj_UpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem selectAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem selectLeveToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStripMenuItem saveROMAsToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox movObj_UpDown;
        private System.Windows.Forms.PictureBox moveObj;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Panel moveObjectPanel;
        private System.Windows.Forms.Label moveObjectLabel;
        private System.Windows.Forms.Panel moveCamPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox moveCam_strafe;
        private System.Windows.Forms.PictureBox moveCam_InOut;
        private System.Windows.Forms.Panel rotateObjectPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox rotObj;
        private System.Windows.Forms.PictureBox rotObj_Yaw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label objSpeedLabel;
        private System.Windows.Forms.Label fovText;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label camSpeedLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ToolStripMenuItem rOMInfoToolStripMenuItem;
        private System.Windows.Forms.Button dropToGround;
        private System.Windows.Forms.CheckBox keepOnGround;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton act1;
        private System.Windows.Forms.RadioButton act2;
        private System.Windows.Forms.RadioButton act6;
        private System.Windows.Forms.RadioButton act5;
        private System.Windows.Forms.RadioButton act4;
        private System.Windows.Forms.RadioButton act3;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectComboPresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Area0Button;
        private System.Windows.Forms.ToolStripMenuItem Area1Button;
        private System.Windows.Forms.ToolStripMenuItem Area2Button;
        private System.Windows.Forms.ToolStripMenuItem Area3Button;
        private System.Windows.Forms.ToolStripMenuItem Area4Button;
        private System.Windows.Forms.ToolStripMenuItem Area5Button;
        private System.Windows.Forms.ToolStripMenuItem Area6Button;
        private System.Windows.Forms.ToolStripMenuItem Area7Button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem testROMToolStripMenuItem;
    }
}

