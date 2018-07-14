namespace Quad64
{
    partial class SelectLevelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLevelForm));
            this.level_list = new System.Windows.Forms.ComboBox();
            this.load_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // level_list
            // 
            this.level_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.level_list.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.level_list.FormattingEnabled = true;
            this.level_list.Location = new System.Drawing.Point(69, 16);
            this.level_list.Name = "level_list";
            this.level_list.Size = new System.Drawing.Size(214, 21);
            this.level_list.TabIndex = 0;
            // 
            // load_button
            // 
            this.load_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.load_button.Location = new System.Drawing.Point(69, 55);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(104, 29);
            this.load_button.TabIndex = 1;
            this.load_button.Text = "Load";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel_button.Location = new System.Drawing.Point(179, 55);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(104, 29);
            this.cancel_button.TabIndex = 2;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // SelectLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 97);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.load_button);
            this.Controls.Add(this.level_list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectLevelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select a Level";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox level_list;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.Button cancel_button;
    }
}