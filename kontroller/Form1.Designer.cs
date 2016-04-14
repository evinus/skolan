namespace kontroller
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
            this.labelTextbox1 = new kontroller.LabelTextbox();
            this.emailAdrresTextbox1 = new kontroller.EmailAdrresTextbox();
            this.SuspendLayout();
            // 
            // labelTextbox1
            // 
            this.labelTextbox1.ForeColor = System.Drawing.Color.Gray;
            this.labelTextbox1.LedText = "Hejsan";
            this.labelTextbox1.Location = new System.Drawing.Point(62, 142);
            this.labelTextbox1.Name = "labelTextbox1";
            this.labelTextbox1.Size = new System.Drawing.Size(100, 20);
            this.labelTextbox1.TabIndex = 1;
            this.labelTextbox1.Text = "Hejsan";
            // 
            // emailAdrresTextbox1
            // 
            this.emailAdrresTextbox1.Location = new System.Drawing.Point(62, 73);
            this.emailAdrresTextbox1.Name = "emailAdrresTextbox1";
            this.emailAdrresTextbox1.Size = new System.Drawing.Size(136, 20);
            this.emailAdrresTextbox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.labelTextbox1);
            this.Controls.Add(this.emailAdrresTextbox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EmailAdrresTextbox emailAdrresTextbox1;
        private LabelTextbox labelTextbox1;
    }
}

