using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progettoRistorante
{
    public class PiattoMenu : Piatto
    {
        public int quantita { get; set; }
        public float tempo { get; set; }
        public PiattoMenu(string desc, int tipo, int quantita,float tempo) : base(desc, tipo)
        {
            this.quantita = quantita;
            this.tempo = tempo;
        }

    }
}
