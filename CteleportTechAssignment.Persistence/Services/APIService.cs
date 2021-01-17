using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CteleportTechAssignment.Persistence
{
    public class APIService : IAPIService
    {
        private readonly IConfiguration _config;
        private readonly string _APIURL;
        public APIService(IConfiguration config)
        {
            _config = config;
            _APIURL = _config["APIURL"];
        }

        public async Task<HttpResponseMessage> Post(string Code, object param)
        {

            using (var client = new HttpClient())
            {

                var SendData = JsonConvert.SerializeObject(param);
                var stringContent = new StringContent(SendData, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                return await client.PostAsync(_APIURL+ Code, stringContent);

            }
        }
        public async Task<HttpResponseMessage> Get(string Code, object param)
        {

            using (var client = new HttpClient())
            {

                var SendData = JsonConvert.SerializeObject(param);
                var responsesVMList = new StringContent(SendData, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                return await client.GetAsync(_APIURL + Code);

            }
        }
    }
        
}
