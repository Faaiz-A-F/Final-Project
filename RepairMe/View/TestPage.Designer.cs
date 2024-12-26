namespace RepairMe
{
    partial class TestPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestPage));
            this.btnTestConnextion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestConnextion
            // 
            this.btnTestConnextion.Location = new System.Drawing.Point(283, 164);
            this.btnTestConnextion.Name = "btnTestConnextion";
            this.btnTestConnextion.Size = new System.Drawing.Size(216, 104);
            this.btnTestConnextion.TabIndex = 0;
            this.btnTestConnextion.Text = "Test";
            this.btnTestConnextion.UseVisualStyleBackColor = true;
            this.btnTestConnextion.Click += new System.EventHandler(this.btnTestConnextion_Click);
            // 
            // TestPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTestConnextion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestConnextion;
    }
}