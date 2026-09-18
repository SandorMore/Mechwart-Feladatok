using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace EuroGUI.Models
{
    internal class Verseny
    {
        public int ev {  get; set; }
        public System.DateTime datum { get; set; }
        public string varos { get; set; }
        public string orszag { get; set; }
        public int induloszam {  get; set; }
    }
}
