using MDC.SQLite;
using MDC.SQLite.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiBookingServices.Messages.Request.Implementation;
using TaxiBookingServices.Messages.Response.Implementation;
using TaxiBookingServices.Messages.Response.Utility;
using TaxiBookingServices.Models;
using TaxiBookingServices.Services.Interfaces;

namespace TaxiBookingServices.Services.Implementations
{
    public class BookingService: IBookingService
    {
        private static readonly object locker = new object();
        private static BookingService uniqueInstance;

        public static BookingService GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new BookingService();
                    }
                }
            }
            return uniqueInstance;
        }

        public ResponseBase AddBooking(BookingRequest request)
        {
            //current location latitude and longitude information should be called in frontend
            if (request == null) return ResponseUtility.Fail(ResponseUtility.Msg_Bad_Request);

            Guid g = Guid.NewGuid();

            string sql = "insert into Booking values (@Id, @Date, @Time, @PickupPoint, @Destination, @Current_Location_Latitude, @Current_Location_Longitude)";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"Id", g.ToString() },
                {"Date", request.Date },
                {"Time", request.Time },
                {"PickupPoint", request.PickupPoint },
                {"Destination", request.Destination },
                {"Current_Location_Latitude", request.Current_Location_Latitude.Value },
                {"Current_Location_Longitude", request.Current_Location_Longitude.Value }
            };
            int count = SQLiteHelper.ExecuteNonQuery(sql, parameters);

            if (count > 0)
                return ResponseUtility.Success("Added Booking Record Successfully");
            else
                return ResponseUtility.Fail(ResponseUtility.Msg_Date_Insert_Fail);
        }

        public ResponseBase UpdateBooking(BookingRequest request)
        {
            //current location latitude and longitude information should be called in frontend

            if (request == null || request.Id == null) return ResponseUtility.Fail(ResponseUtility.Msg_Bad_Request);           

            string sql = "update Booking set Date=@Date, Time=@Time, PickupPoint=@PickupPoint, Destination=@Destination, Current_Location_Latitude=@Current_Location_Latitude, Current_Location_Longitude=@Current_Location_Longitude where Id=@Id";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"Id", request.Id },
                {"Date", request.Date },
                {"Time", request.Time },
                {"PickupPoint", request.PickupPoint },
                {"Destination", request.Destination },
                {"Current_Location_Latitude", request.Current_Location_Latitude.Value },
                {"Current_Location_Longitude", request.Current_Location_Longitude.Value },
            };
            int count = SQLiteHelper.ExecuteNonQuery(sql, parameters);

            if (count > 0)
                return ResponseUtility.Success("Updated Booking Record Successfully");
            else
                return ResponseUtility.Fail(ResponseUtility.Msg_Date_Update_Fail);
        }

        public ResponseBase DeleteBooking(IDRequest request)
        {
            if (request == null || request.Id == null) return ResponseUtility.Fail(ResponseUtility.Msg_Bad_Request);

            string sql = "delete from Booking where Id=@Id";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"Id", request.Id },
            };
            int count = SQLiteHelper.ExecuteNonQuery(sql, parameters);

            if (count > 0)
                return ResponseUtility.Success("Delete Booking Record Successfully");
            else
                return ResponseUtility.Fail(ResponseUtility.Msg_Date_Update_Fail);
        }

        public ResponseBase GetBooking(IDRequest request)
        {
            if (request == null || request.Id == null) return ResponseUtility.Fail(ResponseUtility.Msg_Bad_Request);
            string sql = "select * from Booking where Id=@Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"Id", request.Id },
            };
            var dt = SQLiteHelper.ExecuteQuery(sql, parameters);

            if(dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                return ResponseUtility.Fail(ResponseUtility.Msg_No_Content);
            Booking booking = new Booking();
            booking.Id = dt.Rows[0].Field<string>("Id");
            booking.Date = dt.Rows[0].Field<DateTime>("Date");
            booking.Time = dt.Rows[0].Field<DateTime>("Time");
            booking.PickupPoint = dt.Rows[0].Field<string>("PickupPoint");
            booking.Destination = dt.Rows[0].Field<string>("Destination");
            booking.Current_Location_Latitude = dt.Rows[0].Field<float>("Current_Location_Latitude");
            booking.Current_Location_Longitude = dt.Rows[0].Field<float>("Current_Location_Longitude");

            return ResponseUtility.Success(booking);
        }

        public ResponseBase GetAllBooking()
        {
            string sql = "select * from Booking";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var dt = SQLiteHelper.ExecuteQuery(sql, parameters);

            if (dt == null || dt.Rows == null || dt.Rows.Count <= 0)
                return ResponseUtility.Fail(ResponseUtility.Msg_No_Content);
            List<Booking> bookingList = new List<Booking>();

            foreach (DataRow dr in dt.Rows)
            {
                Booking booking = new Booking();
                booking.Id = dr.Field<string>("Id");
                booking.Date = dr.Field<DateTime>("Date");
                booking.Time = dr.Field<DateTime>("Time");
                booking.PickupPoint = dr.Field<string>("PickupPoint");
                booking.Destination = dr.Field<string>("Destination");
                booking.Current_Location_Latitude = dr.Field<float>("Current_Location_Latitude");
                booking.Current_Location_Longitude = dr.Field<float>("Current_Location_Longitude");
                bookingList.Add(booking);
            }

            return ResponseUtility.Success(bookingList);
        }
    }
}
