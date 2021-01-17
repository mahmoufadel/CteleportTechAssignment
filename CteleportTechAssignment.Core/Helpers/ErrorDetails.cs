using Newtonsoft.Json;
using System.Collections.Generic;

namespace CteleportTechAssignment.Core.Models
{

    public class Error
    {
        public int StatusCode { get; set; }
        public List<ErrorDetails> ErrorDetails { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
