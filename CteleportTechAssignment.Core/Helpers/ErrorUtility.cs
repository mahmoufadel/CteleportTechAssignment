using Newtonsoft.Json;
using System.Collections.Generic;

namespace CteleportTechAssignment.Core.Models
{


    public static class ErrorUtility
    {
        public static string GetErrorDetails(int StatusCode, string Message)
        {
            List<ErrorDetails> errorDetails = new List<ErrorDetails> { new ErrorDetails { StatusCode = StatusCode, Message = Message } };
              return new Error()
              {
                  StatusCode = StatusCode,
                  ErrorDetails = errorDetails

              }.ToString(); 
        }

        public static string GetErrorDetails(int StatusCode, List<ErrorDetails> errorDetails) => new Error()
        {
            StatusCode = StatusCode,
            ErrorDetails = errorDetails

        }.ToString();

    }

}
