using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace richtextbox
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            
            InitializeComponent();
            for(int i =0; i < FontFamily.Families.Length;i++)
            {
                cbxFont.Items.Add(FontFamily.Families[i].Name);
            }
            cbxFont.SelectedItem = "Arial";
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult r = fontDialog1.ShowDialog();
            if(r==DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
                
            }
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog cldia = new ColorDialog();
            DialogResult resultat = cldia.ShowDialog();
            if(resultat==DialogResult.OK)
            {
                richTextBox1.SelectionColor = cldia.Color;
                pictureBox1.BackColor = cldia.Color;
            }
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            float värde = (float)nUD1.Value;
            richTextBox1.SelectionFont = new Font(cbxFont.SelectedItem.ToString(),värde);
        }

        private void cbxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            float värde = (float)nUD1.Value;
            richTextBox1.SelectionFont = new Font(cbxFont.SelectedItem.ToString(), värde);
        }
    }
}
