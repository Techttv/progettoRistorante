﻿using CCStatusOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Timer = System.Timers.Timer;

namespace progettoRistorante
{
    public class Piatto
    {
        public string desc { get; set; }
        public int Status{ get; set; }

        public piattoStatus Icona { get; set; }

        public double prezzo { get; set; }
        public int tipo { get; set; }

        public Piatto( string desc,  int tipo) { 
            this.desc = desc;
           this.tipo = tipo;
            Icona = new piattoStatus();
        }

        public override string ToString()
        {
            return desc+""+tipo+""+Status;
        }

    }
}
