namespace Quad64.src.Forms
{
    partial class textureEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(textureEditor));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.iconsPanel = new System.Windows.Forms.Panel();
            this.infoTabPanel = new System.Windows.Forms.Panel();
            this.info_Format = new System.Windows.Forms.TextBox();
            this.info_SegmentAddress = new System.Windows.Forms.TextBox();
            this.lt_importImage = new System.Windows.Forms.Button();
            this.lt_exportImage = new System.Windows.Forms.Button();
            this.info_Height = new System.Windows.Forms.TextBox();
            this.info_Width = new System.Windows.Forms.TextBox();
            this.info_bitmapImage = new System.Windows.Forms.PictureBox();
            this.info_Address = new System.Windows.Forms.TextBox();
            this.lt_info_label = new System.Windows.Forms.Label();
            this.layoutTabPanel = new System.Windows.Forms.Panel();
            this.lt_rb_objects = new System.Windows.Forms.RadioButton();
            this.lt_rb_models = new System.Windows.Forms.RadioButton();
            this.lt_rb_level = new System.Windows.Forms.RadioButton();
            this.lt_rb_all = new System.Windows.Forms.RadioButton();
            this.lt_category_label = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ot_pan_icons = new System.Windows.Forms.Panel();
            this.ot_pan_info = new System.Windows.Forms.Panel();
            this.ot_name = new System.Windows.Forms.TextBox();
            this.ot_info_Format = new System.Windows.Forms.TextBox();
            this.ot_info_SegAddress = new System.Windows.Forms.TextBox();
            this.ot_importImage = new System.Windows.Forms.Button();
            this.ot_exportImage = new System.Windows.Forms.Button();
            this.ot_info_height = new System.Windows.Forms.TextBox();
            this.ot_info_width = new System.Windows.Forms.TextBox();
            this.ot_bitmap_preview = new System.Windows.Forms.PictureBox();
            this.ot_info_RomAddress = new System.Windows.Forms.TextBox();
            this.ot_info_label = new System.Windows.Forms.Label();
            this.ot_pan_groups = new System.Windows.Forms.Panel();
            this.ot_pan_groupButtons = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.sb_info = new System.Windows.Forms.Panel();
            this.checkBox_matchFogColor = new System.Windows.Forms.CheckBox();
            this.blue_label = new System.Windows.Forms.Label();
            this.green_label = new System.Windows.Forms.Label();
            this.red_label = new System.Windows.Forms.Label();
            this.numericUpDown_Blue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Green = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Red = new System.Windows.Forms.NumericUpDown();
            this.sb_useSolidColor = new System.Windows.Forms.RadioButton();
            this.sb_useTexture = new System.Windows.Forms.RadioButton();
            this.sb_info_Format = new System.Windows.Forms.TextBox();
            this.sb_info_SegAddress = new System.Windows.Forms.TextBox();
            this.sb_importImage = new System.Windows.Forms.Button();
            this.sb_exportImage = new System.Windows.Forms.Button();
            this.sb_info_Height = new System.Windows.Forms.TextBox();
            this.sb_info_Width = new System.Windows.Forms.TextBox();
            this.sb_info_RomAddress = new System.Windows.Forms.TextBox();
            this.sb_TextureInfo_label = new System.Windows.Forms.Label();
            this.sb_imageBox = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.infoTabPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.info_bitmapImage)).BeginInit();
            this.layoutTabPanel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.ot_pan_info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ot_bitmap_preview)).BeginInit();
            this.ot_pan_groups.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.sb_info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb_imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(661, 423);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.iconsPanel);
            this.tabPage1.Controls.Add(this.infoTabPanel);
            this.tabPage1.Controls.Add(this.layoutTabPanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(1);
            this.tabPage1.Size = new System.Drawing.Size(653, 397);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Level Textures";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // iconsPanel
            // 
            this.iconsPanel.AutoScroll = true;
            this.iconsPanel.BackColor = System.Drawing.Color.Green;
            this.iconsPanel.Location = new System.Drawing.Point(100, 0);
            this.iconsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.iconsPanel.Name = "iconsPanel";
            this.iconsPanel.Size = new System.Drawing.Size(402, 397);
            this.iconsPanel.TabIndex = 1;
            // 
            // infoTabPanel
            // 
            this.infoTabPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.infoTabPanel.Controls.Add(this.info_Format);
            this.infoTabPanel.Controls.Add(this.info_SegmentAddress);
            this.infoTabPanel.Controls.Add(this.lt_importImage);
            this.infoTabPanel.Controls.Add(this.lt_exportImage);
            this.infoTabPanel.Controls.Add(this.info_Height);
            this.infoTabPanel.Controls.Add(this.info_Width);
            this.infoTabPanel.Controls.Add(this.info_bitmapImage);
            this.infoTabPanel.Controls.Add(this.info_Address);
            this.infoTabPanel.Controls.Add(this.lt_info_label);
            this.infoTabPanel.Location = new System.Drawing.Point(502, 0);
            this.infoTabPanel.Margin = new System.Windows.Forms.Padding(0);
            this.infoTabPanel.Name = "infoTabPanel";
            this.infoTabPanel.Size = new System.Drawing.Size(150, 397);
            this.infoTabPanel.TabIndex = 1;
            // 
            // info_Format
            // 
            this.info_Format.BackColor = System.Drawing.Color.CornflowerBlue;
            this.info_Format.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.info_Format.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_Format.Location = new System.Drawing.Point(0, 158);
            this.info_Format.Margin = new System.Windows.Forms.Padding(0);
            this.info_Format.Name = "info_Format";
            this.info_Format.ReadOnly = true;
            this.info_Format.Size = new System.Drawing.Size(150, 13);
            this.info_Format.TabIndex = 13;
            this.info_Format.TabStop = false;
            this.info_Format.Text = "Format: N/A";
            this.info_Format.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // info_SegmentAddress
            // 
            this.info_SegmentAddress.BackColor = System.Drawing.Color.CornflowerBlue;
            this.info_SegmentAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.info_SegmentAddress.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_SegmentAddress.Location = new System.Drawing.Point(0, 138);
            this.info_SegmentAddress.Margin = new System.Windows.Forms.Padding(0);
            this.info_SegmentAddress.Name = "info_SegmentAddress";
            this.info_SegmentAddress.ReadOnly = true;
            this.info_SegmentAddress.Size = new System.Drawing.Size(150, 13);
            this.info_SegmentAddress.TabIndex = 12;
            this.info_SegmentAddress.TabStop = false;
            this.info_SegmentAddress.Text = "Seg Addr: N/A";
            this.info_SegmentAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lt_importImage
            // 
            this.lt_importImage.BackColor = System.Drawing.Color.LightGray;
            this.lt_importImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lt_importImage.Location = new System.Drawing.Point(0, 301);
            this.lt_importImage.Margin = new System.Windows.Forms.Padding(0);
            this.lt_importImage.Name = "lt_importImage";
            this.lt_importImage.Size = new System.Drawing.Size(150, 23);
            this.lt_importImage.TabIndex = 11;
            this.lt_importImage.Text = "Import from image file";
            this.lt_importImage.UseVisualStyleBackColor = false;
            this.lt_importImage.Click += new System.EventHandler(this.lt_importImage_Click);
            // 
            // lt_exportImage
            // 
            this.lt_exportImage.BackColor = System.Drawing.Color.LightGray;
            this.lt_exportImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lt_exportImage.Location = new System.Drawing.Point(0, 325);
            this.lt_exportImage.Margin = new System.Windows.Forms.Padding(0);
            this.lt_exportImage.Name = "lt_exportImage";
            this.lt_exportImage.Size = new System.Drawing.Size(150, 23);
            this.lt_exportImage.TabIndex = 10;
            this.lt_exportImage.Text = "Export to image file";
            this.lt_exportImage.UseVisualStyleBackColor = false;
            this.lt_exportImage.Click += new System.EventHandler(this.lt_exportImage_Click);
            // 
            // info_Height
            // 
            this.info_Height.BackColor = System.Drawing.Color.CornflowerBlue;
            this.info_Height.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.info_Height.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_Height.Location = new System.Drawing.Point(0, 198);
            this.info_Height.Margin = new System.Windows.Forms.Padding(0);
            this.info_Height.Name = "info_Height";
            this.info_Height.ReadOnly = true;
            this.info_Height.Size = new System.Drawing.Size(150, 13);
            this.info_Height.TabIndex = 9;
            this.info_Height.TabStop = false;
            this.info_Height.Text = "Height: N/A";
            this.info_Height.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // info_Width
            // 
            this.info_Width.BackColor = System.Drawing.Color.CornflowerBlue;
            this.info_Width.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.info_Width.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_Width.Location = new System.Drawing.Point(0, 178);
            this.info_Width.Margin = new System.Windows.Forms.Padding(0);
            this.info_Width.Name = "info_Width";
            this.info_Width.ReadOnly = true;
            this.info_Width.Size = new System.Drawing.Size(150, 13);
            this.info_Width.TabIndex = 8;
            this.info_Width.TabStop = false;
            this.info_Width.Text = "Width: N/A";
            this.info_Width.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // info_bitmapImage
            // 
            this.info_bitmapImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.info_bitmapImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.info_bitmapImage.Location = new System.Drawing.Point(46, 40);
            this.info_bitmapImage.Name = "info_bitmapImage";
            this.info_bitmapImage.Size = new System.Drawing.Size(64, 64);
            this.info_bitmapImage.TabIndex = 7;
            this.info_bitmapImage.TabStop = false;
            // 
            // info_Address
            // 
            this.info_Address.BackColor = System.Drawing.Color.CornflowerBlue;
            this.info_Address.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.info_Address.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_Address.Location = new System.Drawing.Point(0, 118);
            this.info_Address.Margin = new System.Windows.Forms.Padding(0);
            this.info_Address.Name = "info_Address";
            this.info_Address.ReadOnly = true;
            this.info_Address.Size = new System.Drawing.Size(150, 13);
            this.info_Address.TabIndex = 6;
            this.info_Address.TabStop = false;
            this.info_Address.Text = "ROM Address: N/A";
            this.info_Address.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lt_info_label
            // 
            this.lt_info_label.AutoSize = true;
            this.lt_info_label.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_info_label.Location = new System.Drawing.Point(55, 6);
            this.lt_info_label.Name = "lt_info_label";
            this.lt_info_label.Size = new System.Drawing.Size(45, 19);
            this.lt_info_label.TabIndex = 5;
            this.lt_info_label.Text = "Info";
            // 
            // layoutTabPanel
            // 
            this.layoutTabPanel.BackColor = System.Drawing.Color.Firebrick;
            this.layoutTabPanel.Controls.Add(this.lt_rb_objects);
            this.layoutTabPanel.Controls.Add(this.lt_rb_models);
            this.layoutTabPanel.Controls.Add(this.lt_rb_level);
            this.layoutTabPanel.Controls.Add(this.lt_rb_all);
            this.layoutTabPanel.Controls.Add(this.lt_category_label);
            this.layoutTabPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutTabPanel.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTabPanel.Name = "layoutTabPanel";
            this.layoutTabPanel.Size = new System.Drawing.Size(100, 397);
            this.layoutTabPanel.TabIndex = 0;
            // 
            // lt_rb_objects
            // 
            this.lt_rb_objects.Appearance = System.Windows.Forms.Appearance.Button;
            this.lt_rb_objects.BackColor = System.Drawing.Color.LightGray;
            this.lt_rb_objects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lt_rb_objects.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_rb_objects.Location = new System.Drawing.Point(0, 106);
            this.lt_rb_objects.Name = "lt_rb_objects";
            this.lt_rb_objects.Size = new System.Drawing.Size(100, 24);
            this.lt_rb_objects.TabIndex = 4;
            this.lt_rb_objects.Text = "Objects Only";
            this.lt_rb_objects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lt_rb_objects.UseVisualStyleBackColor = false;
            this.lt_rb_objects.Click += new System.EventHandler(this.radioButton4_lt_Click);
            // 
            // lt_rb_models
            // 
            this.lt_rb_models.Appearance = System.Windows.Forms.Appearance.Button;
            this.lt_rb_models.BackColor = System.Drawing.Color.LightGray;
            this.lt_rb_models.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lt_rb_models.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_rb_models.Location = new System.Drawing.Point(0, 82);
            this.lt_rb_models.Name = "lt_rb_models";
            this.lt_rb_models.Size = new System.Drawing.Size(100, 24);
            this.lt_rb_models.TabIndex = 3;
            this.lt_rb_models.Text = "Models Only";
            this.lt_rb_models.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lt_rb_models.UseVisualStyleBackColor = false;
            this.lt_rb_models.Click += new System.EventHandler(this.radioButton3_lt_Click);
            // 
            // lt_rb_level
            // 
            this.lt_rb_level.Appearance = System.Windows.Forms.Appearance.Button;
            this.lt_rb_level.BackColor = System.Drawing.Color.LightGray;
            this.lt_rb_level.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lt_rb_level.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_rb_level.Location = new System.Drawing.Point(0, 58);
            this.lt_rb_level.Name = "lt_rb_level";
            this.lt_rb_level.Size = new System.Drawing.Size(100, 24);
            this.lt_rb_level.TabIndex = 2;
            this.lt_rb_level.Text = "Level Only";
            this.lt_rb_level.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lt_rb_level.UseVisualStyleBackColor = false;
            this.lt_rb_level.Click += new System.EventHandler(this.radioButton2_lt_Click);
            // 
            // lt_rb_all
            // 
            this.lt_rb_all.Appearance = System.Windows.Forms.Appearance.Button;
            this.lt_rb_all.BackColor = System.Drawing.Color.LightGray;
            this.lt_rb_all.Checked = true;
            this.lt_rb_all.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lt_rb_all.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_rb_all.Location = new System.Drawing.Point(0, 34);
            this.lt_rb_all.Name = "lt_rb_all";
            this.lt_rb_all.Size = new System.Drawing.Size(100, 24);
            this.lt_rb_all.TabIndex = 1;
            this.lt_rb_all.TabStop = true;
            this.lt_rb_all.Text = "All";
            this.lt_rb_all.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lt_rb_all.UseVisualStyleBackColor = false;
            this.lt_rb_all.Click += new System.EventHandler(this.radioButton1_lt_Click);
            // 
            // lt_category_label
            // 
            this.lt_category_label.AutoSize = true;
            this.lt_category_label.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lt_category_label.Location = new System.Drawing.Point(9, 6);
            this.lt_category_label.Name = "lt_category_label";
            this.lt_category_label.Size = new System.Drawing.Size(81, 19);
            this.lt_category_label.TabIndex = 0;
            this.lt_category_label.Text = "Category";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ot_pan_icons);
            this.tabPage2.Controls.Add(this.ot_pan_info);
            this.tabPage2.Controls.Add(this.ot_pan_groups);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(653, 397);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Other Textures";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ot_pan_icons
            // 
            this.ot_pan_icons.AutoScroll = true;
            this.ot_pan_icons.BackColor = System.Drawing.Color.Green;
            this.ot_pan_icons.Location = new System.Drawing.Point(166, 0);
            this.ot_pan_icons.Margin = new System.Windows.Forms.Padding(0);
            this.ot_pan_icons.Name = "ot_pan_icons";
            this.ot_pan_icons.Size = new System.Drawing.Size(336, 397);
            this.ot_pan_icons.TabIndex = 4;
            // 
            // ot_pan_info
            // 
            this.ot_pan_info.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ot_pan_info.Controls.Add(this.ot_name);
            this.ot_pan_info.Controls.Add(this.ot_info_Format);
            this.ot_pan_info.Controls.Add(this.ot_info_SegAddress);
            this.ot_pan_info.Controls.Add(this.ot_importImage);
            this.ot_pan_info.Controls.Add(this.ot_exportImage);
            this.ot_pan_info.Controls.Add(this.ot_info_height);
            this.ot_pan_info.Controls.Add(this.ot_info_width);
            this.ot_pan_info.Controls.Add(this.ot_bitmap_preview);
            this.ot_pan_info.Controls.Add(this.ot_info_RomAddress);
            this.ot_pan_info.Controls.Add(this.ot_info_label);
            this.ot_pan_info.Location = new System.Drawing.Point(502, 0);
            this.ot_pan_info.Margin = new System.Windows.Forms.Padding(0);
            this.ot_pan_info.Name = "ot_pan_info";
            this.ot_pan_info.Size = new System.Drawing.Size(150, 397);
            this.ot_pan_info.TabIndex = 3;
            // 
            // ot_name
            // 
            this.ot_name.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ot_name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ot_name.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_name.Location = new System.Drawing.Point(0, 117);
            this.ot_name.Margin = new System.Windows.Forms.Padding(0);
            this.ot_name.Name = "ot_name";
            this.ot_name.ReadOnly = true;
            this.ot_name.Size = new System.Drawing.Size(150, 13);
            this.ot_name.TabIndex = 14;
            this.ot_name.TabStop = false;
            this.ot_name.Text = "Name: N/A";
            this.ot_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ot_info_Format
            // 
            this.ot_info_Format.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ot_info_Format.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ot_info_Format.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_info_Format.Location = new System.Drawing.Point(0, 176);
            this.ot_info_Format.Margin = new System.Windows.Forms.Padding(0);
            this.ot_info_Format.Name = "ot_info_Format";
            this.ot_info_Format.ReadOnly = true;
            this.ot_info_Format.Size = new System.Drawing.Size(150, 13);
            this.ot_info_Format.TabIndex = 13;
            this.ot_info_Format.TabStop = false;
            this.ot_info_Format.Text = "Format: N/A";
            this.ot_info_Format.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ot_info_SegAddress
            // 
            this.ot_info_SegAddress.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ot_info_SegAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ot_info_SegAddress.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_info_SegAddress.Location = new System.Drawing.Point(0, 156);
            this.ot_info_SegAddress.Margin = new System.Windows.Forms.Padding(0);
            this.ot_info_SegAddress.Name = "ot_info_SegAddress";
            this.ot_info_SegAddress.ReadOnly = true;
            this.ot_info_SegAddress.Size = new System.Drawing.Size(150, 13);
            this.ot_info_SegAddress.TabIndex = 12;
            this.ot_info_SegAddress.TabStop = false;
            this.ot_info_SegAddress.Text = "Seg Addr: N/A";
            this.ot_info_SegAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ot_importImage
            // 
            this.ot_importImage.BackColor = System.Drawing.Color.LightGray;
            this.ot_importImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ot_importImage.Location = new System.Drawing.Point(0, 301);
            this.ot_importImage.Margin = new System.Windows.Forms.Padding(0);
            this.ot_importImage.Name = "ot_importImage";
            this.ot_importImage.Size = new System.Drawing.Size(150, 23);
            this.ot_importImage.TabIndex = 11;
            this.ot_importImage.Text = "Import from image file";
            this.ot_importImage.UseVisualStyleBackColor = false;
            this.ot_importImage.Click += new System.EventHandler(this.ot_importImage_Click);
            // 
            // ot_exportImage
            // 
            this.ot_exportImage.BackColor = System.Drawing.Color.LightGray;
            this.ot_exportImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ot_exportImage.Location = new System.Drawing.Point(0, 325);
            this.ot_exportImage.Margin = new System.Windows.Forms.Padding(0);
            this.ot_exportImage.Name = "ot_exportImage";
            this.ot_exportImage.Size = new System.Drawing.Size(150, 23);
            this.ot_exportImage.TabIndex = 10;
            this.ot_exportImage.Text = "Export to image file";
            this.ot_exportImage.UseVisualStyleBackColor = false;
            this.ot_exportImage.Click += new System.EventHandler(this.ot_exportImage_Click);
            // 
            // ot_info_height
            // 
            this.ot_info_height.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ot_info_height.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ot_info_height.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_info_height.Location = new System.Drawing.Point(0, 216);
            this.ot_info_height.Margin = new System.Windows.Forms.Padding(0);
            this.ot_info_height.Name = "ot_info_height";
            this.ot_info_height.ReadOnly = true;
            this.ot_info_height.Size = new System.Drawing.Size(150, 13);
            this.ot_info_height.TabIndex = 9;
            this.ot_info_height.TabStop = false;
            this.ot_info_height.Text = "Height: N/A";
            this.ot_info_height.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ot_info_width
            // 
            this.ot_info_width.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ot_info_width.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ot_info_width.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_info_width.Location = new System.Drawing.Point(0, 196);
            this.ot_info_width.Margin = new System.Windows.Forms.Padding(0);
            this.ot_info_width.Name = "ot_info_width";
            this.ot_info_width.ReadOnly = true;
            this.ot_info_width.Size = new System.Drawing.Size(150, 13);
            this.ot_info_width.TabIndex = 8;
            this.ot_info_width.TabStop = false;
            this.ot_info_width.Text = "Width: N/A";
            this.ot_info_width.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ot_bitmap_preview
            // 
            this.ot_bitmap_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ot_bitmap_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ot_bitmap_preview.Location = new System.Drawing.Point(3, 28);
            this.ot_bitmap_preview.Name = "ot_bitmap_preview";
            this.ot_bitmap_preview.Size = new System.Drawing.Size(144, 80);
            this.ot_bitmap_preview.TabIndex = 7;
            this.ot_bitmap_preview.TabStop = false;
            // 
            // ot_info_RomAddress
            // 
            this.ot_info_RomAddress.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ot_info_RomAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ot_info_RomAddress.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_info_RomAddress.Location = new System.Drawing.Point(0, 136);
            this.ot_info_RomAddress.Margin = new System.Windows.Forms.Padding(0);
            this.ot_info_RomAddress.Name = "ot_info_RomAddress";
            this.ot_info_RomAddress.ReadOnly = true;
            this.ot_info_RomAddress.Size = new System.Drawing.Size(150, 13);
            this.ot_info_RomAddress.TabIndex = 6;
            this.ot_info_RomAddress.TabStop = false;
            this.ot_info_RomAddress.Text = "ROM Address: N/A";
            this.ot_info_RomAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ot_info_label
            // 
            this.ot_info_label.AutoSize = true;
            this.ot_info_label.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ot_info_label.Location = new System.Drawing.Point(55, 6);
            this.ot_info_label.Name = "ot_info_label";
            this.ot_info_label.Size = new System.Drawing.Size(45, 19);
            this.ot_info_label.TabIndex = 5;
            this.ot_info_label.Text = "Info";
            // 
            // ot_pan_groups
            // 
            this.ot_pan_groups.BackColor = System.Drawing.Color.Firebrick;
            this.ot_pan_groups.Controls.Add(this.ot_pan_groupButtons);
            this.ot_pan_groups.Location = new System.Drawing.Point(0, 0);
            this.ot_pan_groups.Margin = new System.Windows.Forms.Padding(0);
            this.ot_pan_groups.Name = "ot_pan_groups";
            this.ot_pan_groups.Size = new System.Drawing.Size(166, 397);
            this.ot_pan_groups.TabIndex = 2;
            // 
            // ot_pan_groupButtons
            // 
            this.ot_pan_groupButtons.AutoScroll = true;
            this.ot_pan_groupButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ot_pan_groupButtons.Location = new System.Drawing.Point(0, 0);
            this.ot_pan_groupButtons.Margin = new System.Windows.Forms.Padding(0);
            this.ot_pan_groupButtons.Name = "ot_pan_groupButtons";
            this.ot_pan_groupButtons.Size = new System.Drawing.Size(166, 397);
            this.ot_pan_groupButtons.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.sb_info);
            this.tabPage3.Controls.Add(this.sb_imageBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(653, 397);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sky Background";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // sb_info
            // 
            this.sb_info.BackColor = System.Drawing.Color.CornflowerBlue;
            this.sb_info.Controls.Add(this.checkBox_matchFogColor);
            this.sb_info.Controls.Add(this.blue_label);
            this.sb_info.Controls.Add(this.green_label);
            this.sb_info.Controls.Add(this.red_label);
            this.sb_info.Controls.Add(this.numericUpDown_Blue);
            this.sb_info.Controls.Add(this.numericUpDown_Green);
            this.sb_info.Controls.Add(this.numericUpDown_Red);
            this.sb_info.Controls.Add(this.sb_useSolidColor);
            this.sb_info.Controls.Add(this.sb_useTexture);
            this.sb_info.Controls.Add(this.sb_info_Format);
            this.sb_info.Controls.Add(this.sb_info_SegAddress);
            this.sb_info.Controls.Add(this.sb_importImage);
            this.sb_info.Controls.Add(this.sb_exportImage);
            this.sb_info.Controls.Add(this.sb_info_Height);
            this.sb_info.Controls.Add(this.sb_info_Width);
            this.sb_info.Controls.Add(this.sb_info_RomAddress);
            this.sb_info.Controls.Add(this.sb_TextureInfo_label);
            this.sb_info.Location = new System.Drawing.Point(471, 0);
            this.sb_info.Margin = new System.Windows.Forms.Padding(0);
            this.sb_info.Name = "sb_info";
            this.sb_info.Size = new System.Drawing.Size(182, 397);
            this.sb_info.TabIndex = 4;
            // 
            // checkBox_matchFogColor
            // 
            this.checkBox_matchFogColor.AutoSize = true;
            this.checkBox_matchFogColor.Enabled = false;
            this.checkBox_matchFogColor.Location = new System.Drawing.Point(14, 87);
            this.checkBox_matchFogColor.Name = "checkBox_matchFogColor";
            this.checkBox_matchFogColor.Size = new System.Drawing.Size(163, 17);
            this.checkBox_matchFogColor.TabIndex = 23;
            this.checkBox_matchFogColor.Text = "Match fog color with bg color";
            this.checkBox_matchFogColor.UseVisualStyleBackColor = true;
            // 
            // blue_label
            // 
            this.blue_label.AutoSize = true;
            this.blue_label.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blue_label.Location = new System.Drawing.Point(128, 37);
            this.blue_label.Name = "blue_label";
            this.blue_label.Size = new System.Drawing.Size(31, 13);
            this.blue_label.TabIndex = 22;
            this.blue_label.Text = "Blue";
            // 
            // green_label
            // 
            this.green_label.AutoSize = true;
            this.green_label.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.green_label.Location = new System.Drawing.Point(73, 37);
            this.green_label.Name = "green_label";
            this.green_label.Size = new System.Drawing.Size(37, 13);
            this.green_label.TabIndex = 21;
            this.green_label.Text = "Green";
            // 
            // red_label
            // 
            this.red_label.AutoSize = true;
            this.red_label.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.red_label.Location = new System.Drawing.Point(29, 37);
            this.red_label.Name = "red_label";
            this.red_label.Size = new System.Drawing.Size(25, 13);
            this.red_label.TabIndex = 20;
            this.red_label.Text = "Red";
            // 
            // numericUpDown_Blue
            // 
            this.numericUpDown_Blue.BackColor = System.Drawing.Color.LightSkyBlue;
            this.numericUpDown_Blue.Enabled = false;
            this.numericUpDown_Blue.Location = new System.Drawing.Point(123, 56);
            this.numericUpDown_Blue.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numericUpDown_Blue.Name = "numericUpDown_Blue";
            this.numericUpDown_Blue.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown_Blue.TabIndex = 19;
            this.numericUpDown_Blue.ValueChanged += new System.EventHandler(this.updateBGRGBColor);
            // 
            // numericUpDown_Green
            // 
            this.numericUpDown_Green.BackColor = System.Drawing.Color.LightGreen;
            this.numericUpDown_Green.Enabled = false;
            this.numericUpDown_Green.Location = new System.Drawing.Point(71, 56);
            this.numericUpDown_Green.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numericUpDown_Green.Name = "numericUpDown_Green";
            this.numericUpDown_Green.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown_Green.TabIndex = 18;
            this.numericUpDown_Green.ValueChanged += new System.EventHandler(this.updateBGRGBColor);
            // 
            // numericUpDown_Red
            // 
            this.numericUpDown_Red.BackColor = System.Drawing.Color.LightCoral;
            this.numericUpDown_Red.Enabled = false;
            this.numericUpDown_Red.Location = new System.Drawing.Point(20, 56);
            this.numericUpDown_Red.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numericUpDown_Red.Name = "numericUpDown_Red";
            this.numericUpDown_Red.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown_Red.TabIndex = 17;
            this.numericUpDown_Red.ValueChanged += new System.EventHandler(this.updateBGRGBColor);
            // 
            // sb_useSolidColor
            // 
            this.sb_useSolidColor.AutoSize = true;
            this.sb_useSolidColor.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_useSolidColor.Location = new System.Drawing.Point(27, 11);
            this.sb_useSolidColor.Name = "sb_useSolidColor";
            this.sb_useSolidColor.Size = new System.Drawing.Size(130, 18);
            this.sb_useSolidColor.TabIndex = 16;
            this.sb_useSolidColor.Text = "Use solid color";
            this.sb_useSolidColor.UseVisualStyleBackColor = true;
            this.sb_useSolidColor.CheckedChanged += new System.EventHandler(this.sb_useSolidColor_CheckedChanged);
            // 
            // sb_useTexture
            // 
            this.sb_useTexture.AutoSize = true;
            this.sb_useTexture.Checked = true;
            this.sb_useTexture.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_useTexture.Location = new System.Drawing.Point(39, 119);
            this.sb_useTexture.Name = "sb_useTexture";
            this.sb_useTexture.Size = new System.Drawing.Size(102, 18);
            this.sb_useTexture.TabIndex = 15;
            this.sb_useTexture.TabStop = true;
            this.sb_useTexture.Text = "Use texture";
            this.sb_useTexture.UseVisualStyleBackColor = true;
            // 
            // sb_info_Format
            // 
            this.sb_info_Format.BackColor = System.Drawing.Color.CornflowerBlue;
            this.sb_info_Format.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sb_info_Format.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_info_Format.Location = new System.Drawing.Point(16, 212);
            this.sb_info_Format.Margin = new System.Windows.Forms.Padding(0);
            this.sb_info_Format.Name = "sb_info_Format";
            this.sb_info_Format.ReadOnly = true;
            this.sb_info_Format.Size = new System.Drawing.Size(150, 13);
            this.sb_info_Format.TabIndex = 13;
            this.sb_info_Format.TabStop = false;
            this.sb_info_Format.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sb_info_SegAddress
            // 
            this.sb_info_SegAddress.BackColor = System.Drawing.Color.CornflowerBlue;
            this.sb_info_SegAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sb_info_SegAddress.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_info_SegAddress.Location = new System.Drawing.Point(16, 192);
            this.sb_info_SegAddress.Margin = new System.Windows.Forms.Padding(0);
            this.sb_info_SegAddress.Name = "sb_info_SegAddress";
            this.sb_info_SegAddress.ReadOnly = true;
            this.sb_info_SegAddress.Size = new System.Drawing.Size(150, 13);
            this.sb_info_SegAddress.TabIndex = 12;
            this.sb_info_SegAddress.TabStop = false;
            this.sb_info_SegAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sb_importImage
            // 
            this.sb_importImage.BackColor = System.Drawing.Color.LightGray;
            this.sb_importImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sb_importImage.Location = new System.Drawing.Point(16, 289);
            this.sb_importImage.Margin = new System.Windows.Forms.Padding(0);
            this.sb_importImage.Name = "sb_importImage";
            this.sb_importImage.Size = new System.Drawing.Size(150, 23);
            this.sb_importImage.TabIndex = 11;
            this.sb_importImage.Text = "Import from image file";
            this.sb_importImage.UseVisualStyleBackColor = false;
            this.sb_importImage.Click += new System.EventHandler(this.sb_importImage_Click);
            // 
            // sb_exportImage
            // 
            this.sb_exportImage.BackColor = System.Drawing.Color.LightGray;
            this.sb_exportImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sb_exportImage.Location = new System.Drawing.Point(16, 316);
            this.sb_exportImage.Margin = new System.Windows.Forms.Padding(0);
            this.sb_exportImage.Name = "sb_exportImage";
            this.sb_exportImage.Size = new System.Drawing.Size(150, 23);
            this.sb_exportImage.TabIndex = 10;
            this.sb_exportImage.Text = "Export to image file";
            this.sb_exportImage.UseVisualStyleBackColor = false;
            this.sb_exportImage.Click += new System.EventHandler(this.sb_ExportImage_Click);
            // 
            // sb_info_Height
            // 
            this.sb_info_Height.BackColor = System.Drawing.Color.CornflowerBlue;
            this.sb_info_Height.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sb_info_Height.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_info_Height.Location = new System.Drawing.Point(16, 254);
            this.sb_info_Height.Margin = new System.Windows.Forms.Padding(0);
            this.sb_info_Height.Name = "sb_info_Height";
            this.sb_info_Height.ReadOnly = true;
            this.sb_info_Height.Size = new System.Drawing.Size(150, 13);
            this.sb_info_Height.TabIndex = 9;
            this.sb_info_Height.TabStop = false;
            this.sb_info_Height.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sb_info_Width
            // 
            this.sb_info_Width.BackColor = System.Drawing.Color.CornflowerBlue;
            this.sb_info_Width.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sb_info_Width.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_info_Width.Location = new System.Drawing.Point(16, 233);
            this.sb_info_Width.Margin = new System.Windows.Forms.Padding(0);
            this.sb_info_Width.Name = "sb_info_Width";
            this.sb_info_Width.ReadOnly = true;
            this.sb_info_Width.Size = new System.Drawing.Size(150, 13);
            this.sb_info_Width.TabIndex = 8;
            this.sb_info_Width.TabStop = false;
            this.sb_info_Width.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sb_info_RomAddress
            // 
            this.sb_info_RomAddress.BackColor = System.Drawing.Color.CornflowerBlue;
            this.sb_info_RomAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sb_info_RomAddress.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_info_RomAddress.Location = new System.Drawing.Point(16, 172);
            this.sb_info_RomAddress.Margin = new System.Windows.Forms.Padding(0);
            this.sb_info_RomAddress.Name = "sb_info_RomAddress";
            this.sb_info_RomAddress.ReadOnly = true;
            this.sb_info_RomAddress.Size = new System.Drawing.Size(150, 13);
            this.sb_info_RomAddress.TabIndex = 6;
            this.sb_info_RomAddress.TabStop = false;
            this.sb_info_RomAddress.Text = "(no texture found)";
            this.sb_info_RomAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sb_TextureInfo_label
            // 
            this.sb_TextureInfo_label.AutoSize = true;
            this.sb_TextureInfo_label.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sb_TextureInfo_label.Location = new System.Drawing.Point(32, 147);
            this.sb_TextureInfo_label.Name = "sb_TextureInfo_label";
            this.sb_TextureInfo_label.Size = new System.Drawing.Size(117, 19);
            this.sb_TextureInfo_label.TabIndex = 5;
            this.sb_TextureInfo_label.Text = "Texture Info";
            // 
            // sb_imageBox
            // 
            this.sb_imageBox.BackColor = System.Drawing.Color.MediumAquamarine;
            this.sb_imageBox.Location = new System.Drawing.Point(0, 0);
            this.sb_imageBox.Margin = new System.Windows.Forms.Padding(0);
            this.sb_imageBox.Name = "sb_imageBox";
            this.sb_imageBox.Size = new System.Drawing.Size(471, 397);
            this.sb_imageBox.TabIndex = 0;
            this.sb_imageBox.TabStop = false;
            // 
            // textureEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(661, 423);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "textureEditor";
            this.Text = "Texture Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.textureEditor_FormClosed);
            this.Load += new System.EventHandler(this.TextureEditor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.infoTabPanel.ResumeLayout(false);
            this.infoTabPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.info_bitmapImage)).EndInit();
            this.layoutTabPanel.ResumeLayout(false);
            this.layoutTabPanel.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ot_pan_info.ResumeLayout(false);
            this.ot_pan_info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ot_bitmap_preview)).EndInit();
            this.ot_pan_groups.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.sb_info.ResumeLayout(false);
            this.sb_info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sb_imageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel layoutTabPanel;
        private System.Windows.Forms.Panel infoTabPanel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel iconsPanel;
        private System.Windows.Forms.Label lt_category_label;
        private System.Windows.Forms.RadioButton lt_rb_models;
        private System.Windows.Forms.RadioButton lt_rb_level;
        private System.Windows.Forms.RadioButton lt_rb_all;
        private System.Windows.Forms.RadioButton lt_rb_objects;
        private System.Windows.Forms.TextBox info_Address;
        private System.Windows.Forms.Label lt_info_label;
        private System.Windows.Forms.PictureBox info_bitmapImage;
        private System.Windows.Forms.TextBox info_Height;
        private System.Windows.Forms.TextBox info_Width;
        private System.Windows.Forms.Button lt_importImage;
        private System.Windows.Forms.Button lt_exportImage;
        private System.Windows.Forms.TextBox info_Format;
        private System.Windows.Forms.TextBox info_SegmentAddress;
        private System.Windows.Forms.Panel ot_pan_icons;
        private System.Windows.Forms.Panel ot_pan_info;
        private System.Windows.Forms.TextBox ot_info_Format;
        private System.Windows.Forms.TextBox ot_info_SegAddress;
        private System.Windows.Forms.Button ot_importImage;
        private System.Windows.Forms.Button ot_exportImage;
        private System.Windows.Forms.TextBox ot_info_height;
        private System.Windows.Forms.TextBox ot_info_width;
        private System.Windows.Forms.PictureBox ot_bitmap_preview;
        private System.Windows.Forms.TextBox ot_info_RomAddress;
        private System.Windows.Forms.Label ot_info_label;
        private System.Windows.Forms.Panel ot_pan_groups;
        private System.Windows.Forms.Panel ot_pan_groupButtons;
        private System.Windows.Forms.TextBox ot_name;
        private System.Windows.Forms.PictureBox sb_imageBox;
        private System.Windows.Forms.Panel sb_info;
        private System.Windows.Forms.TextBox sb_info_Format;
        private System.Windows.Forms.TextBox sb_info_SegAddress;
        private System.Windows.Forms.Button sb_importImage;
        private System.Windows.Forms.Button sb_exportImage;
        private System.Windows.Forms.TextBox sb_info_Height;
        private System.Windows.Forms.TextBox sb_info_Width;
        private System.Windows.Forms.TextBox sb_info_RomAddress;
        private System.Windows.Forms.Label sb_TextureInfo_label;
        private System.Windows.Forms.RadioButton sb_useSolidColor;
        private System.Windows.Forms.RadioButton sb_useTexture;
        private System.Windows.Forms.NumericUpDown numericUpDown_Blue;
        private System.Windows.Forms.NumericUpDown numericUpDown_Green;
        private System.Windows.Forms.NumericUpDown numericUpDown_Red;
        private System.Windows.Forms.Label blue_label;
        private System.Windows.Forms.Label green_label;
        private System.Windows.Forms.Label red_label;
        private System.Windows.Forms.CheckBox checkBox_matchFogColor;
    }
}