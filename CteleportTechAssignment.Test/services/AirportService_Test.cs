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
		#region Get Distance
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

		#endregion

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
			Assert.AreEqual(airport.Errors.FirstOrDefault().StatusCode, (int)HttpStatusCode.NotFound);
		}

		[Test]
		public async Task GetAsync_BadPramType_Test()
		{
			var code = "123";
			var airport = await _airportService.GetAsync(code);
			Assert.AreEqual(airport.Errors.FirstOrDefault().StatusCode, (int)HttpStatusCode.NotFound);
		}
		[Test]
		public async Task GetAsync_NotFound_Test()
		{
			var code = "EEE";
			var airport = await _airportService.GetAsync(code);
			Assert.AreEqual(airport.Errors.FirstOrDefault().StatusCode, (int)HttpStatusCode.NotFound);
		}
		#endregion

		#region Get Airports

		[Test]
		public async Task GetAirports_Test()
		{
			var input = new DistanceInputDto { FromAirportCode = "ALX", ToAirportCode = "ALE" };
			var airports = await _airportService.GetAirports(input.FromAirportCode,input.ToAirportCode);
			Assert.AreEqual(airports.FromAirport.iata, input.FromAirportCode);
		}

		[Test]
		public async Task GetAirports_NotFound_Test()
		{
			var input = new DistanceInputDto { FromAirportCode = "AOA", ToAirportCode = "ALE" };
			var airports = await _airportService.GetAirports(input.FromAirportCode, input.ToAirportCode);
			Assert.AreEqual(airports.FromAirport.Errors.Count,1);
		}

		#endregion
	}
}