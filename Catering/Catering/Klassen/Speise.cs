using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering
{
    public class Speise
    {
        public string id { get; set; }
        public string bezeichnung { get; set; }
        public string beschreibung { get; set; }
        public string preis { get; set; }
        public string kat { get; set; } //kategorie
    }
}
