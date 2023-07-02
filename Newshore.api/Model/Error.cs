using System.Net;

namespace Newshore.api.Model
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string? StackTrace { get; set; }
        public string TrackingId { get; set; }

        public Error(HttpStatusCode statusCode, string message, Exception exception)
        {
            StatusCode = (int)statusCode;
            Message = message;
            ExceptionMessage = exception.Message;
            StackTrace = exception.StackTrace;
            TrackingId = Guid.NewGuid().ToString();
        }
    }
}
