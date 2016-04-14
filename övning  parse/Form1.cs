using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace övning__parse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = tbxInput.Text;
            int tal = 0;

            int posionsvärde = 1;
            bool minus = false;
            char min = '-';
            
            for (int i = text.Length - 1; i >=0; i--)
            {
                if (text[i] == min)
                {
                    minus = true;
                    break;
                }
                int teckenvärde = text[i] - 48;
                tal += teckenvärde * posionsvärde;
                posionsvärde *= 10;

            }

            if (minus)
            {
                tal = tal * (-1);
            }

            tal += 5;
            /*här omvadlar vi int till string
             i loopen ta modulos 10 först, då får vi en talet. spara den och ta bort den från det det stora. 
             multi 10 på modulus. */
            tbxOutput.Text = tillString(tal);
            //tbxOutput.Text = tal.ToString();
        }
        string tillString(int tal)
        {
            string utstring = "";
            int mod = 10;
            int temp;
            for (int i = 0; i < 31;i++)
            {
                                
                temp = tal % mod;
                tal = tal / mod;

                temp += 48;
                char teckenKod = (char) temp;

                utstring += teckenKod;
                
                
                if (tal < 1) break;
            }

            string talSträng = "";
            for (int i = utstring.Length - 1; i >= 0; i--) talSträng += utstring[i];

                return talSträng;
        }
    }
}
