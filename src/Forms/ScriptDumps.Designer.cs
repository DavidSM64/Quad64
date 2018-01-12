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
            this.subTabsLevel.SelectedIndexChanged += new System.EventHandler(this.subTabsLevel_SelectedIndexChanged);
            // 
            // lt_lsTab
            // 
            this.lt_lsTab.Controls.Add(this.lt_ls_textbox);
            this.lt_lsTab.Location = new System.Drawing.Point(4, 22);
            this.lt_lsTab.Name = "lt_lsTab";
            this.lt_lsTab.Padding = new System.Windows.Forms.Padding(3);
            this.lt_lsTab.Size = new System.Drawing.Size(699, 365);
            this.lt_lsTab.TabIndex = 0;
            this.lt_lsTab.Text = "Level scripts";
            this.lt_lsTab.UseVisualStyleBackColor = true;
            // 
            // lt_ls_textbox
            // 
            this.lt_ls_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lt_glTab.Text = "Geometry layout scripts";
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
            this.lt_f3dTab.Text = "Fast3D scripts";
            this.lt_f3dTab.UseVisualStyleBackColor = true;
            // 
            // lt_f3d_listbox
            // 
            this.lt_f3d_listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lt_f3d_listbox.BackColor = System.Drawing.SystemColors.Window;
            this.lt_f3d_listbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_f3d_listbox.FormattingEnabled = true;
            this.lt_f3d_listbox.ItemHeight = 14;
            this.lt_f3d_listbox.Location = new System.Drawing.Point(0, 21);
            this.lt_f3d_listbox.Name = "lt_f3d_listbox";
            this.lt_f3d_listbox.Size = new System.Drawing.Size(88, 340);
            this.lt_f3d_listbox.TabIndex = 10;
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
            this.tabObjects.Location = new System.Drawing.Point(4, 22);
            this.tabObjects.Name = "tabObjects";
            this.tabObjects.Size = new System.Drawing.Size(707, 391);
            this.tabObjects.TabIndex = 2;
            this.tabObjects.Text = "Objects";
            this.tabObjects.UseVisualStyleBackColor = true;
            // 
            // IndentCB
            // 
            this.IndentCB.AutoSize = true;
            this.IndentCB.Checked = true;
            this.IndentCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IndentCB.Location = new System.Drawing.Point(527, 4);
            this.IndentCB.Name = "IndentCB";
            this.IndentCB.Size = new System.Drawing.Size(84, 17);
            this.IndentCB.TabIndex = 19;
            this.IndentCB.Text = "Indent bytes";
            this.IndentCB.UseVisualStyleBackColor = true;
            this.IndentCB.CheckedChanged += new System.EventHandler(this.updateScriptDump);
            // 
            // wordWrapCB
            // 
            this.wordWrapCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wordWrapCB.AutoSize = true;
            this.wordWrapCB.Location = new System.Drawing.Point(627, 4);
            this.wordWrapCB.Name = "wordWrapCB";
            this.wordWrapCB.Size = new System.Drawing.Size(78, 17);
            this.wordWrapCB.TabIndex = 18;
            this.wordWrapCB.Text = "Word wrap";
            this.wordWrapCB.UseVisualStyleBackColor = true;
            this.wordWrapCB.CheckedChanged += new System.EventHandler(this.updateScriptDumpWordWrap);
            // 
            // FormatCB
            // 
            this.FormatCB.AutoSize = true;
            this.FormatCB.Checked = true;
            this.FormatCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FormatCB.Location = new System.Drawing.Point(435, 4);
            this.FormatCB.Name = "FormatCB";
            this.FormatCB.Size = new System.Drawing.Size(86, 17);
            this.FormatCB.TabIndex = 17;
            this.FormatCB.Text = "Format bytes";
            this.FormatCB.UseVisualStyleBackColor = true;
            this.FormatCB.CheckedChanged += new System.EventHandler(this.updateScriptDump);
            // 
            // CommCB
            // 
            this.CommCB.AutoSize = true;
            this.CommCB.Checked = true;
            this.CommCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CommCB.Location = new System.Drawing.Point(273, 4);
            this.CommCB.Name = "CommCB";
            this.CommCB.Size = new System.Drawing.Size(155, 17);
            this.CommCB.TabIndex = 16;
            this.CommCB.Text = "Show generated comments";
            this.CommCB.UseVisualStyleBackColor = true;
            this.CommCB.CheckedChanged += new System.EventHandler(this.updateScriptDump);
            // 
            // SegAddrCB
            // 
            this.SegAddrCB.AutoSize = true;
            this.SegAddrCB.Checked = true;
            this.SegAddrCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SegAddrCB.Location = new System.Drawing.Point(130, 4);
            this.SegAddrCB.Name = "SegAddrCB";
            this.SegAddrCB.Size = new System.Drawing.Size(136, 17);
            this.SegAddrCB.TabIndex = 15;
            this.SegAddrCB.Text = "Show segment address";
            this.SegAddrCB.UseVisualStyleBackColor = true;
            this.SegAddrCB.CheckedChanged += new System.EventHandler(this.updateScriptDump);
            // 
            // showRomCB
            // 
            this.showRomCB.AutoSize = true;
            this.showRomCB.Checked = true;
            this.showRomCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showRomCB.Location = new System.Drawing.Point(3, 4);
            this.showRomCB.Name = "showRomCB";
            this.showRomCB.Size = new System.Drawing.Size(121, 17);
            this.showRomCB.TabIndex = 14;
            this.showRomCB.Text = "Show ROM address";
            this.showRomCB.UseVisualStyleBackColor = true;
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
            this.Text = "Script Dumps";
            this.sd_tabs.ResumeLayout(false);
            this.tabLevel.ResumeLayout(false);
            this.subTabsLevel.ResumeLayout(false);
            this.lt_lsTab.ResumeLayout(false);
            this.lt_glTab.ResumeLayout(false);
            this.lt_f3dTab.ResumeLayout(false);
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
    }
}