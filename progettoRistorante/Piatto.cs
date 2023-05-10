using CCStatusOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace progettoRistorante
{
    public class Piatto
    {
        public int id { get; set; }
        public string desc { get; set; }
        public int Status{ get; set; }

        public piattoStatus Icona { get; set; }

        public double prezzo { get; set; }
        public int tipo { get; set; }

        public Piatto( string desc,  int tipo) { 
            this.desc = desc;
           this.tipo = tipo;
            Icona = new piattoStatus();
            Disponibile();
        }

        public override string ToString()
        {
            return id+" "+desc+""+tipo+""+Status;
        }

        public void pronto()
        {
            Status = 1;
            Icona.Pronto();
        }

        public void inPreparazione()
        {
            Status = 2;
            Icona.InCorso();
        }
        public void Disponibile()
        {
            Status = 3;
            Icona.Disponibile();
        }

    }
}
