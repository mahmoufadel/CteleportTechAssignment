using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CteleportTechAssignment.Persistence
{
    public class APIMockService : IAPIService
    {
        Dictionary<string, string> _Airports = new Dictionary<string, string>();       
        public APIMockService()
        {
            _Airports.Add("ALX", "{\"country\":\"United States\",\"city_iata\":\"ALX\",\"iata\":\"ALX\",\"city\":\"Alexander City\",\"timezone_region_name\":\"America / Chicago\",\"country_iata\":\"US\",\"rating\":0,\"name\":\"Alexander City\",\"location\":{\"lon\":-85.95,\"lat\":32.933333},\"type\":\"airport\",\"hubs\":0}");
           _Airports.Add("ALE", "{\"country\":\"United States\",\"city_iata\":\"ALE\",\"iata\":\"ALE\",\"city\":\"Alpine\",\"timezone_region_name\":\"America/Chicago\",\"country_iata\":\"US\",\"rating\":0,\"name\":\"Alpine\",\"location\":{\"lon\":-103.680778,\"lat\":30.3871},\"type\":\"airport\",\"hubs\":0}");
           _Airports.Add("AMS", "{\"country\":\"Netherlands\",\"city_iata\":\"AMS\",\"iata\":\"AMS\",\"city\":\"Amsterdam\",\"timezone_region_name\":\"Europe/Amsterdam\",\"country_iata\":\"NL\",\"rating\":3,\"name\":\"Amsterdam\",\"location\":{\"lon\":4.763385,\"lat\":52.309069},\"type\":\"airport\",\"hubs\":7}");
           _Airports.Add("FRK", "{\"country\":\"Seychelles\",\"city_iata\":\"FRK\",\"iata\":\"FRK\",\"city\":\"Fregate Island\",\"timezone_region_name\":\"Indian/Mahe\",\"country_iata\":\"SC\",\"rating\":0,\"name\":\"Fregate Island\",\"location\":{\"lon\":55.966667,\"lat\":-4.583333},\"type\":\"airport\",\"hubs\":0}");
           _Airports.Add("CAI", "{\"country\":\"Egypt\",\"city_iata\":\"CAI\",\"iata\":\"CAI\",\"city\":\"Cairo\",\"timezone_region_name\":\"Africa/Cairo\",\"country_iata\":\"EG\",\"rating\":3,\"name\":\"Cairo\",\"location\":{\"lon\":31.406469,\"lat\":30.120106},\"type\":\"airport\",\"hubs\":12}");
           _Airports.Add("ROM", "{\"country\":\"Italy\",\"iata\":\"ROM\",\"timezone_region_name\":\"Europe/Rome\",\"country_iata\":\"IT\",\"rating\":3,\"name\":\"Rome\",\"location\":{\"lon\":12.250346,\"lat\":41.794594},\"type\":\"city\"}");


        }

        public async Task<HttpResponseMessage> Post(string Code, object param)
        {
            var result = _Airports[Code];
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;//Setting statuscode    
            response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(result)); // configure your response here    
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); //Setting media type for the response    
            return response;
        }
        public async Task<HttpResponseMessage> Get(string Code, object param)
        {
            var result = _Airports.ContainsKey(Code) ? _Airports[Code] : string.Empty;
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = _Airports.ContainsKey(Code) ? HttpStatusCode.OK : HttpStatusCode.NotFound;  
            response.Content = new StringContent(result); 
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");    
            return response;
        }
    }
        
}
