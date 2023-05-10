using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progettoRistorante
{
    public class Tavolo
    {
        public int numeroTavolo { get; set; }
        public int status { get; set; }
        public List<Piatto> ordine = new List<Piatto>();
        public CCTavoli.IconaTavolo icona;
        public bool primo, secondo, dolce;
        private double totale;
        public int id { get; set; }

        public Tavolo() {
            this.status = 0;
            this.icona = new CCTavoli.IconaTavolo();
            icona.cambiaNumero(numeroTavolo);
        }
        public Tavolo(int numeroTavolo)
        {
            this.numeroTavolo = numeroTavolo;
            this.status = 0;
            this.icona = new CCTavoli.IconaTavolo();
            icona.cambiaNumero(numeroTavolo);
        }

        public void aggiungiPiatto(Piatto piatto)
        {
            ordine.Add(piatto);
            this.totale += piatto.prezzo;
            if (piatto.tipo == 1)
            {
                primo = true;
            }
            else if(piatto.tipo==2){
                secondo = true;
            }
            else if (piatto.tipo == 3)
            {
                dolce = true;
            }
        }

        public void rimuoviPiatto(Piatto piatto)
        {
            ordine.Remove(piatto);
            this.totale -= piatto.prezzo;
        }

        public void cambiaStatus(int status)
        {
            this.status = status;
            //0 Libero
            //1 Occupato
            //2 Pagare
            if(status  == 0)
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

            return this.totale;
        }
    }
}
