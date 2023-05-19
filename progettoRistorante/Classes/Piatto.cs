using CCStatusOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Timer = System.Timers.Timer;

namespace progettoRistorante.Classes
{
    public class Piatto
    {
        public int id { get; set; }
        public string desc { get; set; }
        public int Status { get; set; }

        public int tavolo { get; set; } // si lo so è ricursivo ma serve per poter passare gli ordini più facilmente

        public double prezzo { get; set; }
        public int tipo { get; set; }

        public bool inQueue { get; set; }

        public Piatto(string desc, int tipo)
        {
            this.desc = desc;
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return desc + "" + tipo + "" + Status;
        }

        public void pronto()
        {
            Status = 1;

        }

        public void inPreparazione()
        {
            inQueue = true;
            Status = 2;

        }
        public void Disponibile()
        {
            inQueue = false;
            Status = 3;
        }

        public void Finito()
        {
            Status = 0;
        }

    }
}
