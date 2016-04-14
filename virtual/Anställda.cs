using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtuel
{
    class Anställda
    {
        private string namn;
        public string Namn 
        {
            get { return namn;}
            set { namn = value;}
        }

         public Anställda(string namn)
         {
             this.Namn = namn;
         }

         public virtual double Beräknalön()
         {
             return 0;
         }

    }
}
