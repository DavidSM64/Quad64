namespace Quad64.src.Forms
{
    partial class ScriptDumps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptDumps));
            this.sd_tabs = new System.Windows.Forms.TabControl();
            this.tabLevel = new System.Windows.Forms.TabPage();
            this.subTabsLevel = new System.Windows.Forms.TabControl();
            this.lt_lsTab = new System.Windows.Forms.TabPage();
            this.lt_ls_textbox = new System.Windows.Forms.RichTextBox();
            this.lt_glTab = new System.Windows.Forms.TabPage();
            this.lt_gls_radioPanel = new System.Windows.Forms.Panel();
            this.lt_gls_textbox = new System.Windows.Forms.RichTextBox();
            this.lt_f3dTab = new System.Windows.Forms.TabPage();
            this.lt_f3d_listbox = new System.Windows.Forms.ListBox();
            this.lt_f3d_radioPanel = new System.Windows.Forms.Panel();
            this.lt_f3d_textbox = new System.Windows.Forms.RichTextBox();
            this.tabObjects = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listObjectsSortPanel = new System.Windows.Forms.Panel();
            this.comboBoxObjectSort = new System.Windows.Forms.ComboBox();
            this.ListObjectsSortLabel = new System.Windows.Forms.Label();
            this.listBoxObjects = new System.Windows.Forms.ListBox();
            this.subTabsObjects = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ot_gls_textbox = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ot_f3d_listbox = new System.Windows.Forms.ListBox();
            this.ot_f3d_textbox = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ot_beh_textbox = new System.Windows.Forms.RichTextBox();
            this.IndentCB = new System.Windows.Forms.CheckBox();
            this.wordWrapCB = new System.Windows.Forms.CheckBox();
            this.FormatCB = new System.Windows.Forms.CheckBox();
            this.CommCB = new System.Windows.Forms.CheckBox();
            this.SegAddrCB = new System.Windows.Forms.CheckBox();
            this.showRomCB = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sd_tabs.SuspendLayout();
            this.tabLevel.SuspendLayout();
            this.subTabsLevel.SuspendLayout();
            this.lt_lsTab.SuspendLayout();
            this.lt_glTab.SuspendLayout();
            this.lt_f3dTab.SuspendLayout();
            this.tabObjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.listObjectsSortPanel.SuspendLayout();
            this.subTabsObjects.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sd_tabs
            // 
            this.sd_tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sd_tabs.Controls.Add(this.tabLevel);
            this.sd_tabs.Controls.Add(this.tabObjects);
            this.sd_tabs.Location = new System.Drawing.Point(-1, 25);
            this.sd_tabs.Name = "sd_tabs";
            this.sd_tabs.SelectedIndex = 0;
            this.sd_tabs.Size = new System.Drawing.Size(715, 417);
            this.sd_tabs.TabIndex = 0;
            this.sd_tabs.SelectedIndexChanged += new System.EventHandler(this.sd_tabs_SelectedIndexChanged);
            // 
            // tabLevel
            // 
            this.tabLevel.Controls.Add(this.subTabsLevel);
            this.tabLevel.Location = new System.Drawing.Point(4, 22);
            this.tabLevel.Name = "tabLevel";
            this.tabLevel.Padding = new System.Windows.Forms.Padding(3);
            this.tabLevel.Size = new System.Drawing.Size(707, 391);
            this.tabLevel.TabIndex = 0;
            this.tabLevel.Text = "Level";
            this.tabLevel.UseVisualStyleBackColor = true;
            // 
            // subTabsLevel
            // 
            this.subTabsLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subTabsLevel.Controls.Add(this.lt_lsTab);
            this.subTabsLevel.Controls.Add(this.lt_glTab);
            this.subTabsLevel.Controls.Add(this.lt_f3dTab);
            this.subTabsLevel.Location = new System.Drawing.Point(0, 0);
            this.subTabsLevel.Name = "subTabsLevel";
            this.subTabsLevel.SelectedIndex = 0;
            this.subTabsLevel.Size = new System.Drawing.Size(707, 391);
            this.subTabsLevel.TabIndex = 0;
            this.subTabsLevel.TabStop = false;
            this.subTabsLevel.SelectedIndexChanged += new System.EventHandler(this.subTabsLevel_SelectedIndexChanged);
            // 
            // lt_lsTab
            // 
            this.lt_lsTab.BackColor = System.Drawing.Color.Transparent;
            this.lt_lsTab.Controls.Add(this.lt_ls_textbox);
            this.lt_lsTab.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lt_lsTab.Location = new System.Drawing.Point(4, 22);
            this.lt_lsTab.Name = "lt_lsTab";
            this.lt_lsTab.Padding = new System.Windows.Forms.Padding(3);
            this.lt_lsTab.Size = new System.Drawing.Size(699, 365);
            this.lt_lsTab.TabIndex = 0;
            this.lt_lsTab.Text = "Level script";
            // 
            // lt_ls_textbox
            // 
            this.lt_ls_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lt_ls_textbox.BackColor = System.Drawing.SystemColors.Window;
            this.lt_ls_textbox.DetectUrls = false;
            this.lt_ls_textbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_ls_textbox.HideSelection = false;
            this.lt_ls_textbox.Location = new System.Drawing.Point(0, 0);
            this.lt_ls_textbox.Name = "lt_ls_textbox";
            this.lt_ls_textbox.Size = new System.Drawing.Size(699, 363);
            this.lt_ls_textbox.TabIndex = 5;
            this.lt_ls_textbox.TabStop = false;
            this.lt_ls_textbox.Text = "";
            this.lt_ls_textbox.WordWrap = false;
            // 
            // lt_glTab
            // 
            this.lt_glTab.Controls.Add(this.lt_gls_radioPanel);
            this.lt_glTab.Controls.Add(this.lt_gls_textbox);
            this.lt_glTab.Location = new System.Drawing.Point(4, 22);
            this.lt_glTab.Name = "lt_glTab";
            this.lt_glTab.Padding = new System.Windows.Forms.Padding(3);
            this.lt_glTab.Size = new System.Drawing.Size(699, 365);
            this.lt_glTab.TabIndex = 1;
            this.lt_glTab.Text = "Geometry layout script";
            this.lt_glTab.UseVisualStyleBackColor = true;
            // 
            // lt_gls_radioPanel
            // 
            this.lt_gls_radioPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lt_gls_radioPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lt_gls_radioPanel.Location = new System.Drawing.Point(0, 0);
            this.lt_gls_radioPanel.Name = "lt_gls_radioPanel";
            this.lt_gls_radioPanel.Size = new System.Drawing.Size(699, 21);
            this.lt_gls_radioPanel.TabIndex = 7;
            this.lt_gls_radioPanel.Resize += new System.EventHandler(this.lt_gls_radioPanel_Resize);
            // 
            // lt_gls_textbox
            // 
            this.lt_gls_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lt_gls_textbox.DetectUrls = false;
            this.lt_gls_textbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_gls_textbox.HideSelection = false;
            this.lt_gls_textbox.Location = new System.Drawing.Point(0, 21);
            this.lt_gls_textbox.Name = "lt_gls_textbox";
            this.lt_gls_textbox.Size = new System.Drawing.Size(699, 344);
            this.lt_gls_textbox.TabIndex = 6;
            this.lt_gls_textbox.TabStop = false;
            this.lt_gls_textbox.Text = "";
            this.lt_gls_textbox.WordWrap = false;
            // 
            // lt_f3dTab
            // 
            this.lt_f3dTab.Controls.Add(this.lt_f3d_listbox);
            this.lt_f3dTab.Controls.Add(this.lt_f3d_radioPanel);
            this.lt_f3dTab.Controls.Add(this.lt_f3d_textbox);
            this.lt_f3dTab.Location = new System.Drawing.Point(4, 22);
            this.lt_f3dTab.Name = "lt_f3dTab";
            this.lt_f3dTab.Size = new System.Drawing.Size(699, 365);
            this.lt_f3dTab.TabIndex = 2;
            this.lt_f3dTab.Text = "Fast3D script";
            this.lt_f3dTab.UseVisualStyleBackColor = true;
            // 
            // lt_f3d_listbox
            // 
            this.lt_f3d_listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lt_f3d_listbox.BackColor = System.Drawing.SystemColors.Window;
            this.lt_f3d_listbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lt_f3d_listbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_f3d_listbox.FormattingEnabled = true;
            this.lt_f3d_listbox.IntegralHeight = false;
            this.lt_f3d_listbox.ItemHeight = 14;
            this.lt_f3d_listbox.Location = new System.Drawing.Point(0, 21);
            this.lt_f3d_listbox.Name = "lt_f3d_listbox";
            this.lt_f3d_listbox.Size = new System.Drawing.Size(88, 341);
            this.lt_f3d_listbox.TabIndex = 10;
            this.lt_f3d_listbox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lt_f3d_listbox_DrawItem);
            this.lt_f3d_listbox.SelectedIndexChanged += new System.EventHandler(this.lt_f3d_listbox_SelectedIndexChanged);
            // 
            // lt_f3d_radioPanel
            // 
            this.lt_f3d_radioPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lt_f3d_radioPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lt_f3d_radioPanel.Location = new System.Drawing.Point(0, 0);
            this.lt_f3d_radioPanel.Name = "lt_f3d_radioPanel";
            this.lt_f3d_radioPanel.Size = new System.Drawing.Size(699, 21);
            this.lt_f3d_radioPanel.TabIndex = 9;
            this.lt_f3d_radioPanel.Resize += new System.EventHandler(this.lt_f3d_radioPanel_Resize);
            // 
            // lt_f3d_textbox
            // 
            this.lt_f3d_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lt_f3d_textbox.DetectUrls = false;
            this.lt_f3d_textbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_f3d_textbox.HideSelection = false;
            this.lt_f3d_textbox.Location = new System.Drawing.Point(87, 21);
            this.lt_f3d_textbox.Name = "lt_f3d_textbox";
            this.lt_f3d_textbox.Size = new System.Drawing.Size(611, 342);
            this.lt_f3d_textbox.TabIndex = 8;
            this.lt_f3d_textbox.TabStop = false;
            this.lt_f3d_textbox.Text = "";
            this.lt_f3d_textbox.WordWrap = false;
            // 
            // tabObjects
            // 
            this.tabObjects.Controls.Add(this.splitContainer1);
            this.tabObjects.Location = new System.Drawing.Point(4, 22);
            this.tabObjects.Name = "tabObjects";
            this.tabObjects.Size = new System.Drawing.Size(707, 391);
            this.tabObjects.TabIndex = 2;
            this.tabObjects.Text = "Objects";
            this.tabObjects.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.listObjectsSortPanel);
            this.splitContainer1.Panel1.Controls.Add(this.listBoxObjects);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.subTabsObjects);
            this.splitContainer1.Size = new System.Drawing.Size(711, 391);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 2;
            // 
            // listObjectsSortPanel
            // 
            this.listObjectsSortPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listObjectsSortPanel.BackColor = System.Drawing.Color.Transparent;
            this.listObjectsSortPanel.Controls.Add(this.comboBoxObjectSort);
            this.listObjectsSortPanel.Controls.Add(this.ListObjectsSortLabel);
            this.listObjectsSortPanel.Location = new System.Drawing.Point(0, 0);
            this.listObjectsSortPanel.Name = "listObjectsSortPanel";
            this.listObjectsSortPanel.Size = new System.Drawing.Size(224, 25);
            this.listObjectsSortPanel.TabIndex = 1;
            // 
            // comboBoxObjectSort
            // 
            this.comboBoxObjectSort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxObjectSort.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxObjectSort.DisplayMember = "0";
            this.comboBoxObjectSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxObjectSort.DropDownWidth = 180;
            this.comboBoxObjectSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxObjectSort.FormattingEnabled = true;
            this.comboBoxObjectSort.Items.AddRange(new object[] {
            "Combo Name (Alphabetical)",
            "Geo Layout Offset (Segment Address)",
            "Geo Layout Offset (ROM Address)",
            "Fast3D Offset (Segment Address)",
            "Fast3D Offset (ROM Address)",
            "Behavior Offset (Segment Address)",
            "Behavior Offset (ROM Address)"});
            this.comboBoxObjectSort.Location = new System.Drawing.Point(58, 2);
            this.comboBoxObjectSort.Name = "comboBoxObjectSort";
            this.comboBoxObjectSort.Size = new System.Drawing.Size(166, 21);
            this.comboBoxObjectSort.TabIndex = 1;
            this.comboBoxObjectSort.SelectedIndexChanged += new System.EventHandler(this.comboBoxObjectSort_SelectedIndexChanged);
            // 
            // ListObjectsSortLabel
            // 
            this.ListObjectsSortLabel.AutoSize = true;
            this.ListObjectsSortLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListObjectsSortLabel.Location = new System.Drawing.Point(1, 6);
            this.ListObjectsSortLabel.Name = "ListObjectsSortLabel";
            this.ListObjectsSortLabel.Size = new System.Drawing.Size(55, 13);
            this.ListObjectsSortLabel.TabIndex = 0;
            this.ListObjectsSortLabel.Text = "Sort by:";
            // 
            // listBoxObjects
            // 
            this.listBoxObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxObjects.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxObjects.FormattingEnabled = true;
            this.listBoxObjects.IntegralHeight = false;
            this.listBoxObjects.Location = new System.Drawing.Point(0, 25);
            this.listBoxObjects.Name = "listBoxObjects";
            this.listBoxObjects.Size = new System.Drawing.Size(224, 363);
            this.listBoxObjects.TabIndex = 0;
            this.listBoxObjects.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxObjects_DrawItem);
            this.listBoxObjects.SelectedIndexChanged += new System.EventHandler(this.listBoxObjects_SelectedIndexChanged);
            // 
            // subTabsObjects
            // 
            this.subTabsObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subTabsObjects.Controls.Add(this.tabPage1);
            this.subTabsObjects.Controls.Add(this.tabPage2);
            this.subTabsObjects.Controls.Add(this.tabPage3);
            this.subTabsObjects.Location = new System.Drawing.Point(0, 0);
            this.subTabsObjects.Name = "subTabsObjects";
            this.subTabsObjects.SelectedIndex = 0;
            this.subTabsObjects.Size = new System.Drawing.Size(476, 390);
            this.subTabsObjects.TabIndex = 1;
            this.subTabsObjects.SelectedIndexChanged += new System.EventHandler(this.subTabsObjects_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ot_gls_textbox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(468, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Geometry layout script";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ot_gls_textbox
            // 
            this.ot_gls_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ot_gls_textbox.DetectUrls = false;
            this.ot_gls_textbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_gls_textbox.HideSelection = false;
            this.ot_gls_textbox.Location = new System.Drawing.Point(0, 0);
            this.ot_gls_textbox.Name = "ot_gls_textbox";
            this.ot_gls_textbox.Size = new System.Drawing.Size(468, 362);
            this.ot_gls_textbox.TabIndex = 5;
            this.ot_gls_textbox.TabStop = false;
            this.ot_gls_textbox.Text = "";
            this.ot_gls_textbox.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ot_f3d_listbox);
            this.tabPage2.Controls.Add(this.ot_f3d_textbox);
            this.tabPage2.Controls.Add(this.richTextBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(468, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fast3D script";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ot_f3d_listbox
            // 
            this.ot_f3d_listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ot_f3d_listbox.BackColor = System.Drawing.SystemColors.Window;
            this.ot_f3d_listbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ot_f3d_listbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_f3d_listbox.FormattingEnabled = true;
            this.ot_f3d_listbox.IntegralHeight = false;
            this.ot_f3d_listbox.ItemHeight = 14;
            this.ot_f3d_listbox.Location = new System.Drawing.Point(0, 0);
            this.ot_f3d_listbox.Name = "ot_f3d_listbox";
            this.ot_f3d_listbox.Size = new System.Drawing.Size(88, 364);
            this.ot_f3d_listbox.TabIndex = 12;
            this.ot_f3d_listbox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ot_f3d_listbox_DrawItem);
            this.ot_f3d_listbox.SelectedIndexChanged += new System.EventHandler(this.ot_f3d_listbox_SelectedIndexChanged);
            // 
            // ot_f3d_textbox
            // 
            this.ot_f3d_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ot_f3d_textbox.DetectUrls = false;
            this.ot_f3d_textbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_f3d_textbox.HideSelection = false;
            this.ot_f3d_textbox.Location = new System.Drawing.Point(88, 2);
            this.ot_f3d_textbox.Name = "ot_f3d_textbox";
            this.ot_f3d_textbox.Size = new System.Drawing.Size(379, 362);
            this.ot_f3d_textbox.TabIndex = 11;
            this.ot_f3d_textbox.TabStop = false;
            this.ot_f3d_textbox.Text = "";
            this.ot_f3d_textbox.WordWrap = false;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.DetectUrls = false;
            this.richTextBox2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.HideSelection = false;
            this.richTextBox2.Location = new System.Drawing.Point(0, 0);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(468, 364);
            this.richTextBox2.TabIndex = 6;
            this.richTextBox2.TabStop = false;
            this.richTextBox2.Text = "";
            this.richTextBox2.WordWrap = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ot_beh_textbox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(468, 364);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Behavior script";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ot_beh_textbox
            // 
            this.ot_beh_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ot_beh_textbox.DetectUrls = false;
            this.ot_beh_textbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_beh_textbox.HideSelection = false;
            this.ot_beh_textbox.Location = new System.Drawing.Point(0, 1);
            this.ot_beh_textbox.Name = "ot_beh_textbox";
            this.ot_beh_textbox.Size = new System.Drawing.Size(468, 362);
            this.ot_beh_textbox.TabIndex = 6;
            this.ot_beh_textbox.TabStop = false;
            this.ot_beh_textbox.Text = "";
            this.ot_beh_textbox.WordWrap = false;
            // 
            // IndentCB
            // 
            this.IndentCB.AutoSize = true;
            this.IndentCB.BackColor = System.Drawing.Color.Transparent;
            this.IndentCB.Checked = true;
            this.IndentCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IndentCB.Location = new System.Drawing.Point(527, 4);
            this.IndentCB.Name = "IndentCB";
            this.IndentCB.Size = new System.Drawing.Size(84, 17);
            this.IndentCB.TabIndex = 19;
            this.IndentCB.Text = "Indent bytes";
            this.IndentCB.UseVisualStyleBackColor = false;
            this.IndentCB.CheckedChanged += new System.EventHandler(this.updateScriptDump);
            // 
            // wordWrapCB
            // 
            this.wordWrapCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wordWrapCB.AutoSize = true;
            this.wordWrapCB.BackColor = System.Drawing.Color.Transparent;
            this.wordWrapCB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.wordWrapCB.Location = new System.Drawing.Point(627, 4);
            this.wordWrapCB.Name = "wordWrapCB";
            this.wordWrapCB.Size = new System.Drawing.Size(78, 17);
            this.wordWrapCB.TabIndex = 18;
            this.wordWrapCB.Text = "Word wrap";
            this.wordWrapCB.UseVisualStyleBackColor = false;
            this.wordWrapCB.CheckedChanged += new System.EventHandler(this.updateScriptDumpWordWrap);
            // 
            // FormatCB
            // 
            this.FormatCB.AutoSize = true;
            this.FormatCB.BackColor = System.Drawing.Color.Transparent;
            this.FormatCB.Checked = true;
            this.FormatCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FormatCB.Location = new System.Drawing.Point(435, 4);
            this.FormatCB.Name = "FormatCB";
            this.FormatCB.Size = new System.Drawing.Size(86, 17);
            this.FormatCB.TabIndex = 17;
            this.FormatCB.Text = "Format bytes";
            this.FormatCB.UseVisualStyleBackColor = false;
            this.FormatCB.CheckedChanged += new System.EventHandler(this.updateScriptDump);
            // 
            // CommCB
            // 
            this.CommCB.AutoSize = true;
            this.CommCB.BackColor = System.Drawing.Color.Transparent;
            this.CommCB.Checked = true;
            this.CommCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CommCB.Location = new System.Drawing.Point(273, 4);
            this.CommCB.Name = "CommCB";
            this.CommCB.Size = new System.Drawing.Size(155, 17);
            this.CommCB.TabIndex = 16;
            this.CommCB.Text = "Show generated comments";
            this.CommCB.UseVisualStyleBackColor = false;
            this.CommCB.CheckedChanged += new System.EventHandler(this.updateScriptDump);
            // 
            // SegAddrCB
            // 
            this.SegAddrCB.AutoSize = true;
            this.SegAddrCB.BackColor = System.Drawing.Color.Transparent;
            this.SegAddrCB.Checked = true;
            this.SegAddrCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SegAddrCB.Location = new System.Drawing.Point(130, 4);
            this.SegAddrCB.Name = "SegAddrCB";
            this.SegAddrCB.Size = new System.Drawing.Size(136, 17);
            this.SegAddrCB.TabIndex = 15;
            this.SegAddrCB.Text = "Show segment address";
            this.SegAddrCB.UseVisualStyleBackColor = false;
            this.SegAddrCB.CheckedChanged += new System.EventHandler(this.updateScriptDump);
            // 
            // showRomCB
            // 
            this.showRomCB.AutoSize = true;
            this.showRomCB.BackColor = System.Drawing.Color.Transparent;
            this.showRomCB.Checked = true;
            this.showRomCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showRomCB.Location = new System.Drawing.Point(3, 4);
            this.showRomCB.Name = "showRomCB";
            this.showRomCB.Size = new System.Drawing.Size(121, 17);
            this.showRomCB.TabIndex = 14;
            this.showRomCB.Text = "Show ROM address";
            this.showRomCB.UseVisualStyleBackColor = false;
            this.showRomCB.CheckedChanged += new System.EventHandler(this.updateScriptDump);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.showRomCB);
            this.panel1.Controls.Add(this.IndentCB);
            this.panel1.Controls.Add(this.SegAddrCB);
            this.panel1.Controls.Add(this.wordWrapCB);
            this.panel1.Controls.Add(this.CommCB);
            this.panel1.Controls.Add(this.FormatCB);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 24);
            this.panel1.TabIndex = 20;
            // 
            // ScriptDumps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 440);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sd_tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(730, 180);
            this.Name = "ScriptDumps";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Script Dumps";
            this.sd_tabs.ResumeLayout(false);
            this.tabLevel.ResumeLayout(false);
            this.subTabsLevel.ResumeLayout(false);
            this.lt_lsTab.ResumeLayout(false);
            this.lt_glTab.ResumeLayout(false);
            this.lt_f3dTab.ResumeLayout(false);
            this.tabObjects.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.listObjectsSortPanel.ResumeLayout(false);
            this.listObjectsSortPanel.PerformLayout();
            this.subTabsObjects.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl sd_tabs;
        private System.Windows.Forms.TabPage tabLevel;
        private System.Windows.Forms.TabControl subTabsLevel;
        private System.Windows.Forms.TabPage lt_lsTab;
        private System.Windows.Forms.TabPage lt_glTab;
        private System.Windows.Forms.TabPage lt_f3dTab;
        private System.Windows.Forms.RichTextBox lt_ls_textbox;
        private System.Windows.Forms.TabPage tabObjects;
        private System.Windows.Forms.CheckBox IndentCB;
        private System.Windows.Forms.CheckBox wordWrapCB;
        private System.Windows.Forms.CheckBox FormatCB;
        private System.Windows.Forms.CheckBox CommCB;
        private System.Windows.Forms.CheckBox SegAddrCB;
        private System.Windows.Forms.CheckBox showRomCB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox lt_gls_textbox;
        private System.Windows.Forms.Panel lt_gls_radioPanel;
        private System.Windows.Forms.Panel lt_f3d_radioPanel;
        private System.Windows.Forms.RichTextBox lt_f3d_textbox;
        private System.Windows.Forms.ListBox lt_f3d_listbox;
        private System.Windows.Forms.ListBox listBoxObjects;
        private System.Windows.Forms.TabControl subTabsObjects;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox ot_gls_textbox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox ot_f3d_listbox;
        private System.Windows.Forms.RichTextBox ot_f3d_textbox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel listObjectsSortPanel;
        private System.Windows.Forms.Label ListObjectsSortLabel;
        private System.Windows.Forms.ComboBox comboBoxObjectSort;
        private System.Windows.Forms.RichTextBox ot_beh_textbox;
    }
}