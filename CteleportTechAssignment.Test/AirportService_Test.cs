using CteleportTechAssignment.Core;
using CteleportTechAssignment.Core.Model;
using System.Linq;
using Microsoft.AspNetCore.TestHost;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Net;
using CteleportTechAssignment.Core.dtos;
using CteleportTechAssignment.Application;

namespace CteleportTechAssignment.Test
{
	[TestFixture]
	public class AirportService_Test
	{
			
		private IAirportService _airportService;		
		[SetUp]
		public void Setup()
		{
			_airportService = (IAirportService)nUnitSetup._server.Services.GetService(typeof(IAirportService));
		}

		[Test]
		public async Task Get0Distance_Test()
		{
			var input = new DistanceInputDto { FromAirportCode = "AMS", ToAirportCode = "AMS" };			
			var distanceOutputDto = await _airportService.CalculateDistance(input);
			Assert.AreEqual(distanceOutputDto.Distance, 0);
		}

		[Test]
		public async Task GetDistance1_Test()
		{
			var input = new DistanceInputDto { FromAirportCode = "ALX", ToAirportCode = "ALE" };			
			var distanceOutputDto = await _airportService.CalculateDistance(input);
			Assert.AreEqual(distanceOutputDto.Distance, 1701214.96);
		}

		#region GetAsync
		[Test]
		public async Task GetAsync_Test()
		{
			var code = "ALX";			
			var airport = await _airportService.GetAsync(code);
			Assert.AreEqual(airport.iata, code);
		}

		[Test]
		public async Task GetAsync_BadPram_Test()
		{
			var code = "AL";
			var airport = await _airportService.GetAsync(code);
			Assert.AreEqual(airport.Errors.FirstOrDefault().StatusCode, HttpStatusCode.NotFound);
		}

		[Test]
		public async Task GetAsync_BadPramType_Test()
		{
			var code = "123";
			var airport = await _airportService.GetAsync(code);
			Assert.AreEqual(airport.Errors.FirstOrDefault().StatusCode, HttpStatusCode.NotFound);
		}
		[Test]
		public async Task GetAsync_NotFound_Test()
		{
			var code = "EEE";
			var airport = await _airportService.GetAsync(code);
			Assert.AreEqual(airport.Errors.FirstOrDefault().StatusCode, HttpStatusCode.NotFound);
		}
		#endregion


	}
}