using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Datavy
{
    public partial class Form1 : Form
    {
        private List<Match> matcher = new List<Match>();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void dgvKontakter_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            Match nyMatch = dgvKontakter.Rows[e.Row.Index - 1].Tag as Match;
            matcher.Add(nyMatch);
        }

        private void dgvKontakter_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Match match = dgvKontakter.Rows[e.RowIndex].Tag as Match;

            matcher[e.RowIndex].HemmaLag = dgvKontakter.Rows[e.RowIndex].Cells[0].Value as string;
            match.BortaLag = dgvKontakter.Rows[e.RowIndex].Cells[1].Value as string;
            match.MålHemmaLag = int.Parse(dgvKontakter.Rows[e.RowIndex].Cells[2].Value as string);
            match.MålBortaLag = int.Parse(dgvKontakter.Rows[e.RowIndex].Cells[3].Value as string);

        }

        private void dgvKontakter_Click(object sender, EventArgs e)
        {

        }

        private void dgvKontakter_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            Match nyMatch = new Match()
            {
                HemmaLag = "",
                BortaLag = "",
                MålHemmaLag = 0,
                MålBortaLag = 0
            };
            

            e.Row.Tag = nyMatch;
            e.Row.Cells[0].Value = "";
            e.Row.Cells[1].Value = nyMatch.BortaLag;
            e.Row.Cells[2].Value = nyMatch.MålHemmaLag.ToString();
            e.Row.Cells[3].Value = nyMatch.MålBortaLag.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hemmalag = "";
            string bortalag = "";
            int antalmål = 0;
            for (int i =0; i < matcher.Count;i++)
            {
                if(matcher[i].MålBortaLag + matcher[i].MålHemmaLag > antalmål)
                {
                    antalmål = matcher[i].MålBortaLag + matcher[i].MålHemmaLag;
                    hemmalag = matcher[i].HemmaLag.ToString();
                    bortalag = matcher[i].BortaLag.ToString();
                }
            }
            textBox1.Text = hemmalag + " " + bortalag + " " + antalmål.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string hemmalag = "";
            string bortalag = "";
            int skillnad = 0;
            for(int i =0;i<matcher.Count;i++)
            {
                if(matcher[i].MålHemmaLag - matcher[i].MålBortaLag > skillnad)
                {
                    skillnad = matcher[i].MålHemmaLag - matcher[i].MålBortaLag;
                    hemmalag = matcher[i].HemmaLag;
                    bortalag = matcher[i].BortaLag;
                    if( matcher[i].MålBortaLag - matcher[i].MålHemmaLag > skillnad)
                    {
                        skillnad = matcher[i].MålBortaLag - matcher[i].MålHemmaLag;
                        hemmalag = matcher[i].HemmaLag;
                        bortalag = matcher[i].BortaLag;
                    }
                }
            }
            textBox2.Text = hemmalag + " " + bortalag + " " + skillnad.ToString();
        }
    }
    public class Match
    {
        public string HemmaLag { get; set; }
        public string BortaLag { get; set; }
        public int MålHemmaLag { get; set; }
        public int MålBortaLag { get; set; }
    }
}
