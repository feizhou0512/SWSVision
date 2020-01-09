using TestClass.SWS;

namespace VisionTest1
{
    partial class Form1
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.m_pic_ShowImage = new System.Windows.Forms.PictureBox();
            this.SetReferenceImage = new System.Windows.Forms.Button();
            this.ExitProgram = new System.Windows.Forms.Button();
            this.ShowMatchImage = new System.Windows.Forms.CheckBox();
            this.showBlobs = new System.Windows.Forms.CheckBox();
            this.DemoShow = new System.Windows.Forms.Button();
            this.showHist = new System.Windows.Forms.CheckBox();
            this.Can_DO = new System.Windows.Forms.Button();
            this.Snapshot = new System.Windows.Forms.Button();
            this.Can_init = new System.Windows.Forms.Button();
            this.Can_Close = new System.Windows.Forms.Button();
            this.Flip = new System.Windows.Forms.CheckBox();
            this.Debug = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_pic_ShowImage)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1334, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // m_pic_ShowImage
            // 
            this.m_pic_ShowImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pic_ShowImage.Location = new System.Drawing.Point(308, 45);
            this.m_pic_ShowImage.Name = "m_pic_ShowImage";
            this.m_pic_ShowImage.Size = new System.Drawing.Size(945, 699);
            this.m_pic_ShowImage.TabIndex = 13;
            this.m_pic_ShowImage.TabStop = false;
            // 
            // SetReferenceImage
            // 
            this.SetReferenceImage.Location = new System.Drawing.Point(34, 812);
            this.SetReferenceImage.Name = "SetReferenceImage";
            this.SetReferenceImage.Size = new System.Drawing.Size(180, 40);
            this.SetReferenceImage.TabIndex = 15;
            this.SetReferenceImage.Text = "SetReferenceImage";
            this.SetReferenceImage.UseVisualStyleBackColor = true;
            this.SetReferenceImage.Click += new System.EventHandler(this.SetReferenceImage_Click);
            // 
            // ExitProgram
            // 
            this.ExitProgram.Location = new System.Drawing.Point(24, 382);
            this.ExitProgram.Name = "ExitProgram";
            this.ExitProgram.Size = new System.Drawing.Size(101, 51);
            this.ExitProgram.TabIndex = 22;
            this.ExitProgram.Text = "Exit";
            this.ExitProgram.UseVisualStyleBackColor = true;
            this.ExitProgram.Click += new System.EventHandler(this.ExitProgram_Click);
            // 
            // ShowMatchImage
            // 
            this.ShowMatchImage.AutoSize = true;
            this.ShowMatchImage.Location = new System.Drawing.Point(308, 830);
            this.ShowMatchImage.Name = "ShowMatchImage";
            this.ShowMatchImage.Size = new System.Drawing.Size(160, 22);
            this.ShowMatchImage.TabIndex = 24;
            this.ShowMatchImage.Text = "ShowMatchImage";
            this.ShowMatchImage.UseVisualStyleBackColor = true;
            // 
            // showBlobs
            // 
            this.showBlobs.AutoSize = true;
            this.showBlobs.Location = new System.Drawing.Point(501, 830);
            this.showBlobs.Name = "showBlobs";
            this.showBlobs.Size = new System.Drawing.Size(124, 22);
            this.showBlobs.TabIndex = 35;
            this.showBlobs.Text = "Show Blobs";
            this.showBlobs.UseVisualStyleBackColor = true;
            // 
            // DemoShow
            // 
            this.DemoShow.Location = new System.Drawing.Point(24, 245);
            this.DemoShow.Name = "DemoShow";
            this.DemoShow.Size = new System.Drawing.Size(156, 35);
            this.DemoShow.TabIndex = 37;
            this.DemoShow.Text = "VisionTest";
            this.DemoShow.UseVisualStyleBackColor = true;
            this.DemoShow.Click += new System.EventHandler(this.DemoShow_Click);
            // 
            // showHist
            // 
            this.showHist.AutoSize = true;
            this.showHist.Location = new System.Drawing.Point(671, 830);
            this.showHist.Name = "showHist";
            this.showHist.Size = new System.Drawing.Size(124, 22);
            this.showHist.TabIndex = 38;
            this.showHist.Text = "Show Hists";
            this.showHist.UseVisualStyleBackColor = true;
            // 
            // Can_DO
            // 
            this.Can_DO.Location = new System.Drawing.Point(24, 112);
            this.Can_DO.Name = "Can_DO";
            this.Can_DO.Size = new System.Drawing.Size(156, 35);
            this.Can_DO.TabIndex = 40;
            this.Can_DO.Text = "Key Test";
            this.Can_DO.UseVisualStyleBackColor = true;
            this.Can_DO.Click += new System.EventHandler(this.Can_DO_Click);
            // 
            // Snapshot
            // 
            this.Snapshot.Location = new System.Drawing.Point(24, 176);
            this.Snapshot.Name = "Snapshot";
            this.Snapshot.Size = new System.Drawing.Size(156, 34);
            this.Snapshot.TabIndex = 42;
            this.Snapshot.Text = "Snapshot";
            this.Snapshot.UseVisualStyleBackColor = true;
            this.Snapshot.Click += new System.EventHandler(this.Snapshot_Click);
            // 
            // Can_init
            // 
            this.Can_init.Location = new System.Drawing.Point(24, 45);
            this.Can_init.Name = "Can_init";
            this.Can_init.Size = new System.Drawing.Size(156, 35);
            this.Can_init.TabIndex = 43;
            this.Can_init.Text = "Can Init";
            this.Can_init.UseVisualStyleBackColor = true;
            this.Can_init.Click += new System.EventHandler(this.Can_init_Click_1);
            // 
            // Can_Close
            // 
            this.Can_Close.Location = new System.Drawing.Point(24, 319);
            this.Can_Close.Name = "Can_Close";
            this.Can_Close.Size = new System.Drawing.Size(156, 35);
            this.Can_Close.TabIndex = 44;
            this.Can_Close.Text = "Can Close";
            this.Can_Close.UseVisualStyleBackColor = true;
            this.Can_Close.Click += new System.EventHandler(this.Can_Close_Click_1);
            // 
            // Flip
            // 
            this.Flip.AutoSize = true;
            this.Flip.Location = new System.Drawing.Point(196, 240);
            this.Flip.Name = "Flip";
            this.Flip.Size = new System.Drawing.Size(70, 22);
            this.Flip.TabIndex = 45;
            this.Flip.Text = "Flip";
            this.Flip.UseVisualStyleBackColor = true;
            // 
            // Debug
            // 
            this.Debug.AutoSize = true;
            this.Debug.Location = new System.Drawing.Point(196, 268);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(79, 22);
            this.Debug.TabIndex = 46;
            this.Debug.Text = "Debug";
            this.Debug.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 932);
            this.Controls.Add(this.Debug);
            this.Controls.Add(this.Flip);
            this.Controls.Add(this.Can_Close);
            this.Controls.Add(this.Can_init);
            this.Controls.Add(this.Snapshot);
            this.Controls.Add(this.Can_DO);
            this.Controls.Add(this.showHist);
            this.Controls.Add(this.DemoShow);
            this.Controls.Add(this.showBlobs);
            this.Controls.Add(this.ShowMatchImage);
            this.Controls.Add(this.ExitProgram);
            this.Controls.Add(this.SetReferenceImage);
            this.Controls.Add(this.m_pic_ShowImage);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.m_pic_ShowImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.PictureBox m_pic_ShowImage;
        private System.Windows.Forms.Button SetReferenceImage;
        private System.Windows.Forms.Button ExitProgram;
        private System.Windows.Forms.CheckBox ShowMatchImage;
        private System.Windows.Forms.CheckBox showBlobs;
        private System.Windows.Forms.Button DemoShow;
        private System.Windows.Forms.CheckBox showHist;
        private System.Windows.Forms.Button Can_DO;
        private System.Windows.Forms.Button Snapshot;
        private System.Windows.Forms.Button Can_init;
        private System.Windows.Forms.Button Can_Close;
        private System.Windows.Forms.CheckBox Flip;
        private System.Windows.Forms.CheckBox Debug;
        //private PressAll Ki;
    }
}

