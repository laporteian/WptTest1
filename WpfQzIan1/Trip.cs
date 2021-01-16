using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQzIan1
{
    class Trip
    {
        public string Destination { get; set; }
        public string Name { get; set; }
        public string Passport { get; set; }
        public string Departure { get; set; }
        public string Weturn { get; set; }

        public Trip(string destination, string name, string passport, string departure, string weturn)
        {
            this.Destination = destination;
            this.Name = name;
            this.Passport = passport;
            this.Departure = departure;
            this.Weturn = weturn;
        }
        
        public Trip(string dataLine) // to create a trip (from intake file)
        {
            string[] aTrip = dataLine.Split(';');

            if (aTrip.Length != 5)
            {
                throw new InvalidDataException("Make sure all the fields are filled!  " + dataLine);
            }

            string destination = aTrip[0];
            string name = aTrip[1];
            string passport = aTrip[2];
            string departure = aTrip[3];
            string weturn = aTrip[4];
        }

        public string ToDataString()
        {
            return $"{Destination};{Name};{Passport};{Departure};{Weturn}";
        }

    }
}




