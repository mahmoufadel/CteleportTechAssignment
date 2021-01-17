using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CteleportTechAssignment.Core.Resources;

namespace CteleportTechAssignment.Core.dtos
{
	
	public sealed class DistanceInputDto
	{
		[Required, RegularExpression("^[a-zA-Z]{3}$",ErrorMessage = Resources.Resources.InvalidFromAirportCode)]
		public string FromAirportCode { get; set; }
		[Required, RegularExpression("^[a-zA-Z]{3}$", ErrorMessage = Resources.Resources.InvalidToAirportCode)]

		public string ToAirportCode { get; set; }

		public bool IsValid() => string.Compare(FromAirportCode,ToAirportCode,true)!=0;
		
	}
}
