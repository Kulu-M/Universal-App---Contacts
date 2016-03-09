using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering
{
    public class Kunde
    {
        public string id { get; set; }
        public Anrede anrede { get; set; }
        public string vorname { get; set; }
        public string nachname { get; set; }
        public DateTime kundeSeit { get; set; }
        public string strasse { get; set; }
        public int plz { get; set; }
        public string ort { get; set; }
        public string telefon { get; set; }
        public string eMail { get; set; }

     

    }

    public enum Anrede
    {
        Frau, Herr
    }
}
