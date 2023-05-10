using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progettoRistorante
{
    public class PiattoMenu : Piatto
    {   
        int porzioniPronte { get; set; }
        public int quantita { get; set; }
        public float tempoDiCottura { get; set; }
        public PiattoMenu(string desc, int tipo, int quantita,float tempo) : base(desc, tipo)
        {
            this.quantita = quantita;
            this.tempoDiCottura = tempo;
        }

    }
}
