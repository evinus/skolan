using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace virtuel
{
    public partial class Form1 : Form
    {
        List<Anställda> anställda = new List<Anställda>();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Försäljare säljare = new Försäljare(tbxFöNamn.Text, double.Parse(tbxProvi.Text), double.Parse(tbxFörsälj.Text));
            tbxRegister.AppendText(säljare.ToString());
            anställda.Add(säljare);
            tbxLönRegister.AppendText(säljare.Namn + " " + säljare.Beräknalön().ToString() + " kr" + "\n");
        }

        private void btnRegiKonsul_Click(object sender, EventArgs e)
        {
            Konsult konsult = new Konsult(tbxKonsNamn.Text, double.Parse(tbxTimlön.Text), double.Parse(tbxArbetat.Text));
            tbxRegister.AppendText(konsult.ToString());
            anställda.Add(konsult);
            tbxLönRegister.AppendText(konsult.Namn + " " + konsult.Beräknalön().ToString() + " kr" + "\n");
        }

        private void btnRegiKonto_Click(object sender, EventArgs e)
        {
            Kontorist kontorist = new Kontorist(double.Parse(tbxMånadslön.Text), tbxKontNamn.Text);
            tbxRegister.AppendText(kontorist.ToString());
            anställda.Add(kontorist);
            tbxLönRegister.AppendText(kontorist.Namn + " " + kontorist.Beräknalön().ToString() + " kr" + "\n");
        }

        private void btnBeräklöner_Click(object sender, EventArgs e)
        {
            tbxTotallöner.Clear();
            double tal;
            double total = 0;
            for (int i = 0; i < anställda.Count; i++)
            {
                tal = anställda[i].Beräknalön();
                total += tal;
            }
            tbxTotallöner.AppendText(total.ToString());

        }

      
    }
}
