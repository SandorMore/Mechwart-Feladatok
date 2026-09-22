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
            this.nev = data[1];
            this.ertek = int.Parse(data[2]);
            this.since = DateTime.Parse(data[3]);
            this.fajta = data[3];
        }

        public int id { get; set; }
        public string nev { get; set; }
        public int ertek { get; set; }
        public DateTime since { get; set; }
        public string fajta { get; set; }
    }
}
