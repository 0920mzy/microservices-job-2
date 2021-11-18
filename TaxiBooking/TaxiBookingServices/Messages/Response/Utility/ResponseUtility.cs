using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiBookingServices.Messages.Response.Implementation;

namespace TaxiBookingServices.Messages.Response.Utility
{
    public enum ResponseStatusCode
    {
        OK = 200,
        BAD_REQUEST = 400,
        UNAUTHORIZED = 401,
        FORBIDDEN = 403,
        NOT_FOUND = 404,
        INTERNAL_SERVER_ERROR = 500,

    }

    public static class ResponseUtility
    {
        public const string Msg_Bad_Request = "Bad Request";
        public const string Msg_No_Content = "No Content";
        public const string Msg_Unique_Constrain_Violated = "Unique Constrain Violated";
        public const string Msg_Internal_Error_Happened = "Internal Error Happened";
        public const string Msg_Data_Updated = " Rows of data inserted/updated.";
        public const string Msg_Date_Insert_Fail = "Data Insert Fail.";
        public const string Msg_Date_Update_Fail = "Data Update Fail.";
        public const string Msg_Date_Delete_Fail = "Data Delete Fail.";

        public const string Msg_Date_Relationship_Fail = "Cannot Find Relational Object.";

        public static ResponseBase Success(Object data)
        {
            return Success(ResponseStatusCode.OK, data);
        }

        public static ResponseBase Success(ResponseStatusCode statusCode, Object data)
        {
            ResponseBase responseBase = new ResponseBase();
            responseBase.StatusCode = statusCode;
            responseBase.Message = "Success";
            responseBase.Data = data;
            return responseBase;

        }

        public static ResponseBase Fail(string message)
        {
            ResponseBase responseBase = new ResponseBase();
            responseBase.StatusCode = ResponseStatusCode.BAD_REQUEST;
            responseBase.Message = message;
            responseBase.Data = null;
            return responseBase;
        }

        public static ResponseBase Fail(ResponseStatusCode statusCode, string message)
        {
            ResponseBase responseBase = new ResponseBase();
            responseBase.StatusCode = statusCode;
            responseBase.Message = message;
            responseBase.Data = null;
            return responseBase;
        }

        public static ResponseBase Error(Exception ex)
        {
            ResponseBase responseBase = new ResponseBase();
            responseBase.StatusCode = ResponseStatusCode.INTERNAL_SERVER_ERROR;
            responseBase.Message = string.Format("{0} {1}", "INTERNAL SERVER ERROR:", ex.ToString());
            responseBase.Data = null;
            return responseBase;
        }
    }
}
