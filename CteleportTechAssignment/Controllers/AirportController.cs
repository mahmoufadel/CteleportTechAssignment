using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CteleportTechAssignment.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CteleportTechAssignment.Core;

using CteleportTechAssignment.Core.dtos;
using CteleportTechAssignment.Application;

using System.Net;
using CteleportTechAssignment.Core.Resources;
using CteleportTechAssignment.Core.Models;

namespace CteleportTechAssignment.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AirportController : ControllerBase
	{
		private readonly ILogger<AirportController> _logger;		
		private readonly IAirportService _airportService;

		public AirportController(ILogger<AirportController> logger, IAirportService airportService)
		{
			_logger = logger;					
			_airportService = airportService;
		}

		[HttpPost]
		public async Task<IActionResult> GetDistance(DistanceInputDto inputDto)
		{
			if (!inputDto.IsValid()) return BadRequest(ErrorUtility.GetErrorDetails((int)HttpStatusCode.BadRequest, Resources.DuplicateAirportCode));

			_logger.LogInformation($"Get Distance from Airport {inputDto.FromAirportCode} and {inputDto.ToAirportCode}");
			var DistanceDto = await _airportService.CalculateDistance(inputDto);
			_logger.LogInformation($"Returning {DistanceDto.Distance} Miles.");
			return Ok(DistanceDto);
		}
	}
}
