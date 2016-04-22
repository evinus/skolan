namespace SägHej
{
	partial class KlientForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( )
		{
            this.btnSägHej = new System.Windows.Forms.Button();
            this.lblIPAdress = new System.Windows.Forms.Label();
            this.tbxIPAdress = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.btnAnslut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSägHej
            // 
            this.btnSägHej.Enabled = false;
            this.btnSägHej.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSägHej.ForeColor = System.Drawing.Color.DarkRed;
            this.btnSägHej.Location = new System.Drawing.Point(15, 113);
            this.btnSägHej.Name = "btnSägHej";
            this.btnSägHej.Size = new System.Drawing.Size(162, 38);
            this.btnSägHej.TabIndex = 0;
            this.btnSägHej.Text = "Säg Hej!";
            this.btnSägHej.UseVisualStyleBackColor = true;
            this.btnSägHej.Click += new System.EventHandler(this.btnSägHej_Click);
            // 
            // lblIPAdress
            // 
            this.lblIPAdress.AutoSize = true;
            this.lblIPAdress.Location = new System.Drawing.Point(12, 20);
            this.lblIPAdress.Name = "lblIPAdress";
            this.lblIPAdress.Size = new System.Drawing.Size(51, 13);
            this.lblIPAdress.TabIndex = 1;
            this.lblIPAdress.Text = "IP-adress";
            // 
            // tbxIPAdress
            // 
            this.tbxIPAdress.Location = new System.Drawing.Point(77, 17);
            this.tbxIPAdress.Name = "tbxIPAdress";
            this.tbxIPAdress.Size = new System.Drawing.Size(100, 20);
            this.tbxIPAdress.TabIndex = 2;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(12, 46);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 3;
            this.lblPort.Text = "Port";
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(77, 43);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(100, 20);
            this.tbxPort.TabIndex = 4;
            // 
            // btnAnslut
            // 
            this.btnAnslut.Location = new System.Drawing.Point(102, 69);
            this.btnAnslut.Name = "btnAnslut";
            this.btnAnslut.Size = new System.Drawing.Size(75, 23);
            this.btnAnslut.TabIndex = 5;
            this.btnAnslut.Text = "Anslut";
            this.btnAnslut.UseVisualStyleBackColor = true;
            this.btnAnslut.Click += new System.EventHandler(this.btnAnslut_Click);
            // 
            // KlientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 165);
            this.Controls.Add(this.btnAnslut);
            this.Controls.Add(this.tbxPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.tbxIPAdress);
            this.Controls.Add(this.lblIPAdress);
            this.Controls.Add(this.btnSägHej);
            this.Name = "KlientForm";
            this.Text = "Säg Hej!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KlientForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSägHej;
		private System.Windows.Forms.Label lblIPAdress;
		private System.Windows.Forms.TextBox tbxIPAdress;
		private System.Windows.Forms.Label lblPort;
		private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.Button btnAnslut;
	}
}

