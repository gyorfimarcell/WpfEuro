using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroGUI
{
    class Dal
    {
        int ev, helyezes, pontszam, id;
        string eloado, cim, orszag, datum;

        public Dal(int ev, int helyezes, int pontszam, string eloado, string cim, string orszag, string datum, int id)
        {
            this.ev = ev;
            this.helyezes = helyezes;
            this.pontszam = pontszam;
            this.eloado = eloado;
            this.cim = cim;
            this.orszag = orszag;
            this.datum = datum;
            this.id = id;
        }

        public int Ev => ev;
        public int Helyezes => helyezes;
        public int Pontszam => pontszam;
        public string Eloado => eloado;
        public string Cim => cim;
        public string Orszag => orszag;
        public string Datum => datum;
        public int Id => id;
    }
}
