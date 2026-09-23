using System;
using System.Collections.Generic;
using System.Text;

namespace PMFeladat2
{
    internal class VedettAllat
    {
        public VedettAllat(string sor)
        {
            string[] data = sor.Split(";");
            this.nev = data[0];
            this.ertek = int.Parse(data[1]);
            this.since = int.Parse(data[2]);
            this.fajta = data[3];
        }

        public int id { get; set; }
        public string nev { get; set; }
        public int ertek { get; set; }
        public int since { get; set; }
        public string fajta { get; set; }
    }
}
