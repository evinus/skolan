using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtuel
{
    class Konsult : Anställda
    {
        private double timön;
        public double Timlön
        {
            get { return timön; }
            set { timön = value; }
        }
        private double arbetadtid;

        public double Arbetadtid
        {
            get { return arbetadtid; }
            set { arbetadtid = value; }
        }

        public Konsult( string namn,double lön, double tid) : base (namn)
        {
            this.Arbetadtid = tid;
            this.timön = lön;
        }
        public override double Beräknalön()
        {
            return Timlön * Arbetadtid;
        }
        public override string ToString()
        {
            return Namn + " (Konsult)" + "\n";
        }
    }
}
