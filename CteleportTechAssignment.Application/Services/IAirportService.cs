using CteleportTechAssignment.Core.dtos;
using CteleportTechAssignment.Core.Model;
using CteleportTechAssignment.Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CteleportTechAssignment.Application
{
	
	public interface IAirportService
    {
		Task<DistanceOutputDto> CalculateDistance(DistanceInputDto inputDto);
		
		Task<(AirportDto FromAirport, AirportDto ToAirport)> GetAirports(string FromAirportCode, string ToAirportCode);
		

		Task<AirportDto> GetAsync(string code);

		DistanceOutputDto GetOutputDto(DistanceInputDto inputDto, List<ErrorDetails> errors);
	}
}
