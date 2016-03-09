using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering
{
    public class Bestellung
    {
        public string id { get; set; }
        public string kd_id { get; set; }
        public string bezeichnung { get; set; }
        public string hinweise { get; set; }
        public DateTime bestellDatum { get; set; }
        public DateTime lieferDatum { get; set; }
        public int anzPersonen { get; set; }
        public List<Position> positionen { get; set; }

        
    }

    public class Position
    {
        public string es_id { get; set; }
        public int menge { get; set; }
        public double preis { get; set; }
    }
}
