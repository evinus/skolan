namespace TrädVy
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("QE", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("AQ", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Bataljon", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tvwBataljon = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tbxNamn = new System.Windows.Forms.TextBox();
            this.tbxNr = new System.Windows.Forms.TextBox();
            this.btnLäggTill = new System.Windows.Forms.Button();
            this.tbxInfo = new System.Windows.Forms.TextBox();
            this.btnTaBort = new System.Windows.Forms.Button();
            this.btnÅngra = new System.Windows.Forms.Button();
            this.gbxNyEnhet = new System.Windows.Forms.GroupBox();
            this.gbxNyEnhet.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvwBataljon
            // 
            this.tvwBataljon.HideSelection = false;
            this.tvwBataljon.ImageIndex = 0;
            this.tvwBataljon.ImageList = this.imageList1;
            this.tvwBataljon.Location = new System.Drawing.Point(13, 12);
            this.tvwBataljon.Name = "tvwBataljon";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "Node1";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "QE";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "AQ";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "AQ";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Bataljon";
            this.tvwBataljon.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.tvwBataljon.SelectedImageIndex = 0;
            this.tvwBataljon.Size = new System.Drawing.Size(202, 216);
            this.tvwBataljon.TabIndex = 0;
            this.tvwBataljon.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bataljon.png");
            this.imageList1.Images.SetKeyName(1, "kompani.png");
            this.imageList1.Images.SetKeyName(2, "pluton.png");
            this.imageList1.Images.SetKeyName(3, "grupp.png");
            // 
            // tbxNamn
            // 
            this.tbxNamn.Location = new System.Drawing.Point(29, 27);
            this.tbxNamn.Name = "tbxNamn";
            this.tbxNamn.Size = new System.Drawing.Size(120, 20);
            this.tbxNamn.TabIndex = 1;
            // 
            // tbxNr
            // 
            this.tbxNr.Location = new System.Drawing.Point(29, 54);
            this.tbxNr.Name = "tbxNr";
            this.tbxNr.Size = new System.Drawing.Size(120, 20);
            this.tbxNr.TabIndex = 2;
            // 
            // btnLäggTill
            // 
            this.btnLäggTill.Location = new System.Drawing.Point(29, 102);
            this.btnLäggTill.Name = "btnLäggTill";
            this.btnLäggTill.Size = new System.Drawing.Size(75, 20);
            this.btnLäggTill.TabIndex = 3;
            this.btnLäggTill.Text = "Lägg Till";
            this.btnLäggTill.UseVisualStyleBackColor = true;
            this.btnLäggTill.Click += new System.EventHandler(this.btnLäggTill_Click);
            // 
            // tbxInfo
            // 
            this.tbxInfo.Location = new System.Drawing.Point(276, 251);
            this.tbxInfo.Multiline = true;
            this.tbxInfo.Name = "tbxInfo";
            this.tbxInfo.Size = new System.Drawing.Size(197, 98);
            this.tbxInfo.TabIndex = 4;
            // 
            // btnTaBort
            // 
            this.btnTaBort.Location = new System.Drawing.Point(13, 294);
            this.btnTaBort.Name = "btnTaBort";
            this.btnTaBort.Size = new System.Drawing.Size(75, 23);
            this.btnTaBort.TabIndex = 5;
            this.btnTaBort.Text = "Ta Bort";
            this.btnTaBort.UseVisualStyleBackColor = true;
            this.btnTaBort.Click += new System.EventHandler(this.btnTaBort_Click);
            // 
            // btnÅngra
            // 
            this.btnÅngra.Location = new System.Drawing.Point(110, 101);
            this.btnÅngra.Name = "btnÅngra";
            this.btnÅngra.Size = new System.Drawing.Size(75, 23);
            this.btnÅngra.TabIndex = 6;
            this.btnÅngra.Text = "Ångra";
            this.btnÅngra.UseVisualStyleBackColor = true;
            this.btnÅngra.Click += new System.EventHandler(this.btnÅngra_Click);
            // 
            // gbxNyEnhet
            // 
            this.gbxNyEnhet.Controls.Add(this.tbxNr);
            this.gbxNyEnhet.Controls.Add(this.btnÅngra);
            this.gbxNyEnhet.Controls.Add(this.tbxNamn);
            this.gbxNyEnhet.Controls.Add(this.btnLäggTill);
            this.gbxNyEnhet.Location = new System.Drawing.Point(248, 12);
            this.gbxNyEnhet.Name = "gbxNyEnhet";
            this.gbxNyEnhet.Size = new System.Drawing.Size(200, 162);
            this.gbxNyEnhet.TabIndex = 7;
            this.gbxNyEnhet.TabStop = false;
            this.gbxNyEnhet.Text = "Ny Enhet";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 361);
            this.Controls.Add(this.gbxNyEnhet);
            this.Controls.Add(this.btnTaBort);
            this.Controls.Add(this.tbxInfo);
            this.Controls.Add(this.tvwBataljon);
            this.Name = "Form1";
            this.Text = "Form1";
            this.gbxNyEnhet.ResumeLayout(false);
            this.gbxNyEnhet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvwBataljon;
        private System.Windows.Forms.TextBox tbxNamn;
        private System.Windows.Forms.TextBox tbxNr;
        private System.Windows.Forms.Button btnLäggTill;
        private System.Windows.Forms.TextBox tbxInfo;
        private System.Windows.Forms.Button btnTaBort;
        private System.Windows.Forms.Button btnÅngra;
        private System.Windows.Forms.GroupBox gbxNyEnhet;
        private System.Windows.Forms.ImageList imageList1;
    }
}

