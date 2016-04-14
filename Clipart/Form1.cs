using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Clipart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Figur> figur = new List<Figur>();
        int x = 500;
        int y = 100;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
        private void btnSkapaTriangel_Click(object sender, EventArgs e)
        {
            Rektangel rektangel = new Rektangel(double.Parse(tbxBredd.Text), double.Parse(tbxHöjd.Text));
            figur.Add(rektangel);
            tbxRegister.AppendText("Rektangel: " + rektangel.Bredd + " " + rektangel.Höjd + "\r\n");
        }

        private void btnSkapaCirkel_Click(object sender, EventArgs e)
        {
            Cirkel cirkel = new Cirkel(double.Parse(tbxBredd.Text), double.Parse(tbxHöjd.Text));
            figur.Add(cirkel);
            tbxRegister.AppendText("Cirkel: " + cirkel.Bredd + " " + cirkel.Höjd + "\r\n");
        }

        private void btnSkapaLinje_Click(object sender, EventArgs e)
        {
            Linje linje = new Linje(double.Parse(tbxBredd.Text), double.Parse(tbxHöjd.Text));
            figur.Add(linje);
            tbxRegister.AppendText("Linje: " + linje.Bredd + " " + linje.Höjd + "\r\n");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double area = 0;
            for (int i = 0; i < figur.Count;i++ )
            {
                if (figur[i] is IArea)
                    area += (figur[i] as IArea).BeräknaArea();
            }
            tbxSammanlagdArea.Text =("\r\n" + " Area:"+ area.ToString());
        }

    }
   public interface IArea
    {
       double BeräknaArea();
    }
    public abstract class Figur
    {
        private double bredd;
        public double Bredd
        {
            get { return bredd; }
            set { bredd = value; }
        }
        private double höjd;
        public double Höjd
        {
            get { return höjd; }
            set { höjd = value; }
        }

        public Figur(double bredd,double höjd)
        {
            this.Bredd = bredd;
            this.Höjd = höjd;
        }
    }
    class Rektangel : Figur, IArea
    {
        public Rektangel(double bredd,double höjd) : base(bredd,höjd)
        {

        }
        public double BeräknaArea()
        {
            return Bredd * Höjd;
        }
    }
    public class Cirkel : Figur,IArea
    {
        public Cirkel(double bredd,double höjd) : base(bredd,höjd)
        {

        }
        public double BeräknaArea()
        {
            return (Bredd / 2) * (Höjd/2) * Math.PI;
        }
    }
    class Linje:Figur
    {
         public Linje(double bredd,double höjd) : base(bredd,höjd)
        {

        }
    }
}
