using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CteleportTechAssignment.Core.Model
{
	

	public class Airport
	{
		
		public string country { get; set; } //{"country":"Seychelles"
		public string city_iata { get; set; } //,"city_iata":"FRK"
		public string iata { get; set; } //,"iata":"FRK"
		public string city { get; set; } //,"city":"Fregate Island"
		public string timezone_region_name { get; set; } //,"timezone_region_name":"Indian/Mahe"
		public string country_iata { get; set; } //,"country_iata":"SC"
		public EAirportTypes type { get; set; } //,"type":"airport"
		public string name { get; set; } //,"name":"Fregate Island"
		public int rating { get; set; } //,"rating":0
		public location location { get; set; } //,"location":{"lon":55.966667,"lat":-4.583333}
		public int hubs { get; set; } //,"hubs":0}

		
		
	}

	
	
}
