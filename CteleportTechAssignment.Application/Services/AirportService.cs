using CteleportTechAssignment.Core.dtos;
using CteleportTechAssignment.Core.Model;
using CteleportTechAssignment.Cache;
using CteleportTechAssignment.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CteleportTechAssignment.Persistence;
using AutoMapper;
using System.Net.Http;
using Newtonsoft.Json;
using CteleportTechAssignment.Core.Models;

namespace CteleportTechAssignment.Application
{
	public class AirportService : IAirportService
	{
		private readonly IAPIService _aPIService;
		private readonly ICacheService _cacheService;
		private readonly IMapper _mapper;
		public AirportService(IAPIService aPIService, ICacheService cacheService, IMapper mapper)
		{
			_aPIService = aPIService;
			_cacheService = cacheService;
			_mapper = mapper;
		}
		/// <summary>
		/// Calculate Distance between 2 airport codes provided bu consumer .
		/// </summary>
		/// <param name="inputDto"></param>
		/// <returns></returns>
		public async Task<DistanceOutputDto> CalculateDistance(DistanceInputDto inputDto)
		{
			var _fromAirportCode = inputDto.FromAirportCode.ToUpper();
			var _toAirportCode = inputDto.ToAirportCode.ToUpper();

			string cacheKey = _fromAirportCode + "_" + _toAirportCode;
			var cachedData = _cacheService.Current().Get<DistanceOutputDto>(cacheKey);
			if (cachedData != null)
				return cachedData;

			var Airports = await this.GetAirports(_fromAirportCode, _toAirportCode);

			if (this.ValidateReports(Airports))
			{
				var distanceResult = new DistanceOutputDto
				{
					FromAirportCode = _fromAirportCode,
					ToAirportCode = _toAirportCode,
					Distance = Airports.FromAirport.location.CalculateDistanceTo(Airports.ToAirport.location)
				};				
				_cacheService.Current().Add<DistanceOutputDto>(cacheKey, distanceResult);
				return distanceResult;
			}
			return GetOutputDto(inputDto, GetReportsErrors(Airports));

		}

		/// <summary>
		/// Get 2 Airports to calc distance between them async in one call with [whenALL].
		/// </summary>
		/// <param name="FromAirportCode"></param>
		/// <param name="ToAirportCode"></param>
		/// <returns></returns>
		public async Task<(AirportDto FromAirport, AirportDto ToAirport)> GetAirports(string FromAirportCode, string ToAirportCode)
		{

			//prepare to tasks to get both reports 
			var FromAirportTask = this.GetAsync(FromAirportCode);
			var ToAirportTask = this.GetAsync(ToAirportCode);

			//Call both tasks on the same time 
			await Task.WhenAll(FromAirportTask, ToAirportTask);

			AirportDto FromAirport = await FromAirportTask;
			AirportDto ToAirport = await ToAirportTask;

			return (FromAirport, ToAirport);
		}

		public async Task<AirportDto> GetAsync(string Code)
		{
			var cacheKey = Code.ToUpper();
			var cachedData = await _cacheService.Current().GetAsync<AirportDto>(cacheKey);
			if (cachedData != null)
				return cachedData;


			var airportResponse = await _aPIService.Get(Code, null);
			var airportDto = await this.Map(airportResponse,Code);			

			if(airportDto.Errors.Count==0)
				_cacheService.Current().Add<AirportDto>(cacheKey, airportDto);
			return airportDto;
		}

		/// <summary>
		/// check id 2 reports are retrived correctlly without erros
		/// </summary>
		/// <param name="Airports"></param>
		/// <returns></returns>
		public bool ValidateReports((AirportDto FromAirport, AirportDto ToAirport) Airports)
			=> (Airports.FromAirport.Errors.Count == 0 && Airports.ToAirport.Errors.Count == 0);

		/// <summary>
		/// Get 2 Airports errors to be sent on response .
		/// </summary>
		/// <param name="Airports"></param>
		/// <returns></returns>
		List<ErrorDetails> GetReportsErrors((AirportDto FromAirport, AirportDto ToAirport) Airports)
		{
			return Airports.FromAirport.Errors.Concat(Airports.ToAirport.Errors).ToList();
		}

		/// <summary>
		/// Prepare Output response with Error List if found .
		/// </summary>
		/// <param name="inputDto"></param>
		/// <param name="errors"></param>
		/// <returns></returns>
		public DistanceOutputDto GetOutputDto(DistanceInputDto inputDto, List<ErrorDetails> errors)
			=> new DistanceOutputDto { FromAirportCode = inputDto.FromAirportCode, ToAirportCode = inputDto.ToAirportCode, Errors = errors };

		/// <summary>
		/// Map APi Json To AirportDto Object Or add Errorto it's Error list 
		/// </summary>
		/// <param name="responseMessage"></param>
		/// <param name="Code"></param>
		/// <returns></returns>
		private  async Task<AirportDto> Map(HttpResponseMessage responseMessage , string Code)
		{
			AirportDto airportDto = new AirportDto { iata = Code };
			if (responseMessage.StatusCode == HttpStatusCode.OK)
			{
				var jsonContent = await responseMessage.Content.ReadAsStringAsync();
				var airport = JsonConvert.DeserializeObject<Airport>(jsonContent);
				airportDto = _mapper.Map<AirportDto>(airport);
			}
			else { airportDto.Errors.Add(new ErrorDetails { StatusCode = (int)responseMessage.StatusCode,Message=$" Code : {Code} is :{ responseMessage.StatusCode.ToString()}" }); }

			return airportDto;
		}

	}

}
