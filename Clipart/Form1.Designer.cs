namespace Clipart
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSkapaLinje = new System.Windows.Forms.Button();
            this.btnSkapaCirkel = new System.Windows.Forms.Button();
            this.btnSkapaTriangel = new System.Windows.Forms.Button();
            this.tbxHöjd = new System.Windows.Forms.TextBox();
            this.tbxBredd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbxRegister = new System.Windows.Forms.TextBox();
            this.tbxSammanlagdArea = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSkapaLinje);
            this.groupBox1.Controls.Add(this.btnSkapaCirkel);
            this.groupBox1.Controls.Add(this.btnSkapaTriangel);
            this.groupBox1.Controls.Add(this.tbxHöjd);
            this.groupBox1.Controls.Add(this.tbxBredd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Skapa";
            // 
            // btnSkapaLinje
            // 
            this.btnSkapaLinje.Location = new System.Drawing.Point(46, 143);
            this.btnSkapaLinje.Name = "btnSkapaLinje";
            this.btnSkapaLinje.Size = new System.Drawing.Size(105, 23);
            this.btnSkapaLinje.TabIndex = 6;
            this.btnSkapaLinje.Text = "Skapa Linje";
            this.btnSkapaLinje.UseVisualStyleBackColor = true;
            this.btnSkapaLinje.Click += new System.EventHandler(this.btnSkapaLinje_Click);
            // 
            // btnSkapaCirkel
            // 
            this.btnSkapaCirkel.Location = new System.Drawing.Point(46, 113);
            this.btnSkapaCirkel.Name = "btnSkapaCirkel";
            this.btnSkapaCirkel.Size = new System.Drawing.Size(105, 23);
            this.btnSkapaCirkel.TabIndex = 5;
            this.btnSkapaCirkel.Text = "Skapa Cirkel";
            this.btnSkapaCirkel.UseVisualStyleBackColor = true;
            this.btnSkapaCirkel.Click += new System.EventHandler(this.btnSkapaCirkel_Click);
            // 
            // btnSkapaTriangel
            // 
            this.btnSkapaTriangel.Location = new System.Drawing.Point(46, 84);
            this.btnSkapaTriangel.Name = "btnSkapaTriangel";
            this.btnSkapaTriangel.Size = new System.Drawing.Size(105, 23);
            this.btnSkapaTriangel.TabIndex = 4;
            this.btnSkapaTriangel.Text = "Skapa Rektangel";
            this.btnSkapaTriangel.UseVisualStyleBackColor = true;
            this.btnSkapaTriangel.Click += new System.EventHandler(this.btnSkapaTriangel_Click);
            // 
            // tbxHöjd
            // 
            this.tbxHöjd.Location = new System.Drawing.Point(64, 58);
            this.tbxHöjd.Name = "tbxHöjd";
            this.tbxHöjd.Size = new System.Drawing.Size(100, 20);
            this.tbxHöjd.TabIndex = 3;
            // 
            // tbxBredd
            // 
            this.tbxBredd.Location = new System.Drawing.Point(64, 31);
            this.tbxBredd.Name = "tbxBredd";
            this.tbxBredd.Size = new System.Drawing.Size(100, 20);
            this.tbxBredd.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Höjd";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bredd";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Sammanlagd Area";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbxRegister
            // 
            this.tbxRegister.Location = new System.Drawing.Point(225, 23);
            this.tbxRegister.Multiline = true;
            this.tbxRegister.Name = "tbxRegister";
            this.tbxRegister.Size = new System.Drawing.Size(160, 173);
            this.tbxRegister.TabIndex = 2;
            // 
            // tbxSammanlagdArea
            // 
            this.tbxSammanlagdArea.Location = new System.Drawing.Point(182, 260);
            this.tbxSammanlagdArea.Name = "tbxSammanlagdArea";
            this.tbxSammanlagdArea.Size = new System.Drawing.Size(100, 20);
            this.tbxSammanlagdArea.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 335);
            this.Controls.Add(this.tbxSammanlagdArea);
            this.Controls.Add(this.tbxRegister);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSkapaLinje;
        private System.Windows.Forms.Button btnSkapaCirkel;
        private System.Windows.Forms.Button btnSkapaTriangel;
        private System.Windows.Forms.TextBox tbxHöjd;
        private System.Windows.Forms.TextBox tbxBredd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbxRegister;
        private System.Windows.Forms.TextBox tbxSammanlagdArea;
    }
}

