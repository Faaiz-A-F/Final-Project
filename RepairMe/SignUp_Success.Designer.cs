namespace RepairMe
{
    partial class SignUp_Success
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btndashboard = new Guna.UI2.WinForms.Guna2Button();
            this.btnhome = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(529, 277);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welcome to Repair Me";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(469, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(369, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Kamu sudah berhasil terdaftar bersama kami.";
            // 
            // btndashboard
            // 
            this.btndashboard.Animated = true;
            this.btndashboard.BackColor = System.Drawing.Color.Transparent;
            this.btndashboard.BorderRadius = 10;
            this.btndashboard.CheckedState.Parent = this.btndashboard;
            this.btndashboard.CustomImages.Parent = this.btndashboard;
            this.btndashboard.FillColor = System.Drawing.SystemColors.HotTrack;
            this.btndashboard.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndashboard.ForeColor = System.Drawing.Color.White;
            this.btndashboard.HoverState.Parent = this.btndashboard;
            this.btndashboard.Location = new System.Drawing.Point(564, 426);
            this.btndashboard.Name = "btndashboard";
            this.btndashboard.ShadowDecoration.Parent = this.btndashboard;
            this.btndashboard.Size = new System.Drawing.Size(187, 45);
            this.btndashboard.TabIndex = 4;
            this.btndashboard.Text = "My Dashboard";
            // 
            // btnhome
            // 
            this.btnhome.Animated = true;
            this.btnhome.BackColor = System.Drawing.Color.Transparent;
            this.btnhome.BorderRadius = 10;
            this.btnhome.CheckedState.Parent = this.btnhome;
            this.btnhome.CustomImages.Parent = this.btnhome;
            this.btnhome.FillColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnhome.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhome.ForeColor = System.Drawing.Color.White;
            this.btnhome.HoverState.Parent = this.btnhome;
            this.btnhome.Location = new System.Drawing.Point(564, 477);
            this.btnhome.Name = "btnhome";
            this.btnhome.ShadowDecoration.Parent = this.btnhome;
            this.btnhome.Size = new System.Drawing.Size(187, 45);
            this.btnhome.TabIndex = 5;
            this.btnhome.Text = "Home Page";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Location = new System.Drawing.Point(560, 84);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.ShadowDecoration.Parent = this.guna2PictureBox1;
            this.guna2PictureBox1.Size = new System.Drawing.Size(187, 175);
            this.guna2PictureBox1.TabIndex = 0;
            this.guna2PictureBox1.TabStop = false;
            // 
            // SignUp_Success
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::RepairMe.Properties.Resources._05;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.btnhome);
            this.Controls.Add(this.btndashboard);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2PictureBox1);
            this.Name = "SignUp_Success";
            this.Text = "Repair Me";
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button btndashboard;
        private Guna.UI2.WinForms.Guna2Button btnhome;
    }
}