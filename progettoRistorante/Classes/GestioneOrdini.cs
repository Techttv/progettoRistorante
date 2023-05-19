using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progettoRistorante.Classes
{
    public class GestioneOrdini
    {
        private static Queue<Piatto> primi = new Queue<Piatto>();
        private static Queue<Piatto> secondi = new Queue<Piatto>();
        private static Queue<Piatto> dolci = new Queue<Piatto>();

        public static void aggiungiOrdine(Tavolo tavolo, int tipo)
        {
            List<Piatto> temp = new List<Piatto>();
            foreach (Piatto piatto in tavolo.ordine)
            {
                doesNotContain(piatto, temp, tipo);
            }
            foreach (Piatto piatto in temp)
            {
                PiattoMenu piattoNuovo = new PiattoMenu(MainWindow.menu.ElementAt(piatto.id).desc, MainWindow.menu.ElementAt(piatto.id).tipo, MainWindow.menu.ElementAt(piatto.id).quantita, MainWindow.menu.ElementAt(piatto.id).tempoDiCottura);
                piattoNuovo.Disponibile();
                piattoNuovo.prezzo = MainWindow.menu.ElementAt(piatto.id).prezzo;
                piattoNuovo.id = piatto.id;

                piatto.Disponibile();
                if (piatto.desc.Equals(MainWindow.menu.ElementAt(piatto.id).desc) && !piatto.inQueue)
                {
                    piatto.tavolo = tavolo.numeroTavolo;
                    piatto.inQueue=true;
                    switch (piatto.tipo)
                    {
                        case 1:
                            primi.Enqueue(piatto);
                            break;
                        case 2:
                            secondi.Enqueue(piatto);
                            break;
                        case 3:

                            dolci.Enqueue(piatto);
                            break;
                    }
                }
            }
        }

        public static void aggiungiOrdine(Piatto piatto, int tipo)
        {
            if (!piatto.inQueue)
            {
                switch (piatto.tipo)
                {
                    case 1:
                        if (tipo == 1)
                        {
                            piatto.inQueue = true;
                            primi.Enqueue(piatto);
                        }
                        break;
                    case 2:
                        if (tipo == 2)
                        {
                            piatto.inQueue = true;
                            secondi.Enqueue(piatto);
                        }
                        break;
                    case 3:
                        if (tipo == 3)
                        {
                            piatto.inQueue = true;
                            dolci.Enqueue(piatto);
                        }
                        break;
                }
            }
        }


        public static Piatto prossimoPiatto()
        {
            Piatto temp= null;
            if (primi.Count > 0)
            {
                temp = primi.Dequeue();
                temp.inPreparazione();
            }
            else if (secondi.Count > 0)
            {
                temp = secondi.First();

                if(!MainWindow.tavoli.ElementAt(temp.tavolo-1).timer.IsEnabled)
                {
                    temp =secondi.Dequeue();
                    temp.inPreparazione();
                }
            }
            else if (dolci.Count > 0)
            {
                temp = dolci.First();
                if (!MainWindow.tavoli.ElementAt(temp.tavolo-1).timer.IsEnabled&&!temp.inQueue)
                {
                    temp = dolci.Dequeue();
                    temp.inPreparazione();
                }
            }
            return temp;
        }

        static void doesNotContain(Piatto piatto, List<Piatto> temp,int tipo)
        {
            int trovati = 0;
            foreach(Piatto piatto1 in temp)
            {
                if (piatto1.desc == piatto.desc)
                {
                    trovati++;
                }
            }
            if (trovati == 0&& piatto.tipo == tipo && piatto.Status == 3 && !piatto.inQueue)
            {
                temp.Add(piatto);
            }
        }
    }
}
