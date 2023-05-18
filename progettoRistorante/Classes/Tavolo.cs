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
            
            if (m=="m")
            {
                ordine.Remove(piatto);
                totale -= piatto.prezzo;
            }
                
            else
            {
                piatto.Status = 0;
                int prossimo = prossimoTipo();
                Arrivati(prossimo);
            }
        }

        private int prossimoTipo()
        {
            int primiArrivati = 0, secondiArrivati = 0, dolciArrivati = 0;
            foreach (Piatto piatto1 in ordine)
            {
                if (piatto1.tipo == 1 && piatto1.Status != 0)
                {
                    primiArrivati++;
                }
                if (piatto1.tipo == 2 && piatto1.Status != 0)
                {
                    secondiArrivati++;
                }
                if (piatto1.tipo == 3 && piatto1.Status != 0)
                {
                    dolciArrivati++;
                }
            }
            if (primiArrivati == 0&&primo)
            {
                return 1;
            }
            else if (secondiArrivati == 0&&secondo)
            {
                return 2;
            }
            else if (dolciArrivati == 0&&dolce)
            {
                return 3;
            }
            return 0;
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

        public void Arrivati(int tipo)
        {
            if (tipo == 3)
            {
                return;
            }
            if (tipo != 0)
            {
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 15, 0);
                timer.Tick += (s, e) => { GestioneOrdini.aggiungiOrdine(this, tipo++); VistaCucina.preparaPiatto(); };
                timer.Start();
                canSkip = true;
            }



                MainWindow.ricarica();

        }

        public void skip()
        {
            timer.Stop();
            int attuale = prossimoTipo();
            canSkip = false;
            if (attuale == 3)
            {
                return;
            }
            attuale++;
            GestioneOrdini.aggiungiOrdine(this, attuale);
            VistaCucina.preparaPiatto();
            MainWindow.ricarica();
        }

    }
}
