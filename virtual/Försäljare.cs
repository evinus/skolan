using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtuel
{
    class Försäljare : Anställda
    {
        private double provision;
        private double försäljning;
        public double Provision
        {
            get { return provision; }
            set { provision = value; }
        }
        public double Försäljning
        {
            get { return försäljning; }
            set { försäljning = value; }
        }
        public Försäljare(string namn, double prov, double sälj) : base (namn)
        {
            this.Provision = prov;
            this.Försäljning = sälj;
        }
        public override double Beräknalön()
        {
            return Försäljning * (Provision/100);
        }

        public override string ToString()
        {
            return Namn + " (Säljare)" + "\n";
        }
    }
}
