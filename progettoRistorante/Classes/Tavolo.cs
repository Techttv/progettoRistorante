using progettoRistorante.Finestre;
using progettoRistorante.UserControllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace progettoRistorante.Classes
{
    public class Tavolo
    {
        public DispatcherTimer timer;
        public int numeroTavolo { get; set; }
        public int status { get; set; }
        public List<Piatto> ordine = new List<Piatto>();
        public CCTavoli.IconaTavolo icona;
        public bool primo    , secondo = false, dolce = false;
        private double totale;
        public bool canSkip=false;
        public int id { get; set; }
        int sel = 0;
        public Tavolo()
        {
            status = 0;
            icona = new CCTavoli.IconaTavolo();
            icona.cambiaNumero(numeroTavolo);
        }
        public Tavolo(int numeroTavolo)
        {
            this.numeroTavolo = numeroTavolo;
            status = 0;
            icona = new CCTavoli.IconaTavolo();
            icona.cambiaNumero(numeroTavolo);
        }

        public void aggiungiPiatto(Piatto piatto)
        {
            ordine.Add(piatto);
            totale += piatto.prezzo;
            if (piatto.tipo == 1)
            {
                primo = true;
            }
            else if (piatto.tipo == 2)
            {
                secondo = true;
            }
            else if (piatto.tipo == 3)
            {
                dolce = true;
            }
        }

        public void rimuoviPiatto(Piatto piatto,string m)
        {
            var piattiArrivati = 0; 
            var tuttiArrivati=0;
            
            if (m.Length > 0)
            {
                ordine.Remove(piatto);
                totale -= piatto.prezzo;
            }
                
            else
            {
                piatto.Status = 0;
                foreach (Piatto piatto1 in ordine)
                {
                    if (piatto.tipo == piatto1.tipo && piatto1.Status != 0)
                    {
                        piattiArrivati++;
                    }
                    if (piatto1.Status == 0 || piatto1.tipo == 4)
                    {
                        tuttiArrivati++;
                    }
                }
                int numeroPiatti = MainWindow.tavoli.ElementAt(piatto.tavolo-1).ordine.Count;
                if (piattiArrivati == 0&&numeroPiatti!=tuttiArrivati)
                {
                    Arrivati();
                }
            }
        }

        public void cambiaStatus(int status)
        {
            this.status = status;
            //0 Libero
            //1 Occupato
            //2 Pagare
            if (status == 0)
            {
                icona.Disponibile();
            }
            if (status == 1)
            {
                icona.inPreparazione();
            }
            if (status == 2)
            {
                icona.Finito();
            }
        }
        public double getTotale()
        {

            return totale;
        }

        public void Arrivati()
        {
            if (secondo == true)
            {
                sel = 2;
                secondo = false;
            }
            else if (dolce==true) 
            {
                sel = 3; dolce = false; 
            }
            int tuttoFinito=0;
            foreach(Piatto piatto in ordine)
            {
                if (piatto.Status == 0&&piatto.tipo!=4)
                {
                    tuttoFinito++;
                }
            }

            if (sel>1&&tuttoFinito!=0)
            {
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 15, 0);
                timer.Tick += (s, e) => { GestioneOrdini.aggiungiOrdine(this, sel); VistaCucina.preparaPiatto(); Arrivati(); };
                timer.Start();
                canSkip = true;

                MainWindow.ricarica();
                sel = 0;
            }
            else
            {
                canSkip = false;
            }

        }

        public void skip()
        {
            timer.Stop();
            if (secondo == false)
            {
                sel = 2;
                secondo = true;
            }
            else { sel = 3; dolce = true; }
            canSkip = false;
            GestioneOrdini.aggiungiOrdine(this, sel);
            VistaCucina.preparaPiatto();
            MainWindow.ricarica();
        }

    }
}
