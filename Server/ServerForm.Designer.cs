namespace Server
{
	partial class ServerForm
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
            this.btnLyssna = new System.Windows.Forms.Button();
            this.tbxKlientData = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnLyssna
            // 
            this.btnLyssna.Location = new System.Drawing.Point(197, 12);
            this.btnLyssna.Name = "btnLyssna";
            this.btnLyssna.Size = new System.Drawing.Size(75, 23);
            this.btnLyssna.TabIndex = 0;
            this.btnLyssna.Text = "Lyssna";
            this.btnLyssna.UseVisualStyleBackColor = true;
            this.btnLyssna.Click += new System.EventHandler(this.btnLyssna_Click);
            // 
            // tbxKlientData
            // 
            this.tbxKlientData.Location = new System.Drawing.Point(12, 41);
            this.tbxKlientData.Multiline = true;
            this.tbxKlientData.Name = "tbxKlientData";
            this.tbxKlientData.Size = new System.Drawing.Size(260, 209);
            this.tbxKlientData.TabIndex = 1;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(12, 17);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port";
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(91, 14);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(100, 20);
            this.tbxPort.TabIndex = 3;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tbxPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.tbxKlientData);
            this.Controls.Add(this.btnLyssna);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLyssna;
		private System.Windows.Forms.TextBox tbxKlientData;
		private System.Windows.Forms.Label lblPort;
		private System.Windows.Forms.TextBox tbxPort;
	}
}

