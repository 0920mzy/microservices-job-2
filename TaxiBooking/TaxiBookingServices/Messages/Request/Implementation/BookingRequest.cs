using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiBookingServices.Messages.Request.Implementation
{
    public class BookingRequest
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string PickupPoint { get; set; }
        public string Destination { get; set; }
        public double? Current_Location_Latitude { get; set; }
        public double? Current_Location_Longitude { get; set; }
    }
}
