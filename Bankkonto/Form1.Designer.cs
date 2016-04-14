namespace Bankkonto
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
            this.tbxPersonNR = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUT = new System.Windows.Forms.Button();
            this.btnsätt = new System.Windows.Forms.Button();
            this.tbxBelopp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxStartbelopp = new System.Windows.Forms.TextBox();
            this.btnRegis = new System.Windows.Forms.Button();
            this.cmbTyp = new System.Windows.Forms.ComboBox();
            this.tbxRänta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxRegister = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnÖppna = new System.Windows.Forms.Button();
            this.btnSpara = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxPersonNR
            // 
            this.tbxPersonNR.Location = new System.Drawing.Point(123, 12);
            this.tbxPersonNR.Name = "tbxPersonNR";
            this.tbxPersonNR.Size = new System.Drawing.Size(170, 20);
            this.tbxPersonNR.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Person Nummer";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUT);
            this.groupBox1.Controls.Add(this.btnsätt);
            this.groupBox1.Controls.Add(this.tbxBelopp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(33, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 120);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Insättning/Uttag";
            // 
            // btnUT
            // 
            this.btnUT.Location = new System.Drawing.Point(40, 86);
            this.btnUT.Name = "btnUT";
            this.btnUT.Size = new System.Drawing.Size(75, 23);
            this.btnUT.TabIndex = 3;
            this.btnUT.Text = "Ta Ut";
            this.btnUT.UseVisualStyleBackColor = true;
            this.btnUT.Click += new System.EventHandler(this.btnUT_Click);
            // 
            // btnsätt
            // 
            this.btnsätt.Location = new System.Drawing.Point(40, 56);
            this.btnsätt.Name = "btnsätt";
            this.btnsätt.Size = new System.Drawing.Size(75, 23);
            this.btnsätt.TabIndex = 2;
            this.btnsätt.Text = "Sätt in";
            this.btnsätt.UseVisualStyleBackColor = true;
            this.btnsätt.Click += new System.EventHandler(this.btnsätt_Click);
            // 
            // tbxBelopp
            // 
            this.tbxBelopp.Location = new System.Drawing.Point(53, 19);
            this.tbxBelopp.Name = "tbxBelopp";
            this.tbxBelopp.Size = new System.Drawing.Size(100, 20);
            this.tbxBelopp.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Belopp";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxStartbelopp);
            this.groupBox2.Controls.Add(this.btnRegis);
            this.groupBox2.Controls.Add(this.cmbTyp);
            this.groupBox2.Controls.Add(this.tbxRänta);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(207, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 159);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Öppna Konto";
            // 
            // tbxStartbelopp
            // 
            this.tbxStartbelopp.Location = new System.Drawing.Point(68, 56);
            this.tbxStartbelopp.Name = "tbxStartbelopp";
            this.tbxStartbelopp.Size = new System.Drawing.Size(100, 20);
            this.tbxStartbelopp.TabIndex = 4;
            // 
            // btnRegis
            // 
            this.btnRegis.Location = new System.Drawing.Point(56, 110);
            this.btnRegis.Name = "btnRegis";
            this.btnRegis.Size = new System.Drawing.Size(75, 23);
            this.btnRegis.TabIndex = 3;
            this.btnRegis.Text = "Registrera";
            this.btnRegis.UseVisualStyleBackColor = true;
            this.btnRegis.Click += new System.EventHandler(this.btnRegis_Click);
            // 
            // cmbTyp
            // 
            this.cmbTyp.FormattingEnabled = true;
            this.cmbTyp.Items.AddRange(new object[] {
            "Sparkonto",
            "Lånekonto"});
            this.cmbTyp.Location = new System.Drawing.Point(68, 83);
            this.cmbTyp.Name = "cmbTyp";
            this.cmbTyp.Size = new System.Drawing.Size(100, 21);
            this.cmbTyp.TabIndex = 2;
            // 
            // tbxRänta
            // 
            this.tbxRänta.Location = new System.Drawing.Point(68, 22);
            this.tbxRänta.Name = "tbxRänta";
            this.tbxRänta.Size = new System.Drawing.Size(100, 20);
            this.tbxRänta.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Räntesats";
            // 
            // tbxRegister
            // 
            this.tbxRegister.Location = new System.Drawing.Point(429, 11);
            this.tbxRegister.Multiline = true;
            this.tbxRegister.Name = "tbxRegister";
            this.tbxRegister.Size = new System.Drawing.Size(202, 188);
            this.tbxRegister.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(492, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Betala/Dra ränta";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnÖppna
            // 
            this.btnÖppna.Location = new System.Drawing.Point(13, 202);
            this.btnÖppna.Name = "btnÖppna";
            this.btnÖppna.Size = new System.Drawing.Size(75, 23);
            this.btnÖppna.TabIndex = 6;
            this.btnÖppna.Text = "Öppna";
            this.btnÖppna.UseVisualStyleBackColor = true;
            this.btnÖppna.Click += new System.EventHandler(this.btnÖppna_Click);
            // 
            // btnSpara
            // 
            this.btnSpara.Location = new System.Drawing.Point(94, 202);
            this.btnSpara.Name = "btnSpara";
            this.btnSpara.Size = new System.Drawing.Size(75, 23);
            this.btnSpara.TabIndex = 7;
            this.btnSpara.Text = "Spara";
            this.btnSpara.UseVisualStyleBackColor = true;
            this.btnSpara.Click += new System.EventHandler(this.btnSpara_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 237);
            this.Controls.Add(this.btnSpara);
            this.Controls.Add(this.btnÖppna);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbxRegister);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxPersonNR);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxPersonNR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUT;
        private System.Windows.Forms.Button btnsätt;
        private System.Windows.Forms.TextBox tbxBelopp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRegis;
        private System.Windows.Forms.ComboBox cmbTyp;
        private System.Windows.Forms.TextBox tbxRänta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxRegister;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbxStartbelopp;
        private System.Windows.Forms.Button btnÖppna;
        private System.Windows.Forms.Button btnSpara;
    }
}

