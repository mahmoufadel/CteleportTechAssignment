using NUnit.Framework;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CteleportTechAssignment.Test
{
	[SetUpFixture]
	public static class nUnitSetup
	{
		public static TestServer _server;

		[OneTimeSetUp]
		public static void Setup()
		{
			var applicationPath = Directory.GetCurrentDirectory();
			_server = new TestServer(new WebHostBuilder()
				.UseEnvironment("Testing")
				.UseContentRoot(applicationPath)
				.UseConfiguration(new ConfigurationBuilder()
					.SetBasePath(applicationPath)
					.AddJsonFile("appsettings.Testing.json")
					.Build()
				)
				.UseStartup<TestStartup>());			
		}

		[OneTimeTearDown]
		public static void Teardown()
		{
			_server.Dispose();
		}
	}
}
