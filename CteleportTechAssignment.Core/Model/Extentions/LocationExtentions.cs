using CteleportTechAssignment.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CteleportTechAssignment.Core.Extentions
{
	public static class LocationExtentions
	{
		/// <summary>
		/// Calc Distance between 2 Locations in miles .
		/// </summary>
		/// <param name="location"></param>
		/// <param name="ToLocation"></param>
		/// <returns></returns>
		public static double CalculateDistanceTo(this location location, location ToLocation)
		{
			var d1 = location.lat * (Math.PI / 180.0);
			var num1 = location.lon * (Math.PI / 180.0);
			var d2 = ToLocation.lat * (Math.PI / 180.0);
			var num2 = ToLocation.lon * (Math.PI / 180.0) - num1;
			var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
			double distncae= 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));

			return Math.Round(distncae, 2);

		}

		
	}

	
}
