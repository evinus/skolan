namespace KundKorg
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnChecka = new System.Windows.Forms.Button();
            this.tbxVara1 = new System.Windows.Forms.TextBox();
            this.tbxVara2 = new System.Windows.Forms.TextBox();
            this.tbxVara3 = new System.Windows.Forms.TextBox();
            this.tbxPris1 = new System.Windows.Forms.TextBox();
            this.tbxPris2 = new System.Windows.Forms.TextBox();
            this.tbxPris3 = new System.Windows.Forms.TextBox();
            this.btnPlus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnChecka);
            this.splitContainer1.Size = new System.Drawing.Size(652, 304);
            this.splitContainer1.SplitterDistance = 460;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPlus);
            this.panel1.Controls.Add(this.tbxPris3);
            this.panel1.Controls.Add(this.tbxPris2);
            this.panel1.Controls.Add(this.tbxPris1);
            this.panel1.Controls.Add(this.tbxVara3);
            this.panel1.Controls.Add(this.tbxVara2);
            this.panel1.Controls.Add(this.tbxVara1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(458, 302);
            this.panel1.TabIndex = 0;
            // 
            // btnChecka
            // 
            this.btnChecka.Location = new System.Drawing.Point(68, 250);
            this.btnChecka.Name = "btnChecka";
            this.btnChecka.Size = new System.Drawing.Size(75, 23);
            this.btnChecka.TabIndex = 0;
            this.btnChecka.Text = "Checka ut";
            this.btnChecka.UseVisualStyleBackColor = true;
            // 
            // tbxVara1
            // 
            this.tbxVara1.Location = new System.Drawing.Point(12, 41);
            this.tbxVara1.Name = "tbxVara1";
            this.tbxVara1.Size = new System.Drawing.Size(269, 20);
            this.tbxVara1.TabIndex = 0;
            // 
            // tbxVara2
            // 
            this.tbxVara2.Location = new System.Drawing.Point(13, 68);
            this.tbxVara2.Name = "tbxVara2";
            this.tbxVara2.Size = new System.Drawing.Size(268, 20);
            this.tbxVara2.TabIndex = 1;
            // 
            // tbxVara3
            // 
            this.tbxVara3.Location = new System.Drawing.Point(13, 95);
            this.tbxVara3.Name = "tbxVara3";
            this.tbxVara3.Size = new System.Drawing.Size(268, 20);
            this.tbxVara3.TabIndex = 2;
            // 
            // tbxPris1
            // 
            this.tbxPris1.Location = new System.Drawing.Point(316, 41);
            this.tbxPris1.Name = "tbxPris1";
            this.tbxPris1.Size = new System.Drawing.Size(100, 20);
            this.tbxPris1.TabIndex = 3;
            // 
            // tbxPris2
            // 
            this.tbxPris2.Location = new System.Drawing.Point(316, 68);
            this.tbxPris2.Name = "tbxPris2";
            this.tbxPris2.Size = new System.Drawing.Size(100, 20);
            this.tbxPris2.TabIndex = 4;
            // 
            // tbxPris3
            // 
            this.tbxPris3.Location = new System.Drawing.Point(316, 95);
            this.tbxPris3.Name = "tbxPris3";
            this.tbxPris3.Size = new System.Drawing.Size(100, 20);
            this.tbxPris3.TabIndex = 5;
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(423, 91);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(23, 23);
            this.btnPlus.TabIndex = 6;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 304);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.TextBox tbxPris3;
        private System.Windows.Forms.TextBox tbxPris2;
        private System.Windows.Forms.TextBox tbxPris1;
        private System.Windows.Forms.TextBox tbxVara3;
        private System.Windows.Forms.TextBox tbxVara2;
        private System.Windows.Forms.TextBox tbxVara1;
        private System.Windows.Forms.Button btnChecka;
    }
}

