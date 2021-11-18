using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiBookingServices.Messages.Request.Implementation;
using TaxiBookingServices.Messages.Request.Interface;
using TaxiBookingServices.Messages.Response.Implementation;
using TaxiBookingServices.Services.Implementations;

namespace TaxiBooking.Controllers.Booking
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookingController : Controller
    {
        private BookingService bookingService = BookingService.GetInstance();

        [HttpGet]
        [Route("get")]
        public ResponseBase Get(IDRequest request)
        {
            return bookingService.GetBooking(request);
        }


        [HttpGet]
        [Route("get/all")]
        public ResponseBase GetAll()
        {
            return bookingService.GetAllBooking();
        }

        [HttpPost]
        [Route("add")]
        public ResponseBase Add(BookingRequest request)
        {
            return bookingService.AddBooking(request);
        }

        [HttpPost]
        [Route("delete")]
        public ResponseBase Delete(IDRequest request)
        {
            return bookingService.DeleteBooking(request);
        }

        [HttpPost]
        [Route("update")]
        public ResponseBase Update(BookingRequest request)
        {
            return bookingService.UpdateBooking(request);
        }
    }
}
