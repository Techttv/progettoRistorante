using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progettoRistorante
{
    public class GestioneOrdini
    {
        private static Queue<Piatto> primi = new Queue<Piatto>();
        private static Queue<Piatto> secondi = new Queue<Piatto>();
        private static Queue<Piatto> dolci = new Queue<Piatto>();

        public static void aggiungiOrdine(Tavolo tavolo,int tipo)
        {
            foreach(Piatto piatto in tavolo.ordine)
            {
                switch (piatto.tipo)
                {
                    case 1:
                        if(tipo==1)
                        primi.Enqueue(piatto);
                        break;
                    case 2:
                        if (tipo == 2)
                            secondi.Enqueue(piatto);
                        break;
                    case 3:
                        if (tipo == 3)
                            dolci.Enqueue(piatto);
                        break;
                }
            }
        }

        public static Piatto prossimoPiatto()
        {
            if (primi.Count > 0)
            {
                return primi.Dequeue();
            }
            if (secondi.Count > 0)
            {
                return secondi.Dequeue();
            }
            if (dolci.Count > 0)
            {
                return dolci.Dequeue();
            }
            return null;
        }
    }
}
