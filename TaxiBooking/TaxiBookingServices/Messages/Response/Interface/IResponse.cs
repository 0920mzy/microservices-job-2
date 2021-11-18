using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiBookingServices.Messages.Response.Utility;

namespace TaxiBookingServices.Messages.Response.Interface
{
    public interface IResponse
    {
        ResponseStatusCode StatusCode { get; set; }
        string Message { get; set; }
        Object Data { get; set; }
    }
}
