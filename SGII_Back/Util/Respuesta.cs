using System.Net;

namespace SGII_Back.Util
{
    public class Respuesta<T>
    {
        public Respuesta(HttpStatusCode statusCode, T? result, bool success, string message, string? error)
        {
            StatusCode = statusCode;
            Result = result;
            Success = success;
            Message = message;
            Error = error;
        }

       HttpStatusCode StatusCode { get; set; }
        T? Result { get; }
        bool Success { get; }
        string Message { get; set; }
        string? Error { get; set; }
    }


}
