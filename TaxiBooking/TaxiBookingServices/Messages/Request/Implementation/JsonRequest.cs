using System;
using System.Collections.Generic;
using System.Text;
using TaxiBookingServices.Messages.Request.Interface;

namespace TaxiBookingServices.Messages.Request
{
    public class JsonRequet:IRequest
    {
        public string JsonString { get; set; }

    }
}
