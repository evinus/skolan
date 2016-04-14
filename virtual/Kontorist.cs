using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtuel
{
    class Kontorist : Anställda
    {
        private double månadslön;
        public double Måndadslön
        {
            get { return månadslön; }
            set { månadslön = value; }
        }
        public Kontorist(double lön, string namn): base (namn)
        {
            this.Måndadslön = lön;
        }

        public override double Beräknalön()
        {
            return Måndadslön;
        }
        public override string ToString()
        {
            return Namn + " (Kontorist)" + "\n";
        }
    }
}
