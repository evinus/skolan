using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Samling<T> :  IEnumerable<T>
  {
        protected int buffert;  // Storlek på buffert.
        protected T[] lista;    // Samling av element av vilken typ som helst.
        protected int längd;    // Antal tillgängliga platser.
        protected int antal;    // Antal använda platser.

        public Samling()
        {
            buffert = 30;
            antal = 0;
            längd = 30;
            lista = new T[längd];
        }
    
        protected void Expandera(int storlek)
        {
            if (storlek < 1) return;

            // Gör ett nytt större fält.
            T[] temp = new T[längd + storlek];

            //Kopiera över värden från det gamla fältet.
            for (int i = 0; i < antal; i++) temp[i] = lista[i];

            lista = temp;
            längd += storlek;
        }

        protected void Reducera()
        {
            // Gör en ny mindre lista.
            T[] temp = new T[antal];

            //Kopiera över värden från det gamla fältet.
            for (int i = 0; i < antal; i++) temp[i] = lista[i];

            lista = temp;
            längd = antal;
        }

        public virtual void LäggTill(T e)
        {
            // Skaffa fler platser om det behövs.
            if (antal + 1 > längd) Expandera(1 + buffert);

            lista[antal++] = e;
        }

        public T TaBort(int index)
        {
            T temp = lista[index];

            // Flytta alla element efter index ett steg åt vänster.
            for (int i = index; i < antal - 1; i++)
            {
                lista[i] = lista[i + 1];
            }

            antal--;

            // Krymp fältet om det finns för många tomma platser.
            if (längd - antal > buffert) Reducera();

            return temp;
        }
        public int Antal
        {
            get { return antal; }
        }

  

    public T ElementFrån(int index)
        {
            return lista[index];
        }
        public bool Har(T värde)
        {
            for(int i= 0; i < antal; i++)
            {
                if (lista[i].Equals(värde))
                    return true;
            }
            return false;
        }
        public int Sök(T värde)
        {
            for(int i=0;i<antal;i++)
            {
                if(lista[i].Equals(värde))
                {
                    return i;
                }
            }
            return -1;
        }

        public void LäggTill(Samling<T> samnling)
        {
            for(int i=0;i <samnling.antal;i++)
            {
                LäggTill(samnling.ElementFrån(i));
            }
        }
        public void Rensa()
        {
            buffert = 30;
            antal = 0;
            längd = 30;
            lista = new T[längd];
        }
    
    /// <summary>
    /// Returnerar fösta halvan av samlingen.
    /// </summary>
    /// <param name="samling"></param>
    public virtual void Förstahalvan(Samling<T> samling)
    {
        for(int i =0;i< Antal/2;i++)
        {
            LäggTill(samling.ElementFrån(i));
        }
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
    public void KopieraTill(T[] uppställning)
    {
        for(int i =0;i<Antal;i++)
        {
            uppställning[i] = lista[i];
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="uppställning"></param>
    /// <param name="startIndex">Skriv nummret som den ska starta på, kom ihåg index startar på talet 0</param>
    public void KopieraTill(T[] uppställning, int startIndex)
    {
        for (int i = startIndex; i < Antal; i++)
        {
            uppställning[i] = lista[i];
        }
    }

    public T this[int index]
        {
            get { return lista[index]; }
        }
    }
    public class Mängd <T>:Samling<T>
    {
        /// <summary>
        /// Lägg till ett obejct till din mängd
        /// </summary>
        /// <param name="e"></param>
        public override void LäggTill(T e)
        {
            
            if(Har(e)){}
            else
            {
                if (antal + 1 > längd) Expandera(1 + buffert);
                lista[antal++] = e;
            }
        }
        public T this[ int index ]
        {
            get { return lista[index];}
        }

    }
    public class SorteraMängd<T>:Mängd<T>  where T :  IComparable<T>
    {
        public override void LäggTill(T e)
        {
            
            if(!Har(e))
            {
                if (antal + 1 > längd) Expandera(1 + buffert);
                lista[antal++] = e;
            }
            Sortera();
        }

        public void Sortera ( )
        {
            Array.Sort(lista,0, antal );

            /*int i = 0, j;
            int n = antal;
            T a = lista[1];

            if (n < 2) return;
            for (j = 1; j < n; j++)
            {
                a = lista[j];
                i = j - 1;
                while (i >= 0 && lista[i].CompareTo(a) > 0)
                    lista[i + 1] = lista[i];
                i--;
            }
            lista[i + 1] = a;*/
        }
    }

