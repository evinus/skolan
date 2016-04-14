using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KundKorg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int nr = 3;
        int x1 = 95;
        int i = 1;
        
        private void btnPlus_Click(object sender, EventArgs e)
        {
            nr++;
            string namn1 = "tbxVara" + nr.ToString();
            string namn2 = "tbxPris" + nr.ToString();
            TextBox tbxVara = new TextBox();
            TextBox tbxPris = new TextBox();
            tbxPris.Name = namn2;
            tbxVara.Name = namn1;
            tbxVara.Size = new Size(269, 20);
            tbxVara.Location = new Point(13 , (x1 + 27 * i));
            tbxPris.Location = new Point(316, (x1 + 27 * i));
            panel1.Controls.Add(tbxVara);
            panel1.Controls.Add(tbxPris);
            btnPlus.Location = new Point(423, (x1 + 27 * i));
            i++;
            
        }
    }
}
