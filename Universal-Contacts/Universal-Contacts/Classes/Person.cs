using System;

namespace Universal_Contacts
{
    public class Person
    {
        public int id { get; set; } 
        public string vorname { get; set; }
        public string nachname { get; set; }
        public DateTime dateofbirth { get; set; }
        public int zipcode { get; set; }
        public string gender { get; set; }
        public enum sex
        {
            Male,
            Female,
            Other
        }

        public string familyStatz { get; set; }
        public enum familyStatus
        {
            Unmarried,
            Married,
            Widowed,
            Divorced
        }

        //Vom Server: name, plz, alter
        public string name { get; set; }
        public string plz { get; set; }
        public string alter { get; set; }
        
    }
}