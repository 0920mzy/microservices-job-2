using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiBookingServices.Messages.Request.Interface;

namespace TaxiBookingServices.Messages.Request.Implementation
{
    public class IDRequest:IRequest
    {
        public string Id { get; set; }
    }
}
