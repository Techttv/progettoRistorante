using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progettoRistorante
{
    public class PiattoMenu : Piatto
    {   
        public int porzioniPerCottura { get; set; }
        public int porzioniPronte { get; set; }
        public int quantita { get; set; }
        public int tempoDiCottura { get; set; }
        public PiattoMenu(string desc, int tipo, int quantita,int tempo) : base(desc, tipo)
        {
            this.quantita = quantita;
            this.tempoDiCottura = tempo;
        }

    }
}
