using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bankkonto
{
    public partial class Form1 : Form
    {
        Bank bank = new Bank();

        public Form1()
        {
            InitializeComponent();
            fildialog.DefaultExt = ".txt";
        }
        OpenFileDialog fildialog = new OpenFileDialog();
        SaveFileDialog sparadialog = new SaveFileDialog();

        private void btnsätt_Click(object sender, EventArgs e)
        {
            string info = tbxPersonNR.Text;
            for (int i = 0; i < bank.konton.Antal;i++ )
            {
                if (info == bank.konton[i].PersonNr)
                {
                    bank.konton[i].Insättning(double.Parse(tbxBelopp.Text));
                    uppdatera();
                }
                else
                    MessageBox.Show("skrinv in personnumer");
            }
        }
        private void btnUT_Click(object sender, EventArgs e)
        {
            string info = tbxPersonNR.Text;
            bool uttagmöjligt = true;
            for (int i = 0; i < bank.konton.Antal;i++ )
            {
                if (info == bank.konton[i].PersonNr)
                {

                    uttagmöjligt = bank.konton[i].Uttag(double.Parse(tbxBelopp.Text));
                    if (uttagmöjligt == false)
                        MessageBox.Show("Det går inte");
                    else

                        uppdatera();
                }
                else
                    MessageBox.Show("skriv in personnummer");
            }
        }

        private void btnRegis_Click(object sender, EventArgs e)
        {
            if (cmbTyp.Text == "Lånekonto")
            {
                Lånekonto konto = new Lånekonto(tbxPersonNR.Text, double.Parse(tbxStartbelopp.Text), double.Parse(tbxRänta.Text));
                bank.konton.LäggTill(konto);
                uppdatera();
            }
            else if (cmbTyp.Text == "Sparkonto")
            {
                Sparkonto konto = new Sparkonto(tbxPersonNR.Text, double.Parse(tbxStartbelopp.Text), double.Parse(tbxRänta.Text));
                bank.konton.LäggTill(konto);
                
                uppdatera();
            }
            else
                MessageBox.Show("Måste välja typ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double medelRänta = bank.Ränta();
            for (int i = 0; i < bank.konton.Antal;i++ )
            {
                bank.konton[i].Behållning += bank.konton[i].Beräknaränta();
            }
            uppdatera();
            tbxRegister.AppendText("\r\n" + bank.Kapital().ToString() + " totalt kr" + "\r\n" + medelRänta);
        }
        void uppdatera()
        {
            tbxRegister.Clear();
            for(int i =0; i < bank.konton.Antal;i++)
            {
                tbxRegister.AppendText(bank.konton[i].PersonNr + ": " + bank.konton[i].Behållning.ToString() + " kr, ränta: " + bank.konton[i].Räntesats.ToString() + " %." + "\r\n");
            }
        }

        private void btnÖppna_Click(object sender, EventArgs e)
        {
            
             DialogResult resultat = fildialog.ShowDialog();
             if (resultat == DialogResult.OK)
             {
                 FileStream inStröm = new FileStream(fildialog.FileName, FileMode.Open,FileAccess.Read);
                 StreamReader läsare = new StreamReader(inStröm);
                 int nr = int.Parse(läsare.ReadLine());
                 bank.konton.Rensa();
                 for(int i=0;i < nr;i++)
                 {
                    string typ= läsare.ReadLine();
                    if (typ == "Sparkonto")
                    {
                        Sparkonto nykonto = new Sparkonto(läsare.ReadLine(), double.Parse(läsare.ReadLine()), double.Parse(läsare.ReadLine()));
                        bank.konton.LäggTill(nykonto);
                    }
                    else if (typ == "Lånekonto")
                    {
                        Lånekonto nykonto = new Lånekonto(läsare.ReadLine(), double.Parse(läsare.ReadLine()), double.Parse(läsare.ReadLine()));
                        bank.konton.LäggTill(nykonto);
                    }
                    else
                        MessageBox.Show("Filen är korrupt");
                 }
                 läsare.Dispose();
                 uppdatera();
             }
        }

        private void btnSpara_Click(object sender, EventArgs e)
        {
            DialogResult resultat = sparadialog.ShowDialog();
            if(resultat == DialogResult.OK)
            {
                FileStream utström = new FileStream(sparadialog.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter skrivare = new StreamWriter(utström);
                int nr = bank.konton.Antal;
                skrivare.WriteLine(nr.ToString());
                for(int i=0;i<nr;)
                {
                    if (bank.konton[i] is Sparkonto)
                    {
                        skrivare.WriteLine("Sparkonto");
                        skrivare.WriteLine(bank.konton[i].PersonNr);
                        skrivare.WriteLine(bank.konton[i].Behållning);
                        skrivare.WriteLine(bank.konton[i].Räntesats);
                    }
                    else if (bank.konton[i] is Lånekonto)
                    {
                        skrivare.WriteLine("Lånekonto");
                        skrivare.WriteLine(bank.konton[i].PersonNr);
                        skrivare.WriteLine(bank.konton[i].Behållning);
                        skrivare.WriteLine(bank.konton[i].Räntesats);
                    }
                    else
                        MessageBox.Show("Ett fel uppstod");
                    i++;
                }
                skrivare.Dispose();
            }
        }
    }
    public abstract class Bankkonton : IComparable<Bankkonton>
    {
        private string personNr;
        public string PersonNr
        {
            get { return personNr; }
            set { personNr = value; }
        }
        private double behållning;
        public double Behållning
        {   get { return behållning; }
            set { behållning = value; }}

        private double räntesats;
        public double Räntesats
        {   get { return räntesats; }
            set { räntesats = value; }
        }
        public Bankkonton(string nr, double behål, double ränta)
        {
            this.Behållning = behål;
            this.PersonNr = nr;
            this.Räntesats = ränta;
        }
        public abstract bool Uttag(double belopp);
        public abstract double Beräknaränta();
        public int CompareTo(Bankkonton konto)
        {
            return personNr.CompareTo(konto.PersonNr);
        }
        public void Insättning(double belopp)
        {
            Behållning += belopp;
        }
        public override string ToString()
        {
            return PersonNr + " ";
        }
    }
    public class Lånekonto : Bankkonton
    {
        public Lånekonto(string Nr, double Behål, double Ränta) : base  (Nr, Behål, Ränta)
        {

        }

        public override bool Uttag(double belopp)
        {
            double max = -3000;
            if (Behållning + belopp == max)
            {
                Behållning -= belopp;
                return true;
            }
            else
                return false;
        }
        public override double Beräknaränta()
        {
            return -Behållning * ( Räntesats / 100);
        }
    }
    public class Sparkonto : Bankkonton
    {

        public Sparkonto(string nr, double behåll, double Ränta) : base (nr,behåll,Ränta)
        {

        }
        public override bool Uttag(double belopp)
        {
            if (Behållning >= belopp)
            {
                Behållning -= belopp;
                return true;
            }
            else
                return false;
        }
        public override double Beräknaränta()
        {
            return Behållning * (Räntesats / 100);
        }
    }
     class Bank
    {
         public SorteraMängd<Bankkonton> konton = new SorteraMängd<Bankkonton>();
         private double total;
         public double Total
         {
             get { return total; }
             set { total = value; }
         }
          public double Kapital()
         {
             Total = 0;
              for(int i =0;i < konton.Antal; i++)
              {
                  Total += konton[i].Behållning;
              }
              return Total;
         }
          public double Ränta()
          {
              double tal = 0;
              for (int i=0; i < konton.Antal; i++)
              {
                  tal += konton[i].Beräknaränta();
              }
              tal /= konton.Antal;
              return tal;
          }
    }
}
