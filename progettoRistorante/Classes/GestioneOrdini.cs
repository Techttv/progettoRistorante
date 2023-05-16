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
                if (!temp.Contains(piatto))
                {
                    temp.Add(piatto);
                }
            }
            foreach (Piatto piatto in temp)
                foreach (PiattoMenu piattoMenu in MainWindow.menu)
                {
                    if (piatto.desc.Equals(piattoMenu.desc)&&!piatto.inQueue)
                    {
                        piatto.tavolo = tavolo.numeroTavolo;
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
            }
            else if (secondi.Count > 0)
            {
                temp = secondi.First();

                if(!MainWindow.tavoli.ElementAt(temp.tavolo-1).timer.IsEnabled)
                {
                    temp =secondi.Dequeue();
                }
            }
            else if (dolci.Count > 0)
            {
                temp = dolci.First();
                if (!MainWindow.tavoli.ElementAt(temp.tavolo-1).timer.IsEnabled)
                {
                    temp = dolci.Dequeue();
                }
            }
            return temp;
        }
    }
}
