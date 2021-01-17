using CteleportTechAssignment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CteleportTechAssignment.Core.Model
{


	public class DistanceOutputDto
	{
		public double Distance { get; set; }
		public string FromAirportCode { get; set; }
		public string ToAirportCode { get; set; }
		public List<ErrorDetails> Errors { get; set; }

		
	}


}
