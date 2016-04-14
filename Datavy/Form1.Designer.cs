namespace Datavy
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
            this.dgvKontakter = new System.Windows.Forms.DataGridView();
            this.colHemmaMatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBortaMatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMålHemma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMålBorta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKontakter)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvKontakter
            // 
            this.dgvKontakter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKontakter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHemmaMatch,
            this.colBortaMatch,
            this.colMålHemma,
            this.colMålBorta});
            this.dgvKontakter.Location = new System.Drawing.Point(13, 13);
            this.dgvKontakter.Name = "dgvKontakter";
            this.dgvKontakter.Size = new System.Drawing.Size(446, 257);
            this.dgvKontakter.TabIndex = 0;
            this.dgvKontakter.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKontakter_CellEndEdit);
            this.dgvKontakter.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvKontakter_DefaultValuesNeeded);
            this.dgvKontakter.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvKontakter_UserAddedRow);
            this.dgvKontakter.Click += new System.EventHandler(this.dgvKontakter_Click);
            // 
            // colHemmaMatch
            // 
            this.colHemmaMatch.HeaderText = "Hemmamatch";
            this.colHemmaMatch.Name = "colHemmaMatch";
            // 
            // colBortaMatch
            // 
            this.colBortaMatch.HeaderText = "Bortamatch";
            this.colBortaMatch.Name = "colBortaMatch";
            // 
            // colMålHemma
            // 
            this.colMålHemma.HeaderText = "Mål Hemma";
            this.colMålHemma.Name = "colMålHemma";
            // 
            // colMålBorta
            // 
            this.colMålBorta.HeaderText = "Mål Borta";
            this.colMålBorta.Name = "colMålBorta";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 291);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(140, 291);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(261, 291);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(343, 293);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 338);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvKontakter);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvKontakter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKontakter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHemmaMatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBortaMatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMålHemma;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMålBorta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
    }
}

