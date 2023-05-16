using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progettoRistorante.Classes
{
    public class PiattoMenu : Piatto
    {

        public int quantita { get; set; }
        public int tempoDiCottura { get; set; }
        public PiattoMenu(string desc, int tipo, int quantita, int tempo) : base(desc, tipo)
        {
            this.quantita = quantita;
            tempoDiCottura = tempo;
        }

    }
}
