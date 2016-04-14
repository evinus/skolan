namespace Fontstyle
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
            this.btnVerkställ = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbxFet = new System.Windows.Forms.CheckBox();
            this.cbxKursiv = new System.Windows.Forms.CheckBox();
            this.cbxUnderstryken = new System.Windows.Forms.CheckBox();
            this.rbUpphöjt = new System.Windows.Forms.RadioButton();
            this.rbNedsänkt = new System.Windows.Forms.RadioButton();
            this.cbxÖverstruken = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnVerkställ
            // 
            this.btnVerkställ.Location = new System.Drawing.Point(305, 279);
            this.btnVerkställ.Name = "btnVerkställ";
            this.btnVerkställ.Size = new System.Drawing.Size(75, 23);
            this.btnVerkställ.TabIndex = 0;
            this.btnVerkställ.Text = "Ok";
            this.btnVerkställ.UseVisualStyleBackColor = true;
            this.btnVerkställ.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 47);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 120);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Här testar vi lite grejer, man kan ha olika fonts och lite sådana grejer";
            // 
            // cbxFet
            // 
            this.cbxFet.AutoSize = true;
            this.cbxFet.Location = new System.Drawing.Point(32, 256);
            this.cbxFet.Name = "cbxFet";
            this.cbxFet.Size = new System.Drawing.Size(41, 17);
            this.cbxFet.TabIndex = 2;
            this.cbxFet.Text = "Fet";
            this.cbxFet.UseVisualStyleBackColor = true;
            // 
            // cbxKursiv
            // 
            this.cbxKursiv.AutoSize = true;
            this.cbxKursiv.Location = new System.Drawing.Point(32, 279);
            this.cbxKursiv.Name = "cbxKursiv";
            this.cbxKursiv.Size = new System.Drawing.Size(55, 17);
            this.cbxKursiv.TabIndex = 3;
            this.cbxKursiv.Text = "Kursiv";
            this.cbxKursiv.UseVisualStyleBackColor = true;
            // 
            // cbxUnderstryken
            // 
            this.cbxUnderstryken.AutoSize = true;
            this.cbxUnderstryken.Location = new System.Drawing.Point(92, 256);
            this.cbxUnderstryken.Name = "cbxUnderstryken";
            this.cbxUnderstryken.Size = new System.Drawing.Size(89, 17);
            this.cbxUnderstryken.TabIndex = 4;
            this.cbxUnderstryken.Text = "Understryken";
            this.cbxUnderstryken.UseVisualStyleBackColor = true;
            // 
            // rbUpphöjt
            // 
            this.rbUpphöjt.AutoSize = true;
            this.rbUpphöjt.Location = new System.Drawing.Point(92, 280);
            this.rbUpphöjt.Name = "rbUpphöjt";
            this.rbUpphöjt.Size = new System.Drawing.Size(62, 17);
            this.rbUpphöjt.TabIndex = 5;
            this.rbUpphöjt.TabStop = true;
            this.rbUpphöjt.Text = "Upphöjt";
            this.rbUpphöjt.UseVisualStyleBackColor = true;
            // 
            // rbNedsänkt
            // 
            this.rbNedsänkt.AutoSize = true;
            this.rbNedsänkt.Location = new System.Drawing.Point(176, 279);
            this.rbNedsänkt.Name = "rbNedsänkt";
            this.rbNedsänkt.Size = new System.Drawing.Size(71, 17);
            this.rbNedsänkt.TabIndex = 6;
            this.rbNedsänkt.TabStop = true;
            this.rbNedsänkt.Text = "Nedsänkt";
            this.rbNedsänkt.UseVisualStyleBackColor = true;
            // 
            // cbxÖverstruken
            // 
            this.cbxÖverstruken.AutoSize = true;
            this.cbxÖverstruken.Location = new System.Drawing.Point(32, 303);
            this.cbxÖverstruken.Name = "cbxÖverstruken";
            this.cbxÖverstruken.Size = new System.Drawing.Size(84, 17);
            this.cbxÖverstruken.TabIndex = 7;
            this.cbxÖverstruken.Text = "Överstruken";
            this.cbxÖverstruken.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 330);
            this.Controls.Add(this.cbxÖverstruken);
            this.Controls.Add(this.rbNedsänkt);
            this.Controls.Add(this.rbUpphöjt);
            this.Controls.Add(this.cbxUnderstryken);
            this.Controls.Add(this.cbxKursiv);
            this.Controls.Add(this.cbxFet);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnVerkställ);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVerkställ;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox cbxFet;
        private System.Windows.Forms.CheckBox cbxKursiv;
        private System.Windows.Forms.CheckBox cbxUnderstryken;
        private System.Windows.Forms.RadioButton rbUpphöjt;
        private System.Windows.Forms.RadioButton rbNedsänkt;
        private System.Windows.Forms.CheckBox cbxÖverstruken;
    }
}

