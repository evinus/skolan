using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ränta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float belopp = float.Parse(textBox1.Text);
            float ränta = float.Parse(textBox3.Text);
            float räntatal;
            float totalränta =0;
            int tal = 0;
            while (belopp > 0)
            {
                float avbetalning = 271;
                räntatal = belopp*(ränta/100)/12;
                avbetalning = avbetalning - räntatal;
                belopp = belopp - avbetalning;
                totalränta = totalränta+ räntatal + 29;
                textBox2.AppendText("\r\n" + belopp.ToString());
                tal++;
            }
            textBox2.AppendText("\r\n" + tal.ToString());
            textBox2.AppendText("\r\n" + totalränta.ToString());

        }
    }
}
