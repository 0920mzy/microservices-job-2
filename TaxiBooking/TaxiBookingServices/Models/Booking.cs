using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiBookingServices.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string PickupPoint { get; set; }
        public string Destination { get; set; }
        public float Current_Location_Latitude { get; set; }
        public float Current_Location_Longitude { get; set; }
    }
}
