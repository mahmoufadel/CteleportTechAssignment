using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CteleportTechAssignment.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CteleportTechAssignment.Core;
using CteleportTechAssignment.Core.Model;
using CteleportTechAssignment.Core.dtos;

namespace CteleportTechAssignment.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CacheController : ControllerBase
	{
		
		private readonly ILogger<AirportController> _logger;
		private readonly ICacheService _cacheService;
		

		public CacheController(
			ILogger<AirportController> logger, 			
			ICacheService cacheService
			)
		{
			_logger = logger;			
			_cacheService = cacheService;
			
		}

		[HttpPost]
		public void Clear()
		{
			_cacheService.Current().Clear();			
		}

		[HttpGet]
		public void ClearKey(string cacheKey)
		{

			_cacheService.Current().Remove(cacheKey.ToUpper());
		}
		
	}
}
