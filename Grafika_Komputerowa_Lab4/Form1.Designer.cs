
namespace Grafika_Komputerowa_Lab4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fogHScrollBar = new System.Windows.Forms.HScrollBar();
            this.shadingComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cameraComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FOVlabel = new System.Windows.Forms.Label();
            this.FOVhScrollBar = new System.Windows.Forms.HScrollBar();
            this.pointLight1CheckBox = new System.Windows.Forms.CheckBox();
            this.pointLight2CheckBox = new System.Windows.Forms.CheckBox();
            this.spotLightCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.spotLightCheckBox);
            this.splitContainer1.Panel2.Controls.Add(this.pointLight2CheckBox);
            this.splitContainer1.Panel2.Controls.Add(this.pointLight1CheckBox);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.fogHScrollBar);
            this.splitContainer1.Panel2.Controls.Add(this.shadingComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.cameraComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.FOVlabel);
            this.splitContainer1.Panel2.Controls.Add(this.FOVhScrollBar);
            this.splitContainer1.Size = new System.Drawing.Size(1340, 887);
            this.splitContainer1.SplitterDistance = 1005;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1005, 887);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fog";
            // 
            // fogHScrollBar
            // 
            this.fogHScrollBar.LargeChange = 1;
            this.fogHScrollBar.Location = new System.Drawing.Point(23, 335);
            this.fogHScrollBar.Maximum = 50;
            this.fogHScrollBar.Minimum = 10;
            this.fogHScrollBar.Name = "fogHScrollBar";
            this.fogHScrollBar.Size = new System.Drawing.Size(134, 28);
            this.fogHScrollBar.TabIndex = 6;
            this.fogHScrollBar.Value = 10;
            this.fogHScrollBar.ValueChanged += new System.EventHandler(this.fogHScrollBar_ValueChanged);
            // 
            // shadingComboBox
            // 
            this.shadingComboBox.FormattingEnabled = true;
            this.shadingComboBox.Items.AddRange(new object[] {
            "Constant",
            "Phong",
            "Gourad"});
            this.shadingComboBox.Location = new System.Drawing.Point(37, 260);
            this.shadingComboBox.Name = "shadingComboBox";
            this.shadingComboBox.Size = new System.Drawing.Size(121, 28);
            this.shadingComboBox.TabIndex = 5;
            this.shadingComboBox.DropDownClosed += new System.EventHandler(this.shadingComboBox_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(35, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Shading";
            // 
            // cameraComboBox
            // 
            this.cameraComboBox.FormattingEnabled = true;
            this.cameraComboBox.Items.AddRange(new object[] {
            "Static",
            "Watch",
            "Move"});
            this.cameraComboBox.Location = new System.Drawing.Point(35, 155);
            this.cameraComboBox.Name = "cameraComboBox";
            this.cameraComboBox.Size = new System.Drawing.Size(121, 28);
            this.cameraComboBox.TabIndex = 3;
            this.cameraComboBox.DropDownClosed += new System.EventHandler(this.cameraComboBox_DropDownClosed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(33, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Camera";
            // 
            // FOVlabel
            // 
            this.FOVlabel.AutoSize = true;
            this.FOVlabel.Location = new System.Drawing.Point(59, 21);
            this.FOVlabel.Name = "FOVlabel";
            this.FOVlabel.Size = new System.Drawing.Size(36, 20);
            this.FOVlabel.TabIndex = 1;
            this.FOVlabel.Text = "FOV";
            // 
            // FOVhScrollBar
            // 
            this.FOVhScrollBar.LargeChange = 1;
            this.FOVhScrollBar.Location = new System.Drawing.Point(23, 52);
            this.FOVhScrollBar.Minimum = 45;
            this.FOVhScrollBar.Name = "FOVhScrollBar";
            this.FOVhScrollBar.Size = new System.Drawing.Size(134, 28);
            this.FOVhScrollBar.TabIndex = 0;
            this.FOVhScrollBar.Value = 45;
            this.FOVhScrollBar.ValueChanged += new System.EventHandler(this.FOVhScrollBar_ValueChanged);
            // 
            // pointLight1CheckBox
            // 
            this.pointLight1CheckBox.AutoSize = true;
            this.pointLight1CheckBox.Checked = true;
            this.pointLight1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pointLight1CheckBox.Location = new System.Drawing.Point(33, 388);
            this.pointLight1CheckBox.Name = "pointLight1CheckBox";
            this.pointLight1CheckBox.Size = new System.Drawing.Size(76, 24);
            this.pointLight1CheckBox.TabIndex = 8;
            this.pointLight1CheckBox.Text = "Point 1";
            this.pointLight1CheckBox.UseVisualStyleBackColor = true;
            this.pointLight1CheckBox.CheckedChanged += new System.EventHandler(this.pointLight1CheckBox_CheckedChanged);
            // 
            // pointLight2CheckBox
            // 
            this.pointLight2CheckBox.AutoSize = true;
            this.pointLight2CheckBox.Location = new System.Drawing.Point(33, 418);
            this.pointLight2CheckBox.Name = "pointLight2CheckBox";
            this.pointLight2CheckBox.Size = new System.Drawing.Size(76, 24);
            this.pointLight2CheckBox.TabIndex = 9;
            this.pointLight2CheckBox.Text = "Point 2";
            this.pointLight2CheckBox.UseVisualStyleBackColor = true;
            this.pointLight2CheckBox.CheckedChanged += new System.EventHandler(this.pointLight2CheckBox_CheckedChanged);
            // 
            // spotLightCheckBox
            // 
            this.spotLightCheckBox.AutoSize = true;
            this.spotLightCheckBox.Location = new System.Drawing.Point(33, 448);
            this.spotLightCheckBox.Name = "spotLightCheckBox";
            this.spotLightCheckBox.Size = new System.Drawing.Size(66, 24);
            this.spotLightCheckBox.TabIndex = 10;
            this.spotLightCheckBox.Text = "Spot ";
            this.spotLightCheckBox.UseVisualStyleBackColor = true;
            this.spotLightCheckBox.CheckedChanged += new System.EventHandler(this.spotLightCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 887);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label FOVlabel;
        private System.Windows.Forms.HScrollBar FOVhScrollBar;
        private System.Windows.Forms.ComboBox cameraComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox shadingComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.HScrollBar fogHScrollBar;
        private System.Windows.Forms.CheckBox spotLightCheckBox;
        private System.Windows.Forms.CheckBox pointLight2CheckBox;
        private System.Windows.Forms.CheckBox pointLight1CheckBox;
    }
}

