using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bildtabell
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PictureBox[,] bilder = new PictureBox[5, 5];
            int bredd = 50, x = 0, y = 0, höjd = 50;
            for (int i = 0; i < 5; i++)
            {
                for (int l = 0; l < 5; l++)
                {
                    bilder[i, l] = new PictureBox();
                    bilder[i, l].Left = x;
                    bilder[i, l].Top = y;
                    bilder[i, l].Width = bredd;
                    bilder[i, l].Height = höjd;
                    bilder[i, l].BackgroundImage = Image.FromFile("cirkel.png");
                    bilder[i, l].Click += pbxBlomma_Click;
                    this.Controls.Add(bilder[i, l]);
                    x += bredd;
                }
                x = 0;
                y += höjd;
            }
        }
        public void pbxBlomma_Click (object sender, EventArgs e)
        {
            PictureBox klickadblomma = (PictureBox) sender;
            klickadblomma.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
