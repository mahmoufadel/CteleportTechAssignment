using CteleportTechAssignment.Core.Model;
using CteleportTechAssignment.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CteleportTechAssignment.Core.dtos
{
	

	public class AirportDto
	{
		public AirportDto()
		{
			Errors = new List<ErrorDetails>();
		}
	
		public string iata { get; set; } //,"iata":"FRK"		
		public location location { get; set; } //,"location":{"lon":55.966667,"lat":-4.583333}		
		public List<ErrorDetails> Errors { get; set; }		
	}
	
}
