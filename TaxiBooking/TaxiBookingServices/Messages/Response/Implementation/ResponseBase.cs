

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiBookingServices.Messages.Response.Interface;
using TaxiBookingServices.Messages.Response.Utility;

namespace TaxiBookingServices.Messages.Response.Implementation
{
    public class ResponseBase : IResponse
    {
        public string Message { get; set; }
        public ResponseStatusCode StatusCode { get; set; }
        public object Data { get; set; }
    }
}
