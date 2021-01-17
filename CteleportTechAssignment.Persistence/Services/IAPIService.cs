using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CteleportTechAssignment.Persistence
{
	
	public interface IAPIService
	{
		Task<HttpResponseMessage> Get(string uri, object BodyData);
		Task<HttpResponseMessage> Post(string uri, object BodyData);

	}
}
