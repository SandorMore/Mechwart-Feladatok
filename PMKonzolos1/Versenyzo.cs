using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMKonzolos1
{
    internal class Versenyzo
    {
        public Versenyzo(int szakasz_, TimeOnly ido_, string nev_, string nemzetiseg_, string csapat_)
        {
            szakasz = szakasz_;
            ido = ido_; 
            nev = nev_;
            nemzetiseg = nemzetiseg_;
            csapat = csapat_;
        }
        public Versenyzo(string line)
        {
            string[] data = line.Trim().Split(";");
            szakasz = int.Parse(data[0]);
            ido = TimeOnly.Parse(data[1]);
            nev = data[2];
            nemzetiseg = data[3];
            csapat= data[4];
        }
        public int szakasz { get; set; }
        public TimeOnly ido { get; set; }
        public string nev { get; set; }
        public string nemzetiseg { get; set; }
        public string csapat { get; set; }

        public int get_seconds()
        {
            return ido.Hour * 3600 + ido.Minute * 60 + ido.Second;
        }
    }
}
